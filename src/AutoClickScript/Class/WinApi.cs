using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.IO;

namespace AutoClickScript
{
    public class WinApi
    {
        public static ImageInfo GetWindowCapture(IntPtr hWnd)
        {
            try
            {
                IntPtr hscrdc = GetWindowDC(hWnd);
                Rect windowRect = new Rect();
                GetWindowRect(hWnd, ref windowRect);
                int width = windowRect.Right-windowRect.Left;
                int height = windowRect.Bottom - windowRect.Top;

                //System.Windows.Interop.HwndSource xx = System.Windows.Interop.HwndSource.FromHwnd(hWnd);
                //IntPtr hbitmap = CreateCompatibleBitmap(hscrdc, 100, Convert.ToInt32(100));
                IntPtr hbitmap = CreateCompatibleBitmap(hscrdc, width, height);
                IntPtr hmemdc = CreateCompatibleDC(hscrdc);
                SelectObject(hmemdc, hbitmap);
                PrintWindow(hWnd, hmemdc, 0);

                //Image bmp = Image.FromHbitmap(hbitmap);
                Bitmap bmp = Bitmap.FromHbitmap(hbitmap);

                DeleteDC(hscrdc);//删除用过的对象  
                DeleteDC(hmemdc);//删除用过的对象

                ImageInfo finfo = new ImageInfo();
                finfo._img = bmp;
                finfo._screenLoca1 = new Point(windowRect.Left, windowRect.Top);
                return finfo;
            }
            catch
            { }
            return null;
        }
        /// <summary>
        /// 截取程序目标区域
        /// </summary>
        /// <returns></returns>
        public static ImageInfo GetWindowCaptureOnLoca(IntPtr hWnd, Point i_loca, int i_width, int i_height)
        {
            try
            {
                IntPtr hscrdc = GetWindowDC(hWnd);
                Rectangle windowRect = new Rectangle();
                //GetWindowRect(hWnd, ref windowRect);
                int width = windowRect.Right - windowRect.Left;
                int height = windowRect.Bottom - windowRect.Top;

                IntPtr hbitmap = CreateCompatibleBitmap(hscrdc, width, height);
                IntPtr hmemdc = CreateCompatibleDC(hscrdc);
                SelectObject(hmemdc, hbitmap);
                PrintWindow(hWnd, hmemdc, 0);

                Bitmap bmp = Bitmap.FromHbitmap(hbitmap);

                DeleteDC(hscrdc);//删除用过的对象  
                DeleteDC(hmemdc);//删除用过的对象

                ImageInfo finfo = new ImageInfo();
                finfo._img = CutImage(bmp, i_loca, i_width, i_height);
                finfo._screenLoca1 = windowRect.Location;

                bmp.Dispose();
                return finfo;
            }
            catch(Exception ex)
            { 
            }
            return null;
        }

        /// <summary>
        /// 截取图片区域，返回所截取的图片
        /// </summary>
        public static Bitmap CutImage(Bitmap SrcImage, Point pos, int cutWidth, int cutHeight)
        {
            //先初始化一个位图对象，来存储截取后的图像
            Bitmap bmpDest = new Bitmap(cutWidth, cutHeight, PixelFormat.Format32bppRgb);
            Graphics g = Graphics.FromImage(bmpDest);

            //矩形定义,将要在被截取的图像上要截取的图像区域的左顶点位置和截取的大小
            Rectangle rectSource = new Rectangle(pos.X, pos.Y, cutWidth, cutHeight);

            //矩形定义,将要把 截取的图像区域 绘制到初始化的位图的位置和大小
            //rectDest说明，将把截取的区域，从位图左顶点开始绘制，绘制截取的区域原来大小
            Rectangle rectDest = new Rectangle(0, 0, cutWidth, cutHeight);

            //第一个参数就是加载你要截取的图像对象，第二个和第三个参数及如上所说定义截取和绘制图像过程中的相关属性，第四个属性定义了属性值所使用的度量单位
            g.DrawImage(SrcImage, rectDest, rectSource, GraphicsUnit.Pixel);

            //在GUI上显示被截取的图像
            //cutedImage = (Image)bmpDest;

            g.Dispose();
            return bmpDest;
        }

