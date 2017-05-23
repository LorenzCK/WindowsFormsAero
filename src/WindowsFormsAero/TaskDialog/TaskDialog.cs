/*****************************************************
 * WindowsFormsAero
 * https://github.com/LorenzCK/WindowsFormsAero
 * http://windowsformsaero.codeplex.com
 *
 * Author: Lorenz Cuno Klopfenstein <lck@klopfenstein.net>
 *****************************************************/

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using WindowsFormsAero.Native;

namespace WindowsFormsAero.TaskDialog {

    /// <summary>
    /// Displays a complex dialog box that can contain text, icons, buttons, command
    /// links, radio buttons, and/or a progress bar.
    /// </summary>
    public partial class TaskDialog {

        #region Initialization and C'tors

        private void Init() {
            Title = string.Empty;
            Instruction = string.Empty;
            Content = null;

            CommonIcon = defaultIcon;
            CustomIcon = null;

            CommonButtons = 0;
            CustomButtons = null;
            DefaultButton = (int)CommonButton.OK;

            RadioButtons = null;
            EnabledRadioButton = 0;

            VerificationText = null;
            ExpandedInformation = null;
            ExpandedControlText = null;
            CollapsedControlText = null;

            Footer = null;
            FooterCommonIcon = CommonIcon.None;
            FooterCustomIcon = null;

            Width = 0;

            config = new NativeMethods.TaskDialogConfig();
        }

        /// <summary>
        /// Initializes a new Task Dialog instance without text.
        /// </summary>
        public TaskDialog() {
            Init();
        }

        /// <summary>
        /// Initializes a new Task Dialog instance with text.
        /// </summary>
        /// <param name="instruction">The main instruction to display.</param>
        public TaskDialog(string instruction) {
            Init();

            Instruction = instruction;
        }

        /// <summary>
        /// Initializes a new Task Dialog instance with an instruction and a title.
        /// </summary>
        /// <param name="instruction">The main instruction to display.</param>
        /// <param name="title">The title of the Task Dialog.</param>
        public TaskDialog(string instruction, string title) {
            Init();

            Title = title;
            Instruction = instruction;
        }

        /// <summary>
        /// Initializes a new Task Dialog instance with an instruction, a title and some
        /// content text.</summary>
        /// <param name="instruction">The main instruction to display.</param>
        /// <param name="title">The title of the Task Dialog.</param>
        /// <param name="content">The content text that will be shown below the main instruction.</param>
        public TaskDialog(string instruction, string title, string content) {
            Init();

            Title = title;
            Instruction = instruction;
            Content = content;
        }

        /// <summary>
        /// Initializes a new Task Dialog instance with an instruction, a title, some
        /// content text and a specific button.</summary>
        /// <param name="instruction">The main instruction to display.</param>
        /// <param name="title">The title of the Task Dialog.</param>
        /// <param name="content">The content text that will be shown below the main instruction.</param>
        /// <param name="commonButtons">Specifies one or more buttons to be displayed on the bottom of the dialog, instead of the default OK button.</param>
        public TaskDialog(string instruction, string title, string content, CommonButton commonButtons) {
            Init();

            Title = title;
            Instruction = instruction;
            Content = content;
            CommonButtons = commonButtons;
        }

        /// <summary>
        /// Initializes a new Task Dialog instance with an instruction, a title, some
        /// content text, a specific button and an icon.</summary>
        /// <param name="instruction">The main instruction to display.</param>
        /// <param name="title">The title of the Task Dialog.</param>
        /// <param name="content">The content text that will be shown below the main instruction.</param>
        /// <param name="commonButtons">Specifies one or more buttons to be displayed on the bottom of the dialog, instead of the default OK button.</param>
        /// <param name="icon">The icon to display.</param>
        public TaskDialog(string instruction, string title, string content, CommonButton commonButtons, CommonIcon icon) {
            Init();

            Title = title;
            Instruction = instruction;
            Content = content;
            CommonButtons = commonButtons;
            CommonIcon = icon;
        }

        #endregion

        #region Data & Properties

        // Defaults
        const int defaultProgressBarMax = 100;
        const int defaultMarqueeSpeed = 50;
        const CommonButton defaultButton = CommonButton.OK;
        const CommonIcon defaultIcon = CommonIcon.None;

