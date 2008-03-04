/*****************************************************
 *            Vista Controls for .NET 2.0
 * 
 * http://www.codeplex.com/vistacontrols
 * 
 * @author: Lorenz Cuno Klopfenstein
 * Licensed under Microsoft Community License (Ms-CL)
 * 
 *****************************************************/
/*
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace VistaControls {

	public class SplitButton : ButtonBase {

		SplitButtonInner _innerButton = new SplitButtonInner();

		/// <summary>Creates a new instance of SplitButton.</summary>
		public SplitButton() {
			//Hook inner button to events
			_innerButton.Click += new EventHandler(_innerButton_Click);
			_innerButton.DoubleClick += new EventHandler(_innerButton_DoubleClick);
			_innerButton.MouseCaptureChanged += new EventHandler(_innerButton_MouseCaptureChanged);
			_innerButton.MouseClick += new MouseEventHandler(_innerButton_MouseClick);
			_innerButton.MouseDoubleClick += new MouseEventHandler(_innerButton_MouseDoubleClick);

			//_innerButton.Paint += new PaintEventHandler(_innerButton_Paint);

			_innerButton.ChangeUICues += new UICuesEventHandler(_innerButton_ChangeUICues);
			_innerButton.StyleChanged += new EventHandler(_innerButton_StyleChanged);
			_innerButton.SystemColorsChanged += new EventHandler(_innerButton_SystemColorsChanged);

			_innerButton.DragDrop += new DragEventHandler(_innerButton_DragDrop);
			_innerButton.DragEnter += new DragEventHandler(_innerButton_DragEnter);
			_innerButton.DragLeave += new EventHandler(_innerButton_DragLeave);
			_innerButton.DragOver += new DragEventHandler(_innerButton_DragOver);
			_innerButton.GiveFeedback += new GiveFeedbackEventHandler(_innerButton_GiveFeedback);
			_innerButton.QueryContinueDrag += new QueryContinueDragEventHandler(_innerButton_QueryContinueDrag);

			//Not needed
			//_innerButton.Enter += new EventHandler(_innerButton_Enter);
			//_innerButton.Leave += new EventHandler(_innerButton_Leave);
			_innerButton.Validated += new EventHandler(_innerButton_Validated);
			_innerButton.Validating += new System.ComponentModel.CancelEventHandler(_innerButton_Validating);

			_innerButton.KeyDown += new KeyEventHandler(_innerButton_KeyDown);
			_innerButton.KeyPress += new KeyPressEventHandler(_innerButton_KeyPress);
			_innerButton.KeyUp += new KeyEventHandler(_innerButton_KeyUp);
			_innerButton.PreviewKeyDown += new PreviewKeyDownEventHandler(_innerButton_PreviewKeyDown);

			_innerButton.MouseDown += new MouseEventHandler(_innerButton_MouseDown);
			_innerButton.MouseEnter += new EventHandler(_innerButton_MouseEnter);
			_innerButton.MouseHover += new EventHandler(_innerButton_MouseHover);
			_innerButton.MouseLeave += new EventHandler(_innerButton_MouseLeave);
			_innerButton.MouseMove += new MouseEventHandler(_innerButton_MouseMove);
			_innerButton.MouseUp += new MouseEventHandler(_innerButton_MouseUp);

			//Dock inner button
			_innerButton.Dock = DockStyle.Fill;
			this.Controls.Add(_innerButton);

			//Receive notifications
			this.SetStyle(ControlStyles.EnableNotifyMessage, true);
		}

		#region Event redirection

		void _innerButton_MouseUp(object sender, MouseEventArgs e) {
			OnMouseUp(e);
		}

		void _innerButton_MouseMove(object sender, MouseEventArgs e) {
			OnMouseMove(e);
		}

		void _innerButton_MouseLeave(object sender, EventArgs e) {
			OnMouseLeave(e);
		}

		void _innerButton_MouseHover(object sender, EventArgs e) {
			OnMouseHover(e);
		}

		void _innerButton_MouseEnter(object sender, EventArgs e) {
			OnMouseEnter(e);
		}

		void _innerButton_MouseDown(object sender, MouseEventArgs e) {
			OnMouseDown(e);
		}

		void _innerButton_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e) {
			OnPreviewKeyDown(e);
		}

		void _innerButton_KeyUp(object sender, KeyEventArgs e) {
			OnKeyUp(e);
		}

		void _innerButton_KeyPress(object sender, KeyPressEventArgs e) {
			OnKeyPress(e);
		}

		void _innerButton_KeyDown(object sender, KeyEventArgs e) {
			OnKeyDown(e);
		}

		void _innerButton_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
			OnValidating(e);
		}

		void _innerButton_Validated(object sender, EventArgs e) {
			OnValidated(e);
		}

		void _innerButton_QueryContinueDrag(object sender, QueryContinueDragEventArgs e) {
			OnQueryContinueDrag(e);
		}

		void _innerButton_GiveFeedback(object sender, GiveFeedbackEventArgs e) {
			OnGiveFeedback(e);
		}

		void _innerButton_DragOver(object sender, DragEventArgs e) {
			OnDragOver(e);
		}

		void _innerButton_DragLeave(object sender, EventArgs e) {
			OnDragLeave(e);
		}

		void _innerButton_DragEnter(object sender, DragEventArgs e) {
			OnDragEnter(e);
		}

		void _innerButton_DragDrop(object sender, DragEventArgs e) {
			OnDragDrop(e);
		}

		void _innerButton_SystemColorsChanged(object sender, EventArgs e) {
			OnSystemColorsChanged(e);
		}

		void _innerButton_StyleChanged(object sender, EventArgs e) {
			OnStyleChanged(e);
		}

		void _innerButton_ChangeUICues(object sender, UICuesEventArgs e) {
			OnChangeUICues(e);
		}

		void _innerButton_Paint(object sender, PaintEventArgs e) {
			OnPaint(e);
		}

		void _innerButton_MouseDoubleClick(object sender, MouseEventArgs e) {
			OnMouseDoubleClick(e);
		}

		void _innerButton_MouseClick(object sender, MouseEventArgs e) {
			OnMouseClick(e);
		}

		void _innerButton_MouseCaptureChanged(object sender, EventArgs e) {
			OnMouseCaptureChanged(e);
		}

		void _innerButton_DoubleClick(object sender, EventArgs e) {
			OnDoubleClick(e);
		}

		void _innerButton_Click(object sender, EventArgs e) {
			OnClick(e);
		}

		#endregion

		#region Button Properties

		public override bool AllowDrop {
			get {
				return base.AllowDrop;
			}
			set {
				_innerButton.AllowDrop = value;
				base.AllowDrop = value;
			}
		}

		public override System.Drawing.Color BackColor {
			get {
				return base.BackColor;
			}
			set {
				_innerButton.BackColor = value;
				base.BackColor = value;
			}
		}

		public override System.Drawing.Image BackgroundImage {
			get {
				return _innerButton.BackgroundImage;
			}
			set {
				_innerButton.BackgroundImage = value;
			}
		}

		public override ImageLayout BackgroundImageLayout {
			get {
				return base.BackgroundImageLayout;
			}
			set {
				_innerButton.BackgroundImageLayout = value;
				base.BackgroundImageLayout = value;
			}
		}

		public override Cursor Cursor {
			get {
				return base.Cursor;
			}
			set {
				_innerButton.Cursor = value;
				base.Cursor = value;
			}
		}

		public override bool Focused {
			get {
				return _innerButton.Focused;
			}
		}

		public override System.Drawing.Font Font {
			get {
				return base.Font;
			}
			set {
				_innerButton.Font = value;
				base.Font = value;
			}
		}

		public override System.Drawing.Color ForeColor {
			get {
				return base.ForeColor;
			}
			set {
				_innerButton.ForeColor = value;
				base.ForeColor = value;
			}
		}

		public override string Text {
			get {
				return _innerButton.Text;
			}
			set {
				_innerButton.Text = value;
			}
		}

		public override System.Drawing.ContentAlignment TextAlign {
			get {
				return _innerButton.TextAlign;
			}
			set {
				_innerButton.TextAlign = value;
			}
		}

		#endregion

		#region Split Context Menu

		[Description("Occurs when the split button is clicked."), Category("Action")]
		/// <summary>Occurs when the split label is clicked.</summary>
		public event EventHandler<SplitMenuEventArgs> SplitClick;

		[Description("Occurs when the split label is clicked, but before the associated context menu is displayed."), Category("Action")]
		/// <summary>Occurs when the split label is clicked, but before the associated
		/// context menu is displayed by the control.</summary>
		public event EventHandler<SplitMenuEventArgs> SplitMenuOpening;

		/// <summary>Provides data for the clicking of split buttons and the opening
		/// of context menus.</summary>
		public class SplitMenuEventArgs : EventArgs {
			public SplitMenuEventArgs(Rectangle drawArea) {
				DrawArea = drawArea;
			}

			public Rectangle DrawArea { get; set; }
		}

		protected virtual void OnSplitClick(SplitMenuEventArgs e){
			if (SplitMenu != null) {
				if(SplitMenuOpening != null)
					SplitMenuOpening(this, e);

				SplitMenu.Width = e.DrawArea.Width;
				SplitMenu.Show(this, new Point(e.DrawArea.Left, e.DrawArea.Bottom));
			}

			if(SplitClick != null)
				SplitClick(this, e);
		}

		[Description("Sets the context menu that is displayed by clicking on the split button."), Category("Behavior"), DefaultValue(null)]
		/// <summary>Get and set the associated context menu that is displayed when the split
		/// label of the button is clicked.</summary>
		public ContextMenuStrip SplitMenu { get; set; }

		#endregion

		#region Split button Properties

		/*bool _SplitButtonAlignLeft = false;

		[Description("Align the split button on the left side of the button."), Category("Appearance"), DefaultValue(false)]
		/// <summary>Gets or sets whether the split button should be aligned on the left side of the button.</summary>
		public bool SplitButtonAlignLeft {
			get {
				return _SplitButtonAlignLeft;
			}
			set {
				_SplitButtonAlignLeft = value;
				SendSplitStyleMsg();
			}
		}

		bool _SplitButtonNoSplit = false;

		[Description("Set No Split option."), Category("Appearance"), DefaultValue(false)]
		/// <summary>Gets or sets whether the split button should be shown or not.</summary>
		public bool SplitButtonNoSplit {
			get {
				return _SplitButtonNoSplit;
			}
			set {
				_SplitButtonNoSplit = value;
				SendSplitStyleMsg();
			}
		}

		bool _SplitButtonStretch = false;

		private void SendSplitStyleMsg() {
			NativeMethods.BUTTON_SPLITINFO info = new NativeMethods.BUTTON_SPLITINFO();
			info.Mask = NativeMethods.SplitInfoMask.Style;
			info.SplitStyle = (NativeMethods.SplitInfoStyle)(
				((uint)NativeMethods.SplitInfoStyle.AlignLeft & (_SplitButtonAlignLeft ? 1 : 0)) |
				((uint)NativeMethods.SplitInfoStyle.NoSplit & (_SplitButtonNoSplit ? 1 : 0)) |
				((uint)NativeMethods.SplitInfoStyle.Stretch & (_SplitButtonStretch ? 1 : 0))
			);

			NativeMethods.SendMessage(_innerButton.Handle, NativeMethods.BCM_SETSPLITINFO, 0, ref info);
		}

		#endregion

		protected override void OnNotifyMessage(Message m) {
			if (m.Msg == NativeMethods.WM_NOTIFY) {
				NativeMethods.NMHDR info = (NativeMethods.NMHDR)Marshal.PtrToStructure(m.LParam, typeof(NativeMethods.NMHDR));

				if (info.Code == NativeMethods.BCN_DROPDOWN) {
					NativeMethods.NMBCDROPDOWN dropinfo = (NativeMethods.NMBCDROPDOWN)Marshal.PtrToStructure(m.LParam, typeof(NativeMethods.NMBCDROPDOWN));

					OnSplitClick(new SplitMenuEventArgs(dropinfo.DropDownArea.ToRectangle()));
				}
			}

			base.OnNotifyMessage(m);
		}
	}
}
*/