        /// <summary>
        /// 给程序发送点击事件，模拟器不知道为什么要执行两次才成功（测试是夜神模拟器
        /// </summary>
        public static void SendMsg(int x, int y,IntPtr hWnd)
        {
            //SendMessage(hWnd, WM_MOUSEMOVE, IntPtr.Zero, (IntPtr)(y * 65536 + x));
            //SendMessage(hWnd, WM_LBUTTONDOWN, (IntPtr)MK_LBUTTON, (IntPtr)(y * 65536 + x));
            //SendMessage(hWnd, WM_LBUTTONUP, (IntPtr)MK_LBUTTON, (IntPtr)(y * 65536 + x));

            //SendMessage(hWnd, WM_MOUSEMOVE, IntPtr.Zero, (IntPtr)(y * 65536 + x));
            SendMessage(hWnd, WM_LBUTTONDOWN, (IntPtr)MK_LBUTTON, (IntPtr)(y * 65536 + x));
            SendMessage(hWnd, WM_LBUTTONUP, (IntPtr)MK_LBUTTON, (IntPtr)(y * 65536 + x));
        }

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(String lpclassName, String lpWindowText);

        [DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Unicode)]
        public static extern int SendMessage(IntPtr hWnd,int message,IntPtr wParam,IntPtr lParam);

        [DllImport("user32.dll", EntryPoint = "PostMessage", CharSet = CharSet.Unicode)]
        public static extern int PostMessage(IntPtr hWnd, int message, IntPtr wParam, IntPtr lParam);

        public const int WM_MOUSEMOVE = 0x0200;
        public const int WM_LBUTTONDOWN = 0x0201;
        public const int WM_LBUTTONUP = 0x0202;
        public const int WM_LBUTTONDBLCLK = 0x0203;
        public const int MK_LBUTTON = 1;

        public struct Rect 
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowRect(IntPtr hWnd, ref Rect rect);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateDC(
         string lpszDriver,         // driver name驱动名  
         string lpszDevice,         // device name设备名  
         string lpszOutput,         // not used; should be NULL  
         IntPtr lpInitData   // optional printer data  
         );
        [DllImport("gdi32.dll")]
        public static extern int BitBlt(
         IntPtr hdcDest, // handle to destination DC目标设备的句柄  
         int nXDest,   // x-coord of destination upper-left corner目标对象的左上角的X坐标  
         int nYDest,   // y-coord of destination upper-left corner目标对象的左上角的Y坐标  
         int nWidth,   // width of destination rectangle目标对象的矩形宽度  
         int nHeight, // height of destination rectangle目标对象的矩形长度  
         IntPtr hdcSrc,   // handle to source DC源设备的句柄  
         int nXSrc,    // x-coordinate of source upper-left corner源对象的左上角的X坐标  
         int nYSrc,    // y-coordinate of source upper-left corner源对象的左上角的Y坐标  
         UInt32 dwRop   // raster operation code光栅的操作值  
         );


        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleDC(
         IntPtr hdc // handle to DC  
         );


        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleBitmap(
         IntPtr hdc,         // handle to DC  
         int nWidth,      // width of bitmap, in pixels  
         int nHeight      // height of bitmap, in pixels  
         );


        [DllImport("gdi32.dll")]
        public static extern IntPtr SelectObject(
         IntPtr hdc,           // handle to DC  
         IntPtr hgdiobj    // handle to object  
         );


        [DllImport("gdi32.dll")]
        public static extern int DeleteDC(
         IntPtr hdc           // handle to DC  
         );


        [DllImport("user32.dll")]
        public static extern bool PrintWindow(
         IntPtr hwnd,                // Window to copy,Handle to the window that will be copied.  
         IntPtr hdcBlt,              // HDC to print into,Handle to the device context.  
         UInt32 nFlags               // Optional flags,Specifies the drawing options. It can be one of the following values.  
         );


        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowDC(
         IntPtr hwnd
         );
    }
}
