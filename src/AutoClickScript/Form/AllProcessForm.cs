using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoClickScript
{
    /// <summary>
    /// 读取正在运行的所有程序
    /// </summary>
    public partial class AllProcessForm : Form
    {
        public string SelectedName = string.Empty;
        public IntPtr SelectedHandle = IntPtr.Zero;
        public AllProcessForm()
        {
            InitializeComponent();
            this.Load += AllProcessForm_Load;
            this.grid.CellContentClick += Grid_CellContentClick;
        }

        private void Grid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == grid.Columns[ColChoice.Name].Index)
            {
                this.DialogResult = DialogResult.OK;
                SelectedName = grid[ColName.Name, e.RowIndex].Value.ToString();
                SelectedHandle = (IntPtr)grid[ColHandle.Name, e.RowIndex].Value;
                this.Close();
            }
        }

        private void AllProcessForm_Load(object sender, EventArgs e)
        {
            Process[] ps = Process.GetProcesses();
            ps.Where(x => x.MainWindowHandle != IntPtr.Zero).ForEach(x => this.grid.Rows.Add(x.ProcessName, x.MainWindowTitle, x.MainWindowHandle, "选这个"));
        }
    }
}
