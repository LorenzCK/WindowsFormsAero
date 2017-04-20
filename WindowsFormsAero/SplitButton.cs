/*****************************************************
 * WindowsFormsAero
 * https://github.com/LorenzCK/WindowsFormsAero
 * http://windowsformsaero.codeplex.com
 *
 * Author: Nicholas Kwan
 *****************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WindowsFormsAero.Native;

namespace WindowsFormsAero {

    /// <summary>
    /// A complex button provided with a secondary split push button
    /// that open up a context menu.
    /// </summary>
    /// <remarks>
    /// See: http://www.codeproject.com/KB/vista/themedvistacontrols.aspx
    /// </remarks>
    public class SplitButton : Button {

        protected override CreateParams CreateParams {
            get {
                CreateParams p = base.CreateParams;

                p.Style |= (int)(
                    (IsDefault) ? ButtonStyle.DefaultSplitButton : ButtonStyle.SplitButton
                );

                return p;
            }
        }

        #region Split Context Menu

        /// <summary>
        /// Occurs when the split label is clicked.
        /// </summary>
        [
        Description("Occurs when the split button is clicked."),
        Category("Action")
        ]
        public event EventHandler<SplitMenuEventArgs> SplitClick;

        /// <summary>
        /// Occurs when the split label is clicked, but before the associated
        /// context menu is displayed by the control.
        /// </summary>
        [
        Description("Occurs when the split label is clicked, but before the associated context menu is displayed."),
        Category("Action")
        ]
        public event EventHandler<SplitMenuEventArgs> SplitMenuOpening;

        protected virtual void OnSplitClick(SplitMenuEventArgs e) {
            SplitClick?.Invoke(this, e);

            if (SplitMenu == null && SplitMenuStrip == null)
                return;

            SplitMenuOpening?.Invoke(this, e);

            if (e.PreventOpening)
                return;

            var pBottomLeft = new System.Drawing.Point(e.DrawArea.Left, e.DrawArea.Bottom);
            if (SplitMenu != null) {
                SplitMenu.Show(this, pBottomLeft);
            }
            else if (SplitMenuStrip != null) {
                SplitMenuStrip.Width = e.DrawArea.Width;
                SplitMenuStrip.Show(this, pBottomLeft);
            }
        }

        /// <summary>
        /// Gets or sets the associated context menu that is displayed when the split
        /// glyph of the button is clicked.
        /// </summary>
        [
        Description("Sets the context menu that is displayed by clicking on the split button."),
        Category("Behavior"),
        DefaultValue(null)
        ]
        public ContextMenuStrip SplitMenuStrip { get; set; }

        /// <summary>
        /// Gets or sets the associated context menu that is displayed when the split
        /// glyph of the button is clicked.
        /// </summary>
        /// <remarks>
        /// Exposed for backward compatibility with legacy context menu classes.
        /// If both <see cref="SplitMenuStrip"/> and <see cref="SplitMenu"/> are
        /// set, the first is preferred.
        /// </remarks>
        [
        Description("Sets the context menu that is displayed by clicking on the split button."),
        Category("Behavior"),
        DefaultValue(null)
        ]
        public ContextMenu SplitMenu { get; set; }

        #endregion Split Context Menu

        bool _alignLeft = false;

        /// <summary>
        /// Gets or sets whether the split button should be aligned on the left side of the button.
        /// </summary>
        [
        Description("Align the split button on the left side of the button."),
        Category("Appearance"),
        DefaultValue(false)
        ]
        public bool SplitButtonAlignLeft {
            get {
                return _alignLeft;
            }
            set {
                _alignLeft = value;
                UpdateStyle();
            }
        }

        bool _noSplit = false;

        /// <summary>
        /// Gets or sets whether the split button should be shown or not.
        /// </summary>
        [
        Description("Hide the split button."),
        Category("Appearance"),
        DefaultValue(false)
        ]
        public bool SplitButtonNoSplit {
            get {
                return _noSplit;
            }
            set {
                _noSplit = value;
                UpdateStyle();
            }
        }

        private void UpdateStyle() {
            //TODO: add "no split" style

            var info = new ButtonSplitInfo();
            info.Mask = ButtonSplitInfo.MaskType.Style;
            info.Style = (
                (SplitButtonAlignLeft) ? ButtonSplitInfo.SplitStyle.AlignLeft : ButtonSplitInfo.SplitStyle.None
            );

            using(var hSplitInfo = new StructWrapper<ButtonSplitInfo>(info)) {
                Methods.SendMessage(Handle,
                    (int)WindowMessage.BCM_SETSPLITINFO,
                    IntPtr.Zero,
                    hSplitInfo);
            }
        }

        protected override void WndProc(ref Message m) {
            if (m.Msg == (int)WindowMessage.BCM_SETDROPDOWNSTATE) {
                if (m.WParam.ToInt32() == 1) {
                    OnSplitClick(new SplitMenuEventArgs(ClientRectangle));
                }
            }

            base.WndProc(ref m);
        }

    }

}
