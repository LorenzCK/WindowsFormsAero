using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsAero
{
    internal sealed class TabStripButtonLayout
    {
        public readonly Rectangle DisplayRectangle;
        public readonly Rectangle ImageRectangle;
        public readonly Rectangle TextRectangle;
        public readonly Rectangle CloseRectangle;

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

            if (!string.IsNullOrEmpty(btn.TextOrDefault) && ((btn.DisplayStyle & ToolStripItemDisplayStyle.Text) != 0))
            {
                TextRectangle = DisplayRectangle;
            }
        }

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

        private static Rectangle CutRectangleFromRight(ref Rectangle displayRect, Size subRectSize, Padding padding, ContentAlignment alignment)
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

        private static Rectangle GetAlignedRectagle(Size size, Rectangle container, ContentAlignment alignment)
        {
            if ((size.Height == 0 || size.Width == 0) ||
                (container.Height == 0 || container.Width == 0))
            {
                return Rectangle.Empty;
            }

            if ((size.Width >= container.Width) &&
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
}