        // State (is automatically updated on reponse to events)
        private IntPtr _hwnd = IntPtr.Zero;

        /// <summary>
        /// Is true if the task dialog is currently displayed.
        /// </summary>
        public bool IsShowing {
            get {
                return _hwnd != IntPtr.Zero;
            }
        }

        /// <summary>
        /// Gets or sets the title of the dialog.
        /// </summary>
        public string Title { get; set; }

        string _Instruction;
        string _Content;
        
        /// <summary>
        /// Gets or sets the icon of the dialog, from a set of common icons.
        /// </summary>
        public CommonIcon CommonIcon { get; set; }
        
        /// <summary>
        /// Gets or sets the icon of the dialog, from a custom Icon instance.
        /// </summary>
        public System.Drawing.Icon CustomIcon { get; set; }

        /// <summary>
        /// Gets or sets the dialog's buttons, from one or more common button types.
        /// </summary>
        public CommonButton CommonButtons { get; set; }

        /// <summary>
        /// Gets or sets a set of custom buttons which will be displayed on the dialog.
        /// </summary>
        /// <remarks>These buttons can also be shown as Command Links optionally.</remarks>
        public CustomButton[] CustomButtons { get; set; }

        /// <summary>
        /// Gets or sets the integer ID of the dialog's default button.
        /// </summary>
        /// <remarks>
        /// You may use any custom ID or one of the common button IDs in
        /// <see cref="CommonButtonResult"/>.
        /// </remarks>
        public int DefaultButton { get; set; }
        
        /// <summary>
        /// Gets or sets a set of custom buttons which will be displayed as radio buttons.
        /// </summary>
        public CustomButton[] RadioButtons { get; set; }

        /// <summary>
        /// Gets or sets the identificator of the enabled radio button by default.
        /// </summary>
        public int EnabledRadioButton { get; set; }

        /// <summary>
        /// Gets or sets the text that will be shown next to a verification checkbox.
        /// </summary>
        public string VerificationText { get; set; }
        
        string _ExpandedInformation;

        /// <summary>
        /// Gets or sets the text displayed on the control that enables the user to
        /// expand and collapse the dialog, when the dialog is in expanded mode.
        /// </summary>
        public string ExpandedControlText { get; set; }

        /// <summary>
        /// Gets or sets the text displayed on the control that enables the user to
        /// expand and collapse the dialog, when the dialog is in collapsed mode.
        /// </summary>
        public string CollapsedControlText { get; set; }

        string _Footer;

        /// <summary>
        /// Gets or sets the icon shown in the dialog's footer, from a set of common icons.
        /// </summary>
        public CommonIcon FooterCommonIcon { get; set; }

        /// <summary>
        /// Gets or sets the icon shown in the dialog's footer, from a custom Icon instance.
        /// </summary>
        public System.Drawing.Icon FooterCustomIcon { get; set; }

        /// <summary>
        /// Explicitly sets the desired width in pixels of the dialog.
        /// </summary>
        /// <remarks>
        /// Will be set automatically by the task dialog to an optimal size.
        /// </remarks>
        public uint Width { get; set; }

        int _ProgressBarPosition = 0,
            _ProgressBarMinRange = 0,
            _ProgressBarMaxRange = defaultProgressBarMax;

        ProgressBarState _ProgressBarState = ProgressBarState.Normal;

        #endregion

        #region Properties (with message support)

        /// <summary>
        /// Gets or Sets the Main Instruction text of the TaskDialog.
        /// Can be updated when the dialog is shown.
        /// </summary>
        /// <remarks>
        /// Text written in blue and slightly bigger font in Windows Aero.
        /// </remarks>
        public string Instruction { 
            get {
                return _Instruction;
            }
            set {
                if (IsShowing) {
                    PostMessage(new Message(NativeMethods.TaskDialogMessages.TDM_SET_ELEMENT_TEXT,
                        Message.DialogElements.TDE_MAIN_INSTRUCTION, value));
                }
                
                _Instruction = value;
            }
        }

        /// <summary>
        /// Gets or sets the Content text of the TaskDialog.
        /// Can be updated when the dialog is shown.
        /// </summary>
        /// <remarks>
        /// Text written with standard font, right below the Main instruction.
        /// </remarks>
        public string Content {
            get {
                return _Content;
            }
            set {
                if (IsShowing) {
                    PostMessage(new Message(NativeMethods.TaskDialogMessages.TDM_SET_ELEMENT_TEXT,
                        Message.DialogElements.TDE_CONTENT, value));
                }

                _Content = value;
            }
        }

