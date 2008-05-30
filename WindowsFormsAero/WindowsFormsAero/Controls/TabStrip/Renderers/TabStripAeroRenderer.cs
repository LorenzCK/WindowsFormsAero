//--
// Windows Forms Aero Controls
// http://www.CodePlex.com/VistaControls
//
// Copyright (c) 2008 Jachym Kouba
// Licensed under Microsoft Reciprocal License (Ms-RL) 
//--
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace WindowsFormsAero
{
    public class TabStripAeroRenderer : TabStripRenderer
    {
        private const float TabBackgroundRatio = 0.25f;
        private const float TabStripBackgroundRatio = 0.4f;

        private const int BusyTimerInterval = 100;
        private const int BusyImagePadding = 12;

        private static readonly Color TabBorderColor = Color.FromArgb(145, 150, 162);
        
        private static readonly Color[] SelectedColors = new Color[]
        {
            Color.FromArgb(252, 253, 253),
            Color.FromArgb(198, 221, 247),
            Color.FromArgb(153, 198, 238),
            Color.FromArgb(224, 236, 251),
        };

        private static readonly Color[] CheckedColors = new Color[]
        {
            Color.FromArgb(252, 253, 253),
            Color.FromArgb(231, 245, 251),
            Color.FromArgb(207, 231, 250),
            Color.FromArgb(185, 209, 250),
        };

        private static readonly Color[] NormalColors = new Color[]
        {
            Color.White,
            Color.FromArgb(227, 232, 244),
            Color.FromArgb(207, 215, 235),
            Color.FromArgb(233, 236, 250),
        };

        private Int32 _currentTickCount;
        private Image _currentBusyImage;
        
        public TabStripAeroRenderer()
            : base(new ToolStripAeroRenderer())
        {
            BusyTabRefreshInterval = TimeSpan.FromMilliseconds(BusyTimerInterval);
            RenderUsingVisualStyles = true;
            RenderBackground = true;
        }

        public bool RenderBackground
        {
            get;
            set;
        }

        public bool RenderUsingVisualStyles
        {
            get;
            set;
        }

        public override Color SelectedTabBottomColor
        {
            get { return CheckedColors[3]; }
        }

        public override Size GetBusyImageSize(ToolStripItem item)
        {
            return BusyImageSize;
        }

        public override Size GetCloseButtonSize(ToolStripItem item)
        {
            return new Size(17, 16);
        }

        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            if (e.ToolStrip is TabStrip)
            {
                if (VisualStylesAvailable && RenderBackground)
                {
                    FillGradient(e.Graphics,
                        GetTabStripUpperBorderPath(e),
                        NormalColors,
                        TabStripBackgroundRatio);
                }
                else if (VisualStylesAvailable)
                {
                    e.Graphics.Clear(Color.FromArgb(0, Color.White));
                }
                else
                {
                    e.Graphics.Clear(SystemColors.Control);
                }
            }
            else
            {
                base.OnRenderToolStripBackground(e);
            }
        }

        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            var tabStrip = e.ToolStrip as TabStrip;

            if (tabStrip != null)
            {
                if (VisualStylesAvailable)
                {
                    RenderToolStripBorderUsingVisualStyles(e, tabStrip);
                }
            }
            else
            {
                base.OnRenderToolStripBorder(e);
            }
        }

        protected override void OnRenderTabInsertionMark(TabStripInsertionMarkRenderEventArgs e)
        {
            var padding = e.TabStrip.Padding;
            var height = e.TabStrip.Height;

            var boundsDown = new Rectangle(e.Location - 3, padding.Top + 1, 7, 4);
            var boundsUp = new Rectangle(e.Location - 4, height - padding.Bottom - 5, 8, 5);
            
            using (var path = GetTriangleDownPath(boundsDown))
            {
                e.Graphics.FillPath(Brushes.Black, path);
            }

            using (var path = GetTriangleUpPath(boundsUp))
            {
                e.Graphics.FillPath(Brushes.Black, path);
            }
        }
        
        protected override void OnRenderTabItemBackground(TabStripItemRenderEventArgs e)
        {
            var isChecked = false;
            var button = e.Item as ToolStripButton;

            if (button != null && button.Checked)
            {
                isChecked = true;
            }

            if (VisualStylesAvailable)
            {
                RenderTabItemBackgroundUsingVisualStyles(e, isChecked);
            }
            else
            {
                var rect = new Rectangle(0, 0, e.Item.Width - 1, e.Item.Height - 1);
                if (isChecked)
                {
                    using (var brush = new LinearGradientBrush(rect,
                        SystemColors.ControlLightLight,
                        SystemColors.Control,
                        LinearGradientMode.Vertical))
                    {
                        e.Graphics.FillRectangle(brush, rect);
                    }
                }

                ControlPaint.DrawBorder3D(e.Graphics, rect, Border3DStyle.Raised,
                    Border3DSide.Left | Border3DSide.Top | Border3DSide.Right);

                if (!e.CloseButtonRectangle.IsEmpty)
                {
                    var closeRect = e.CloseButtonRectangle;

                    if (e.CloseButtonState != TabStripCloseButtonState.Pressed)
                    {
                        closeRect.Offset(-2, -2);
                    }

                    using (var marlett = new Font("Marlett", 8))
                    {
                        TextRenderer.DrawText(e.Graphics, "r", marlett, closeRect, SystemColors.ControlText);
                    }
                }
            }
        }

        protected override void OnRenderTabItemBusyImage(TabStripItemBusyImageRenderEventArgs e)
        {
            UpdateBusyImage(e.TickCount);
            e.Graphics.DrawImageUnscaled(_currentBusyImage, e.ImageRectangle);
        }

        protected override void OnRenderItemImage(ToolStripItemImageRenderEventArgs e)
        {
            if (e.ImageRectangle.Width > 0 && e.ImageRectangle.Height > 0)
            {
                e.Graphics.DrawImage(e.Image, e.ImageRectangle);
            }
        }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            if (e.Item is TabStripButtonBase)
            {
                var sf = new StringFormat(StringFormatFlags.NoWrap)
                {
                    LineAlignment = StringAlignment.Center,
                    Trimming = StringTrimming.EllipsisCharacter
                };

                if ((e.ToolStrip != null) && (e.ToolStrip.RightToLeft == RightToLeft.Yes))
                {
                    sf.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
                }

                using (var brush = new SolidBrush(e.TextColor))
                {
                    // use GDI+ instead of TextRenderer to be compatible 
                    // with the glass effect

                    e.Graphics.DrawString(e.Text, e.TextFont, brush, e.TextRectangle, sf);
                }
            }
            else
            {
                base.OnRenderItemText(e);
            }
        }

        protected override void OnRenderTabScrollChevron(TabStripScrollButtonRenderEventArgs e)
        {
            const int ChevronWidth = 8;
            const int ChevronHeight = 5;

            Pen pen = Pens.Black; 
            
            try
            {
                int x = e.ChevronRectangle.Left + ((e.ChevronRectangle.Width - ChevronWidth) / 2);
                int y = e.ChevronRectangle.Top + ((e.ChevronRectangle.Height - ChevronHeight) / 2);

                bool left = (e.ScrollDirection == TabStripScrollDirection.Left) && 
                            (e.ToolStrip.RightToLeft == RightToLeft.No);

                if (e.ToolStrip.RightToLeft == RightToLeft.Yes)
                {
                    left = (e.ScrollDirection == TabStripScrollDirection.Right);
                }

                if (left)
                {
                    x += 2;
                }

                if (!e.Item.Enabled)
                {
                    pen = new Pen(Color.FromArgb(178, 178, 178), 1);
                }

                for (int i = 0; i < ChevronHeight; i++, y++)
                {
                    e.Graphics.DrawLine(pen, x,     y, 
                                             x + 1, y);

                    e.Graphics.DrawLine(pen, x + 4,     y,
                                             x + 4 + 1, y);

                    if (i < (ChevronHeight / 2))
                    {
                        x += (left) ? -1 : +1;
                    }
                    else
                    {
                        x += (left) ? +1 : -1;
                    }
                }
            }
            finally
            {
                if (!e.Item.Enabled)
                {
                    pen.Dispose();
                }
            }
        }

        private void UpdateBusyImage(int tickCount)
        {
            var frameSize = BusyImageSize;

            if (_currentBusyImage == null)
            {
                _currentBusyImage = CreateDesktopCompatibleBitmap(frameSize);
            }
            else if (_currentTickCount == tickCount)
            {
                return;
            }

            _currentTickCount = tickCount;

            int frame = tickCount % (BusyImage.Width / frameSize.Width);
            int srcX = (frame * BusyImage.Height);

            var destRect = new Rectangle(Point.Empty, frameSize);
            var srcRect = new Rectangle(new Point(srcX, 0), frameSize);

            using (Graphics graphics = Graphics.FromImage(_currentBusyImage))
            {
                graphics.Clear(Color.Transparent);
                graphics.DrawImage(BusyImage, destRect, srcRect, GraphicsUnit.Pixel);
            }
        }

        private Boolean VisualStylesAvailable
        {
            get { return VisualStyleRenderer.IsSupported && RenderUsingVisualStyles; }
        }

        private static void RenderToolStripBorderUsingVisualStyles(ToolStripRenderEventArgs e, TabStrip tabStrip)
        {
            var left = e.AffectedBounds.Left;
            var right = e.AffectedBounds.Right - 1;
            var y = e.AffectedBounds.Bottom - tabStrip.Padding.Bottom;

            var selectedTab = tabStrip.SelectedTab;

            using (var paddingPen = new Pen(CheckedColors[3], 1))
            {
                using (var borderPen = new Pen(TabBorderColor))
                {
                    if (selectedTab != null)
                    {
                        e.Graphics.DrawLine(borderPen, left, y, selectedTab.Bounds.Left, y);
                        e.Graphics.DrawLine(Pens.White, left, y + 1, selectedTab.Bounds.Left, y + 1);

                        e.Graphics.DrawLine(paddingPen, selectedTab.Bounds.Left, y, selectedTab.Bounds.Right, y);
                        e.Graphics.DrawLine(paddingPen, selectedTab.Bounds.Left, y + 1, selectedTab.Bounds.Right, y + 1);

                        e.Graphics.DrawLine(borderPen, selectedTab.Bounds.Right, y, right, y);
                        e.Graphics.DrawLine(Pens.White, selectedTab.Bounds.Right, y + 1, right, y + 1);
                    }
                    else
                    {
                        e.Graphics.DrawLine(borderPen, left, y, right, y);
                        e.Graphics.DrawLine(Pens.White, left, y, right, y);
                    }
                }

                using (var paddingBrush = new SolidBrush(paddingPen.Color))
                {
                    var paddingRect = Rectangle.FromLTRB(
                        e.AffectedBounds.Left,
                        y + 2,
                        e.AffectedBounds.Right,
                        e.AffectedBounds.Bottom);

                    e.Graphics.FillRectangle(paddingBrush, paddingRect);
                }
            }
        }

        private static void RenderTabItemBackgroundUsingVisualStyles(TabStripItemRenderEventArgs e, bool isChecked)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            using (GraphicsPath outer = GetTabBorderPath(e.Item.Width, e.Item.Height, isChecked, false))
            {
                var colors = NormalColors;

                if (e.Item.Enabled && isChecked)
                {
                    colors = CheckedColors;
                }
                else if (e.Item.Selected)
                {
                    colors = SelectedColors;
                }

                FillGradient(e.Graphics, outer, colors, TabBackgroundRatio);

                using (Pen pen = new Pen(TabBorderColor, 1))
                {
                    e.Graphics.DrawPath(Pens.Gray, outer);
                }

                using (GraphicsPath inner = GetTabBorderPath(e.Item.Width, e.Item.Height, isChecked, true))
                {
                    e.Graphics.DrawPath(Pens.White, inner);
                }
            }

            if (!e.CloseButtonRectangle.IsEmpty)
            {
                var element = VisualStyleElement.ToolTip.Close.Normal;

                if (e.CloseButtonState == TabStripCloseButtonState.Selected)
                {
                    element = VisualStyleElement.ToolTip.Close.Hot;
                }
                if (e.CloseButtonState == TabStripCloseButtonState.Pressed)
                {
                    element = VisualStyleElement.ToolTip.Close.Pressed;
                }

                VisualStyleRenderer renderer = new VisualStyleRenderer(element);
                renderer.DrawBackground(e.Graphics, e.CloseButtonRectangle);
            }
        }

        private static GraphicsPath GetTabBorderPath(int w, int h, bool isChecked, bool inner)
        {
            int top = isChecked ? 2 : 4;
            int delta = inner ? 1 : 0;

            return new GraphicsPath(
                new Point[]
                {
                    new Point(delta, h),
                    new Point(delta, top + delta + 2),
                    new Point(delta + 2, top + delta),

                    new Point(w - delta - 3, top + delta),
                    new Point(w - delta - 1, top + delta + 2),
                    new Point(w - delta - 1, h),
                },
                new byte[]
                {
                    (byte)(PathPointType.Start),
                    (byte)(PathPointType.Line),
                    (byte)(PathPointType.Line),
                    (byte)(PathPointType.Line),
                    (byte)(PathPointType.Line),
                    (byte)(PathPointType.Line),
                });
        }

        private static GraphicsPath GetTabStripUpperBorderPath(ToolStripRenderEventArgs e)
        {
            var bounds = e.AffectedBounds;

            return new GraphicsPath(new Point[]
            {
                new Point(bounds.Left, bounds.Bottom),
                new Point(bounds.Left, bounds.Top + 1),
                new Point(bounds.Left + 1, bounds.Top),
                new Point(bounds.Right - 2, bounds.Top),
                new Point(bounds.Right - 1, bounds.Top + 1),
                new Point(bounds.Right - 1, bounds.Bottom)
            },
            new byte[]
            {
                (byte)(PathPointType.Start),
                (byte)(PathPointType.Line),
                (byte)(PathPointType.Line),
                (byte)(PathPointType.Line),
                (byte)(PathPointType.Line),
                (byte)(PathPointType.Line),
            });
        }

        private static Brush GetUpperBackgroundBrush(ref RectangleF rect, Color[] colors, float ratio)
        {
            rect.Height = 1 + (float)Math.Ceiling(rect.Height * ratio);

            return new LinearGradientBrush(
                rect,
                colors[0],
                colors[1],
                LinearGradientMode.Vertical);
        }

        private static Brush GetLowerBackgroundBrush(ref RectangleF rect, Color[] colors, float ratio)
        {
            rect.Y += (float)Math.Floor(rect.Height * ratio);
            rect.Height = (float)Math.Ceiling(rect.Height * (1.0f - ratio));

            return new LinearGradientBrush(
                rect,
                colors[2],
                colors[3],
                LinearGradientMode.Vertical);
        }

        private static void FillGradient(Graphics graphics, GraphicsPath path, Color[] colors, float ratio)
        {
            using (var clipRegion = new Region(path))
            {
                var bounds = clipRegion.GetBounds(graphics);

                var upperRect = bounds;
                var lowerRect = bounds;

                using (var oldClip = GraphicsExtensions.SwitchClip(graphics, clipRegion))
                {
                    using (var brush = GetUpperBackgroundBrush(ref upperRect, colors, ratio))
                    {
                        graphics.FillRectangle(brush, upperRect);
                    }

                    using (var brush = GetLowerBackgroundBrush(ref lowerRect, colors, ratio))
                    {
                        graphics.FillRectangle(brush, lowerRect);
                    }
                }
            }
        }

        private static GraphicsPath GetTriangleDownPath(Rectangle bounds)
        {
            return new GraphicsPath
            (
                new Point[]
                {
                    new Point(bounds.Left,                    bounds.Top),
                    new Point(bounds.Left + bounds.Width / 2, bounds.Bottom),
                    new Point(bounds.Right,                   bounds.Top),
                },
                new Byte[]
                {
                    (Byte)PathPointType.Start,
                    (Byte)PathPointType.Line,
                    (Byte)PathPointType.Line,
                }
            );
        }

        private static GraphicsPath GetTriangleUpPath(Rectangle bounds)
        {
            return new GraphicsPath
            (
                new Point[]
                {
                    new Point(bounds.Left,                    bounds.Bottom),
                    new Point(bounds.Left + bounds.Width / 2, bounds.Top),
                    new Point(bounds.Right,                   bounds.Bottom),
                },
                new Byte[]
                {
                    (Byte)PathPointType.Start,
                    (Byte)PathPointType.Line,
                    (Byte)PathPointType.Line,
                }
            );
        }

        private static Bitmap CreateDesktopCompatibleBitmap(Size size)
        {
            using (Graphics desktop = Graphics.FromHwnd(IntPtr.Zero))
            {
                return new Bitmap(size.Width, size.Height, desktop);
            }
        }

        private static Image BusyImage
        {
            get { return Resources.Images.TabBusy; }
        }

        private static Size BusyImageSize
        {
            get
            {
                return new Size(BusyImage.Height, BusyImage.Height);
            }
        }
    }
}
