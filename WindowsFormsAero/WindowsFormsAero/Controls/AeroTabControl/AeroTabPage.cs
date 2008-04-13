using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

namespace WindowsFormsAero
{
    [ToolboxItem(false)]
    [Docking(DockingBehavior.Never)]
    [System.ComponentModel.DesignerCategory("Code")]
    public class AeroTabPage : Panel
    {
        private TabStripButton _button;
        private Boolean _backColorAssigned;

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override AnchorStyles Anchor
        {
            get { return base.Anchor; }
            set { base.Anchor = value; }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override DockStyle Dock
        {
            get { return base.Dock; }
            set { base.Dock = value; }
        }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override Color BackColor
        {
            get { return base.BackColor; }
            set 
            {
                if (value == Color.Empty)
                {
                    ResetBackColor();
                }
                else
                {
                    this._backColorAssigned = true;
                    base.BackColor = value;
                }
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override void ResetBackColor()
        {
            var tabControl = Parent as AeroTabControl;

            if (tabControl != null)
            {
                base.BackColor = tabControl.TabPageBackColor;
            }

            _backColorAssigned = false;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeBackColor()
        {
            return _backColorAssigned;
        }


        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Size MaximumSize
        {
            get { return base.MaximumSize; }
            set { base.MaximumSize = value; }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Size MinimumSize
        {
            get { return base.MinimumSize; }
            set { base.MinimumSize = value; }
        }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public AeroTabControl TabControl
        {
            get { return Parent as AeroTabControl; }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool TabStop
        {
            get { return base.TabStop; }
            set { base.TabStop = value; }
        }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public new event EventHandler TextChanged
        {
            add { base.TextChanged += value; }
            remove { base.TextChanged -= value; }
        }

        protected override void OnParentChanged(EventArgs e)
        {
            if(!_backColorAssigned)
            {
                ResetBackColor();
            }

            base.OnParentChanged(e);
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
