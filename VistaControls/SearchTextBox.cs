using System;
using System.ComponentModel;
using System.Drawing;
using System.Security.Permissions;
using System.Windows.Forms;

namespace VistaControls
{
	[Designer(typeof(VistaControls.Design.SearchTextBoxDesigner))]
	[DefaultEvent("TextChanged")]
	[DefaultProperty("Text")]
	public partial class SearchTextBox : Control
	{
		private const string DefaultInactiveText = "Search";
		private const int DefaultTimerInterval = 400;

		private bool _active;

		private Color _hoverButtonColor;
		private Color _activeBackColor;
		private Color _activeForeColor;
		private Color _inactiveBackColor;
		private Color _inactiveForeColor;

		private Font _inactiveFont;

		private string _inactiveText;

		private Timer _timer;

		#region Construction

		protected override CreateParams CreateParams {
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get {
				CreateParams createParams = base.CreateParams;
				createParams.ExStyle |= NativeMethods.WS_EX_CONTROLPARENT;
				createParams.ExStyle &= ~NativeMethods.WS_EX_CLIENTEDGE;

				// make sure WS_BORDER is present in the style
				createParams.Style |= NativeMethods.WS_BORDER;

				return createParams;
			}
		}


		public SearchTextBox() {
			_hoverButtonColor = SystemColors.GradientInactiveCaption;
			_activeBackColor = SystemColors.Window;
			_activeForeColor = SystemColors.WindowText;
			_inactiveBackColor = SystemColors.InactiveBorder;
			_inactiveForeColor = SystemColors.GrayText;

			_inactiveFont = new Font(this.Font, FontStyle.Italic /*| FontStyle.Bold*/);

			_inactiveText = DefaultInactiveText;

			InitializeComponent();

			BackColor = InactiveBackColor;
			ForeColor = InactiveForeColor;

			searchOverlayLabel.Font = InactiveFont;
			searchOverlayLabel.ForeColor = InactiveForeColor;
			searchOverlayLabel.BackColor = InactiveBackColor;
			searchOverlayLabel.Text = InactiveText;

			searchText.Font = Font;
			searchText.ForeColor = ActiveForeColor;
			searchText.BackColor = InactiveBackColor;

			_active = false;

			SetTextActive(false);
			SetActive(false);

			_timer = new Timer();
			_timer.Interval = DefaultTimerInterval;
			_timer.Tick += new EventHandler(SearchTimer_Tick);
		}

		#endregion

		#region Events

		public new event EventHandler TextChanged
		{
			add { searchText.TextChanged += value; }
			remove { searchText.TextChanged -= value; }
		}

		[Description("Raised after an interval after the last user input."), Category("Action")]
		public event EventHandler SearchStarted;

		protected virtual void OnSearchStarted(EventArgs e) {
			if (SearchStarted != null) {
				SearchStarted(this, e);
			}
		}

		[Description("Raised when the user clicks on the X to cancel the search."), Category("Action")]
		public event EventHandler SearchCancelled;

		protected virtual void OnSearchCancelled(EventArgs e) {
			if (SearchCancelled != null) {
				SearchCancelled(this, e);
			}
		}

		#endregion

		#region Properties

		[Category("Appearance")]
		[DefaultValue(typeof(Color), "GradientInactiveCaption")]
		public Color HoverButtonColor
		{
			get { return _hoverButtonColor; }
			set {
				_hoverButtonColor = value;
				RefreshColors();
			}
		}

		[Category("Appearance")]
		[DefaultValue(typeof(Color), "WindowText")]
		public Color ActiveForeColor
		{
			get { return _activeForeColor; }
			set {
				_activeForeColor = value;
				RefreshColors();
			}
		}

		[Category("Appearance")]
		[DefaultValue(typeof(Color), "Window")]
		public Color ActiveBackColor
		{
			get { return _activeBackColor; }
			set {
				_activeBackColor = value;
				RefreshColors();
			}
		}

		[Category("Appearance")]
		[DefaultValue(typeof(Color), "GrayText")]
		public Color InactiveForeColor
		{
			get { return _inactiveForeColor; }
			set {
				_inactiveForeColor = value;
				RefreshColors();
			}
		}

		[Category("Appearance")]
		[DefaultValue(typeof(Color), "InactiveBorder")]
		public Color InactiveBackColor
		{
			get { return _inactiveBackColor; }
			set {
				_inactiveBackColor = value;
				RefreshColors();
			}
		}

		[Category("Appearance")]
		[DefaultValue(typeof(Cursor), "IBeam")]
		public override Cursor Cursor
		{
			get { return base.Cursor; }
			set { base.Cursor = value; }
		}

