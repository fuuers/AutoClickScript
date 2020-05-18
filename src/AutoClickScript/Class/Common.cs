using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Media;

namespace AutoClickScript
{
    /// <summary>
    /// 拓展类
    /// </summary>
    public static class Common
    {
        /// <summary>
        /// IEnumerable的拓展方法（其实有个完全一样的Array.ForEach）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="iEnumerable"></param>
        /// <param name="func"></param>
        public static void ForEach<T>(this IEnumerable<T> iEnumerable, Action<T> func)
        {
            foreach (var v in iEnumerable)
            {
                func(v);
            }
        }

        public static TResult ToString<TSource, TResult>(this TSource obj, Func<TSource, TResult> func)
        {
            return func(obj);
        }

        /// <summary>
        /// 返回相对坐标
        /// </summary>
        /// <param name="p"></param>
        /// <param name="zeroPoint">以这个为坐标原点</param>
        /// <returns></returns>
        public static Point GetRelativeLoca(Point p,Point zeroPoint)
        {
            Point fLoca = zeroPoint;
            Point return_p = new Point(p.X - fLoca.X, p.Y - fLoca.Y);
            return return_p;
        }

        /// <summary>
        /// 根据窗体全图截取其中某部分
        /// </summary>
        /// <param name="srcmap">窗体全图</param>
        /// <param name="old_img">包含要截取的坐标及大小</param>
        /// <returns></returns>
        public static ImageInfo GetNowUi(Bitmap srcmap, ImageInfo old_img)
        {
            Point p = old_img._relativeLocal;
            Bitmap new_img = WinApi.CutImage(srcmap, p, old_img._img.Width, old_img._img.Height);

            ImageInfo info = new ImageInfo();
            info._img = new_img;
            return info;
        }

        /// <summary>
        /// 比较两图是否相似
        /// </summary>
        /// <returns>true：相似</returns>
        public static bool IsSameImage(ImageInfo img1, ImageInfo img2)
        {
            string s1 = img1.ID;
            string s2 = img2.ID;

            int count = 0;
            for (int i = 0; i < s1.Length; i++)
            {
                if (s1[i] != s2[i])
                {
                    count++;
                }
                if (count > 10)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 比较两图是否相似
        /// </summary>
        /// <returns>true：相似</returns>
        public static bool IsSameImage(Bitmap img1, Bitmap img2)
        {
            ImageDiff im = new ImageDiff(img1);
            ImageDiff im2 = new ImageDiff(img2);
            string s1 = im.GetHash();
            string s2 = im2.GetHash();

            int count = 0;
            for (int i = 0; i < s1.Length; i++)
            {
                if (s1[i] != s2[i])
                {
                    count++;
                }
                if (count > 10)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 比较两图是否相似,返回值越小越相似
        /// </summary>
        public static int GetImageDiffCount(ImageInfo img1, ImageInfo img2)
        {
            string s1 = img1.ID;
            string s2 = img2.ID;

            return ImageDiff.CalcSimilarDegree(s1, s2);
        }

        /// <summary>
        /// 对图片进行缩放
        /// </summary>
        /// <param name="b"></param>
        /// <param name="destHeight"></param>
        /// <param name="destWidth"></param>
        /// <returns></returns>
        public static Bitmap GetThumbnail(Bitmap b, int destHeight, int destWidth)
        {
            System.Drawing.Image imgSource = b;
            System.Drawing.Imaging.ImageFormat thisFormat = imgSource.RawFormat;
            int sW = 0, sH = 0;
            // 按比例缩放           
            int sWidth = imgSource.Width;
            int sHeight = imgSource.Height;
            if (sHeight > destHeight || sWidth > destWidth)
            {
                if ((sWidth * destHeight) > (sHeight * destWidth))
                {
                    sW = destWidth;
                    sH = (destWidth * sHeight) / sWidth;
                }
                else
                {
                    sH = destHeight;
                    sW = (sWidth * destHeight) / sHeight;
                }
            }
            else
            {
                sW = sWidth;
                sH = sHeight;
            }
            Bitmap outBmp = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage(outBmp);
            g.Clear(Color.Transparent);
            // 设置画布的描绘质量         
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(imgSource, new Rectangle((destWidth - sW) / 2, (destHeight - sH) / 2, sW, sH), 0, 0, imgSource.Width, imgSource.Height, GraphicsUnit.Pixel);
            g.Dispose();
            // 以下代码为保存图片时，设置压缩质量     
            EncoderParameters encoderParams = new EncoderParameters();
            long[] quality = new long[1];
            quality[0] = 100;
            EncoderParameter encoderParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            encoderParams.Param[0] = encoderParam;
            imgSource.Dispose();
            return outBmp;
        }

        #region ======================INI相关

        [DllImport("kernel32")]
        public static extern int GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturned, int size, string lpFileName);

        [DllImport("kernel32")]
        public static extern int WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);

        [DllImport("KERNEL32.DLL", EntryPoint = "GetPrivateProfileString")]
        public static extern uint GetPrivateProfileStringByByteArray(
            string lpAppName,
            string lpKeyName,
            string lpDefault,
            byte[] lpReturnedString,
            uint nSize,
            string inifilename);

        public static string ReadIni(string sectionName, string keyName, string path, string defultString)
        {
            if (File.Exists(path) == false)
            {
                return defultString;
            }
            StringBuilder builder = new StringBuilder(1024);

            GetPrivateProfileString(sectionName, keyName, defultString, builder, 1024, path);
            return builder.ToString();
        }

        public static void WriteIni(string sectionName, string keyName, string value, string path)
        {
            WritePrivateProfileString(sectionName, keyName, value, path);
        }

        public static string[] ReadIniSecKey(string i_EntryName, string i_FilePath)
        {
            if (File.Exists(i_FilePath) == false)
            {
                return new string[] { };
            }

            byte[] bytarr = new byte[4096];

            uint resultSize = GetPrivateProfileStringByByteArray(
                i_EntryName, null, "", bytarr,
                (uint)bytarr.Length, i_FilePath);
            if (resultSize <= 0)
            {
                return new string[] { };
            }
            string result = System.Text.Encoding.Default.GetString(bytarr, 0, (int)resultSize - 1);

            string[] keys = result.Split('\0');

            return keys;
        }

        public static Dictionary<string, string> ReadIniSecAllValue(string i_EntryName, string i_FilePath)
        {
            if (File.Exists(i_FilePath) == false)
            {
                return new Dictionary<string, string>();
            }

            Dictionary<string, string> dicResult = new Dictionary<string, string>();

            string[] keys = ReadIniSecKey(i_EntryName, i_FilePath);

            foreach (string key in keys)
            {
                byte[] bytarr = new byte[2048];

                uint resultSize = GetPrivateProfileStringByByteArray(
                    i_EntryName, key, "", bytarr,
                    (uint)bytarr.Length, i_FilePath);
                if (resultSize <= 0)
                {
                    continue;
                }
                string result = System.Text.Encoding.Default.GetString(bytarr, 0, (int)resultSize);

                dicResult[key] = result;
            }

            return dicResult;
        }
        #endregion
    }
}
