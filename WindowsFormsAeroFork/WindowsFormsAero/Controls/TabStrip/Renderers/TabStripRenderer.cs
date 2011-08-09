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

        public TabStripItemRenderEventArgs(Graphics graphics, TabStripButtonBase item, Rectangle closeRectangle, TabStripCloseButtonState closeState)
            : base (graphics, item)
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

    public sealed class TabStripInsertionMarkRenderEventArgs : EventArgs
    {
        private readonly Graphics _graphics;
        private readonly TabStrip _tabStrip;
        private readonly Int32 _location;

        public TabStripInsertionMarkRenderEventArgs(Graphics graphics, TabStrip tabStrip, Int32 location)
        {
            _graphics = graphics;
            _tabStrip = tabStrip;
            _location = location;
        }

        public Graphics Graphics
        {
            get { return _graphics; }
        }

        public TabStrip TabStrip
        {
            get { return _tabStrip; }
        }

        public Int32 Location
        {
            get { return _location; }
        }
    }

    [Serializable]
    public sealed class TabStripItemBusyImageRenderEventArgs : ToolStripItemImageRenderEventArgs
    {
        private Int32 _tickCount;

        public TabStripItemBusyImageRenderEventArgs(Graphics graphics, ToolStripItem item, Rectangle imageRectangle)
            : base(graphics, item, imageRectangle)
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

    public abstract class TabStripRenderer : ToolStripRenderer
    {
        private readonly static object EventRenderTabInsertionMark = new object();
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

        public event EventHandler<TabStripInsertionMarkRenderEventArgs> RenderTabInsertionMark
        {
            add { Events.AddHandler(EventRenderTabInsertionMark, value); }
            remove { Events.RemoveHandler(EventRenderTabInsertionMark, value); }
        }

        public event EventHandler<TabStripItemRenderEventArgs> RenderTabItemBackground
        {
            add { Events.AddHandler(EventRenderTabItemBackground, value); }
            remove { Events.RemoveHandler(EventRenderTabItemBackground, value); }
        }

        public event EventHandler<TabStripItemBusyImageRenderEventArgs> RenderTabBusyItemImage
        {
            add { Events.AddHandler(EventRenderTabItemBusyImage, value); }
            remove { Events.RemoveHandler(EventRenderTabItemBusyImage, value); } 
        }

        public event EventHandler<TabStripScrollButtonRenderEventArgs> RenderTabScrollChevron
        {
            add { Events.AddHandler(EventRenderTabScrollChevron, value); }
            remove { Events.RemoveHandler(EventRenderTabScrollChevron, value); }
        }

        public void DrawTabInsertionMark(TabStripInsertionMarkRenderEventArgs e)
        {
            OnRenderTabInsertionMark(e);

            var handler = Events[EventRenderTabInsertionMark] as EventHandler<TabStripInsertionMarkRenderEventArgs>;

            if (handler != null)
            {
                handler(this, e);
            }
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

            var handler = Events[EventRenderTabItemBusyImage] as EventHandler<TabStripItemBusyImageRenderEventArgs>;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        public void DrawTabScrollChevron(TabStripScrollButtonRenderEventArgs e)
        {
            OnRenderTabScrollChevron(e);

            var handler = Events[EventRenderTabScrollChevron] as EventHandler<TabStripScrollButtonRenderEventArgs>;

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

        protected virtual void OnRenderTabInsertionMark(TabStripInsertionMarkRenderEventArgs e)
        {
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