        /// <summary>
        /// Gets or Sets the expanded information text, that will be optionally shown
        /// by clicking on the Expand control.
        /// </summary>
        public string ExpandedInformation {
            get {
                return _ExpandedInformation;
            }
            set {
                if (IsShowing) {
                    PostMessage(new Message(NativeMethods.TaskDialogMessages.TDM_SET_ELEMENT_TEXT,
                        Message.DialogElements.TDE_EXPANDED_INFORMATION, value));
                }

                _ExpandedInformation = value;
            }
        }

        /// <summary>
        /// Gets or Sets the Footer text.
        /// </summary>
        public string Footer {
            get {
                return _Footer;
            }
            set {
                if (IsShowing) {
                    PostMessage(new Message(NativeMethods.TaskDialogMessages.TDM_SET_ELEMENT_TEXT,
                        Message.DialogElements.TDE_FOOTER, value));
                }

                _Footer = value;
            }
        }

        /// <summary>
        /// Gets or sets the current progress bar value.
        /// </summary>
        public int ProgressBarPosition {
            get {
                return _ProgressBarPosition;
            }
            set {
                PostMessage(new Message(NativeMethods.TaskDialogMessages.TDM_SET_PROGRESS_BAR_POS, value, 0));

                _ProgressBarPosition = value;
            }
        }

        /// <summary>Gets of sets the minimum value allowed by the Progress bar.</summary>
        public int ProgressBarMinRange {
            get {
                return _ProgressBarMinRange;
            }
            set {
                PostMessage(new Message(NativeMethods.TaskDialogMessages.TDM_SET_PROGRESS_BAR_RANGE, 0, value, _ProgressBarMaxRange));

                _ProgressBarMinRange = value;
            }
        }

        /// <summary>Gets or sets the maximum value allowed by the Progress bar.</summary>
        public int ProgressBarMaxRange {
            get {
                return _ProgressBarMaxRange;
            }
            set {
                PostMessage(new Message(NativeMethods.TaskDialogMessages.TDM_SET_PROGRESS_BAR_RANGE, 0, _ProgressBarMinRange, value));

                _ProgressBarMaxRange = value;
            }
        }

        /// <summary>Gets or sets the current Progress bar state.</summary>
        /// <remarks>Determines the bar's color and behavior.</remarks>
        public ProgressBarState ProgressBarState {
            get {
                return _ProgressBarState;
            }
            set {
                PostMessage(new Message(NativeMethods.TaskDialogMessages.TDM_SET_PROGRESS_BAR_STATE, (int)(value.ToNative()), 0));

                _ProgressBarState = value;
            }
        }

        #endregion

        #region Flag Properties

        /// <summary>
        /// Enables or disables Hyperlinks in the dialog's content.
        /// </summary>
        /// <remarks>
        /// Hyperlinks can be expressed using HTML, in the form of &lt;A HREF="link"&gt;
        /// and &lt;/A&gt; tags. The link is reported back through the
        /// <see cref="HyperlinkClick"/> event.
        /// </remarks>
        public bool EnableHyperlinks {
            get { return GetConfigFlag(NativeMethods.TaskDialogFlags.TDF_ENABLE_HYPERLINKS); }
            set { SetConfigFlag(NativeMethods.TaskDialogFlags.TDF_ENABLE_HYPERLINKS, value); }
        }

        /// <summary>
        /// Gets or sets whether the dialog can be cancelled (ESC, ALT+F4 and X button)
        /// even if no common Cancel button has been specified.
        /// </summary>
        public bool AllowDialogCancellation {
            get { return GetConfigFlag(NativeMethods.TaskDialogFlags.TDF_ALLOW_DIALOG_CANCELLATION); }
            set { SetConfigFlag(NativeMethods.TaskDialogFlags.TDF_ALLOW_DIALOG_CANCELLATION, value); }
        }

        /// <summary>
        /// Gets or sets whether Command Link buttons should be used instead of standard
        /// custom buttons (doesn't apply to common buttons, like OK or Cancel).
        /// </summary>
        public bool UseCommandLinks {
            get { return GetConfigFlag(NativeMethods.TaskDialogFlags.TDF_USE_COMMAND_LINKS); }
            set { SetConfigFlag(NativeMethods.TaskDialogFlags.TDF_USE_COMMAND_LINKS, value); }
        }