		[Browsable(false)]
		public override Color ForeColor
		{
			get { return base.ForeColor; }
			set {
				base.ForeColor = value;
				searchText.ForeColor = value;
			}
		}

		[Browsable(false)]
		public override Color BackColor
		{
			get { return base.BackColor; }
			set {
				base.BackColor = value;
				searchText.BackColor = value;
			}
		}

		[Category("Appearance")]
		[DefaultValue(DefaultInactiveText)]
		public string InactiveText
		{
			get { return _inactiveText; }
			set { _inactiveText = value; }
		}

		[Category("Appearance")]
		[DefaultValue(typeof(Font), "Microsoft Sans Serif, 8.25pt")]
		public Font ActiveFont
		{
			get { return base.Font; }
			set { base.Font = value; }
		}

		[Category("Appearance")]
		[DefaultValue(typeof(Font), "Microsoft Sans Serif, 8.25pt, style=Bold, Italic")]
		public Font InactiveFont
		{
			get {
				if (_inactiveFont == null)
					return Parent.Font;
				else
					return _inactiveFont;
			}
			set { _inactiveFont = value; }
		}

		[Browsable(false)]
		public override Font Font
		{
			get { return base.Font; }
			set { base.Font = value; }
		}

		[Category("Appearance")]
		public override string Text
		{
			get { return searchText.Text; }
			set { searchText.Text = value; }
		}

		protected bool TextEntered
		{
			get { return !String.IsNullOrEmpty(searchText.Text); }
		}

		[Description("Determines the delay between when the text is edited and the search event is raised."), Category("Behavior"), DefaultValue(DefaultTimerInterval)]
		public int SearchTimer {
			get { return _timer.Interval; }
			set { _timer.Interval = value; }
		}

		#endregion

		#region Methods

		private void SetActive(bool value)
		{
			if (TextEntered)
				value = true;

			if (_active == value)
				return;

			_active = value;
		}

		private void SetTextActive(bool value)
		{
			bool active = value || TextEntered;

			this.searchOverlayLabel.Visible = !active;
			this.searchText.Visible = active;

			if (value && !searchText.Focused)
				this.searchText.Select();
		}

		private void RefreshColors() {
			this.SuspendLayout();

			//Set correct color
			this.BackColor = _active ? ActiveBackColor : InactiveBackColor;
			this.ForeColor = _active ? ActiveForeColor : InactiveForeColor;

			//Set color of children controls
			this.searchText.ForeColor = this.ForeColor;
			this.searchOverlayLabel.BackColor = this.BackColor;
			this.searchText.BackColor = this.BackColor;

			this.ResumeLayout(true);
		}

		public void SetFocusWithoutSelection() {
			searchText.Select(searchText.Text.Length, 0);
			searchText.Focus();
		}

		#endregion

		#region Event Methods

		protected override void OnGotFocus(EventArgs e)
		{
			SetTextActive(true);
			SetActive(true);

			base.OnGotFocus(e);
		}

		protected override void OnLostFocus(EventArgs e)
		{
			if (this.searchText.Focused)
				return;

			SetTextActive(false);
			SetActive(false);

			base.OnLostFocus(e);
		}

		protected override void OnClick(EventArgs e)
		{
			this.Select();

			base.OnClick(e);
		}

		protected override void OnTextChanged(EventArgs e)
		{
			searchImage.Image = TextEntered ? Resources.Pictures.ActiveSearch : Resources.Pictures.InactiveSearch;

			//Start search timer
			_timer.Stop();
			if(TextEntered)
				_timer.Start();

			base.OnTextChanged(e);
		}

		private void searchImage_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (e.X < 0 || e.X > searchImage.Width || e.Y < 0 || e.Y > searchImage.Height)
			{
				NativeMethods.StopMouseCapture();
				searchImage.BackColor = Color.Empty;
			}
			else
			{
				NativeMethods.StartMouseCapture(searchImage.Handle);

				if (TextEntered)
					searchImage.BackColor = HoverButtonColor;
			}
		}

		private void searchImage_Click(object sender, System.EventArgs e)
		{
			if (TextEntered)
			{
				this.searchText.ResetText();
				OnLostFocus(EventArgs.Empty);

				OnSearchCancelled(EventArgs.Empty);
			}
		}

		private void searchText_TextChanged(object sender, EventArgs e)
		{
			OnTextChanged(e);
		}

		private void searchText_LostFocus(object sender, System.EventArgs e)
		{
			OnLostFocus(e);
		}

		private void searchText_GotFocus(object sender, System.EventArgs e)
		{
			OnGotFocus(e);
		}

		void SearchTimer_Tick(object sender, EventArgs e) {
			_timer.Stop();

			OnSearchStarted(EventArgs.Empty);
		}

		#endregion
	}
}
