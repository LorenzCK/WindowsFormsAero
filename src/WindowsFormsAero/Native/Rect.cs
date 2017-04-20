﻿/*****************************************************
 * WindowsFormsAero
 * https://github.com/LorenzCK/WindowsFormsAero
 * http://windowsformsaero.codeplex.com
 *
 * Author: Lorenz Cuno Klopfenstein <lck@klopfenstein.net>
 *****************************************************/

using System.Runtime.InteropServices;

namespace WindowsFormsAero.Native {

    [StructLayout(LayoutKind.Sequential)]
    internal struct Rect {

        public Rect(int left, int top, int right, int bottom) {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        public Rect(System.Drawing.Rectangle rect) {
            Left = rect.X;
            Top = rect.Y;
            Right = rect.Right;
            Bottom = rect.Bottom;
        }

        public int Left;
        public int Top;
        public int Right;
        public int Bottom;

        public int Width {
            get {
                return Right - Left;
            }
            set {
                Right = Left + value;
            }
        }

        public int Height {
            get {
                return Bottom - Top;
            }
            set {
                Bottom = Top + value;
            }
        }

        public System.Drawing.Rectangle ToRectangle() {
            return new System.Drawing.Rectangle(Left, Top, Right - Left, Bottom - Top);
        }

    }

}
