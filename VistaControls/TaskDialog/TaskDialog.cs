/*****************************************************
 *            Vista Controls for .NET 2.0
 * 
 * http://www.codeplex.com/vistacontrols
 * 
 * @author: Lorenz Cuno Klopfenstein
 * Licensed under Microsoft Community License (Ms-CL)
 * 
 *****************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;

namespace VistaControls.TaskDialog {

    /// <summary>Displays a dialog box that can contain text, icons, buttons, command links, radio buttons and/or a progress bar.</summary>
    public class TaskDialog {

        #region Data

        //Defaults
        const string defaultTitle = "";
        const string defaultInstruction = "";
        const string defaultContent = null;
        const TaskDialogButton defaultButton = TaskDialogButton.OK;
        const TaskDialogIcon defaultIcon = TaskDialogIcon.None;

        //State (is automatically updated on reponse to events)
        private bool _IsShowing = false;
        private IntPtr _hwnd = IntPtr.Zero;
        public bool IsShowing { get { return _hwnd != IntPtr.Zero; } }

        //Public settable data
		//Non public data has a special Property that handles setting via messages (see Data Properties, below)
        public string Title { get; set; }
        string _Instruction;
        string _Content;
        
        public TaskDialogIcon CommonIcon { get; set; }
        public System.Drawing.Icon CustomIcon { get; set; }

        public TaskDialogButton CommonButtons { get; set; }
        public CustomButton[] CustomButtons { get; set; }
        public int DefaultButton { get; set; }
        
        public CustomButton[] RadioButtons { get; set; }
        public int EnabledRadioButton { get; set; }

        public string VerificationText { get; set; }
        string _ExpandedInformation;
        public string ExpandedControlText { get; set; }
        public string CollapsedControlText { get; set; }

        string _Footer;
        public TaskDialogIcon FooterCommonIcon { get; set; }
        public System.Drawing.Icon FooterCustomIcon { get; set; }

        public uint Width { get; set; }

        int _ProgressBarPosition = 0;
        int _ProgressBarMinRange = 0,
            _ProgressBarMaxRange = 100;
        VistaControls.ProgressBar.States _ProgressBarState = ProgressBar.States.Normal;

        #endregion

        #region Initialization and C'tors

        private void Init() {
            Title = defaultTitle;
            Instruction = defaultInstruction;
            Content = defaultContent;

            CommonIcon = defaultIcon;
            CustomIcon = null;

            CommonButtons = 0;
            CustomButtons = null;
            DefaultButton = (int)TaskDialogButton.OK;

            RadioButtons = null;
            EnabledRadioButton = 0;

            VerificationText = null;
            ExpandedInformation = null;
            ExpandedControlText = null;
            CollapsedControlText = null;

            Footer = null;
            FooterCommonIcon = TaskDialogIcon.None;
            FooterCustomIcon = null;

            Width = 0;

            config = new NativeMethods.TaskDialogConfig();
        }

        /// <summary>Initializes a new Task Dialog instance without text.</summary>
        public TaskDialog() {
            Init();
        }

        /// <summary>Initializes a new Task Dialog instance with text.</summary>
        /// <param name="instruction">The main instruction to display.</param>
        public TaskDialog(string instruction) {
            Init();
            Instruction = instruction;
        }

        /// <summary>Initializes a new Task Dialog instance with an instruction and a title.</summary>
        /// <param name="instruction">The main instruction to display.</param>
        /// <param name="title">The title of the Task Dialog.</param>
        public TaskDialog(string instruction, string title) {
            Init();
            
            Title = title;
            Instruction = instruction;
        }

        /// <summary>Initializes a new Task Dialog instance with an instruction, a title and some content text.</summary>
        /// <param name="instruction">The main instruction to display.</param>
        /// <param name="title">The title of the Task Dialog.</param>
        /// <param name="content">The content text that will be displayes below the main instruction.</param>
        public TaskDialog(string instruction, string title, string content) {
            Init();

            Title = title;
            Instruction = instruction;
            Content = content;
        }

        /// <summary>Initializes a new Task Dialog instance with an instruction, a title, some content text and a specific button.</summary>
        /// <param name="instruction">The main instruction to display.</param>
        /// <param name="title">The title of the Task Dialog.</param>
        /// <param name="content">The content text that will be displayes below the main instruction.</param>
        /// <param name="commonButtons">Specifies one or more buttons to be displayed on the bottom of the dialog, instead of the default OK button.</param>
        public TaskDialog(string instruction, string title, string content, TaskDialogButton commonButtons) {
            Init();

            Title = title;
            Instruction = instruction;
            Content = content;
            CommonButtons = commonButtons;
        }

        /// <summary>Initializes a new Task Dialog instance with an instruction, a title, some content text, a specific button and an icon.</summary>
        /// <param name="instruction">The main instruction to display.</param>
        /// <param name="title">The title of the Task Dialog.</param>
        /// <param name="content">The content text that will be displayes below the main instruction.</param>
        /// <param name="commonButtons">Specifies one or more buttons to be displayed on the bottom of the dialog, instead of the default OK button.</param>
        /// <param name="icon">The icon to display.</param>
        public TaskDialog(string instruction, string title, string content, TaskDialogButton commonButtons, TaskDialogIcon icon) {
            Init();

            Title = title;
            Instruction = instruction;
            Content = content;
            CommonButtons = commonButtons;
            CommonIcon = icon;
        }

        #endregion

        #region Data Properties

		/// <summary>Gets or Sets the Main Instruction text of the TaskDialog.</summary>
		/// <remarks>Text written in blue and slightly bigger font in Windows Aero.</remarks>
        public string Instruction { 
            get { return _Instruction; }
            set {
                if(IsShowing)
                    PostMessage(new Message(NativeMethods.TaskDialogMessages.TDM_SET_ELEMENT_TEXT,
                        Message.DialogElements.TDE_MAIN_INSTRUCTION, value));
                
                _Instruction = value;
            }
        }

		/// <summary>Gets or sets the Content text of the TaskDialog.</summary>
		/// <remarks>Text written with standard font, right below the Main instruction.</remarks>
        public string Content {
            get { return _Content; }
            set {
                if (IsShowing)
                    PostMessage(new Message(NativeMethods.TaskDialogMessages.TDM_SET_ELEMENT_TEXT,
                        Message.DialogElements.TDE_CONTENT, value));

                _Content = value;
            }
        }

		/// <summary>Gets or Sets the expanded information text, that will be optionally shown
		/// by clicking on the Expand control.</summary>
        public string ExpandedInformation {
            get { return _ExpandedInformation; }
            set {
                if (IsShowing)
                    PostMessage(new Message(NativeMethods.TaskDialogMessages.TDM_SET_ELEMENT_TEXT,
                        Message.DialogElements.TDE_EXPANDED_INFORMATION, value));

                _ExpandedInformation = value;
            }
        }

		/// <summary>Gets or Sets the Footer text.</summary>
        public string Footer {
            get { return _Footer; }
            set {
                if (IsShowing)
                    PostMessage(new Message(NativeMethods.TaskDialogMessages.TDM_SET_ELEMENT_TEXT,
                        Message.DialogElements.TDE_FOOTER, value));

                _Footer = value;
            }
        }

		/// <summary>Gets or sets the current Progress bar value.</summary>
        public int ProgressBarPosition {
            get { return _ProgressBarPosition; }
            set {
                PostMessage(new Message(NativeMethods.TaskDialogMessages.TDM_SET_PROGRESS_BAR_POS, value, 0));

                _ProgressBarPosition = value;
            }
        }

		/// <summary>Gets of sets the minimum value allowed by the Progress bar.</summary>
        public int ProgressBarMinRange {
            get { return _ProgressBarMinRange; }
            set {
                PostMessage(new Message(NativeMethods.TaskDialogMessages.TDM_SET_PROGRESS_BAR_RANGE, 0, value, _ProgressBarMaxRange));

                _ProgressBarMinRange = value;
            }
        }

		/// <summary>Gets or sets the maximum value allowed by the Progress bar.</summary>
        public int ProgressBarMaxRange {
            get { return _ProgressBarMaxRange; }
            set {
                PostMessage(new Message(NativeMethods.TaskDialogMessages.TDM_SET_PROGRESS_BAR_RANGE, 0, _ProgressBarMinRange, value));

                _ProgressBarMaxRange = value;
            }
        }

		/// <summary>Gets or sets the current Progress bar state.</summary>
		/// <remarks>Determines the bar's color and behavior.</remarks>
        public VistaControls.ProgressBar.States ProgressBarState {
            get { return _ProgressBarState; }
            set {
                int iValue = 0;
                switch (value) {
                    case ProgressBar.States.Normal:
                        iValue = VistaControls.NativeMethods.PBST_NORMAL; break;

                    case ProgressBar.States.Error:
                        iValue = VistaControls.NativeMethods.PBST_ERROR; break;

                    case ProgressBar.States.Paused:
                        iValue = VistaControls.NativeMethods.PBST_PAUSED; break;
                }

                PostMessage(new Message(NativeMethods.TaskDialogMessages.TDM_SET_PROGRESS_BAR_STATE, iValue, 0));

                _ProgressBarState = value;
            }
        }

        #endregion

        #region Flag Properties

        /// <summary>Enables or disables Hyperlinks in the content (in the form of &lt;A HREF="link"&gt;).</summary>
        public bool EnableHyperlinks {
            get { return GetConfigFlag(NativeMethods.TaskDialogFlags.TDF_ENABLE_HYPERLINKS); }
            set { SetConfigFlag(NativeMethods.TaskDialogFlags.TDF_ENABLE_HYPERLINKS, value); }
        }

        /// <summary>Gets or sets whether the dialog can be cancelled (ESC, ALT+F4 and X button) even if no Cancel button has been specified.</summary>
        public bool AllowDialogCancellation {
            get { return GetConfigFlag(NativeMethods.TaskDialogFlags.TDF_ALLOW_DIALOG_CANCELLATION); }
            set { SetConfigFlag(NativeMethods.TaskDialogFlags.TDF_ALLOW_DIALOG_CANCELLATION, value); }
        }

        /// <summary>Gets or sets whether Command Link buttons should be used instead of standard custom buttons (doesn't apply to custom buttons, like OK or Cancel).</summary>
        public bool UseCommandLinks {
            get { return GetConfigFlag(NativeMethods.TaskDialogFlags.TDF_USE_COMMAND_LINKS); }
            set { SetConfigFlag(NativeMethods.TaskDialogFlags.TDF_USE_COMMAND_LINKS, value); }
        }

        /// <summary>Gets or sets whether Command Link buttons wihtout icon should be used instead of standard custom buttons (doesn't apply to custom buttons, like OK or Cancel).</summary>
        public bool UseCommandLinksWithoutIcon {
            get { return GetConfigFlag(NativeMethods.TaskDialogFlags.TDF_USE_COMMAND_LINKS_NO_ICON); }
            set { SetConfigFlag(NativeMethods.TaskDialogFlags.TDF_USE_COMMAND_LINKS_NO_ICON, value); }
        }

        /// <summary>Gets or sets whether the ExpandedInformation should be shown in the Footer area (instead of under the Content text).</summary>
        public bool ShowExpandedInfoInFooter {
            get { return GetConfigFlag(NativeMethods.TaskDialogFlags.TDF_EXPAND_FOOTER_AREA); }
            set { SetConfigFlag(NativeMethods.TaskDialogFlags.TDF_EXPAND_FOOTER_AREA, value); }
        }

        /// <summary>Gets or sets whether the ExpandedInformation is visible on dialog creation.</summary>
        public bool IsExpanded {
            get { return GetConfigFlag(NativeMethods.TaskDialogFlags.TDF_EXPANDED_BY_DEFAULT); }
            set { SetConfigFlag(NativeMethods.TaskDialogFlags.TDF_EXPANDED_BY_DEFAULT, value); }
        }

        /// <summary>Gets or sets whether the Verification checkbox should be checked when the dialog is shown.</summary>
        public bool IsVerificationChecked {
            get { return GetConfigFlag(NativeMethods.TaskDialogFlags.TDF_VERIFICATION_FLAG_CHECKED); }
            set { SetConfigFlag(NativeMethods.TaskDialogFlags.TDF_VERIFICATION_FLAG_CHECKED, value); }
        }

        /// <summary>Gets or sets whether a progress bar should be displayed on the dialog.</summary>
        public bool ShowProgressBar {
            get { return GetConfigFlag(NativeMethods.TaskDialogFlags.TDF_SHOW_PROGRESS_BAR); }
            set { SetConfigFlag(NativeMethods.TaskDialogFlags.TDF_SHOW_PROGRESS_BAR, value); }
        }

        /// <summary>Indicates that the progress bars should be displayed in Marquee mode (no explicit progress).</summary>
        public bool ShowMarqueeProgressBar {
            get { return GetConfigFlag(NativeMethods.TaskDialogFlags.TDF_SHOW_MARQUEE_PROGRESS_BAR); }
            set {
                if (IsShowing)
                    PostMessage(new Message(NativeMethods.TaskDialogMessages.TDM_SET_MARQUEE_PROGRESS_BAR, value, false));

                SetConfigFlag(NativeMethods.TaskDialogFlags.TDF_ALLOW_DIALOG_CANCELLATION, value);
            }
        }

        /// <summary>Sets or gets whether the user specified callback (if any) should be called every 200ms.</summary>
        public bool EnableCallbackTimer {
            get { return GetConfigFlag(NativeMethods.TaskDialogFlags.TDF_CALLBACK_TIMER); }
            set { SetConfigFlag(NativeMethods.TaskDialogFlags.TDF_CALLBACK_TIMER, value); }
        }

        /// <summary>Gets or sets whether the dialog should be positioned centered on the parent window.</summary>
        public bool PositionRelativeToWindow {
            get { return GetConfigFlag(NativeMethods.TaskDialogFlags.TDF_POSITION_RELATIVE_TO_WINDOW); }
            set { SetConfigFlag(NativeMethods.TaskDialogFlags.TDF_POSITION_RELATIVE_TO_WINDOW, value); }
        }

        /// <summary>Enables or disables right to left reading order.</summary>
        public bool RightToLeftLayout {
            get { return GetConfigFlag(NativeMethods.TaskDialogFlags.TDF_RTL_LAYOUT); }
            set { SetConfigFlag(NativeMethods.TaskDialogFlags.TDF_RTL_LAYOUT, value); }
        }

        /// <summary>Gets or sets whether there should be a selected radio button by default when the dialog is shown.</summary>
        public bool NoDefaultRadioButton {
            get { return GetConfigFlag(NativeMethods.TaskDialogFlags.TDF_NO_DEFAULT_RADIO_BUTTON); }
            set { SetConfigFlag(NativeMethods.TaskDialogFlags.TDF_NO_DEFAULT_RADIO_BUTTON, value); }
        }

        /// <summary>Gets or sets whether the dialog may be minimized or not.</summary>
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
        internal Stack<Message> _msgQueue = new Stack<Message>();

        private void DispatchMessageQueue() {
            while (IsShowing && _msgQueue.Count > 0) {
                Message msg = _msgQueue.Peek();

                VistaControls.NativeMethods.SendMessage(_hwnd, (uint)msg.MessageType, msg.wParam, msg.lParam);

                //Delete the message (may contain unmanaged memory pointers)
                msg.Dispose();
                _msgQueue.Pop();
            }
        }

        private void PostMessage(Message msg) {
            if (IsShowing) {
                VistaControls.NativeMethods.SendMessage(_hwnd, (uint)msg.MessageType, msg.wParam, msg.lParam);
            }
            else {
                _msgQueue.Push(msg);
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
                _msgQueue.Push(nextDialog._msgQueue.Pop());
            
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

        #endregion

        #region Events

        /// <summary>Occurs when the Task Dialog is first created and before it is displayed (is sent after Construction event).</summary>
        public event EventHandler Created;
        /// <summary>Occurs when the user clicks a button or a command link. By default the Dialog is closed after the notification.</summary>
        public event EventHandler<ClickEventArgs> ButtonClick;
        /// <summary>Occurs when the user clicks on a Hyperlink in the Content text.</summary>
        public event EventHandler<HyperlinkEventArgs> HyperlinkClick;
        /// <summary>Occurs when a navigation event is raised.</summary>
        public event EventHandler Navigating;
        /// <summary>Occurs approximately every 200ms if the Task Dialog callback timer is enabled.</summary>
        public event EventHandler<TimerEventArgs> Tick;
        /// <summary>Occurs when the Task Dialog is destroyed and the handle to the dialog is not valid anymore.</summary>
        public event EventHandler Destroyed;
        /// <summary>Occurs when the user selects a radio button.</summary>
        public event EventHandler<ClickEventArgs> RadioButtonClick;
        /// <summary>Occurs when the Task Dialog is constructed and before it is displayed (is sent before Creation event).</summary>
        public event EventHandler Constructed;
        /// <summary>Occurs when the user switches the state of the Verification Checkbox.</summary>
        public event EventHandler<CheckEventArgs> VerificationClick;
        /// <summary>Occurs when the user presses F1 when the Task Dialog has focus.</summary>
        public event EventHandler Help;
        /// <summary>Occurs when the user clicks on the expand button of the dialog, before the dialog is expanded.</summary>
        public event EventHandler<ExpandEventArgs> Expanding;


        /// <summary>Common native callback for Task Dialogs. Will route events to the user event handler.</summary>
        /// <param name="refData">TODO: Currently unused, would need complex marshaling of data.</param>
        internal IntPtr CommonCallbackProc(IntPtr hWnd, uint uEvent, UIntPtr wParam, IntPtr lParam, IntPtr refData) {
            //Store window handle
            _hwnd = hWnd;
            
            //Handle event
            switch ((NativeMethods.TaskDialogNotification)uEvent) {
                case NativeMethods.TaskDialogNotification.TDN_CREATED:
                    //Set dialog as "showing"
                    _IsShowing = true;

                    //Dispatch buffered messages
                    DispatchMessageQueue();

                    if (Created != null)
                        Created(this, new EventArgs());
                    break;

                case NativeMethods.TaskDialogNotification.TDN_NAVIGATED:
                    //Dispatch buffered messages (copied in from the new task dialog we are navigating to)
                    DispatchMessageQueue();

                    if (Navigating != null)
                        Navigating(this, new EventArgs());
                    break;

                case NativeMethods.TaskDialogNotification.TDN_BUTTON_CLICKED:
                    if (ButtonClick != null) {
                        ClickEventArgs args = new ClickEventArgs((int)wParam);
                        ButtonClick(this, args);

                        //Return value given by user to prevent closing (false will close)
                        return (IntPtr)((args.PreventClosing) ? 1 : 0);
                    }
                    break;

                case NativeMethods.TaskDialogNotification.TDN_HYPERLINK_CLICKED:
                    if (HyperlinkClick != null)
                        HyperlinkClick(this, new HyperlinkEventArgs(Marshal.PtrToStringUni(lParam)));
                    break;

                case NativeMethods.TaskDialogNotification.TDN_TIMER:
                    if (Tick != null) {
                        TimerEventArgs args = new TimerEventArgs((long)wParam);
                        Tick(this, args);

                        //Return value given by user to reset timer ticks
                        return (IntPtr)((args.ResetCount) ? 1 : 0);
                    }
                    break;

                case NativeMethods.TaskDialogNotification.TDN_DESTROYED:
                    //Set dialog as not "showing" and drop handle to window
                    _IsShowing = false;
                    _hwnd = IntPtr.Zero;

                    if (Destroyed != null)
                        Destroyed(this, new EventArgs());
                    break;

                case NativeMethods.TaskDialogNotification.TDN_RADIO_BUTTON_CLICKED:
                    if (RadioButtonClick != null)
                        RadioButtonClick(this, new ClickEventArgs((int)wParam));
                    break;

                case NativeMethods.TaskDialogNotification.TDN_DIALOG_CONSTRUCTED:
                    if (Constructed != null)
                        Constructed(this, new EventArgs());
                    break;

                case NativeMethods.TaskDialogNotification.TDN_VERIFICATION_CLICKED:
                    if (VerificationClick != null)
                        VerificationClick(this, new CheckEventArgs((uint)wParam == 1));
                    break;

                case NativeMethods.TaskDialogNotification.TDN_HELP:
                    if (Help != null)
                        Help(this, new EventArgs());
                    break;

                case NativeMethods.TaskDialogNotification.TDN_EXPANDO_BUTTON_CLICKED:
                    if (Expanding != null)
                        Expanding(this, new ExpandEventArgs((uint)wParam != 0));
                    break;
            }

            return IntPtr.Zero;
        }

        #endregion

        #region Internal Config structure handling

        //Internal hidden native config structure (is visible to other Task Dialogs -> enables navigation)
        internal NativeMethods.TaskDialogConfig config;

		/// <summary>Prepares the internal configuration structure.</summary>
		/// <remarks>Allocates some unmanaged memory, must always be followed by a PostConfig() call.</remarks>
        internal void PreConfig(IntPtr owner){
            //Setup configuration structure
            config.hwndParent = owner;
            config.hInstance = IntPtr.Zero; //will never use resources
            config.cbSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf(typeof(NativeMethods.TaskDialogConfig));

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
                config.pButtons = Marshal.AllocHGlobal(CustomButton.SizeOf() * CustomButtons.Length);

                for (int i = 0; i < CustomButtons.Length; ++i) {
                    unsafe {
                        Marshal.StructureToPtr(CustomButtons[i], (IntPtr)((byte*)config.pButtons + i * CustomButton.SizeOf()), false);
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
                config.pRadioButtons = Marshal.AllocHGlobal(CustomButton.SizeOf() * RadioButtons.Length);

                for (int i = 0; i < RadioButtons.Length; ++i) {
                    unsafe {
                        Marshal.StructureToPtr(RadioButtons[i], (IntPtr)((byte*)config.pRadioButtons + i * CustomButton.SizeOf()), false);
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
                        Marshal.DestroyStructure((IntPtr)((byte*)config.pButtons + i * CustomButton.SizeOf()), typeof(CustomButton));
                    }
                }
                Marshal.FreeHGlobal(config.pButtons);
            }

            //Free allocated memory for radio buttons
            if (config.pRadioButtons != IntPtr.Zero) {
                for (int i = 0; i < config.cRadioButtons; ++i) {
                    unsafe {
                        Marshal.DestroyStructure((IntPtr)((byte*)config.pRadioButtons + i * CustomButton.SizeOf()), typeof(CustomButton));
                    }
                }
                Marshal.FreeHGlobal(config.pRadioButtons);
            }
        }

        #endregion

        #region Display methods

        /// <summary>Displays the task dialog as a modeless dialog.</summary>
        public Results Show() {
            return InternalShow(IntPtr.Zero);
        }

        /// <summary>Displays the task dialog as a modal dialog.</summary>
        /// <param name="owner">Handle to the parent window of the task dialog.</param>
        public Results Show(IntPtr owner) {
            return InternalShow(owner);
        }

        private Results InternalShow(IntPtr owner) {
            //Return state
            int ret = 0, selRadio = 0;
            bool setVerification = false;

			try {
				//"Unsafe" preparation
				PreConfig(owner);

				//Call native method
				if(NativeMethods.TaskDialogIndirect(config, out ret, out selRadio, out setVerification) != IntPtr.Zero)
					throw new Exception("Native TaskDialogIndirect call failed.");
			}
			catch (Exception ex) {
				throw new Exception("Unable to create Task Dialog.", ex);
			}
            finally {
                PostConfig();
            }

            return new Results(ret, selRadio, setVerification);
        }

        #endregion

        #region Static display methods

        /// <summary>Displays a task dialog that has a message.</summary>
        /// <param name="text">The text to display.</param>
        public static Result Show(string instruction) {
            return InternalShow(IntPtr.Zero, defaultTitle, instruction, defaultContent, TaskDialogButton.OK, TaskDialogIcon.None);
        }

        /// <summary>Displays a task dialog that has a message and a title.</summary>
        /// <param name="text">The text to display.</param>
        /// <param name="title">The title bar caption of the dialog.</param>
        public static Result Show(string instruction, string title) {
            return InternalShow(IntPtr.Zero, title, instruction, defaultContent, TaskDialogButton.OK, TaskDialogIcon.None);
        }

        /// <summary>Displays a task dialog that has a message, a title and an instruction.</summary>
        /// <param name="text">The text to display.</param>
        /// <param name="title">The title bar caption of the dialog.</param>
        /// <param name="instruction">The instruction shown below the main text.</param>
        public static Result Show(string instruction, string title, string content) {
            return InternalShow(IntPtr.Zero, title, instruction, content, TaskDialogButton.OK, TaskDialogIcon.None);
        }

        /// <summary>Displays a task dialog that has a message, a title, an instruction and one or more buttons.</summary>
        /// <param name="text">The text to display.</param>
        /// <param name="title">The title bar caption of the dialog.</param>
        /// <param name="instruction">The instruction shown below the main text.</param>
        /// <param name="buttons">Value that specifies which button or buttons to display.</param>
        public static Result Show(string instruction, string title, string content, TaskDialogButton buttons) {
            return InternalShow(IntPtr.Zero, title, instruction, content, buttons, TaskDialogIcon.None);
        }

        /// <summary>Displays a task dialog that has a message, a title, an instruction, one or more buttons and an icon.</summary>
        /// <param name="text">The text to display.</param>
        /// <param name="title">The title bar caption of the dialog.</param>
        /// <param name="instruction">The instruction shown below the main text.</param>
        /// <param name="buttons">Value that specifies which button or buttons to display.</param>
        /// <param name="icon">The icon to display.</param>
        public static Result Show(string instruction, string title, string content, TaskDialogButton buttons, TaskDialogIcon icon) {
            return InternalShow(IntPtr.Zero, title, instruction, content, buttons, icon);
        }


        private static Result InternalShow(IntPtr parent, string title, string instruction, string content, TaskDialogButton commonButtons, TaskDialogIcon icon) {
            int dlgValue;

            //Get handle for parent window if none specified (behave like MessageBox)
            if(parent == IntPtr.Zero)
                parent = Native.Windows.GetActiveWindow();

            if (NativeMethods.TaskDialog(parent, IntPtr.Zero, title, instruction, content, (int)commonButtons, new IntPtr((long)icon), out dlgValue) != 0)
                throw new Exception("Failed to create TaskDialog.");

            //Convert int value to common dialog result
            if (dlgValue > 0 && dlgValue <= 8)
                return (Result)dlgValue;
            else
                return Result.None;
        }

        #endregion

    }

}
