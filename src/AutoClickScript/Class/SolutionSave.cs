using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoClickScript.Class
{
    /// <summary>
    /// 保存和加载
    /// </summary>
    class SolutionSave
    {
        public void Save(List<UiControl> list_UI)
        {
            FolderBrowserDialog fb = new FolderBrowserDialog();
            if (fb.ShowDialog() == DialogResult.OK)
            {
                string path = fb.SelectedPath;
                string iniPath = path + "\\config.ini";
                
                if (Directory.Exists(path) == false) 
                {
                    Directory.CreateDirectory(path);
                }
                if (File.Exists(iniPath) == false)
                {
                    File.Create(iniPath).Close();
                }
                Common.WriteIni("form", "size", MainForm.processInfo._Image._img.Size.ToString(x => x.Width.ToString() + "," + x.Height.ToString()), iniPath);
                Common.WriteIni("form", "name", MainForm.processInfo._Name, iniPath);
                MainForm.processInfo._Image._img.Save(path + "\\form.jpg");

                Func<Point, string> f = x => x.X.ToString() + "," + x.Y.ToString();
                for (int i = 0; i < list_UI.Count; i++)
                {
                    Common.WriteIni("UI_Location", i.ToString(), list_UI[i].uiInfo._img._relativeLocal.ToString(f), iniPath);
                    Common.WriteIni("UI_ClickPoint", i.ToString(), list_UI[i].uiInfo._clickLoca.ToString(f), iniPath);
                    list_UI[i].uiInfo._img._img.Save(path + "\\" + i.ToString() + ".jpg");
                }
            }
        }

        public string _ProcessName = string.Empty;
        public List<UiInfo> _ListUi = new List<UiInfo>();
        public Bitmap _Bitmap = null;

        public void Load()
        {
            FolderBrowserDialog fb = new FolderBrowserDialog();
            if (fb.ShowDialog() == DialogResult.OK)
            {
                string path = fb.SelectedPath;
                string iniPath = path + "\\config.ini";

                if (File.Exists(iniPath) == false)
                {
                    MessageBox.Show("配置文件不存在");
                }
                _ProcessName = Common.ReadIni("form", "name", iniPath, "");

                using (FileStream sr = new FileStream(path + "\\form.jpg",FileMode.Open))
                {
                    _Bitmap = new Bitmap(sr);
                    sr.Close();
                }
                Dictionary<string, string> ui_loca = Common.ReadIniSecAllValue("UI_Location", iniPath);
                Dictionary<string, string> click_loca = Common.ReadIniSecAllValue("UI_ClickPoint", iniPath);

                foreach (string key in ui_loca.Keys)
                {
                    UiInfo ui = new UiInfo();
                    ImageInfo imageInfo = new ImageInfo();

                    using (FileStream sr = new FileStream(path + "\\" + key + ".jpg", FileMode.Open))
                    {
                        imageInfo._img = new Bitmap(sr);
                        sr.Close();
                    }
                    imageInfo._screenLoca1 = imageInfo._relativeLocal = GetPoint(ui_loca[key]);
                    ui._img = imageInfo;
                    ui._clickLoca = GetPoint(click_loca[key]);
                    _ListUi.Add(ui);
                }
            }
        }

        public Point GetPoint(string s)
        {
            string[] temps = s.Split(',');
            return new Point(int.Parse(temps[0]), int.Parse(temps[1]));
        }
    }
}
