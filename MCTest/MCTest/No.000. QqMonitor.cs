using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MCTest
{
    public class QqMonitor: IOutput
    {
        [DllImport("user32.dll ")]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll ")]
        static extern IntPtr GetDlgItem(IntPtr hDlg, int nIDDlgItem);
        [DllImport("user32.dll ", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr parentHandle, int childAfter, string className, int windowTitle);
        [DllImport("user32.dll ", EntryPoint = "SendMessage ")]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, int wParam, string lParam);
        [DllImport("kernel32.dll ", CharSet = CharSet.Auto)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        IntPtr hwndQQ;
        IntPtr hwnd1;
        IntPtr hwnd2;
        IntPtr hwnd3;
        IntPtr hwnd4;

        // 在默认情况下，CharSet为CharSet.Ansi
        [DllImport("user32.dll")]
        public static extern int MessageBox(IntPtr hWnd, String text, String caption, uint type);


        public void Main()
        {
            MessageBox(new IntPtr(0), "Learning Hard", "欢迎", 0);
            string machinename = System.Environment.MachineName;//获得计算机名 
            Process[] processlist = Process.GetProcesses(machinename);//得到所有进程 
            var qqlist = processlist.ToList().Where(r => r.ProcessName.Contains("QQ"));
            foreach (Process p in qqlist) //列举每个进程 
            {
                    hwndQQ = FindWindow("#32770 ", p.MainWindowTitle.ToString());
                    hwnd1 = GetDlgItem(hwndQQ, 0);
                    hwnd2 = GetDlgItem(hwnd1, 0);
                    hwnd3 = GetDlgItem(hwnd2, 894);
                    SendMessage(hwnd3, 194, 0, "Helloworld"); //向QQ输入框粘贴字符，this.textBox1.Text是要发送的文字信息 
                    hwnd4 = GetDlgItem(hwnd1, 1);
                    SendMessage(hwnd4, 245, 0, Convert.ToString(0));
            }

        }
    }
}
