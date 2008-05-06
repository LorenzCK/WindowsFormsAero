//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.ComponentModel;
//using System.Windows.Forms;
//using WindowsFormsAero.InteropServices;

//namespace WindowsFormsAero
//{
//    [System.ComponentModel.DesignerCategory("Code")]
//    public class AeroToolTip : Component
//    {
//        private ToolTipWindow _window;

//        private void CreateHandle()
//        {
//            var cp = new CreateParams()
//            {
//                ClassName = "tooltips_class32",
//                ExStyle = WindowStyles.WS_EX_TOPMOST,
//                Style = WindowStyles.WS_POPUP |
//                        WindowStyles.TTS_NOPREFIX |
//                        WindowStyles.TTS_ALWAYSTIP 
//            };

//            _window = new ToolTipWindow();
//            _window.CreateHandle(cp);
//        }

//        private sealed class ToolTipWindow : NativeWindow
//        {
//            private void AddTool(TOOLINFO info)
//            {

//            }
//        }
//    }
//}
