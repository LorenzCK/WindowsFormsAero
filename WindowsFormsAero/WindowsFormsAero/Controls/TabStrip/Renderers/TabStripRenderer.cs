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

namespace WindowsFormsAero
{
    [Serializable]
    public sealed class TabStripItemRenderEventArgs : ToolStripItemRenderEventArgs
    {
        private readonly Rectangle _closeRectangle;
        private readonly TabStripCloseButtonState _closeState;

        public TabStripItemRenderEventArgs(Graphics g, TabStripButtonBase item, Rectangle closeRectangle, TabStripCloseButtonState closeState)
            : base (g, item)
        {
            _closeRectangle = closeRectangle;
            _closeState = closeState;
        }

        public Rectangle CloseButtonRectangle
        {
            get { return _closeRectangle; }
        }

        public TabStripCloseButtonState CloseButtonState
        {
            get { return _closeState; }
        }

        public new TabStripButtonBase Item
        {
            get { return (TabStripButtonBase)(base.Item); }
        }
    }

    [Serializable]
    public sealed class TabStripScrollButtonRenderEventArgs : ToolStripItemRenderEventArgs
    {
        private readonly Rectangle _rectangle;

        public TabStripScrollButtonRenderEventArgs(Graphics g, ToolStripItem item, Rectangle rectangle)
            : base(g, item)
        {
            _rectangle = rectangle;
        }

        internal TabStripScrollButtonRenderEventArgs(ToolStripItemRenderEventArgs e)
            : this(e.Graphics, e.Item, new Rectangle(Point.Empty, e.Item.Size))
        {
        }

        public Rectangle ChevronRectangle
        {
            get { return _rectangle; }
        }

        public TabStripScrollDirection ScrollDirection
        {
            get
            {
                var button = (Item as TabStripScrollButton);

                if (button != null)
                {
                    return button.ScrollDirection;
                }

                return TabStripScrollDirection.None;
            }
        }
    }

    [Serializable]
    public sealed class TabStripItemBusyImageRenderEventArgs : ToolStripItemImageRenderEventArgs
    {
        private Int32 _tickCount;

        public TabStripItemBusyImageRenderEventArgs(Graphics g, ToolStripItem item, Rectangle imageRectangle)
            : base(g, item, imageRectangle)
        {
            Initialize();
        }

        internal TabStripItemBusyImageRenderEventArgs(ToolStripItemImageRenderEventArgs e)
            : base(e.Graphics, e.Item, e.ImageRectangle)
        {
            Initialize();
        }

        public Int32 TickCount
        {
            get { return _tickCount; }
        }

        private void Initialize()
        {
            var tabStrip = (Item.Owner as TabStrip);

            if (tabStrip != null)
            {
                _tickCount = tabStrip.BusyTabTicker;
            }
        }
    }

    public delegate void TabStripItemRenderEventHandler(object sender, TabStripItemRenderEventArgs e);
    public delegate void TabStripScrollButtonRenderEventHandler(object sender, TabStripScrollButtonRenderEventArgs e);
    public delegate void TabStripItemBusyImageRenderEventHandler(object sender, TabStripItemBusyImageRenderEventArgs e);

    public abstract class TabStripRenderer : ToolStripRenderer
    {
        private readonly static object EventRenderTabItemBackground = new object();
        private readonly static object EventRenderTabItemBusyImage = new object();
        private readonly static object EventRenderTabScrollChevron = new object();

        private readonly EventHandlerList  Events = new EventHandlerList();
        private readonly ToolStripRenderer ActualRenderer;

        protected TabStripRenderer(ToolStripRenderer actualRenderer)
        {
            if (actualRenderer == null)
            {
                throw new ArgumentNullException("actualRenderer");
            }

            ActualRenderer = actualRenderer;
        }

        public TimeSpan BusyTabRefreshInterval
        {
            get;
            set;
        }

        public virtual Color SelectedTabBottomColor
        {
            get { return SystemColors.Control; }
        }

        public event TabStripItemRenderEventHandler RenderTabItemBackground
        {
            add { Events.AddHandler(EventRenderTabItemBackground, value); }
            remove { Events.RemoveHandler(EventRenderTabItemBackground, value); }
        }