        /// <summary>
        /// Gets or sets whether Command Link buttons without icon should be used instead
        /// of standard custom buttons (doesn't apply to common buttons, like OK or Cancel).
        /// </summary>
        public bool UseCommandLinksWithoutIcon {
            get { return GetConfigFlag(NativeMethods.TaskDialogFlags.TDF_USE_COMMAND_LINKS_NO_ICON); }
            set { SetConfigFlag(NativeMethods.TaskDialogFlags.TDF_USE_COMMAND_LINKS_NO_ICON, value); }
        }

        /// <summary>
        /// Gets or sets whether the ExpandedInformation should be shown in the Footer
        /// area (instead of under the Content text).
        /// </summary>
        public bool ShowExpandedInfoInFooter {
            get { return GetConfigFlag(NativeMethods.TaskDialogFlags.TDF_EXPAND_FOOTER_AREA); }
            set { SetConfigFlag(NativeMethods.TaskDialogFlags.TDF_EXPAND_FOOTER_AREA, value); }
        }

        /// <summary>
        /// Gets or sets whether the ExpandedInformation is visible on dialog creation.
        /// </summary>
        public bool IsExpanded {
            get { return GetConfigFlag(NativeMethods.TaskDialogFlags.TDF_EXPANDED_BY_DEFAULT); }
            set { SetConfigFlag(NativeMethods.TaskDialogFlags.TDF_EXPANDED_BY_DEFAULT, value); }
        }

        /// <summary>
        /// Gets or sets whether the Verification checkbox should be checked when the
        /// dialog is shown.
        /// </summary>
        public bool IsVerificationChecked {
            get { return GetConfigFlag(NativeMethods.TaskDialogFlags.TDF_VERIFICATION_FLAG_CHECKED); }
            set { SetConfigFlag(NativeMethods.TaskDialogFlags.TDF_VERIFICATION_FLAG_CHECKED, value); }
        }

        /// <summary>
        /// Gets or sets whether a progress bar should be displayed on the dialog.
        /// </summary>
        public bool ShowProgressBar {
            get { return GetConfigFlag(NativeMethods.TaskDialogFlags.TDF_SHOW_PROGRESS_BAR); }
            set { SetConfigFlag(NativeMethods.TaskDialogFlags.TDF_SHOW_PROGRESS_BAR, value); }
        }

        /// <summary>
        /// Sets or gets whether the user specified callback (if any) should be called every 200ms.
        /// </summary>
        public bool EnableCallbackTimer {
            get { return GetConfigFlag(NativeMethods.TaskDialogFlags.TDF_CALLBACK_TIMER); }
            set { SetConfigFlag(NativeMethods.TaskDialogFlags.TDF_CALLBACK_TIMER, value); }
        }

        /// <summary>
        /// Gets or sets whether the dialog should be positioned centered on the parent window.
        /// </summary>
        public bool PositionRelativeToWindow {
            get { return GetConfigFlag(NativeMethods.TaskDialogFlags.TDF_POSITION_RELATIVE_TO_WINDOW); }
            set { SetConfigFlag(NativeMethods.TaskDialogFlags.TDF_POSITION_RELATIVE_TO_WINDOW, value); }
        }

        /// <summary>
        /// Enables or disables right to left reading order.
        /// </summary>
        public bool RightToLeftLayout {
            get { return GetConfigFlag(NativeMethods.TaskDialogFlags.TDF_RTL_LAYOUT); }
            set { SetConfigFlag(NativeMethods.TaskDialogFlags.TDF_RTL_LAYOUT, value); }
        }

        /// <summary>
        /// Gets or sets whether there should be no selected radio button by default
        /// when the dialog is shown.
        /// </summary>
        public bool NoDefaultRadioButton {
            get { return GetConfigFlag(NativeMethods.TaskDialogFlags.TDF_NO_DEFAULT_RADIO_BUTTON); }
            set { SetConfigFlag(NativeMethods.TaskDialogFlags.TDF_NO_DEFAULT_RADIO_BUTTON, value); }
        }

