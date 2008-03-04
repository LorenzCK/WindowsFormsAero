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
using System.Windows.Forms;

namespace VistaControls {
	internal class SplitButtonInner : Button {

		int BS_SPLITBUTTON = (int)0x0000000CL;
		int BS_DEFSPLITBUTTON = (int)0x0000000DL;

		protected override CreateParams CreateParams {
			get {
				CreateParams p = base.CreateParams;
				p.Style |= (this.IsDefault) ? BS_DEFSPLITBUTTON : BS_SPLITBUTTON;
				return p;
			}
		}

	}
}
