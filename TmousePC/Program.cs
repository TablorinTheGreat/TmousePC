using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace TmousePC
{
    class Program
    {
       
        [DllImport("user32.dll")]
        static extern short VkKeyScan(char ch);
        [DllImport("user32.dll")]
    public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);
        const int VK_DOWN = 0x28;  //down key
        const int VK_LEFT = 0x25;
        const int VK_RIGHT = 0x46;
        const int VK_SPACE = 0x20;  //down key
        const int VK_RETURN = 0x0D;
        const int VK_DELETE = 0x08;
        const uint KEYEVENTF_KEYUP = 0x0002;
        const uint KEYEVENTF_EXTENDEDKEY = 0x0001;
        [DllImport("user32.dll")]
        static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);
        private const int MOUSEEVENTF_MOVE = 0x0001;
        private const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const int MOUSEEVENTF_LEFTUP = 0x0004;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        private const int MOUSEEVENTF_RIGHTUP = 0x0010;
        private const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        private const int MOUSEEVENTF_MIDDLEUP = 0x0040;
        private const int MOUSEEVENTF_ABSOLUTE = 0x8000;
        private const int MOUSEEVENTF_WHEEL = 0x0800;
        public static double x, y;
        static void Main(string[] args)
        { 
        Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            var bytes = new byte[1024];
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            for(int r=0;r< ipHostInfo.AddressList.Length;r++)
            {
                Console.WriteLine(r + ": " + ipHostInfo.AddressList[r]);
            }
            IPAddress ipAddress = ipHostInfo.AddressList[Convert.ToInt32(Console.ReadLine())];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);
            s.Bind(localEndPoint);
            s.Listen(1);
            Socket p = s.Accept();
            while (true)
            {
            var data = p.Receive(bytes);
                var g = Encoding.UTF8.GetString(bytes, 0, data);
                var e = g.Split(',');
                for (int i = 0; i< e.Length - 1; i++)
                {
                    if (e[i] == "(wheelup)")
                    {
                       mouse_event(MOUSEEVENTF_WHEEL, System.Windows.Forms.Control.MousePosition.X, System.Windows.Forms.Control.MousePosition.Y, 20, 0);
                    }
                    else if (e[i] == "(wheeldown)")
                    {
                        mouse_event(MOUSEEVENTF_WHEEL, System.Windows.Forms.Control.MousePosition.X, System.Windows.Forms.Control.MousePosition.Y, -20, 0);
                    }
                    else if (e[i] == "Vup")
                    {
                        keybd_event((byte)Keys.VolumeUp, 0, 0, 0);
                    }
                    else if (e[i] == "Vdown")
                    {
                        keybd_event((byte)Keys.VolumeDown, 0, 0, 0);
                    }
                    else if (e[i] == "mute")
                    {
                        keybd_event((byte)Keys.VolumeMute, 0, 0, 0);
                    }
                    else if (e[i] == "his")
                    {
                        keybd_event(0x11, 0, KEYEVENTF_EXTENDEDKEY | 0, 0);
                        keybd_event((byte)Keys.H, 0, KEYEVENTF_EXTENDEDKEY | 0, 0);
                        keybd_event(0x11, 0, KEYEVENTF_KEYUP | 0, 0); 
                    }
                    else if (e[i] == "clo")
                    {
                        keybd_event(0x11, 0, KEYEVENTF_EXTENDEDKEY | 0, 0);
                        keybd_event((byte)Keys.W, 0, KEYEVENTF_EXTENDEDKEY | 0, 0);
                        keybd_event(0x11, 0, KEYEVENTF_KEYUP | 0, 0);
                    }
                    else
                   if (e[i]=="(click)")
                    {
                        mouse_event(MOUSEEVENTF_LEFTDOWN, System.Windows.Forms.Control.MousePosition.X, System.Windows.Forms.Control.MousePosition.Y, 0, 0);
                        mouse_event(MOUSEEVENTF_LEFTUP, System.Windows.Forms.Control.MousePosition.X, System.Windows.Forms.Control.MousePosition.Y, 0, 0);
                    }
                    else
                    if (e[i] == "(rightclick)")
                    { mouse_event(MOUSEEVENTF_RIGHTDOWN, System.Windows.Forms.Control.MousePosition.X, System.Windows.Forms.Control.MousePosition.Y, 0, 0);
                        mouse_event(MOUSEEVENTF_RIGHTUP, System.Windows.Forms.Control.MousePosition.X, System.Windows.Forms.Control.MousePosition.Y, 0, 0);
                       
                    }
                    else
                    if (e[i] == "(rightdown)")
                    {
                        mouse_event(MOUSEEVENTF_RIGHTDOWN, System.Windows.Forms.Control.MousePosition.X, System.Windows.Forms.Control.MousePosition.Y, 0, 0);
                       

                    }
                    else
                    if (e[i] == "(rightup)")
                    {
                       
                        mouse_event(MOUSEEVENTF_RIGHTUP, System.Windows.Forms.Control.MousePosition.X, System.Windows.Forms.Control.MousePosition.Y, 0, 0);

                    }
                    else
                    if (e[i] == "(leftdown)")
                    {
                        mouse_event(MOUSEEVENTF_LEFTDOWN, System.Windows.Forms.Control.MousePosition.X, System.Windows.Forms.Control.MousePosition.Y, 0, 0);
                       
                    }
                    else
                    if (e[i] == "(leftup)")
                    {
                        mouse_event(MOUSEEVENTF_LEFTUP, System.Windows.Forms.Control.MousePosition.X, System.Windows.Forms.Control.MousePosition.Y, 0, 0);
                       

                    }
                    else if (e[i][0]=='_')
                    {
                        int b=0;
                        string r;
                        if (e[i][1] == '/') {
                            switch (e[i][2])
                            {
                                case 's':
                                    b = VK_SPACE;
                                    break;
                                case 'e':
                                    b = VK_RETURN;
                                    break;
                                case 'd':
                                    b = VK_DELETE;
                                    break;
                            }
                        }
                        else {
                            b = VkKeyScan(e[i][1]);
                        }
                     
                        
                       
                        keybd_event((byte)b, 0, KEYEVENTF_EXTENDEDKEY | 0, 0);
                    }
                    else
                    {
                        try
                        {
                            var h = e[i].Split(';');
                            var a1 = Convert.ToDouble(h[i]);
                            var a2 = Convert.ToDouble(h[i+1]);
                            var a3 = Cursor.Position.X;
                            var a4 = Cursor.Position.Y;
                            moveto(a1, a2, a3, a4,h[i+2]);
                            Cursor.Position = new Point(Convert.ToInt32(x), Convert.ToInt32(y));
                       }
                        catch { }
                    }
                }
            }
        }
        public static void moveto(double len,double dir, double posX, double posY,string d)
        {
            double m = Math.Pow(dir, 2);
            m = m + 1;
            m = Math.Sqrt(m);
            m = len / m;
            if (d == "-")
                m = m * -1;
            x = posX + m;
            double b = posY - (dir * posX);
            y = dir * x;
            y = y + b;
        }
    }
}