        /// <summary>
        /// Gets or sets whether the dialog may be minimized or not.
        /// </summary>
        public bool CanBeMinimized {
            get { return GetConfigFlag(NativeMethods.TaskDialogFlags.TDF_CAN_BE_MINIMIZED); }
            set { SetConfigFlag(NativeMethods.TaskDialogFlags.TDF_CAN_BE_MINIMIZED, value); }
        }

        private void SetConfigFlag(NativeMethods.TaskDialogFlags f, bool value){
            if (value)
                config.dwFlags |= f;
            else
                config.dwFlags &= ~f; //add complement of f
        }

        private bool GetConfigFlag(NativeMethods.TaskDialogFlags f) {
            return (config.dwFlags & f) != 0;
        }

        #endregion

        #region Message handling and buffering

        //Local message queue
        //Buffers message before the dialog is created and shown
        internal Queue<Message> _msgQueue = new Queue<Message>(5);

        private void DispatchMessageQueue() {
            while (IsShowing && _msgQueue.Count > 0) {
                Message msg = _msgQueue.Peek();

                Methods.SendMessage(_hwnd, (uint)msg.MessageType, msg.WParam, msg.LParam);

                //Delete the message (may contain unmanaged memory pointers)
                Message.Cleanup(msg);
                _msgQueue.Dequeue();
            }
        }

        private void PostMessage(Message msg) {
            if (IsShowing) {
                Methods.SendMessage(_hwnd, (uint)msg.MessageType, msg.WParam, msg.LParam);
                Message.Cleanup(msg);
            }
            else {
                _msgQueue.Enqueue(msg);
            }
        }

        #endregion

        #region Methods

        /// <summary>Injects a virtual button click.</summary>
        /// <param name="buttonId">Numeric id of the clicked button.</param>
        public void SimulateButtonClick(int buttonId) {
            PostMessage(new Message(NativeMethods.TaskDialogMessages.TDM_CLICK_BUTTON, buttonId, 0));
        }

        /// <summary>Injects a virtual radio button click.</summary>
        /// <param name="buttonId">Numeric id of the clicked radio button.</param>
        public void SimulateRadioButtonClick(int buttonId) {
            PostMessage(new Message(NativeMethods.TaskDialogMessages.TDM_CLICK_RADIO_BUTTON, buttonId, 0));
        }

        /// <summary>Injects a virtual checkbox click.</summary>
        /// <param name="isChecked">New state of the verification checkbox.</param>
        /// <param name="hasKeyboardFocus">Sets whether the checkbox should have focus after state change.</param>
        public void SimulateVerificationClick(bool isChecked, bool hasKeyboardFocus) {
            PostMessage(new Message(NativeMethods.TaskDialogMessages.TDM_CLICK_VERIFICATION, isChecked, hasKeyboardFocus));
        }


        /// <summary>Enables or disables a button of the dialog.</summary>
        /// <param name="buttonId">Id of the button whose state will be changed.</param>
        /// <param name="isEnabled">New state of the button.</param>
        public void EnableButton(int buttonId, bool isEnabled) {
            PostMessage(new Message(NativeMethods.TaskDialogMessages.TDM_ENABLE_BUTTON, buttonId, isEnabled));
        }

        /// <summary>Enables or disables a radio button of the dialog.</summary>
        /// <param name="buttonId">Id of the radio button whose state will be changed.</param>
        /// <param name="isEnabled">New state of the button.</param>
        public void EnableRadioButton(int buttonId, bool isEnabled) {
            PostMessage(new Message(NativeMethods.TaskDialogMessages.TDM_ENABLE_RADIO_BUTTON, buttonId, isEnabled));
        }

        /// <summary>Creates a new Task Dialog setup and replaces the existing one. Note that the window will not be
        /// destroyed and that you should keep the existing TaskDialog reference (event handlers will still be
        /// registered). The existing Task Dialog will simply reset and use the options of the new one.</summary>
        /// <param name="nextDialog">An instance of Task Dialog, whose settings will be copied into the existing dialog.
        /// You may safely destroy the nextDialog instance after use (do not register to events on it).</param>
        public void Navigate(TaskDialog nextDialog) {
            //Prepare config structure of target dialog
            nextDialog.PreConfig(IntPtr.Zero);
            //Keep callback reference to the current dialog, since the nextDialog instance will eventually be destroyed
            nextDialog.config.pfCallback = config.pfCallback;
            //Copy queued messages
            while (nextDialog._msgQueue.Count > 0)
                _msgQueue.Enqueue(nextDialog._msgQueue.Dequeue());
            
            //Navigate
            PostMessage(new Message(NativeMethods.TaskDialogMessages.TDM_NAVIGATE_PAGE, 0, nextDialog.config));

            //Clean up
            nextDialog.PostConfig();
        }


