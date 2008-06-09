using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using WindowsFormsAero.InteropServices;

namespace WindowsFormsAero
{
    public class ToolStripAeroRenderer : ToolStripSystemRenderer
    {
        private const string MenuClassName = "MENU";

        private VisualStyleRenderer _renderer;

        private readonly Dictionary<ToolStrip, Padding> _paddings =
            new Dictionary<ToolStrip, Padding>();

        protected override void Initialize(ToolStrip toolStrip)
        {
            Attach(toolStrip);
            base.Initialize(toolStrip);
        }

        //This is broken for RTL
        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
        {
            //if (IsSupported)
            //{
            //    int stateId = e.Item.Enabled ?
            //        MenuPopupSubMenuState.Normal :
            //        MenuPopupSubMenuState.Disabled;

            //    SetParameters(MenuPart.PopupSubmenu, stateId);
            //    _renderer.DrawBackground(e.Graphics, e.ArrowRectangle);
            //}
            //else
            //{
            //    base.OnRenderArrow(e);
            //}

            base.OnRenderArrow(e);
        }

        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            if (!IsSupported)
            {
                base.OnRenderToolStripBackground(e);
                return;
            }

            if (e.ToolStrip.IsDropDown)
            {
                SetParameters(MenuPart.PopupBackground, 0);
                DrawBackground(e);
            }
            else if (e.ToolStrip is MenuStrip)
            {
                SetParameters(MenuPart.BarBackground, e.ToolStrip.Enabled ?
                    MenuBarState.Active : MenuBarState.Inactive);

                DrawBackground(e);
            }
        }

        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            if (!IsSupported)
            {
                base.OnRenderToolStripBorder(e);
                return;
            }

            SetParameters(MenuPart.PopupBorders, 0);

