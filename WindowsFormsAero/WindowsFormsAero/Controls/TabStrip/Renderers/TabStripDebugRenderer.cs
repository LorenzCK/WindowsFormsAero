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
    public class TabStripDebugRenderer : TabStripRenderer
    {
        public TabStripDebugRenderer() : base(new ToolStripSystemRenderer())
        {
        }

        public override Size GetBusyImageSize(ToolStripItem item)
        {
            return new Size(16, 16);
        }

        public override Size GetCloseButtonSize(ToolStripItem item)
        {
            return new Size(16, 16);
        }

        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            e.Graphics.FillRectangle(SystemBrushes.Control, e.AffectedBounds);
        }

        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            var rect = new Rectangle(
                e.AffectedBounds.X,
                e.AffectedBounds.Y,
                e.AffectedBounds.Width - 1,
                e.AffectedBounds.Height - 1);

            e.Graphics.DrawRectangle(Pens.Brown, rect);
        }

        protected override void OnRenderTabItemBusyImage(TabStripItemBusyImageRenderEventArgs e)
        {
            DrawDottedRectangle(e.Graphics, Color.Gold, e.ImageRectangle);

        }

        protected override void OnRenderItemBackground(ToolStripItemRenderEventArgs e)
        {
        }

        protected override void OnRenderTabItemBackground(TabStripItemRenderEventArgs e)
        {
            var rect = new Rectangle(0, 0, e.Item.Width - 1, e.Item.Height - 1);
            e.Graphics.DrawRectangle(Pens.Green, rect);

            rect.Offset(e.Item.Padding.Left, e.Item.Padding.Top);
            rect.Width -= e.Item.Padding.Horizontal;
            rect.Height -= e.Item.Padding.Vertical;

            e.Graphics.DrawRectangle(Pens.Red, rect);

            DrawDottedRectangle(e.Graphics, Color.Black, e.CloseButtonRectangle);

            var tab = e.Item as TabStripButton;

            if (tab != null)
            {
                using (var font = new Font("Segoe UI", 10, FontStyle.Bold))
                {
                    int tabIndex = 0;

                    foreach (var item in e.ToolStrip.Items)
                    {
                        if (item == tab)
                        {
                            goto Render;
                        }

                        if (item is TabStripButton)
                        {
                            ++tabIndex;
                        }
                    }

                    tabIndex = -1;

                Render:
                    TextRenderer.DrawText(e.Graphics, tabIndex.ToString(), font, rect, Color.Red, TextFormatFlags.Left);
                }
            }
        }

        protected override void OnRenderItemImage(ToolStripItemImageRenderEventArgs e)
        {
            DrawDottedRectangle(e.Graphics, Color.HotPink, e.ImageRectangle);
        }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            DrawDottedRectangle(e.Graphics, Color.Fuchsia, e.TextRectangle);
        }

        private static void DrawDottedRectangle(Graphics graphics, Color color, Rectangle rect)
        {
            if (!rect.IsEmpty)
            {
                using (Pen p = new Pen(color, 1))
                {
                    p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

                    rect.Width -= 1;
                    rect.Height -= 1;

                    graphics.DrawRectangle(p, rect);
                }
            }
        }
    }
}
