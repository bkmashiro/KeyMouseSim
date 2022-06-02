using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace Butto
{
    class MouseSim
    {
        [DllImport("user32.dll")]
        public static extern uint SendInput(uint nInputs, Input[] pInputs, int cbSize);
        [StructLayout(LayoutKind.Explicit)] public struct Input {[FieldOffset(0)] public int type;[FieldOffset(4)] public MouseInput mi;[FieldOffset(4)] public tagKEYBDINPUT ki;[FieldOffset(4)] public tagHARDWAREINPUT hi; }
        [StructLayout(LayoutKind.Sequential)] public struct MouseInput { public int dx; public int dy; public int Mousedata; public int dwFlag; public int time; public IntPtr dwExtraInfo; }
        [StructLayout(LayoutKind.Sequential)] public struct tagKEYBDINPUT { short wVk; short wScan; int dwFlags; int time; IntPtr dwExtraInfo; }
        [StructLayout(LayoutKind.Sequential)] public struct tagHARDWAREINPUT { int uMsg; short wParamL; short wParamH; }

        public enum MouseEvent
        {
            Absolute = 0x8000,
            Hwheel = 0x01000,
            Move = 0x0001,
            Move_noCoalesce = 0x2000,
            LeftDown = 0x0002,
            LeftUp = 0x0004,
            MiddleDown = 0x0020,
            MiddleUp = 0x0040,
            RightDown = 0x0008,
            RightUp = 0x0010,
            Wheel = 0x0800,
            XUp = 0x0100,
            XDown = 0x0080,
        }

        private (int, int) GetAbslutePos(int x, int y) => (x * (65335 / System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width), y * (65335 / System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height));

        public void Mouse(MouseEvent @event)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(o=> { 
            
            MouseInput myMinput = new MouseInput();
            myMinput.dx = 0;
            myMinput.dy = 0;
            myMinput.Mousedata = 0;
            myMinput.dwFlag = (int)MouseEvent.Absolute | (int)@event;

            myMinput.time = 0;
            Input[] myInput = new Input[1];
            myInput[0] = new Input();
            myInput[0].type = 0;
            myInput[0].mi = myMinput;

            SendInput((uint)myInput.Length, myInput, Marshal.SizeOf(myInput[0].GetType()));
            }));
        }

        public void MouseLeftClick()
        {
            Mouse(MouseEvent.LeftDown);
            Mouse(MouseEvent.LeftUp);
        }
        public void MouseLeftClick(int time)
        {
            Mouse(MouseEvent.LeftDown);
            Thread.Sleep(time);
            Mouse(MouseEvent.LeftUp);
        }
        public void MouseRightClick()
        {
            Mouse(MouseEvent.RightDown);
            Mouse(MouseEvent.RightUp);
        }
        public void MouseRightClick(int time)
        {
            Mouse(MouseEvent.RightDown);
            Thread.Sleep(time);
            Mouse(MouseEvent.RightUp);
        }
        int wheelDelta = System.Windows.Forms.SystemInformation.MouseWheelScrollDelta;
        public void MouseWheel(int val,int wait=20)
        {
            MouseInput myMinput = new MouseInput();
            myMinput.dx = 0;
            myMinput.dy = 0;
            myMinput.Mousedata = val * wheelDelta;
            myMinput.dwFlag = (int)MouseEvent.Absolute | (int)MouseEvent.Wheel;
            myMinput.time = 0;
            Input[] myInput = new Input[1];
            myInput[0] = new Input();
            myInput[0].type = 0;
            myInput[0].mi = myMinput;
            if (val>0)
            {
                for (int i = 0; i < val; i++)
                {
                    SendInput((uint)myInput.Length, myInput, Marshal.SizeOf(myInput[0].GetType()));
                    Thread.Sleep(wait);
                }
            }
            else if (val<0)
            {
                for (int i = val; i < 0; i++)
                {
                    SendInput((uint)myInput.Length, myInput, Marshal.SizeOf(myInput[0].GetType()));
                    Thread.Sleep(wait);
                }
            }
        }

        public void MouseMoveTo(int x ,int y)
        {
            MouseInput myMinput = new MouseInput();
            var absloutePos = GetAbslutePos(x, y);
            myMinput.dx = absloutePos.Item1;
            myMinput.dy = absloutePos.Item2;
            myMinput.Mousedata = 0;
            myMinput.dwFlag = (int)MouseEvent.Absolute | (int)MouseEvent.Move;

            myMinput.time = 0;
            Input[] myInput = new Input[1];
            myInput[0] = new Input();
            myInput[0].type = 0;
            myInput[0].mi = myMinput;

            SendInput((uint)myInput.Length, myInput, Marshal.SizeOf(myInput[0].GetType()));

        }

    }
}
