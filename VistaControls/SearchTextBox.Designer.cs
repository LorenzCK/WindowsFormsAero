namespace VistaControls
{
	partial class SearchTextBox
	{
		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				_inactiveFont.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.searchOverlayLabel = new System.Windows.Forms.Label();
			this.searchText = new System.Windows.Forms.TextBox();
			this.searchImage = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.searchImage)).BeginInit();
			this.SuspendLayout();
			// 
			// searchOverlayLabel
			// 
			this.searchOverlayLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)));
			this.searchOverlayLabel.AutoSize = true;
			this.searchOverlayLabel.Location = new System.Drawing.Point(2, 3);
			this.searchOverlayLabel.Margin = new System.Windows.Forms.Padding(0);
			this.searchOverlayLabel.Name = "searchOverlayLabel";
			this.searchOverlayLabel.Size = new System.Drawing.Size(0, 13);
			this.searchOverlayLabel.TabIndex = 0;
			// 
			// searchText
			// 
			this.searchText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.searchText.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.searchText.Location = new System.Drawing.Point(2, 3);
			this.searchText.Margin = new System.Windows.Forms.Padding(0);
			this.searchText.Name = "searchText";
			this.searchText.Size = new System.Drawing.Size(125, 13);
			this.searchText.TabIndex = 0;
			this.searchText.TabStop = false;
			this.searchText.TextChanged += new System.EventHandler(this.searchText_TextChanged);
			this.searchText.GotFocus += new System.EventHandler(searchText_GotFocus);
			this.searchText.LostFocus += new System.EventHandler(searchText_LostFocus);
			// 
			// searchImage
			// 
			this.searchImage.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.searchImage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.searchImage.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.searchImage.Image = global::VistaControls.Resources.Pictures.InactiveSearch;
			this.searchImage.Location = new System.Drawing.Point(127, 0);
			this.searchImage.Margin = new System.Windows.Forms.Padding(0);
			this.searchImage.Name = "searchImage";
			this.searchImage.Size = new System.Drawing.Size(23, 20);
			this.searchImage.TabIndex = 1;
			this.searchImage.TabStop = false;
			this.searchImage.Click += new System.EventHandler(searchImage_Click);
			this.searchImage.MouseMove += new System.Windows.Forms.MouseEventHandler(searchImage_MouseMove);
			// 
			// SearchTextBox
			// 
			this.BackColor = System.Drawing.SystemColors.Window;
			this.Controls.Add(this.searchOverlayLabel);
			this.Controls.Add(this.searchText);
			this.Controls.Add(this.searchImage);
			this.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.Name = "SearchTextBox";
			this.Size = new System.Drawing.Size(150, 20);
			((System.ComponentModel.ISupportInitialize)(this.searchImage)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label searchOverlayLabel;
		private System.Windows.Forms.TextBox searchText;
		private System.Windows.Forms.PictureBox searchImage;
	}
}