            if (e.ToolStrip.IsDropDown)
            {
                Region oldClip = e.Graphics.Clip;

                //Tool strip borders are rendered *after* the content, for some reason.
                //So we have to exclude the inside of the popup otherwise we'll draw over it.

                Rectangle insideRect = e.ToolStrip.ClientRectangle;

                insideRect.Inflate(-1, -1);
                e.Graphics.ExcludeClip(insideRect);

                Renderer.DrawBackground(e.Graphics, e.ToolStrip.ClientRectangle, e.AffectedBounds);

                //Restore the old clip in case the Graphics is used again (does that ever happen?)
                e.Graphics.Clip = oldClip;
            }
        }

        protected override void OnRenderToolStripPanelBackground(ToolStripPanelRenderEventArgs e)
        {
            if (!IsSupported)
            {
                base.OnRenderToolStripPanelBackground(e);
                return;
            }

            Renderer.SetParameters(VisualStyleElement.Rebar.Band.Normal.ClassName, 0, 0);

            if (Renderer.IsBackgroundPartiallyTransparent())
            {
                Renderer.DrawParentBackground(e.Graphics, e.ToolStripPanel.ClientRectangle, e.ToolStripPanel);
            }

            Renderer.DrawBackground(e.Graphics, e.ToolStripPanel.ClientRectangle);

            Renderer.SetParameters(VisualStyleElement.Rebar.Band.Normal);

            foreach (ToolStripPanelRow row in e.ToolStripPanel.Rows)
            {
                Rectangle rowBounds = row.Bounds;
                rowBounds.Offset(0, -1);

                Renderer.DrawEdge(e.Graphics, rowBounds, Edges.Top, EdgeStyle.Etched, EdgeEffects.None);
            }

            e.Handled = true;
        }

        protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
        {
            if (!IsSupported)
            {
                base.OnRenderImageMargin(e);
                return;
            }

            if (e.ToolStrip.IsDropDown)
            {
                Rectangle displayRect = e.ToolStrip.DisplayRectangle;
                Rectangle marginRect = new Rectangle(0, displayRect.Top, displayRect.Left - 4, displayRect.Height);

                SetParameters(MenuPart.PopupGutter, 0);
                Renderer.DrawBackground(e.Graphics, marginRect, marginRect);
            }
        }

        protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e)
        {
            ToolStripMenuItem item = e.Item as ToolStripMenuItem;

            if (IsSupported && (item != null))
            {
                if (item.Checked)
                {
                    int checkBackground = MenuPopupCheckBackgroundState.Normal;
                    int checkType = MenuPopupCheckState.CheckmarkNormal;

                    if (!e.Item.Enabled)
                    {
                        checkBackground = MenuPopupCheckBackgroundState.Disabled;
                        checkType = MenuPopupCheckState.CheckmarkDisabled;
                    }

                    var rect = e.Item.Bounds;

                    rect.X += 2;
                    rect.Y -= 1;
                    rect.Width = rect.Height - 1;
                    rect.Height -= 2;

                    SetParameters(MenuPart.PopupCheckBackground, checkBackground);
                    Renderer.DrawBackground(e.Graphics, rect);

                    SetParameters(MenuPart.PopupCheck, checkType);
                    Renderer.DrawBackground(e.Graphics, rect);
                }
            }
            else
            {
                base.OnRenderItemCheck(e);
            }
        }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            if (!IsSupported)
            {
                base.OnRenderItemText(e);
                return;
            }

            if (e.Item.Owner.IsDropDown || e.Item.Owner is MenuStrip)
            {
                e.TextColor = Renderer.GetColor(ColorProperty.TextColor);
            }

            base.OnRenderItemText(e);
        }

        protected override void OnRenderItemImage(ToolStripItemImageRenderEventArgs e)
        {
            if (e.Item.Enabled)
            {
                base.OnRenderItemImage(e);
            }
            else
            {
                ControlPaint.DrawImageDisabled(e.Graphics, e.Image, e.ImageRectangle.X, e.ImageRectangle.Y, Color.Transparent);
            }
        }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            if (!IsSupported)
            {
                base.OnRenderMenuItemBackground(e);
                return;
            }

            SetItemParameters(e.Item);

            Rectangle rect = e.Item.ContentRectangle;

            if (!e.Item.Owner.IsDropDown)
            {
                rect.Y = 0;
                rect.X += 2;
                rect.Width -= 3;
                rect.Height = e.ToolStrip.Height - 5;
            }

            Renderer.DrawBackground(e.Graphics, rect, rect);
        }

        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            if (IsSupported && e.ToolStrip.IsDropDown)
            {
                Rectangle displayRect = e.ToolStrip.DisplayRectangle;
                Rectangle rect = new Rectangle(displayRect.Left, 0,
                                               displayRect.Width, e.Item.Height);

                SetParameters(MenuPart.PopupSeparator, 0);
                Renderer.DrawBackground(e.Graphics, rect, rect);
            }
            else
            {
                base.OnRenderSeparator(e);
            }
        }

        private VisualStyleRenderer Renderer
        {
            get
            {
                if ((_renderer == null) && IsSupported)
                {
                    _renderer = new VisualStyleRenderer(VisualStyleElement.Button.PushButton.Normal);
                }

                return _renderer;
            }
        }

        //protected override void OnRenderOverflowButtonBackground(ToolStripItemRenderEventArgs e)
        //{
        //    _renderer.SetParameters(VisualStyleElement.Rebar.Chevron.Normal);
        //    _renderer.DrawBackground(e.Graphics, e.Item.ContentRectangle);

        //    //base.OnRenderOverflowButtonBackground(e);
        //}

        private void OnPaddingChanged(object sender, EventArgs e)
        {
            var toolStrip = (ToolStrip)(sender);

            _paddings[toolStrip] = toolStrip.Padding;
        }

        private void OnRendererChanged(object sender, EventArgs e)
        {
            var toolStrip = (ToolStrip)(sender);

            if (toolStrip.Renderer != this)
            {
                Detach(toolStrip);
            }
        }

        private void OnSystemColorsChanged(object sender, EventArgs e)
        {
            var toolStrip = (ToolStrip)(sender);

            if (IsSupported)
            {
                try
                {
                    toolStrip.PaddingChanged -= OnPaddingChanged;
                    toolStrip.Padding = Padding.Empty;
                }
                finally
                {
                    toolStrip.PaddingChanged += OnPaddingChanged;
                }
            }
            else
            {
                try
                {
                    toolStrip.PaddingChanged -= OnPaddingChanged;
                    toolStrip.Padding = _paddings[toolStrip];
                }
                finally
                {
                    toolStrip.PaddingChanged += OnPaddingChanged;
                }
            }
        }

        private void Attach(ToolStrip toolStrip)
        {
            bool attached = _paddings.ContainsKey(toolStrip);

            _paddings[toolStrip] = toolStrip.Padding;

            if (IsSupported)
            {
                toolStrip.Padding = Padding.Empty;
            }

            if (!attached)
            {
                toolStrip.PaddingChanged += OnPaddingChanged;
                toolStrip.RendererChanged += OnRendererChanged;
                toolStrip.SystemColorsChanged += OnSystemColorsChanged;
            }
        }

        private void Detach(ToolStrip toolStrip)
        {
            toolStrip.PaddingChanged -= OnPaddingChanged;
            toolStrip.RendererChanged -= OnRendererChanged;
            toolStrip.SystemColorsChanged -= OnSystemColorsChanged;

            toolStrip.Padding = _paddings[toolStrip];
            _paddings.Remove(toolStrip);
        }

        private void SetParameters(MenuPart part, int state)
        {
            Renderer.SetParameters(MenuClassName, (int)(part), state);
        }

        private void SetItemParameters(ToolStripItem item)
        {
            SetParameters(GetMenuPart(item), GetItemState(item));
        }

        private void DrawBackground(ToolStripRenderEventArgs e)
        {
            if (Renderer.IsBackgroundPartiallyTransparent())
            {
                Renderer.DrawParentBackground(e.Graphics, e.ToolStrip.ClientRectangle, e.ToolStrip);
            }

            Renderer.DrawBackground(e.Graphics, e.ToolStrip.ClientRectangle, e.AffectedBounds);
        }

        public static bool IsSupported
        {
            get
            {
                if (!VisualStyleRenderer.IsSupported)
                {
                    return false;
                }

                return VisualStyleRenderer.IsElementDefined(
                    VisualStyleElement.CreateElement(MenuClassName, (int)MenuPart.BarBackground, MenuBarState.Active));
            }
        }

        private static MenuPart GetMenuPart(ToolStripItem item)
        {
            if (item.Owner.IsDropDown)
            {
                return MenuPart.PopupItem;
            }

            return MenuPart.BarItem;
        }

        private static int GetItemState(ToolStripItem item)
        {
            if (item.Owner.IsDropDown)
            {
                if (item.Enabled)
                {
                    if (item.Selected)
                    {
                        return MenuPopupItemStates.Hot;
                    }

                    return MenuPopupItemStates.Normal;
                }

                if (item.Selected)
                {
                    return MenuPopupItemStates.DisabledHot;
                }

                return MenuPopupItemStates.Disabled;
            }
            else
            {
                if (item.Pressed)
                {
                    if (item.Enabled)
                    {
                        return MenuBarItemStates.Pushed;
                    }

                    return MenuBarItemStates.DisabledPushed;
                }

                if (item.Enabled)
                {
                    if (item.Selected)
                    {
                        return MenuBarItemStates.Hot;
                    }

                    return MenuBarItemStates.Normal;
                }

                if (item.Selected)
                {
                    return MenuBarItemStates.DisabledHot;
                }

                return MenuBarItemStates.Disabled;
            }
        }
    }    
}
