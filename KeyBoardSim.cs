using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Butto
{
    class KeyBoardSim
    {
        /*    -___.             __                                           .__     .__                 
         *     \_ |__  _____   |  | _______           _____  _____     ______|  |__  |__|_______   ____  
         *      | __ \ \__  \  |  |/ /\__  \         /     \ \__  \   /  ___/|  |  \ |  |\_  __ \ /  _ \ 
         *      | \_\ \ / __ \_|    <  / __ \_      |  Y Y  \ / __ \_ \___ \ |   Y  \|  | |  | \/(  <_> )
         *      |___  /(____  /|__|_ \(____  /______|__|_|  /(____  //____  >|___|  /|__| |__|    \____/ 
         *          \/      \/      \/     \//_____/      \/      \/      \/      \/                     
         *
         *        __ __                   ____        __   _____    _             
         *       / //_/  ___    __  __   / __ )  ____/ /  / ___/   (_)   ____ ___ 
         *      / ,<    / _ \  / / / /  / __  | / __  /   \__ \   / /   / __ `__ \
         *     / /| |  /  __/ / /_/ /  / /_/ / / /_/ /   ___/ /  / /   / / / / / /
         *    /_/ |_|  \___/  \__, /  /_____/  \__,_/   /____/  /_/   /_/ /_/ /_/ 
         *                   /____/                                               
         * 
        -*/

        #region Struct

        [StructLayout(LayoutKind.Explicit)]
        private struct INPUT
        {
            [FieldOffset(0)]
            public int type;//0-MOUSEINPUT;1-KEYBDINPUT;2-HARDWAREINPUT     
            [FieldOffset(4)]
            public KEYBDINPUT ki;
            [FieldOffset(4)]
            public MOUSEINPUT mi;
            [FieldOffset(4)]
            public HARDWAREINPUT hi;
        }
        [StructLayout(LayoutKind.Sequential)]
        private struct MOUSEINPUT
        {
            public int dx;
            public int dy;
            public int mouseData;
            public int dwFlags;
            public int time;
            public IntPtr dwExtraInfo;
        }
        [StructLayout(LayoutKind.Sequential)]
        private struct KEYBDINPUT
        {
            public short wVk;
            public short wScan;
            public int dwFlags;
            public int time;
            public IntPtr dwExtraInfo;
        }
        [StructLayout(LayoutKind.Sequential)]
        private struct HARDWAREINPUT
        {
            public int uMsg;
            public short wParamL;
            public short wParamH;
        }

        #endregion
        #region DLL
        [DllImport("user32.dll")]
        private static extern uint SendInput(uint nInputs, ref INPUT pInputs, int cbSize);
        [DllImport("user32.dll", EntryPoint = "GetMessageExtraInfo", SetLastError = true)]
        private static extern IntPtr GetMessageExtraInfo();
        #endregion
        #region Enum
        private enum InputType
        {
            INPUT_MOUSE = 0,
            INPUT_KEYBOARD = 1,
            INPUT_HARDWARE = 2,
        }
        [Flags()]
        private enum MOUSEEVENTF
        {
            MOVE = 0x0001,  //mouse move     
            LEFTDOWN = 0x0002,  //left button down     
            LEFTUP = 0x0004,  //left button up     
            RIGHTDOWN = 0x0008,  //right button down     
            RIGHTUP = 0x0010,  //right button up     
            MIDDLEDOWN = 0x0020, //middle button down     
            MIDDLEUP = 0x0040,  //middle button up     
            XDOWN = 0x0080,  //x button down     
            XUP = 0x0100,  //x button down     
            WHEEL = 0x0800,  //wheel button rolled     
            VIRTUALDESK = 0x4000,  //map to entire virtual desktop     
            ABSOLUTE = 0x8000,  //absolute move     
        }
        [Flags()]
        private enum KEYEVENTF
        {
            EXTENDEDKEY = 0x0001,
            KEYUP = 0x0002,
            UNICODE = 0x0004,
            SCANCODE = 0x0008,
        }
        public enum KEYSCANCODE
        {
            ESC = 0x01,
            M1 = 0x02,
            M2 = 0x03,
            M3 = 0x04,
            M4 = 0x05,
            M5 = 0x06,
            M6 = 0x07,
            M7 = 0x08,
            M8 = 0x09,
            M9 = 0x0A,
            M0 = 0x0B,
            MSubtract = 0x0C,
            MAdd = 0x0D,
            MBackspace = 0x0E,
            MTab = 0x0F,
            MQ = 0x10,
            MW = 0x11,
            ME = 0x12,
            MR = 0x13,
            MT = 0x14,
            MY = 0x15,
            MU = 0x16,
            MI = 0x17,
            MO = 0x18,
            MP = 0x19,
            MLMbracket = 0x1A,
            MRMbracket = 0x1B,
            MEnter = 0x1C,
            MCtrl = 0x1D,
            MA = 0x1E,
            MS = 0x1F,
            MD = 0x20,
            MF = 0x21,
            MG = 0x22,
            MH = 0x23,
            MJ = 0x24,
            MK = 0x25,
            ML = 0x26,
            MSemicolon = 0x27,
            MApostrophe = 0x28,
            MLShift = 0x2A,
            MRSlash = 0x2B,
            MZ = 0x2C,
            MX = 0x2D,
            MC = 0x2E,
            MV = 0x2F,
            MB = 0x30,
            MN = 0x31,
            MM = 0x32,
            MComma = 0x33,
            MPoint = 0x34,
            MSlash = 0x35,
            MRShift = 0x36,
            PrtScSysRq = 0x37,
            MAlt = 0x38,
            MSpace = 0x39,
            MAPPS = 0x5D,
            UP = 0x48,
            DOWN = 0x50,
            LEFT = 0x4B,
            RIGHT = 0x4D,
        }
        /// <summary>
        /// 用于标识键盘按键弹起/按下
        /// </summary>
        public enum KeyBoardKeyState
        {
            /// <summary>
            /// 弹起
            /// </summary>
            UP = 0x0002,
            /// <summary>
            /// 按下
            /// </summary>
            DOWN = 0x0000,
        }
        #endregion

        //发送unicode字符，可发送任意字符     
        public static void SendUnicode(string message)
        {
            for (int i = 0; i < message.Length; i++)
            {
                INPUT input_down = new INPUT();
                input_down.type = (int)InputType.INPUT_KEYBOARD;
                input_down.ki.dwFlags = (int)KEYEVENTF.UNICODE;
                input_down.ki.wScan = (short)message[i];
                input_down.ki.wVk = 0;
                SendInput(1, ref input_down, Marshal.SizeOf(input_down));//keydown     
                INPUT input_up = new INPUT();
                input_up.type = (int)InputType.INPUT_KEYBOARD;
                input_up.ki.wScan = (short)message[i];
                input_up.ki.wVk = 0;
                input_up.ki.dwFlags = (int)(KEYEVENTF.KEYUP | KEYEVENTF.UNICODE);
                SendInput(1, ref input_up, Marshal.SizeOf(input_up));//keyup      
            }
        }
        //发送非unicode字符，只能发送小写字母和数字     
        public static void SendNoUnicode(string message)
        {
            string str = "abcdefghijklmnopqrstuvwxyz";
            for (int i = 0; i < message.Length; i++)
            {
                short sendChar = 0;
                if (str.IndexOf(message[i].ToString().ToLower()) > -1)
                {
                    sendChar = (short)GetKeysByChar(message[i]);
                }
                else
                {
                    sendChar = (short)message[i];
                }

                INPUT input_down = new INPUT();
                input_down.type = (int)InputType.INPUT_KEYBOARD;
                input_down.ki.dwFlags = 0;
                input_down.ki.wVk = sendChar;
                SendInput(1, ref input_down, Marshal.SizeOf(input_down));//keydown     
                INPUT input_up = new INPUT();
                input_up.type = (int)InputType.INPUT_KEYBOARD;
                input_up.ki.wVk = sendChar;
                input_up.ki.dwFlags = (int)KEYEVENTF.KEYUP;
                SendInput(1, ref input_up, Marshal.SizeOf(input_up));//keyup      
            }
        }
        public static void SendSingleKeyBoradKey(short key, short scan, bool KeyUp)
        {
            if (KeyUp)
            {
                INPUT input_up = new INPUT();
                input_up.type = (int)InputType.INPUT_KEYBOARD;
                input_up.ki.wVk = key;
                input_up.ki.dwFlags = (int)KEYEVENTF.KEYUP;
                input_up.ki.wScan = scan;

                SendInput(1, ref input_up, Marshal.SizeOf(input_up));//keyup  
            }
            else
            {
                INPUT input_down = new INPUT();
                input_down.type = (int)InputType.INPUT_KEYBOARD;
                input_down.ki.dwFlags = 0;
                input_down.ki.wVk = key;
                input_down.ki.wScan = scan;
                SendInput(1, ref input_down, Marshal.SizeOf(input_down));//keydown     
            }
        }
        private static Keys GetKeysByChar(char c)
        {
            string str = "abcdefghijklmnopqrstuvwxyz";
            int index = str.IndexOf(c.ToString().ToLower());
            switch (index)
            {
                case 0:
                    return Keys.A;
                case 1:
                    return Keys.B;
                case 2:
                    return Keys.C;
                case 3:
                    return Keys.D;
                case 4:
                    return Keys.E;
                case 5:
                    return Keys.F;
                case 6:
                    return Keys.G;
                case 7:
                    return Keys.H;
                case 8:
                    return Keys.I;
                case 9:
                    return Keys.J;
                case 10:
                    return Keys.K;
                case 11:
                    return Keys.L;
                case 12:
                    return Keys.M;
                case 13:
                    return Keys.N;
                case 14:
                    return Keys.O;
                case 15:
                    return Keys.P;
                case 16:
                    return Keys.Q;
                case 17:
                    return Keys.R;
                case 18:
                    return Keys.S;
                case 19:
                    return Keys.T;
                case 20:
                    return Keys.U;
                case 21:
                    return Keys.V;
                case 22:
                    return Keys.W;
                case 23:
                    return Keys.X;
                case 24:
                    return Keys.Y;
                default:
                    return Keys.Z;
            }
        }
        /// <summary>
        /// 发送按键，与按键状态
        /// </summary>
        /// <param name="key"></param>
        /// <param name="keyState"></param>
        public void SendKeyEvent(Keys key, KeyBoardKeyState keyState)
        {
            INPUT input_up = new INPUT();
            input_up.type = (int)InputType.INPUT_KEYBOARD;
            input_up.ki.wVk = (short)key;
            input_up.ki.dwFlags = (int)keyState;
            input_up.ki.wScan = (short)GetScanCode(key);

            SendInput(1, ref input_up, Marshal.SizeOf(input_up));//keyup  
        }
        /// <summary>
        /// 按照<paramref name="time"/>时间(ms)按下按键，然后弹起按键<paramref name="key"/>
        /// </summary>
        /// <param name="key"></param>
        /// <param name="time"></param>
        public void ClickKey(Keys key ,int time)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(ClickKey_Work), (key, time));
        }

        private void ClickKey_Work(object o)
        {
            var s = (ValueTuple<Keys, int>)o ;
            SendKeyEvent(s.Item1, KeyBoardKeyState.DOWN);
            Thread.Sleep(s.Item2);
            SendKeyEvent(s.Item1, KeyBoardKeyState.UP);
        }
        public void SendKeyEvent(Keys key, KeyBoardKeyState keyState, KEYSCANCODE scanCode)
        {
            INPUT input_up = new INPUT();
            input_up.type = (int)InputType.INPUT_KEYBOARD;
            input_up.ki.wVk = (short)key;
            input_up.ki.dwFlags = (int)keyState;
            input_up.ki.wScan = (short)scanCode;

            SendInput(1, ref input_up, Marshal.SizeOf(input_up));//keyup  
        }
        Dictionary<Keys, KEYSCANCODE> keyValuePairs = new Dictionary<Keys, KEYSCANCODE>
        {
            {Keys.Escape,KEYSCANCODE.ESC},
            {Keys.D1,KEYSCANCODE.M1},
            {Keys.D2,KEYSCANCODE.M2},
            {Keys.D3,KEYSCANCODE.M3},
            {Keys.D4,KEYSCANCODE.M4},
            {Keys.D5,KEYSCANCODE.M5},
            {Keys.D6,KEYSCANCODE.M6},
            {Keys.D7,KEYSCANCODE.M7},
            {Keys.D8,KEYSCANCODE.M8},
            {Keys.D9,KEYSCANCODE.M9},
            {Keys.D0,KEYSCANCODE.M0},
            {Keys.Subtract,KEYSCANCODE.MSubtract},
            {Keys.Add,KEYSCANCODE.MAdd},
            {Keys.Back,KEYSCANCODE.MBackspace},
            {Keys.Tab,KEYSCANCODE.MTab},
            {Keys.Q,KEYSCANCODE.MQ},
            {Keys.W,KEYSCANCODE.MW},
            {Keys.E,KEYSCANCODE.ME},
            {Keys.R,KEYSCANCODE.MR},
            {Keys.T,KEYSCANCODE.MT},
            {Keys.Y,KEYSCANCODE.MY},
            {Keys.U,KEYSCANCODE.MU},
            {Keys.I,KEYSCANCODE.MI},
            {Keys.O,KEYSCANCODE.MO},
            {Keys.P,KEYSCANCODE.MP},
            {Keys.OemOpenBrackets,KEYSCANCODE.MLMbracket},
            {Keys.OemCloseBrackets,KEYSCANCODE.MRMbracket},
            {Keys.Enter,KEYSCANCODE.MEnter},
            {Keys.Control,KEYSCANCODE.MCtrl},
            {Keys.A,KEYSCANCODE.MA},
            {Keys.S,KEYSCANCODE.MS},
            {Keys.D,KEYSCANCODE.MD},
            {Keys.F,KEYSCANCODE.MF},
            {Keys.G,KEYSCANCODE.MG},
            {Keys.H,KEYSCANCODE.MH},
            {Keys.J,KEYSCANCODE.MJ},
            {Keys.K,KEYSCANCODE.MK},
            {Keys.L,KEYSCANCODE.ML},
            {Keys.OemSemicolon,KEYSCANCODE.MSemicolon},
            {Keys.OemQuotes,KEYSCANCODE.MApostrophe},
            {Keys.LShiftKey,KEYSCANCODE.MLShift},
            {Keys.OemBackslash,KEYSCANCODE.MSlash},
            {Keys.Z,KEYSCANCODE.MZ},
            {Keys.X,KEYSCANCODE.MX},
            {Keys.C,KEYSCANCODE.MC},
            {Keys.V,KEYSCANCODE.MV},
            {Keys.B,KEYSCANCODE.MB},
            {Keys.N,KEYSCANCODE.MN},
            {Keys.M,KEYSCANCODE.MM},
            {Keys.Oemcomma,KEYSCANCODE.MComma},
            {Keys.OemPeriod,KEYSCANCODE.MPoint},
            {Keys.Separator,KEYSCANCODE.MRSlash},
            {Keys.RShiftKey,KEYSCANCODE.MRShift},
            {Keys.PrintScreen,KEYSCANCODE.PrtScSysRq},
            {Keys.Alt,KEYSCANCODE.MAlt},
            {Keys.Space,KEYSCANCODE.MSpace},
            {Keys.Apps,KEYSCANCODE.MAPPS},
            {Keys.Up,KEYSCANCODE.UP},
            {Keys.Down,KEYSCANCODE.DOWN},
            {Keys.Left,KEYSCANCODE.LEFT},
            {Keys.Right,KEYSCANCODE.RIGHT},
        };
        private KEYSCANCODE GetScanCode(Keys key)
        {
            KEYSCANCODE ks;
            keyValuePairs.TryGetValue(key, out ks);
            return ks;
        }
    }
}
