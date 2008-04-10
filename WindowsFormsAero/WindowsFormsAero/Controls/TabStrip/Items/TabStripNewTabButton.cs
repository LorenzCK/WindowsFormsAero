//--
// Windows Forms Aero Controls
// http://www.CodePlex.com/VistaControls
//
// Copyright (c) 2008 Jachym Kouba
// Licensed under Microsoft Reciprocal License (Ms-RL) 
//--
using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsAero
{
    [System.ComponentModel.DesignerCategory("Code")]
    internal sealed class TabStripNewTabButton : TabStripButtonBase
    {
        public TabStripNewTabButton()
        {
            ToolTipText = "New Tab (Ctrl+T)";
            Image = Resources.Images.NewTab;
            ImageAlign = ContentAlignment.MiddleCenter;
            DisplayStyle = ToolStripItemDisplayStyle.Image;
        }

        internal override void OnPaintImage(ToolStripItemImageRenderEventArgs e)
        {
            if (Selected)
            {
                base.OnPaintImage(e);
            }
        }
    }
}
