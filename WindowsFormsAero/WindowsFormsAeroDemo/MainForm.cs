//--
// Windows Forms Aero Controls
// http://www.CodePlex.com/VistaControls
//
// Copyright (c) 2008 Jachym Kouba
// Licensed under Microsoft Reciprocal License (Ms-RL) 
//--
using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WindowsFormsAero.Demo
{
    public partial class MainForm : Form
    {
        [StructLayout(LayoutKind.Sequential)]
        private sealed class MARGINS
        {
            private int _left;
            private int _right;
            private int _top;
            private int _bottom;

            public MARGINS()
            {
            }

            public MARGINS(int all)
                : this(all, all, all, all)
            {
            }

            public MARGINS(int left, int top, int right, int bottom)
            {
                _left = left;
                _right = right;
                _top = top;
                _bottom = bottom;
            }

            public int Left
            {
                get { return _left; }
                set { _left = value; }
            }

            public int Right
            {
                get { return _right; }
                set { _right = value; }
            }

            public int Top
            {
                get { return _top; }
                set { _top = value; }
            }

            public int Bottom
            {
                get { return _bottom; }
                set { _bottom = value; }
            }

            public Padding ToPadding()
            {
                return new Padding(_left, _top, _right, _bottom);
            }
        }

        [DllImport("dwmapi", PreserveSig = false)]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        private static extern void DwmExtendFrameIntoClientArea(
            [In] HandleRef hWnd,
            [In] MARGINS pMargins);

        public MainForm()
        {
            InitializeComponent();
        }

        private bool TabStripBackgroud
        {
            get { return Program.Renderer.RenderBackground; }
            set
            {
                if (value != TabStripBackgroud)
                {
                    Program.Renderer.RenderBackground = value;

                    OnHandleCreated(EventArgs.Empty);
                    tabStrip1.Invalidate();
                }
            }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            try
            {
                if (TabStripBackgroud)
                {
                    DwmExtendFrameIntoClientArea(new HandleRef(this, Handle), new MARGINS(0));
                }
                else
                {
                    DwmExtendFrameIntoClientArea(new HandleRef(this, Handle), new MARGINS(0, tabStrip1.Bottom - tabStrip1.Padding.Bottom + 1, 0, 0));
                }
            }
            catch (COMException) { }
        }

        private void cbkTabStripBackground_CheckedChanged(object sender, EventArgs e)
        {
            TabStripBackgroud = cbkTabStripBackground.Checked;
        }

        private void chkTabIsBusy_CheckedChanged(object sender, EventArgs e)
        {
            tabStrip1.SelectedTab.IsBusy = !tabStrip1.SelectedTab.IsBusy;
        }

        private void chkDebugRenderer_CheckedChanged(object sender, EventArgs e)
        {
            if (ToolStripManager.Renderer == Program.Renderer)
            {
                ToolStripManager.Renderer = new TabStripDebugRenderer();
            }
            else
            {
                ToolStripManager.Renderer = Program.Renderer;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (RightToLeft == RightToLeft.Yes)
            {
                RightToLeft = RightToLeft.No;
            }
            else
            {
                RightToLeft = RightToLeft.Yes;
            }
        }
    }
}