using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsAero
{
    [ToolboxItem(false)]
    [Docking(DockingBehavior.Never)]
    [System.ComponentModel.DesignerCategory("Code")]
    public class AeroTabPage : Panel
    {
        private TabStripButton _button;

        [Browsable(false)]
        public AeroTabControl TabControl
        {
            get { return Parent as AeroTabControl; }
        }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public new string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public new event EventHandler TextChanged
        {
            add { base.TextChanged += value; }
            remove { base.TextChanged -= value; }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            if (_button != null)
            {
                _button.Text = Text;
            }

            base.OnTextChanged(e);
        }

        internal TabStripButton TabStripButton
        {
            get
            {
                if (_button == null)
                {
                    _button = new TabStripButton(Text);
                    
                    _button.Click += delegate 
                    {
                        if (TabControl != null)
                        {
                            TabControl.SelectedTab = this;
                        }
                    };

                    _button.CloseButtonClick += delegate 
                    {
                        if (TabControl != null)
                        {
                            TabControl.OnCloseButtonClick(new AeroTabPageEventArgs(this));
                        }
                    };
                }

                return _button;
            }
        }
    }
}
