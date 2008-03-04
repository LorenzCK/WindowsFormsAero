/*****************************************************
 *            Vista Controls for .NET 2.0
 * 
 * http://www.codeplex.com/vistacontrols
 * 
 * @author: Lorenz Cuno Klopfenstein
 * Licensed under Microsoft Community License (Ms-CL)
 * 
 *****************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace VistaControls {

	public class SplitButton : Button {

		/// <summary>Creates a new instance of SplitButton.</summary>
		public SplitButton() {
			this.SetStyle(ControlStyles.EnableNotifyMessage, true);
		}

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

			/// <summary>Represents the bounding box of the clicked button.</summary>
			/// <remarks>A menu should be opened, with top-left coordinates in the left-bottom point of
			/// the rectangle and with width equal (or greater) than the width of the rectangle.</remarks>
			public Rectangle DrawArea { get; set; }
		}

		protected virtual void OnSplitClick(SplitMenuEventArgs e) {
			if (SplitMenu != null) {
				if (SplitMenuOpening != null)
					SplitMenuOpening(this, e);

				SplitMenu.Width = e.DrawArea.Width;
				SplitMenu.Show(this, new Point(e.DrawArea.Left, e.DrawArea.Bottom));
			}

			if (SplitClick != null)
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
		}*/

		#endregion

		protected override void OnNotifyMessage(Message m) {
			/*if (m.Msg == NativeMethods.WM_NOTIFY) {
				NativeMethods.NMHDR info = (NativeMethods.NMHDR)Marshal.PtrToStructure(m.LParam, typeof(NativeMethods.NMHDR));

				if (info.Code == NativeMethods.BCN_DROPDOWN) {
					NativeMethods.NMBCDROPDOWN dropinfo = (NativeMethods.NMBCDROPDOWN)Marshal.PtrToStructure(m.LParam, typeof(NativeMethods.NMBCDROPDOWN));

					OnSplitClick(new SplitMenuEventArgs(dropinfo.DropDownArea.ToRectangle()));
				}
			}*/

			base.OnNotifyMessage(m);
		}
	}
}
