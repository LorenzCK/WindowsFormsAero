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
    internal sealed class TabStripScrollButton : TabStripButtonBase
    {
        private readonly TabStripScrollDirection _direction;

        public TabStripScrollButton(TabStripScrollDirection direction)
        {
            if (direction != TabStripScrollDirection.Left &&
                direction != TabStripScrollDirection.Right)
            {
                throw new ArgumentOutOfRangeException("direction");
            }

            _direction = direction;
        }

        public TabStripScrollDirection ScrollDirection
        {
            get { return _direction; }
        }

        public override Size GetPreferredSize(Size constrainingSize)
        {
            return new Size(17, constrainingSize.Height);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (Renderer != null)
            {
                var chevronRect = new Rectangle(
                    new Point(Padding.Left, Padding.Top),
                    new Size(Width - Padding.Horizontal, Height - Padding.Vertical));
                
                var args = new TabStripScrollButtonRenderEventArgs(e.Graphics, this, chevronRect);
                
                Renderer.DrawTabScrollChevron(args);
            }
        }
    }
}