        public event TabStripItemBusyImageRenderEventHandler RenderTabBusyItemImage
        {
            add { Events.AddHandler(EventRenderTabItemBusyImage, value); }
            remove { Events.RemoveHandler(EventRenderTabItemBusyImage, value); } 
        }

        public event TabStripScrollButtonRenderEventHandler RenderTabScrollChevron
        {
            add { Events.AddHandler(EventRenderTabScrollChevron, value); }
            remove { Events.RemoveHandler(EventRenderTabScrollChevron, value); }
        }

        public void DrawTabItemBackground(TabStripItemRenderEventArgs e)
        {
            OnRenderTabItemBackground(e);

            var handler = Events[EventRenderTabItemBackground] as ToolStripItemRenderEventHandler;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        public void DrawTabItemBusyImage(TabStripItemBusyImageRenderEventArgs e)
        {
            OnRenderTabItemBusyImage(e);

            var handler = Events[EventRenderTabItemBusyImage] as TabStripItemBusyImageRenderEventHandler;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        public void DrawTabScrollChevron(TabStripScrollButtonRenderEventArgs e)
        {
            OnRenderTabScrollChevron(e);

            var handler = Events[EventRenderTabScrollChevron] as TabStripScrollButtonRenderEventHandler;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        public virtual Size GetBusyImageSize(ToolStripItem item)
        {
            return Size.Empty;
        }

        public virtual Size GetCloseButtonSize(ToolStripItem item)
        {
            return Size.Empty;
        }

        protected virtual void OnRenderTabItemBackground(TabStripItemRenderEventArgs e)
        {
        }

        protected virtual void OnRenderTabItemBusyImage(TabStripItemBusyImageRenderEventArgs e)
        {
        }

        protected virtual void OnRenderTabScrollChevron(TabStripScrollButtonRenderEventArgs e)
        {
        }

        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
        {
            ActualRenderer.DrawArrow(e);
        }

        protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
        {
            ActualRenderer.DrawButtonBackground(e);
        }

        protected override void OnRenderDropDownButtonBackground(ToolStripItemRenderEventArgs e)
        {
            ActualRenderer.DrawDropDownButtonBackground(e);
        }

        protected override void OnRenderGrip(ToolStripGripRenderEventArgs e)
        {
            ActualRenderer.DrawGrip(e);
        }

        protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
        {
            ActualRenderer.DrawImageMargin(e);
        }

        protected override void OnRenderItemBackground(ToolStripItemRenderEventArgs e)
        {
            ActualRenderer.DrawItemBackground(e);
        }

        protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e)
        {
            ActualRenderer.DrawItemCheck(e);
        }

        protected override void OnRenderItemImage(ToolStripItemImageRenderEventArgs e)
        {
            ActualRenderer.DrawItemImage(e);
        }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            ActualRenderer.DrawItemText(e);
        }

        protected override void OnRenderLabelBackground(ToolStripItemRenderEventArgs e)
        {
            ActualRenderer.DrawLabelBackground(e);
        }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            ActualRenderer.DrawMenuItemBackground(e);
        }

        protected override void OnRenderOverflowButtonBackground(ToolStripItemRenderEventArgs e)
        {
            ActualRenderer.DrawOverflowButtonBackground(e);
        }

        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            ActualRenderer.DrawSeparator(e);
        }

        protected override void OnRenderSplitButtonBackground(ToolStripItemRenderEventArgs e)
        {
            ActualRenderer.DrawSplitButton(e);
        }

        protected override void OnRenderStatusStripSizingGrip(ToolStripRenderEventArgs e)
        {
            ActualRenderer.DrawStatusStripSizingGrip(e);
        }

        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            ActualRenderer.DrawToolStripBackground(e);
        }

        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            ActualRenderer.DrawToolStripBorder(e);
        }

        protected override void OnRenderToolStripContentPanelBackground(ToolStripContentPanelRenderEventArgs e)
        {
            ActualRenderer.DrawToolStripContentPanelBackground(e);
        }

        protected override void OnRenderToolStripPanelBackground(ToolStripPanelRenderEventArgs e)
        {
            ActualRenderer.DrawToolStripPanelBackground(e);
        }

        protected override void OnRenderToolStripStatusLabelBackground(ToolStripItemRenderEventArgs e)
        {
            ActualRenderer.DrawToolStripStatusLabelBackground(e);
        }
    }
}