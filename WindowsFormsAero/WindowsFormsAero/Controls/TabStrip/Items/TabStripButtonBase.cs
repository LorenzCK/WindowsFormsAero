//--
// Windows Forms Aero Controls
// http://www.CodePlex.com/VistaControls
//
// Copyright (c) 2008 Jachym Kouba
// Licensed under Microsoft Reciprocal License (Ms-RL) 
//--
using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsAero
{
    [System.ComponentModel.DesignerCategory("Code")]
    public abstract class TabStripButtonBase : ToolStripButton
    {
        private sealed class TabStripButtonLayout
        {
            public readonly Rectangle DisplayRectangle;
            public readonly Rectangle ImageRectangle;
            public readonly Rectangle TextRectangle;
            public readonly Rectangle CloseRectangle;

            private static Rectangle CutRectangleFromLeft(ref Rectangle displayRect, Size subRectSize, Padding padding, ContentAlignment alignment)
            {
                Rectangle result = new Rectangle();

                if (!subRectSize.IsEmpty)
                {
                    result = new Rectangle(
                        displayRect.X,
                        displayRect.Y,
                        subRectSize.Width + padding.Left,
                        displayRect.Height);

                    displayRect.X += result.Width;
                    displayRect.Width -= result.Width;

                    result = GetAlignedRectagle(subRectSize, result, alignment);
                }

                return result;
            }

            private Rectangle CutRectangleFromRight(ref Rectangle displayRect, Size subRectSize, Padding padding, ContentAlignment alignment)
            {
                Rectangle result = new Rectangle();

                if (!subRectSize.IsEmpty)
                {
                    result = new Rectangle(
                        displayRect.Right - subRectSize.Width - padding.Right,
                        displayRect.Top,
                        subRectSize.Width + padding.Right,
                        displayRect.Height);

                    displayRect.Width -= result.Width;

                    result = GetAlignedRectagle(subRectSize, result, alignment);
                }

                return result;
            }


            public TabStripButtonLayout(TabStripButtonBase btn)
            {
                DisplayRectangle = new Rectangle
                (
                    new Point(btn.Padding.Left, btn.Padding.Top),
                    new Size(btn.Width - btn.Padding.Horizontal, 
                             btn.Height - btn.Padding.Vertical
                    )
                );

                if (btn.Owner.RightToLeft == RightToLeft.Yes)
                {
                    CloseRectangle = CutRectangleFromLeft(ref DisplayRectangle, btn.CloseButtonSize, btn.Padding, ContentAlignment.MiddleCenter);
                    ImageRectangle = CutRectangleFromRight(ref DisplayRectangle, btn.ImageSize, btn.Padding, btn.ImageAlign);
                }
                else
                {
                    ImageRectangle = CutRectangleFromLeft(ref DisplayRectangle, btn.ImageSize, btn.Padding, btn.ImageAlign);
                    CloseRectangle = CutRectangleFromRight(ref DisplayRectangle, btn.CloseButtonSize, btn.Padding, ContentAlignment.MiddleCenter);
                }

                if (!string.IsNullOrEmpty(btn.Text) && ((btn.DisplayStyle & ToolStripItemDisplayStyle.Text) != 0))
                {
                    TextRectangle = DisplayRectangle;
                }
            }

            private static Rectangle GetAlignedRectagle(Size size, Rectangle container, ContentAlignment alignment)
            {
                if ((     size.Height == 0 ||      size.Width == 0) ||
                    (container.Height == 0 || container.Width == 0))
                {
                    return Rectangle.Empty;
                }

                if ((size.Width  >= container.Width) &&
                    (size.Height >= container.Height))
                {
                    return container;
                }

                Rectangle result = new Rectangle(Point.Empty, size);

                AlignRectVertically(size, container, alignment, ref result);
                AlignRectHorizontally(size, container, alignment, ref result);

                return result;
            }

            private static void AlignRectHorizontally(Size size, Rectangle container, ContentAlignment alignment, ref Rectangle result)
            {
                if (size.Width >= container.Width)
                {
                    result.X = container.Left;
                    result.Width = container.Width;

                    return;
                }

                switch (alignment)
                {
                    case ContentAlignment.BottomLeft:
                    case ContentAlignment.MiddleLeft:
                    case ContentAlignment.TopLeft:
                        result.X = container.Left;
                        return;

                    case ContentAlignment.BottomCenter:
                    case ContentAlignment.MiddleCenter:
                    case ContentAlignment.TopCenter:
                        result.X = container.Left + ((container.Width - size.Width) / 2);
                        return;

                    case ContentAlignment.BottomRight:
                    case ContentAlignment.MiddleRight:
                    case ContentAlignment.TopRight:
                        result.X = container.Right - size.Width;
                        return;
                }

                throw new ArgumentException();
            }

            private static void AlignRectVertically(Size size, Rectangle container, ContentAlignment alignment, ref Rectangle result)
            {
                if (size.Height >= container.Height)
                {
                    result.Y = container.Top;
                    result.Height = container.Height;
                    
                    return;
                }
                
                switch (alignment)
                {
                    case ContentAlignment.TopCenter:
                    case ContentAlignment.TopLeft:
                    case ContentAlignment.TopRight:
                        result.Y = container.Top;
                        return;

                    case ContentAlignment.MiddleCenter:
                    case ContentAlignment.MiddleLeft:
                    case ContentAlignment.MiddleRight:
                        result.Y = container.Top + ((container.Height - size.Height) / 2);
                        return;

                    case ContentAlignment.BottomCenter:
                    case ContentAlignment.BottomLeft:
                    case ContentAlignment.BottomRight:
                        result.Y = container.Bottom - size.Height;
                        return;
                }

                throw new ArgumentException();
            }
        }

        private TabStripButtonLayout _layout;
        private TabStripCloseButtonState _closeButtonState;

        protected TabStripButtonBase()
        {
        }

        protected TabStripButtonBase(String text)
            : base(text)
        {
        }
        
        protected TabStripButtonBase(Image image)
            : base(image)
        {
        }
        
        protected TabStripButtonBase(String text, Image image)
            : base(text, image)
        {
        }
        
        protected TabStripButtonBase(String text, Image image, EventHandler onClick)
            : base(text, image, onClick)
        {
        }

        protected TabStripButtonBase(String text, Image image, EventHandler onClick, String name)
            : base(text, image, onClick, name)
        {
        }

        protected override void OnLayout(LayoutEventArgs e)
        {
            base.OnLayout(e);
            InvalidateInternalLayout();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (InternalLayout.CloseRectangle.Contains(e.Location))
            {
                CloseButtonState = TabStripCloseButtonState.Pressed;
            }

            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if ((CloseButtonState == TabStripCloseButtonState.Pressed) && 
                (InternalLayout.CloseRectangle.Contains(e.Location)))
            {
                CloseButtonState = TabStripCloseButtonState.Normal;
                MessageBox.Show("huuu!");
            }

            base.OnMouseUp(e);
        }

        protected override void OnMouseMove(MouseEventArgs mea)
        {
            if (CloseButtonState != TabStripCloseButtonState.Pressed)
            {
                if (InternalLayout.CloseRectangle.Contains(mea.Location))
                {
                    CloseButtonState = TabStripCloseButtonState.Selected;
                }
                else
                {
                    CloseButtonState = TabStripCloseButtonState.Normal;
                }
            }

            base.OnMouseMove(mea);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            CloseButtonState = TabStripCloseButtonState.Normal;
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

        protected override Padding DefaultPadding
        {
            get { return new Padding(4, 4, 4, 0); }
        }

        protected override Padding DefaultMargin
        {
            get { return new Padding(0, 6, 0, 0); }
        }

        protected TabStripRenderer Renderer
        {
            get
            {
                if (Owner != null)
                {
                    return Owner.Renderer as TabStripRenderer;
                }

                return null;
            }
        }

        internal virtual Size ImageSize
        {
            get
            {
                if (IsBusyInternal && Renderer != null)
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

        internal virtual Size CloseButtonSize
        {
            get
            {
                if (IsClosableInternal && Renderer != null)
                {
                    return Renderer.GetCloseButtonSize(this);
                }

                return Size.Empty;
            }
        }

        internal virtual bool IsBusyInternal
        {
            get { return false; }
        }

        internal virtual bool IsClosableInternal
        {
            get { return false; }
        }

        internal virtual void OnPaintBackground(TabStripItemRenderEventArgs e)
        {
            Renderer.DrawTabItemBackground(e);
        }

        internal virtual void OnPaintImage(ToolStripItemImageRenderEventArgs e)
        {
            if (!InternalLayout.ImageRectangle.IsEmpty)
            {
                if (IsBusyInternal)
                {
                    Renderer.DrawTabItemBusyImage(new TabStripItemBusyImageRenderEventArgs(e));
                }
                else
                {
                    Renderer.DrawItemImage(e);
                }
            }
        }

        internal virtual void OnPaintText(ToolStripItemTextRenderEventArgs e)
        {
            if (!InternalLayout.TextRectangle.IsEmpty)
            {
                Renderer.DrawItemText(e);
            }
        }

        internal void InvalidateInternalLayout()
        {
            _layout = null;
        }

        private TabStripButtonLayout InternalLayout
        {
            get { return _layout ?? (_layout = new TabStripButtonLayout(this)); }
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
    }
}
