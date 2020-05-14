using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace AutoClickScript
{
    public partial class MainForm : Form
    {
        private List<UiControl> list_UI = new List<UiControl>();//所有UI

        private Thread main_thread = null;//脚本线程

        private delegate void upImage(Bitmap bit);//用于实时更新截取到的画面

        public static ProcessInfo processInfo = new ProcessInfo();//绑定的程序

        public MainForm()
        {
            InitializeComponent();
            timer1.Start();
        }

        #region==============================方法

        /// <summary>
        /// 添加UI
        /// </summary>
        /// <param name="ui"></param>
        private void AddNewUI(Bitmap img, Point img_loca, Point click_loca)
        {
            UiInfo ui = new UiInfo(img.Clone() as Bitmap, img_loca, click_loca);

            int y = 177 * (list_UI.Count) + 39;

            UiControl ui_ctr = new UiControl();
            ui_ctr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            ui_ctr.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            ui_ctr.Location = new System.Drawing.Point(6, y);
            ui_ctr.Name = "uI_Control1";
            ui_ctr.Size = new System.Drawing.Size(219, 171);
            ui_ctr.TabIndex = list_UI.Count;
            ui_ctr.uiInfo = ui;
            ui_ctr.DoFixImage = FixImage;

            ui_ctr.TextLoca = ui._clickLoca.X + "," + ui._clickLoca.Y;
            ui_ctr.UiImage = ui._img._img;
            this.tabControl1.TabPages[0].Controls.Add(ui_ctr);
            this.list_UI.Add(ui_ctr);
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
            //Start1();
            Start2();
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
                    form_img._img.Dispose();
                    form_img = null;
                }
                catch (Exception ex)
                {
                    if (form_img != null && form_img._img != null)
                    {
                        form_img._img.Dispose();
                        form_img = null;
                    }
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
                    form_img._img.Dispose();
                    form_img = null;
                }
                catch (Exception ex)
                {
                    if (form_img != null && form_img._img != null)
                    {
                        form_img._img.Dispose();
                        form_img = null;
                    }
                }
            }
        }
        private void UpdateImage(Bitmap bit)
        {
            if (btnRefreshFLg.Tag.ToString() == "true")
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new upImage(UpdateImage), bit);
                }
                else
                {
                    this.pictureBox1.Image = bit.Clone() as Image;
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
                btnFindForm_Click(null, null);
            }
        }

        private void btnRefreshFLg_Click(object sender, EventArgs e)
        {
            if (btnRefreshFLg.Tag.ToString() == "false")
            {
                btnRefreshFLg.Tag = "true";
            }
            else
            {
                btnRefreshFLg.Tag = "false";
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            string path = ".\\已保存";
            string iniPath = path + "\\config.ini";
            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }
            if (File.Exists(iniPath) == false)
            {
                File.Create(iniPath).Close();
            }
            Common.WriteIni("form", "size", processInfo._Image._img.Size.ToString(), iniPath);
            Common.WriteIni("form", "name", processInfo._Name, iniPath);
            processInfo._Image._img.Save(path + "\\form.jpg");

            for (int i = 0; i < list_UI.Count; i++)
            {
                Common.WriteIni("UI_Location", i.ToString(), list_UI[i].uiInfo._img._relativeLocal.ToString(), iniPath);
                Common.WriteIni("UI_ClickPoint", i.ToString(), list_UI[i].uiInfo._clickLoca.ToString(), iniPath);
                list_UI[i].uiInfo._img._img.Save(path + "\\" + i.ToString() + ".jpg");
            }
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
                AddNewUI(cutter._captureImg, cutter._captureLoca, cutter._clickLoca);
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
            Process[] ps = Process.GetProcessesByName(this.txtExeName.Text);

            if (ps.Length == 0)
            {
                MessageBox.Show("目标程序未启动");
                return;
            }

            processInfo = new ProcessInfo();
            processInfo._Handle = ps[0].MainWindowHandle;
            processInfo._Image = WinApi.GetWindowCapture(processInfo._Handle);
            processInfo._Location = processInfo._Image._screenLoca1;
            processInfo._Name = this.txtExeName.Text;
            this.pictureBox1.Image = processInfo._Image._img.Clone() as Image;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.labNowLoca.Text = Control.MousePosition.X + "," + Control.MousePosition.Y;
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
