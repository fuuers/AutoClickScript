using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace AutoClickScript
{
    /// <summary>
    /// 截图画面
    /// </summary>
    public partial class CaptureForm : Form
    {
        Bitmap screenBtmp = null; //电脑屏幕的截图
        /// <summary>
        /// 截到的图
        /// </summary>
        public Bitmap _captureImg = null;
        /// <summary>
        /// 截图的屏幕坐标
        /// </summary>
        public Point _captureLoca = new Point(0, 0);
        /// <summary>
        /// 单击坐标
        /// </summary>
        public Point _clickLoca = new Point(0, 0);

        public CaptureForm(Bitmap btm)
        {
            InitializeComponent();
            
            screenBtmp = btm;

            this.MouseClick += new MouseEventHandler(Cutter_MouseClick);
            this.MouseDown += new MouseEventHandler(Cutter_MouseDown);
            this.MouseMove += new MouseEventHandler(Cutter_MouseMove);
            this.MouseUp += new MouseEventHandler(Cutter_MouseUp);
            this.MouseDoubleClick += new MouseEventHandler(Cutter_MouseDoubleClick);
        }

        //鼠标右键退出
        private void Cutter_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        bool CatchStart = false; //自由截图开始
        Point downPoint; //初始点
        //鼠标左键按下-开始自由截图
        private void Cutter_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (!CatchStart)
                {
                    CatchStart = true;
                    downPoint = new Point(e.X, e.Y); //初始点
                }
                
            }
        }

        Rectangle catchRec;//存放截取范围
        //鼠标移动-绘制自由截图路径
        private void Cutter_MouseMove(object sender, MouseEventArgs e)
        { //路径绘制，核心
            if (CatchStart)
            {
                //
                //二次缓冲
                //不是直接在控件的背景画板上进行绘制鼠标移动路径，那样会造成绘制很多路径，因为前面绘制的路径还在
                //而是在内存中每移动一次鼠标就创建一张和屏幕截图一样的新BImtap,在这个Bitmap中绘制鼠标移动路径
                //然后在窗体背景画板上，绘制这个新的Bitmap,这样就不会造成绘制很多路径，因为每次都绘制了全新的Bitmao
                //但是这样做的话，因为鼠标移动的次数是大量的，所以在内存中会创建大量的Bitmap会造成内存消耗严重，所以每次移动绘制完后，
                //需要释放Dispose() 画板，画笔，Bitmap资源。
                //
                Bitmap copyBtmp = (Bitmap)screenBtmp.Clone(); //创建新的,在其上绘制路径
                //左上角
                Point firstP = new Point(downPoint.X, downPoint.Y);
                //新建画板，画笔
                Graphics g = Graphics.FromImage(copyBtmp);
                Pen p = new Pen(Color.Red, 1);
                //计算路径范围
                int width = Math.Abs(e.X - downPoint.X);
                int height = Math.Abs(e.Y - downPoint.Y);
                if (e.X < downPoint.X)
                {
                    firstP.X = e.X;
                }
                if (e.Y < downPoint.Y)
                {
                    firstP.Y = e.Y;
                }
                //绘制路径
                catchRec = new Rectangle(firstP, new Size(width, height));
                //将路径绘制在新的BItmap上，之后要释放

                g.DrawRectangle(p, catchRec);
                g.Dispose();
                p.Dispose();

                //窗体背景画板
                Graphics gf = this.CreateGraphics();
                //将新图绘制在窗体的画板上   --   自由截图-路径绘制处，其实还是一张和屏幕同样大小的图片，只不过上面有红色的选择路径
                gf.DrawImage(copyBtmp, new Point(0, 0));
                gf.Dispose();
                //释放内存Bimtap
                copyBtmp.Dispose();

            }
        }

        bool catchFinished = false; //自由截图结束标志
        //鼠标左键弹起-结束自由截图
        private void Cutter_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (CatchStart)
                {
                    CatchStart = false;
                    catchFinished = true;
                }
            }
        }

        //鼠标左键双击，保存自由截取的图片
        private void Cutter_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if ((e.Button == MouseButtons.Left) && catchFinished)
                {
                    //创建用户截取的范围大小的空图
                    Bitmap catchBtmp = new Bitmap(catchRec.Width, catchRec.Height);
                    Graphics g = Graphics.FromImage(catchBtmp);
                    //在原始的屏幕截图ScreenBitmap上 截取 用户选择范围大小的区域   绘制到上面的空图
                    //绘制完后，这个空图就是我们想要的截取的图片
                    //参数1  原图
                    //参数2  在空图上绘制的范围区域
                    //参数3  原图的截取范围
                    //参数4  度量单位
                    g.DrawImage(screenBtmp, new Rectangle(0, 0, catchRec.Width, catchRec.Height), catchRec, GraphicsUnit.Pixel);

                    //将自由截取的图片保存到剪切板中
                    //Clipboard.Clear();
                    //Clipboard.SetImage(catchBtmp);

                    _captureImg = catchBtmp;
                    _captureLoca = catchRec.Location;
                    _clickLoca = new Point( Control.MousePosition.X, Control.MousePosition.Y);

                    g.Dispose();
                    catchFinished = false;
                    this.BackgroundImage = screenBtmp;
                    
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }
    }
}
