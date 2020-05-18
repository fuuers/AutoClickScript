using AutoClickScript.Class;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace AutoClickScript
{
    public partial class MainForm : Form
    {
        public List<UiControl> list_UI = new List<UiControl>();//所有UI

        private Thread main_thread = null;//脚本线程

        private delegate void upImage(Bitmap bit);//用于实时更新截取到的画面

        public static ProcessInfo processInfo = new ProcessInfo();//绑定的程序

        public MainForm()
        {
            InitializeComponent();
            timer1.Start();
        }

        /*
         * 
         
            5678
            3478
            2468


         000    1
         001    2
         010    3
         011    4
         100    5
         101    6
         110    7
         111    8

         */

        #region==============================方法

        /// <summary>
        /// 添加UI
        /// </summary>
        /// <param name="ui"></param>
        private void AddNewUI(Bitmap img, Point img_loca, Point click_loca, bool isLoad)
        {
            UiInfo ui = new UiInfo(img.Clone() as Bitmap, img_loca, click_loca);
            if (isLoad)
            {
                ui._img._relativeLocal = img_loca;
                ui._clickLoca = click_loca;
            }

            UiControl ui_ctr = new UiControl();
            ui_ctr.TabIndex = list_UI.Count;
            ui_ctr.Location = CalculateUiLoca(list_UI.Count);
            ui_ctr.uiInfo = ui;
            ui_ctr.DoFixImage = FixImage;
            ui_ctr.DoDeleteImage = DeleteImage;

            ui_ctr.TextLoca = ui._clickLoca.X + "," + ui._clickLoca.Y;
            ui_ctr.UiImage = ui._img._img;
            this.tabControl1.TabPages[0].Controls.Add(ui_ctr);
            this.list_UI.Add(ui_ctr);
        }

        private Point CalculateUiLoca(int index)
        {
            int L = index / 2;
            int x = 6;
            int y = 242 * L + 100;
            if ((index + 1) % 2 == 0)
            {
                x = 370;
            }

            return new Point(x, y);
        }

        /// <summary>
        /// ui修改
        /// </summary>
        /// <param name="uic"></param>
        private void FixImage(UiControl uic)
        {
            this.Hide();
            CaptureForm capture = GetCaptureForm();
            if (capture.ShowDialog() == DialogResult.OK)
            {
                uic.uiInfo = new UiInfo(capture._captureImg, capture._captureLoca, capture._clickLoca);
                uic.UiImage = capture._captureImg;
            }
            this.Show();
        }

        /// <summary>
        /// UI删除
        /// </summary>
        /// <param name="uic"></param>
        private void DeleteImage(UiControl uic)
        {
            this.list_UI.Remove(uic);
            this.tabControl1.TabPages[0].Controls.Remove(uic);
            list_UI.ForEach(x =>
            {
                x.TabIndex = list_UI.IndexOf(x);
                x.Location = CalculateUiLoca(x.TabIndex);
            });
        }

        /// <summary>
        /// 生成截图的Form
        /// </summary>
        /// <returns></returns>
        private CaptureForm GetCaptureForm()
        {
            int swidth = Screen.PrimaryScreen.Bounds.Width;
            int sheight = Screen.PrimaryScreen.Bounds.Height;

            using (Graphics graphics = Graphics.FromHwnd(IntPtr.Zero))
            {
                float dpiX = graphics.DpiX;
                float dpiY = graphics.DpiY;
            }

            Bitmap bit = new Bitmap(swidth, sheight);
            Graphics g = Graphics.FromImage(bit);
            g.CopyFromScreen(new Point(0, 0), new Point(0, 0), new Size(swidth, sheight));

            CaptureForm capture = new CaptureForm(bit);
            capture.WindowState = FormWindowState.Maximized;

            capture.AutoSize = true;
            capture.Size = new System.Drawing.Size(swidth, sheight);
            capture.TopMost = true;
            capture.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            capture.BackgroundImage = bit;
            return capture;
        }

        /// <summary>
        /// 脚本开始
        /// </summary>
        private void DoStart()
        {
            int index = -1;
            if (this.InvokeRequired)
            {
                Func<ComboBox, int> func = x => x.SelectedIndex;
                index = (int)this.Invoke(func, comScriptMode);
            }
            else
            {
                index = comScriptMode.SelectedIndex;
            }

            if (index == 0)
            {
                Start1();
            }
            else
            {
                Start2();
            }
        }

        /// <summary>
        /// 逐一对比，如果相似(汉明距离小于10)就发送消息
        /// </summary>
        private void Start1()
        {
            while (true)
            {
                Thread.Sleep(500);//截图不应过于频繁
                ImageInfo form_img = new ImageInfo();
                try
                {
                    form_img = WinApi.GetWindowCapture(processInfo._Handle);

                    if (form_img != null && form_img._img != null)
                    {
                        for (int i = 0; i < list_UI.Count; i++)
                        {
                            UiInfo ui_info = list_UI[i].uiInfo;
                            ImageInfo now_ui = Common.GetNowUi(form_img._img, ui_info._img);
                            UpdateImage(now_ui._img);
                            if (Common.IsSameImage(ui_info._img, now_ui))
                            {
                                WinApi.SendMsg(ui_info._clickLoca.X, ui_info._clickLoca.Y, processInfo._Handle);
                                Thread.Sleep(500);
                                break;
                            }
                            else
                            {
                                string[] temps = txtFree.Text.Split(',');
                                if (temps.Length > 1)
                                {
                                    WinApi.SendMsg(int.Parse(temps[0]), int.Parse(temps[1]), processInfo._Handle);
                                }
                                Thread.Sleep(300);
                            }
                            now_ui._img.Dispose();
                            now_ui = null;
                        }
                    }
                    form_img?._img?.Dispose();
                    form_img = null;
                }
                catch (Exception ex)
                {
                    form_img?._img?.Dispose();
                    form_img = null;
                }
            }
        }

        /// <summary>
        /// 与所有UI进行对比，取最相似的发送消息
        /// </summary>
        private void Start2()
        {
            while (true)
            {
                Thread.Sleep(500);//截图不应过于频繁
                ImageInfo form_img = new ImageInfo();
                try
                {
                    form_img = WinApi.GetWindowCapture(processInfo._Handle);

                    int diff = 999;
                    int index = 999;
                    if (form_img != null && form_img._img != null)
                    {
                        for (int i = 0; i < list_UI.Count; i++)
                        {
                            UiInfo ui_info = list_UI[i].uiInfo;
                            ImageInfo now_ui = Common.GetNowUi(form_img._img, ui_info._img);
                            UpdateImage(now_ui._img);

                            int temp = Common.GetImageDiffCount(now_ui, ui_info._img);
                            if (temp < diff)
                            {
                                diff = temp;
                                index = i;
                            }
                        }
                        if (index < list_UI.Count && diff < 10)
                        {
                            WinApi.SendMsg(list_UI[index].uiInfo._clickLoca.X, list_UI[index].uiInfo._clickLoca.Y, processInfo._Handle);
                            Thread.Sleep(500);
                        }
                        else
                        {
                            string[] temps = txtFree.Text.Split(',');
                            if (temps.Length > 1)
                            {
                                WinApi.SendMsg(int.Parse(temps[0]), int.Parse(temps[1]), processInfo._Handle);
                            }
                            Thread.Sleep(300);
                        }
                    }

                    form_img?._img?.Dispose();
                    form_img = null;

                }
                catch (Exception ex)
                {
                    form_img?._img?.Dispose();
                    form_img = null;
                }
            }
        }
        private void UpdateImage(Bitmap bit)
        {
            if (chkRefresh.Checked)
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new upImage(UpdateImage), bit);
                }
                else
                {
                    this.picUi.Image = bit.Clone() as Image;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mode">1：运行中，其他：停止运行</param>
        private void SwitchMode(int mode)
        {
            bool enabled = mode == 1 ? false : true;
            this.btnBind.Enabled = enabled;
            this.btnStart.Enabled = enabled;
            this.btnStop.Enabled = !enabled;
            this.btnSaveSlu.Enabled = enabled;
            this.btnShowAllProcess.Enabled = enabled;
            this.btnUIAdd.Enabled = enabled;
            this.btnClear.Enabled = enabled;

            list_UI.ForEach(x => x.Enabled = enabled);
        }

        private void ChangeFormLocationAndSize(int x,int y,int width,int height)
        {
            WinApi.MoveWindow(processInfo._Handle, x, y, width, height, true);
            processInfo._Image = WinApi.GetWindowCapture(processInfo._Handle);
            processInfo._Location = processInfo._Image._screenLoca1 = new Point(x, y);
        }
        #endregion

        #region ==============================事件

        /// <summary>
        /// 脚本开始
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            SwitchMode(1);
            ThreadStart ts = new ThreadStart(DoStart);
            main_thread = new Thread(ts);
            main_thread.Start();
        }

        /// <summary>
        /// 脚本停止
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStop_Click(object sender, EventArgs e)
        {
            SwitchMode(0);
            if (main_thread != null)
            {
                main_thread.Abort();
                main_thread = null;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            btnStop_Click(null, null);
            this.list_UI.Clear();
            for (int i = 0; i < tabPage1.Controls.Count; i++)
            {
                Control ctr = tabPage1.Controls[i];
                if (ctr is UiControl)
                {
                    tabPage1.Controls.Remove(ctr);
                    i--;
                }
            }
        }

        private void btnShowAllProcess_Click(object sender, EventArgs e)
        {
            AllProcessForm f = new AllProcessForm();
            if (f.ShowDialog() == DialogResult.OK)
            {
                this.txtExeName.Text = f.SelectedName;
                processInfo = new ProcessInfo();
                processInfo._Handle = f.SelectedHandle;
                processInfo._Image = WinApi.GetWindowCapture(processInfo._Handle);
                processInfo._Location = processInfo._Image._screenLoca1;
                processInfo._Name = this.txtExeName.Text;
                this.picProcess.Image = Common.GetThumbnail(processInfo._Image._img.Clone() as Bitmap, picProcess.Height, this.picProcess.Width);
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            SolutionSave so = new SolutionSave();
            so.Save(list_UI);
        }

        /// <summary>
        /// UI追加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUIAdd_Click(object sender, EventArgs e)
        {
            this.Hide();
            CaptureForm cutter = GetCaptureForm();

            if (cutter.ShowDialog() == DialogResult.OK)
            {
                AddNewUI(cutter._captureImg, cutter._captureLoca, cutter._clickLoca, false);
            }
            this.Show();
        }

        /// <summary>
        /// 句柄查找
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFindForm_Click(object sender, EventArgs e)
        {
            Process[] ps = Process.GetProcessesByName(this.txtExeName.Text).Where(x => x.MainWindowHandle != IntPtr.Zero).ToArray();

            if (ps.Length == 0)
            {
                MessageBox.Show("目标程序未启动");
                return;
            }
            else if (ps.Length == 1)
            {
                processInfo = new ProcessInfo();
                processInfo._Handle = ps[0].MainWindowHandle;
                processInfo._Image = WinApi.GetWindowCapture(processInfo._Handle);
                processInfo._Location = processInfo._Image._screenLoca1;
                processInfo._Name = this.txtExeName.Text;
                this.picProcess.Image = Common.GetThumbnail(processInfo._Image._img.Clone() as Bitmap, picProcess.Height, this.picProcess.Width);
            }
            else
            {
                AllProcessForm f = new AllProcessForm(ps);
                if (f.ShowDialog() == DialogResult.OK)
                {
                    this.txtExeName.Text = f.SelectedName;
                    processInfo = new ProcessInfo();
                    processInfo._Handle = f.SelectedHandle;
                    processInfo._Image = WinApi.GetWindowCapture(processInfo._Handle);
                    processInfo._Location = processInfo._Image._screenLoca1;
                    processInfo._Name = this.txtExeName.Text;
                    this.picProcess.Image = Common.GetThumbnail(processInfo._Image._img.Clone() as Bitmap, picProcess.Height, this.picProcess.Width);
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.labNowLoca.Text = Control.MousePosition.X + "," + Control.MousePosition.Y;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            list_UI.ForEach(x => this.tabControl1.TabPages[0].Controls.Remove(x));
            list_UI.Clear();
            SolutionSave so = new SolutionSave();
            so.Load();

            if (so._ProcessName == string.Empty) return;
            this.txtExeName.Text = so._ProcessName;
            btnFindForm_Click(null, null);
            if (processInfo._Handle != IntPtr.Zero)
            {
                ChangeFormLocationAndSize(0, 0, so._Bitmap.Width, so._Bitmap.Height);
                //double ratioTempX = (double)processInfo._Image._img.Width/ (double)so._Bitmap.Width;
                //double ratioTempY = (double)processInfo._Image._img.Height / (double)so._Bitmap.Height;

                //double ratioWidth = chkWidth.Checked ? 1 : ratioTempX;
                //double ratioHeight = chkHeight.Checked ? 1 : ratioTempY;
                //double ratioX = chkX.Checked ? 1 : ratioTempX;
                //double ratioY = chkY.Checked ? 1 : ratioTempY;
                foreach (UiInfo ui in so._ListUi)
                {
                    AddNewUI(Common.GetThumbnail(ui._img._img, (ui._img._img.Height), (ui._img._img.Width)),
                        new Point((ui._img._relativeLocal.X), (ui._img._relativeLocal.Y)),
                        new Point((ui._clickLoca.X), (ui._clickLoca.Y)),
                        true
                        );
                }
            }
        }

        #endregion


    }

    /// <summary>
    /// 绑定的程序的基本信息
    /// </summary>
    public class ProcessInfo
    {
        public ImageInfo _Image;
        public Point _Location;
        public IntPtr _Handle = IntPtr.Zero;
        public string _Name = string.Empty;
    }
}
