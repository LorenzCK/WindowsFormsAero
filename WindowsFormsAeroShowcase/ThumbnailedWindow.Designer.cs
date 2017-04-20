namespace WindowsFormsAeroShowcase {
	partial class ThumbnailedWindow {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            this.SuspendLayout();
            // 
            // ThumbnailedWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(373, 238);
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.Name = "ThumbnailedWindow";
            this.ShowInTaskbar = false;
            this.Text = "Thumbnailed Window (resize me!)";
            this.ResumeLayout(false);

		}

		#endregion

	}
}