        /// <summary>Adds or removes an UAC Shield icon from a button.</summary>
        /// <param name="buttonId">Id of the button.</param>
        /// <param name="requiresElevation">Sets whether to display a Shield icon or not.</param>
        public void SetShieldButton(int buttonId, bool requiresElevation) {
            PostMessage(new Message(NativeMethods.TaskDialogMessages.TDM_SET_BUTTON_ELEVATION_REQUIRED_STATE, buttonId, requiresElevation));
        }

        /// <summary>Sets whether the dialog's progress bar should be in standard or in marquee mode.</summary>
        /// <param name="enabled">True if the progress bar should be displayed in marquee mode (no explicit progress).</param>
        public void SetMarqueeProgressBar(bool enabled) {
            SetMarqueeProgressBar(enabled, defaultMarqueeSpeed);
        }

        /// <summary>Sets whether the dialog's progress bar should be in standard or in marquee mode and sets its marquee speed.</summary>
        /// <param name="enabled">True if the progress bar should be displayed in marquee mode (no explicit progress).</param>
        /// <param name="speed">Speed of the progress bar in marquee mode.</param>
        public void SetMarqueeProgressBar(bool enabled, int speed) {
            SetConfigFlag(NativeMethods.TaskDialogFlags.TDF_SHOW_MARQUEE_PROGRESS_BAR, enabled);

            PostMessage(new Message(NativeMethods.TaskDialogMessages.TDM_SET_PROGRESS_BAR_MARQUEE, enabled, speed));
        }

        #endregion

        #region Events

        /// <summary>
        /// Occurs when the Task Dialog is first created and before it is displayed (is sent after Construction event).
        /// </summary>
        public event EventHandler Created;

        /// <summary>
        /// Occurs when the user clicks a button or a command link. By default the Dialog is closed after the notification.
        /// </summary>
        public event EventHandler<ClickEventArgs> ButtonClick;

        /// <summary>
        /// Occurs when the user clicks on a Hyperlink in the Content text.
        /// </summary>
        public event EventHandler<HyperlinkEventArgs> HyperlinkClick;

        /// <summary>
        /// Occurs when a navigation event is raised.
        /// </summary>
        public event EventHandler Navigating;

        /// <summary>
        /// Occurs approximately every 200ms if the Task Dialog callback timer is enabled.
        /// </summary>
        public event EventHandler<TimerEventArgs> Tick;

        /// <summary>
        /// Occurs when the Task Dialog is destroyed and the handle to the dialog is not valid anymore.
        /// </summary>
        public event EventHandler Destroyed;

        /// <summary>
        /// Occurs when the user selects a radio button.
        /// </summary>
        public event EventHandler<ClickEventArgs> RadioButtonClick;

        /// <summary>
        /// Occurs when the Task Dialog is constructed and before it is displayed (is sent before Creation event).
        /// </summary>
        public event EventHandler Constructed;

        /// <summary>
        /// Occurs when the user switches the state of the Verification Checkbox.
        /// </summary>
        public event EventHandler<CheckEventArgs> VerificationClick;

        /// <summary>
        /// Occurs when the user presses F1 when the Task Dialog has focus.
        /// </summary>
        public event EventHandler Help;

        /// <summary>
        /// Occurs when the user clicks on the expand button of the dialog, before the dialog is expanded.
        /// </summary>
        public event EventHandler<ExpandEventArgs> Expanding;

