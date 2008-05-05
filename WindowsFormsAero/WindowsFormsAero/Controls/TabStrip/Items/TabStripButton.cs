//--
// Windows Forms Aero Controls
// http://www.CodePlex.com/VistaControls
//
// Copyright (c) 2008 Jachym Kouba
// Licensed under Microsoft Reciprocal License (Ms-RL) 
//--
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace WindowsFormsAero
{
    [System.ComponentModel.DesignerCategory("Code")]
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.None)]
    public partial class TabStripButton : TabStripButtonBase
    {
        private static readonly object EventCloseButtonClick = new object();

        private Boolean _isBusy;
        private Boolean _isClosable = true;
        private TabStripCloseButtonState _closeButtonState;

        public TabStripButton()
        {
            Initialize();
        }

        public TabStripButton(string text)
            : base(text)
        {
            Initialize();
        }
        
        public TabStripButton(Image image)
            : base(image)
        {
            Initialize();
        }
        
        public TabStripButton(string text, Image image)
            : base(text, image)
        {
            Initialize();
        }
        
        public TabStripButton(string text, Image image, EventHandler onClick)
            : base(text, image, onClick)
        {
            Initialize();
        }

        public TabStripButton(string text, Image image, EventHandler onClick, string name)
            : base(text, image, onClick, name)
        {
            Initialize();
        }

        public event EventHandler CloseButtonClick
        {
            add { Events.AddHandler(EventCloseButtonClick, value); }
            remove { Events.RemoveHandler(EventCloseButtonClick, value); }
        }

        [Browsable(false)]
        [DefaultValue(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool Checked
        {
            get { return base.Checked; }
            set { base.Checked = value; }
        }

        [Browsable(false)]
        [DefaultValue(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool CheckOnClick
        {
            get { return false; }
            set { }
        }

        [Browsable(false)]
        [DefaultValue(CheckState.Unchecked)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new CheckState CheckState
        {
            get { return base.CheckState; }
            set { base.CheckState = value; }
        }

        [DefaultValue(ContentAlignment.MiddleLeft)]
        public new ContentAlignment ImageAlign
        {
            get { return base.ImageAlign; }
            set { base.ImageAlign = value; }
        }

        [DefaultValue(false)]
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                if (_isBusy != value)
                {
                    _isBusy = value;
                    InvalidateInternalLayout();

                    var ownerTabStrip = (Owner as TabStrip);

                    if (ownerTabStrip != null)
                    {
                        if (value)
                        {
                            ++ownerTabStrip.BusyTabCount;
                        }
                        else
                        {
                            --ownerTabStrip.BusyTabCount;
                        }

                        Invalidate();
                    }
                }
            }
        }

        [DefaultValue(true)]
        public bool IsClosable
        {
            get { return _isClosable; }
            set 
            {
                if (_isClosable != value)
                {
                    _isClosable = value;

                    if (Owner != null)
                    {
                        Owner.Invalidate();
                    }
                }
            }
        }

        [DefaultValue(ContentAlignment.MiddleLeft)]
        public override ContentAlignment TextAlign
        {
            get { return base.TextAlign; }
            set { base.TextAlign = value; }
        }

        [DefaultValue(TextImageRelation.ImageBeforeText)]
        public new TextImageRelation TextImageRelation
        {
            get { return base.TextImageRelation; }
            set { base.TextImageRelation = value; }
        }

        public void PerformCloseButtonClick()
        {
            if (CanClose)
            {
                OnCloseButtonClick(EventArgs.Empty);

                if (Owner != null)
                {
                    Owner.PerformCloseButtonClick(this);
                }
            }
        }

        protected override ToolStripItemDisplayStyle DefaultDisplayStyle
        {
            get { return ToolStripItemDisplayStyle.ImageAndText; }
        }

        protected override AccessibleObject CreateAccessibilityInstance()
        {
            return new TabStripButtonAccessibleObject(this);
        }

        protected virtual void OnCloseButtonClick(EventArgs e)
        {
            var handler = Events[EventCloseButtonClick] as EventHandler;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected override void OnClick(EventArgs e)
        {
            if (Owner != null)
            {
                Owner.SelectedTab = this;
            }

            base.OnClick(e);
        }

        protected override void OnLayout(LayoutEventArgs e)
        {
            base.OnLayout(e);
            InvalidateInternalLayout();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (CloseButtonState == TabStripCloseButtonState.Selected)
                {
                    CloseButtonState = TabStripCloseButtonState.Pressed;
                }
                else
                {
                    Owner.SelectedTab = this;
                }
            }
            else if (e.Button == MouseButtons.Middle)
            {
                PerformCloseButtonClick();
            }

            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if ((CloseButtonState == TabStripCloseButtonState.Pressed) &&
                (InternalLayout.CloseRectangle.Contains(e.Location)))
            {
                CloseButtonState = TabStripCloseButtonState.Normal;
                PerformCloseButtonClick();
            }

            base.OnMouseUp(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (CloseButtonState != TabStripCloseButtonState.Pressed)
            {
                if (InternalLayout.CloseRectangle.Contains(e.Location))
                {
                    CloseButtonState = TabStripCloseButtonState.Selected;
                }
                else
                {
                    CloseButtonState = TabStripCloseButtonState.Normal;
                }
            }

            base.OnMouseMove(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            CloseButtonState = TabStripCloseButtonState.Normal;

            base.OnMouseLeave(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (Renderer != null)
            {
                OnPaintBackground(new TabStripItemRenderEventArgs(
                    e.Graphics, this, InternalLayout.CloseRectangle, _closeButtonState));

                OnPaintImage(new ToolStripItemImageRenderEventArgs(
                    e.Graphics, this, InternalLayout.ImageRectangle));

                OnPaintText(new ToolStripItemTextRenderEventArgs(
                    e.Graphics, this, Text, InternalLayout.TextRectangle,
                    ForeColor, Font, TextAlign));
            }
            else
            {
                base.OnPaint(e);
            }
        }

        protected override void OnPaintImage(ToolStripItemImageRenderEventArgs e)
        {
            if (IsBusy)
            {
                Renderer.DrawTabItemBusyImage(new TabStripItemBusyImageRenderEventArgs(e));
            }
            else
            {
                base.OnPaintImage(e);
            }
        }

        protected override void OnParentChanged(ToolStrip oldParent, ToolStrip newParent)
        {
            var oldTabStrip = (oldParent as TabStrip);
            var newTabStrip = (newParent as TabStrip);

            if (_isBusy)
            {
                if (oldTabStrip != null)
                {
                    --oldTabStrip.BusyTabCount;
                }

                if (newTabStrip != null)
                {
                    ++newTabStrip.BusyTabCount;
                }
            }

            base.OnParentChanged(oldParent, newParent);
        }

        internal override Size CloseButtonSize
        {
            get
            {
                if (IsCloseButtonVisible && Renderer != null)
                {
                    return Renderer.GetCloseButtonSize(this);
                }

                return Size.Empty;
            }
        }

        internal override Size ImageSize
        {
            get
            {
                if (IsBusy && Renderer != null)
                {
                    return Renderer.GetBusyImageSize(this);
                }

                if ((Image != null) && ((DisplayStyle & ToolStripItemDisplayStyle.Image) != 0))
                {
                    return Image.Size;
                }

                return Size.Empty;
            }
        }

        internal override Boolean IsCloseButtonVisible
        {
            get { return Checked && CanClose; }
        }

        internal Boolean CanClose
        {
            get
            {
                if (Owner.CloseButtonVisibility == CloseButtonVisibility.Never)
                {
                    return false;
                }

                if (Owner.CloseButtonVisibility == CloseButtonVisibility.ExceptSingleTab)
                {
                    if (IsSingleTab)
                    {
                        return false;
                    }
                }

                return IsClosable;
            }
        }

        private TabStripCloseButtonState CloseButtonState
        {
            get { return _closeButtonState; }
            set
            {
                if (_closeButtonState != value)
                {
                    _closeButtonState = value;
                    Invalidate();
                }
            }
        }

        private Boolean IsSingleTab
        {
            get
            {
                int count = 0;

                foreach (var item in Owner.ItemsOfType<TabStripButton>())
                {
                    ++count;

                    if (count > 1)
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        private void Initialize()
        {
            DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            TextImageRelation = TextImageRelation.ImageBeforeText;
            ImageAlign = ContentAlignment.MiddleLeft;
            TextAlign = ContentAlignment.MiddleLeft;
            CheckOnClick = false;
        }
    }
}
