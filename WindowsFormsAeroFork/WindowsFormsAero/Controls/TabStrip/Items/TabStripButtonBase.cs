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
        private TabStripButtonLayout _layout;

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

        public new TabStrip Owner
        {
            get { return base.Owner as TabStrip; }
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

        protected override void OnPaint(PaintEventArgs e)
        {
            if (Renderer != null)
            {
                OnPaintBackground(new TabStripItemRenderEventArgs(
                    e.Graphics, this, InternalLayout.CloseRectangle, TabStripCloseButtonState.Normal));

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

        protected virtual void OnPaintBackground(TabStripItemRenderEventArgs e)
        {
            Renderer.DrawTabItemBackground(e);
        }

        protected virtual void OnPaintImage(ToolStripItemImageRenderEventArgs e)
        {
            if (!e.ImageRectangle.IsEmpty && e.Image != null)
            {
                Renderer.DrawItemImage(e);
            }
        }

        protected virtual void OnPaintText(ToolStripItemTextRenderEventArgs e)
        {
            if (!e.TextRectangle.IsEmpty && !string.IsNullOrEmpty(e.Text))
            {
                Renderer.DrawItemText(e);
            }
        }

        internal virtual Size CloseButtonSize
        {
            get { return Size.Empty; }
        }

        internal virtual Size ImageSize
        {
            get 
            {
                if (Image != null)
                {
                    return Image.Size;
                }

                return Size.Empty; 
            }
        }

        internal virtual bool IsCloseButtonVisible
        {
            get { return false; }
        }

        internal virtual string TextOrDefault
        {
            get { return Text; }
        }

        internal TabStripButtonLayout InternalLayout
        {
            get { return _layout ?? (_layout = new TabStripButtonLayout(this)); }
        }

        internal void InvalidateInternalLayout()
        {
            _layout = null;
        }
    }
}