        /// <summary>
        /// Common native callback for Task Dialogs.
        /// Will route events to the user event handler.
        /// </summary>
        /// <param name="refData">TODO: Currently unused, would need complex marshaling of data.</param>
        internal IntPtr CommonCallbackProc(IntPtr hWnd, uint uEvent, UIntPtr wParam, IntPtr lParam, IntPtr refData) {
            _hwnd = hWnd;
            
            //Handle event
            switch ((NativeMethods.TaskDialogNotification)uEvent) {
                case NativeMethods.TaskDialogNotification.TDN_CREATED:
                    //Dispatch buffered messages
                    DispatchMessageQueue();

                    Created?.Invoke(this, EventArgs.Empty);
                    break;

                case NativeMethods.TaskDialogNotification.TDN_NAVIGATED:
                    //Dispatch buffered messages (copied in from the new task dialog we are navigating to)
                    DispatchMessageQueue();

                    Navigating?.Invoke(this, EventArgs.Empty);
                    break;

                case NativeMethods.TaskDialogNotification.TDN_BUTTON_CLICKED:
                    var evtButtonClick = ButtonClick;
                    if (evtButtonClick != null) {
                        ClickEventArgs args = new ClickEventArgs((int)wParam);
                        evtButtonClick(this, args);

                        //Return value given by user to prevent closing (false will close)
                        return (IntPtr)((args.PreventClosing) ? 1 : 0);
                    }
                    break;

                case NativeMethods.TaskDialogNotification.TDN_HYPERLINK_CLICKED:
                    HyperlinkClick?.Invoke(this, new HyperlinkEventArgs(Marshal.PtrToStringUni(lParam)));
                    break;

                case NativeMethods.TaskDialogNotification.TDN_TIMER:
                    var evtTick = Tick;
                    if (evtTick != null) {
                        var args = new TimerEventArgs((long)wParam);
                        evtTick(this, args);

                        //Return value given by user to reset timer ticks
                        return (IntPtr)((args.ResetCount) ? 1 : 0);
                    }
                    break;

                case NativeMethods.TaskDialogNotification.TDN_DESTROYED:
                    //Set dialog as not "showing" and drop handle to window
                    _hwnd = IntPtr.Zero;

                    Destroyed?.Invoke(this, EventArgs.Empty);
                    break;

                case NativeMethods.TaskDialogNotification.TDN_RADIO_BUTTON_CLICKED:
                    RadioButtonClick?.Invoke(this, new ClickEventArgs((int)wParam));
                    break;

                case NativeMethods.TaskDialogNotification.TDN_DIALOG_CONSTRUCTED:
                    Constructed?.Invoke(this, EventArgs.Empty);
                    break;

                case NativeMethods.TaskDialogNotification.TDN_VERIFICATION_CLICKED:
                    VerificationClick?.Invoke(this, new CheckEventArgs((uint)wParam == 1));
                    break;

                case NativeMethods.TaskDialogNotification.TDN_HELP:
                    Help?.Invoke(this, EventArgs.Empty);
                    break;

                case NativeMethods.TaskDialogNotification.TDN_EXPANDO_BUTTON_CLICKED:
                    Expanding?.Invoke(this, new ExpandEventArgs((uint)wParam != 0));
                    break;
            }

            return IntPtr.Zero;
        }

        #endregion

        #region Internal Config structure handling

        // Internal hidden native config structure
        // (is visible to other Task Dialogs to enable navigation)
        internal NativeMethods.TaskDialogConfig config;

        /// <summary>Prepares the internal configuration structure.</summary>
        /// <remarks>Allocates some unmanaged memory, must always be followed by a PostConfig() call.</remarks>
        internal void PreConfig(IntPtr owner){
            //Setup configuration structure
            config.hwndParent = owner;
            config.hInstance = IntPtr.Zero; //will never use resources
            config.cbSize = (uint)Marshal.SizeOf(typeof(NativeMethods.TaskDialogConfig));

            //Icons
            config.hMainIcon = (IntPtr)CommonIcon;
            if (CustomIcon != null) {
                config.dwFlags |= NativeMethods.TaskDialogFlags.TDF_USE_HICON_MAIN;
                config.hMainIcon = CustomIcon.Handle;
            }
            config.hFooterIcon = (IntPtr)FooterCommonIcon;
            if (FooterCustomIcon != null) {
                config.dwFlags |= NativeMethods.TaskDialogFlags.TDF_USE_HICON_FOOTER;
                config.hFooterIcon = FooterCustomIcon.Handle;
            }

            //Data
            config.dwCommonButtons = CommonButtons;
            config.pszWindowTitle = Title;
            config.pszMainInstruction = Instruction;
            config.pszContent = Content;

            config.pszVerificationText = VerificationText;
            config.pszExpandedInformation = ExpandedInformation;
            config.pszExpandedControlText = ExpandedControlText;
            config.pszCollapsedControlText = CollapsedControlText;
            config.pszFooter = Footer;

            config.cxWidth = Width;

            //Special Buttons
            if (CustomButtons != null) {
                config.cButtons = (uint)CustomButtons.Length;
                config.pButtons = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(CustomButton)) * CustomButtons.Length);

