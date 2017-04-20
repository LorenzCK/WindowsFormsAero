using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsAeroShowcase {

    public partial class ThumbnailedWindow : Form {

        private Timer timer = new Timer();

        public ThumbnailedWindow() {
            InitializeComponent();

            timer.Interval = 500;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();

            Font = new Font(Font.FontFamily, 20.0f, FontStyle.Bold);
        }

        private bool resizing = false;

        protected override void OnResizeBegin(EventArgs e) {
            timer.Stop();
            resizing = true;
            base.OnResizeBegin(e);
        }

        protected override void OnResizeEnd(EventArgs e) {
            timer.Start();
            resizing = false;
            this.Invalidate();
            base.OnResizeEnd(e);
        }

        private void timer_Tick(object sender, EventArgs e) {
            this.Invalidate();
        }

        private Color[] colors = new Color[] {
            Color.Red,
            Color.Blue,
            Color.Green,
            Color.DarkRed,
            Color.DarkBlue,
            Color.DarkGreen,
            Color.Snow,
            Color.CornflowerBlue,
            Color.Sienna,
            Color.Salmon
        };

        private Random rnd = new Random(DateTime.Now.GetHashCode());

        private Color GetColor() {
            return colors[rnd.Next() % colors.Length];
        }

        private Color lastBackground;
        protected override void OnPaint(PaintEventArgs e) {
            if (resizing) {
                e.Graphics.Clear(lastBackground);
                return;
            }

            lastBackground = GetColor();
            e.Graphics.Clear(lastBackground);

            for (int i = 0; i < 7; ++i) {
                DrawEllipse(e.Graphics);
            }

            e.Graphics.DrawString("WindowsFormsAero", Font, new SolidBrush(GetColor()), GetPos(), GetPos());

            base.OnPaint(e);
        }

        private void DrawEllipse(Graphics graphics) {
            float x = GetPos();
            float y = GetPos();
            float size = (float)(rnd.NextDouble() * ClientSize.Width / 2);

            graphics.FillEllipse(new SolidBrush(GetColor()), x, y, size, size);
        }

        private float GetPos() {
            return (float)((rnd.NextDouble() - 0.20) * Math.Max(ClientSize.Width, ClientSize.Height));
        }

    }

}
