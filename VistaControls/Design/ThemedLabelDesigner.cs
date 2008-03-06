using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms.Design;
using System.Windows.Forms;
using VistaControls.ThemeText;

namespace VistaControls.Design {

	/// <summary>ControlDesigner for ThemedLabel control.</summary>
	/// <remarks>Disabled the Transparent property, enabling hit-testing.</remarks>
	class ThemedLabelDesigner : ControlDesigner {

		public override void Initialize(System.ComponentModel.IComponent component) {
			ThemedLabel label = (ThemedLabel)component;
			label.Transparent = false;

			base.Initialize(component);
		}

	}
}
