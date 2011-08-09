using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace VistaControlsApp {
	public partial class ThumbnailedWindow : Form {
		Timer timer = new Timer();

		public ThumbnailedWindow() {
			InitializeComponent();

			timer.Interval = 500;
			timer.Tick += new EventHandler(timer_Tick);
			timer.Start();

			Font = new Font(Font.FontFamily, 20.0f, FontStyle.Bold);
		}

		bool resizing = false;
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

		void timer_Tick(object sender, EventArgs e) {
			this.Invalidate();
		}

		protected override void OnClosing(CancelEventArgs e) {
			//e.Cancel = true;

			base.OnClosing(e);
		}

		Color[] colors = new Color[] {
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

		Random rnd = new Random(DateTime.Now.GetHashCode());

		Color GetColor() {
			return colors[rnd.Next() % colors.Length];
		}

		Color lastBackground;
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

			e.Graphics.DrawString(".NET", Font, new SolidBrush(GetColor()), GetPos(), GetPos());

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