                for (int i = 0; i < CustomButtons.Length; ++i) {
                    unsafe {
                        Marshal.StructureToPtr(CustomButtons[i], (IntPtr)((byte*)config.pButtons + i * Marshal.SizeOf(typeof(CustomButton))), false);
                    }
                }
            }
            else {
                config.cButtons = 0;
                config.pButtons = IntPtr.Zero;
            }
            config.nDefaultButton = DefaultButton;

            //Radio Buttons
            if (RadioButtons != null) {
                config.cRadioButtons = (uint)RadioButtons.Length;
                config.pRadioButtons = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(CustomButton)) * RadioButtons.Length);

                for (int i = 0; i < RadioButtons.Length; ++i) {
                    unsafe {
                        Marshal.StructureToPtr(RadioButtons[i], (IntPtr)((byte*)config.pRadioButtons + i * Marshal.SizeOf(typeof(CustomButton))), false);
                    }
                }
            }
            else {
                config.cRadioButtons = 0;
                config.pRadioButtons = IntPtr.Zero;
            }
            config.nDefaultRadioButton = EnabledRadioButton;

            //Callback
            config.pfCallback = new NativeMethods.TaskDialogCallback(CommonCallbackProc);
            config.lpCallbackData = IntPtr.Zero;
        }

        /// <summary>Frees the unmanages memory allocated by PreConfig().</summary>
        internal void PostConfig() {
            //Free allocated memory for custom buttons
            if (config.pButtons != IntPtr.Zero) {
                for (int i = 0; i < config.cButtons; ++i) {
                    unsafe {
                        Marshal.DestroyStructure((IntPtr)((byte*)config.pButtons + i * Marshal.SizeOf(typeof(CustomButton))), typeof(CustomButton));
                    }
                }
                Marshal.FreeHGlobal(config.pButtons);
            }

            //Free allocated memory for radio buttons
            if (config.pRadioButtons != IntPtr.Zero) {
                for (int i = 0; i < config.cRadioButtons; ++i) {
                    unsafe {
                        Marshal.DestroyStructure((IntPtr)((byte*)config.pRadioButtons + i * Marshal.SizeOf(typeof(CustomButton))), typeof(CustomButton));
                    }
                }
                Marshal.FreeHGlobal(config.pRadioButtons);
            }

            config.pfCallback = null;
        }

        #endregion

        #region Display methods

        /// <summary>Displays the task dialog without an explicit parent.</summary>
        public TaskDialogResult Show() {
            return InternalShow(IntPtr.Zero);
        }

        /// <summary>Displays the task dialog with an explicit parent window.</summary>
        /// <param name="owner">Handle to the dialog's parent window.</param>
        public TaskDialogResult Show(IntPtr owner) {
            return InternalShow(owner);
        }

        /// <summary>Displays the task dialog with an explicit parent form.</summary>
        /// <param name="owner">Instance of the dialog's parent form.</param>
        public TaskDialogResult Show(Form owner) {
            return InternalShow(owner.Handle);
        }

        private TaskDialogResult InternalShow(IntPtr owner) {
            //Return state
            int ret = 0, selRadio = 0;
            bool setVerification = false;

            try {
                //"Unsafe" preparation
                PreConfig(owner);

                //Call native method
                if (NativeMethods.TaskDialogIndirect(ref config, out ret, out selRadio, out setVerification) != IntPtr.Zero)
                    throw new Exception(string.Format(Resources.ExceptionMessages.NativeCallFailure, "TaskDialogIndirect"));
            }
            catch (EntryPointNotFoundException ex) {
                throw new Exception(Resources.ExceptionMessages.CommonControlEntryPointNotFound, ex);
            }
            catch (Exception ex) {
                throw new Exception(Resources.ExceptionMessages.TaskDialogFailure, ex);
            }
            finally {
                PostConfig();
            }

            return new TaskDialogResult(ret, selRadio, setVerification);
        }

        #endregion

    }

}
