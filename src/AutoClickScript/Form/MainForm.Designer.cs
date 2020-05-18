namespace AutoClickScript
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.txtExeName = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.picUi = new System.Windows.Forms.PictureBox();
            this.chkRefresh = new System.Windows.Forms.CheckBox();
            this.txtFree = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnUIAdd = new System.Windows.Forms.Button();
            this.tabImage = new System.Windows.Forms.TabPage();
            this.picProcess = new System.Windows.Forms.PictureBox();
            this.labNowLoca = new System.Windows.Forms.Label();
            this.btnBind = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnSaveSlu = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnShowAllProcess = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.comScriptMode = new System.Windows.Forms.ComboBox();
            this.btnLoad = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.chkX = new System.Windows.Forms.CheckBox();
            this.chkY = new System.Windows.Forms.CheckBox();
            this.chkWidth = new System.Windows.Forms.CheckBox();
            this.chkHeight = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picUi)).BeginInit();
            this.tabImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picProcess)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 10);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "程序名";
            // 
            // txtExeName
            // 
            this.txtExeName.Location = new System.Drawing.Point(81, 6);
            this.txtExeName.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtExeName.Name = "txtExeName";
            this.txtExeName.Size = new System.Drawing.Size(577, 27);
            this.txtExeName.TabIndex = 1;
            this.txtExeName.Text = "Nox";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabImage);
            this.tabControl1.Location = new System.Drawing.Point(279, 41);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1154, 844);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.AutoScroll = true;
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.txtFree);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.btnClear);
            this.tabPage1.Controls.Add(this.btnUIAdd);
            this.tabPage1.Location = new System.Drawing.Point(4, 30);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage1.Size = new System.Drawing.Size(1146, 810);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "UI";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.picUi);
            this.groupBox2.Controls.Add(this.chkRefresh);
            this.groupBox2.Location = new System.Drawing.Point(737, 19);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(387, 259);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "截取到的画面";
            // 
            // picUi
            // 
            this.picUi.Location = new System.Drawing.Point(15, 65);
            this.picUi.Name = "picUi";
            this.picUi.Size = new System.Drawing.Size(352, 174);
            this.picUi.TabIndex = 1;
            this.picUi.TabStop = false;
            // 
            // chkRefresh
            // 
            this.chkRefresh.AutoSize = true;
            this.chkRefresh.Location = new System.Drawing.Point(15, 31);
            this.chkRefresh.Name = "chkRefresh";
            this.chkRefresh.Size = new System.Drawing.Size(211, 24);
            this.chkRefresh.TabIndex = 0;
            this.chkRefresh.Text = "是否及时更新此画面";
            this.chkRefresh.UseVisualStyleBackColor = true;
            // 
            // txtFree
            // 
            this.txtFree.Location = new System.Drawing.Point(239, 57);
            this.txtFree.Margin = new System.Windows.Forms.Padding(4);
            this.txtFree.Name = "txtFree";
            this.txtFree.Size = new System.Drawing.Size(205, 27);
            this.txtFree.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(235, 19);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(480, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "设置空白区域，当匹配不到任何UI时将会点击这个坐标";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(8, 57);
            this.btnClear.Margin = new System.Windows.Forms.Padding(4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(205, 30);
            this.btnClear.TabIndex = 5;
            this.btnClear.Text = "清除";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnUIAdd
            // 
            this.btnUIAdd.Location = new System.Drawing.Point(8, 19);
            this.btnUIAdd.Margin = new System.Windows.Forms.Padding(4);
            this.btnUIAdd.Name = "btnUIAdd";
            this.btnUIAdd.Size = new System.Drawing.Size(205, 30);
            this.btnUIAdd.TabIndex = 0;
            this.btnUIAdd.Text = "UI添加";
            this.btnUIAdd.UseVisualStyleBackColor = true;
            this.btnUIAdd.Click += new System.EventHandler(this.btnUIAdd_Click);
            // 
            // tabImage
            // 
            this.tabImage.Controls.Add(this.picProcess);
            this.tabImage.Location = new System.Drawing.Point(4, 30);
            this.tabImage.Margin = new System.Windows.Forms.Padding(4);
            this.tabImage.Name = "tabImage";
            this.tabImage.Padding = new System.Windows.Forms.Padding(4);
            this.tabImage.Size = new System.Drawing.Size(1146, 810);
            this.tabImage.TabIndex = 1;
            this.tabImage.Text = "当前绑定的画面";
            this.tabImage.UseVisualStyleBackColor = true;
            // 
            // picProcess
            // 
            this.picProcess.Location = new System.Drawing.Point(8, 8);
            this.picProcess.Margin = new System.Windows.Forms.Padding(4);
            this.picProcess.Name = "picProcess";
            this.picProcess.Size = new System.Drawing.Size(1125, 790);
            this.picProcess.TabIndex = 0;
            this.picProcess.TabStop = false;
            // 
            // labNowLoca
            // 
            this.labNowLoca.AutoSize = true;
            this.labNowLoca.Location = new System.Drawing.Point(55, 47);
            this.labNowLoca.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labNowLoca.Name = "labNowLoca";
            this.labNowLoca.Size = new System.Drawing.Size(57, 20);
            this.labNowLoca.TabIndex = 1;
            this.labNowLoca.Text = "label2";
            // 
            // btnBind
            // 
            this.btnBind.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnBind.Location = new System.Drawing.Point(17, 41);
            this.btnBind.Margin = new System.Windows.Forms.Padding(4);
            this.btnBind.Name = "btnBind";
            this.btnBind.Size = new System.Drawing.Size(205, 30);
            this.btnBind.TabIndex = 3;
            this.btnBind.Text = "绑定";
            this.btnBind.UseVisualStyleBackColor = true;
            this.btnBind.Click += new System.EventHandler(this.btnFindForm_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnStart
            // 
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnStart.Location = new System.Drawing.Point(17, 115);
            this.btnStart.Margin = new System.Windows.Forms.Padding(4);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(205, 30);
            this.btnStart.TabIndex = 4;
            this.btnStart.Text = "开始";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnStop.Location = new System.Drawing.Point(17, 153);
            this.btnStop.Margin = new System.Windows.Forms.Padding(4);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(205, 30);
            this.btnStop.TabIndex = 4;
            this.btnStop.Text = "停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnSaveSlu
            // 
            this.btnSaveSlu.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnSaveSlu.Location = new System.Drawing.Point(17, 191);
            this.btnSaveSlu.Margin = new System.Windows.Forms.Padding(4);
            this.btnSaveSlu.Name = "btnSaveSlu";
            this.btnSaveSlu.Size = new System.Drawing.Size(205, 30);
            this.btnSaveSlu.TabIndex = 6;
            this.btnSaveSlu.Text = "保存";
            this.btnSaveSlu.UseVisualStyleBackColor = true;
            this.btnSaveSlu.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labNowLoca);
            this.groupBox1.Location = new System.Drawing.Point(22, 781);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 100);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "屏幕坐标";
            // 
            // btnShowAllProcess
            // 
            this.btnShowAllProcess.Location = new System.Drawing.Point(17, 78);
            this.btnShowAllProcess.Name = "btnShowAllProcess";
            this.btnShowAllProcess.Size = new System.Drawing.Size(205, 30);
            this.btnShowAllProcess.TabIndex = 8;
            this.btnShowAllProcess.Text = "查看所有程序";
            this.btnShowAllProcess.UseVisualStyleBackColor = true;
            this.btnShowAllProcess.Click += new System.EventHandler(this.btnShowAllProcess_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.comScriptMode);
            this.groupBox3.Location = new System.Drawing.Point(22, 665);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 100);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "脚本运行模式";
            // 
            // comScriptMode
            // 
            this.comScriptMode.FormattingEnabled = true;
            this.comScriptMode.Items.AddRange(new object[] {
            "模式1",
            "模式2"});
            this.comScriptMode.Location = new System.Drawing.Point(6, 36);
            this.comScriptMode.Name = "comScriptMode";
            this.comScriptMode.Size = new System.Drawing.Size(177, 28);
            this.comScriptMode.TabIndex = 0;
            // 
            // btnLoad
            // 
            this.btnLoad.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnLoad.Location = new System.Drawing.Point(17, 229);
            this.btnLoad.Margin = new System.Windows.Forms.Padding(4);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(205, 30);
            this.btnLoad.TabIndex = 9;
            this.btnLoad.Text = "加载已保存方案";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.chkHeight);
            this.groupBox4.Controls.Add(this.chkWidth);
            this.groupBox4.Controls.Add(this.chkY);
            this.groupBox4.Controls.Add(this.chkX);
            this.groupBox4.Location = new System.Drawing.Point(17, 266);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(205, 160);
            this.groupBox4.TabIndex = 10;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "加载选项";
            // 
            // chkX
            // 
            this.chkX.AutoSize = true;
            this.chkX.Location = new System.Drawing.Point(20, 26);
            this.chkX.Name = "chkX";
            this.chkX.Size = new System.Drawing.Size(141, 24);
            this.chkX.TabIndex = 0;
            this.chkX.Text = "UI坐标X固定";
            this.chkX.UseVisualStyleBackColor = true;
            // 
            // chkY
            // 
            this.chkY.AutoSize = true;
            this.chkY.Location = new System.Drawing.Point(20, 56);
            this.chkY.Name = "chkY";
            this.chkY.Size = new System.Drawing.Size(141, 24);
            this.chkY.TabIndex = 1;
            this.chkY.Text = "UI坐标Y固定";
            this.chkY.UseVisualStyleBackColor = true;
            // 
            // chkWidth
            // 
            this.chkWidth.AutoSize = true;
            this.chkWidth.Location = new System.Drawing.Point(20, 86);
            this.chkWidth.Name = "chkWidth";
            this.chkWidth.Size = new System.Drawing.Size(129, 24);
            this.chkWidth.TabIndex = 2;
            this.chkWidth.Text = "UI长度固定";
            this.chkWidth.UseVisualStyleBackColor = true;
            // 
            // chkHeight
            // 
            this.chkHeight.AutoSize = true;
            this.chkHeight.Location = new System.Drawing.Point(20, 116);
            this.chkHeight.Name = "chkHeight";
            this.chkHeight.Size = new System.Drawing.Size(129, 24);
            this.chkHeight.TabIndex = 3;
            this.chkHeight.Text = "UI高度固定";
            this.chkHeight.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1450, 894);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.btnShowAllProcess);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSaveSlu);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnBind);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.txtExeName);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picUi)).EndInit();
            this.tabImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picProcess)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtExeName;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabImage;
        private System.Windows.Forms.PictureBox picProcess;
        private System.Windows.Forms.Button btnBind;
        private System.Windows.Forms.Button btnUIAdd;
        private System.Windows.Forms.Label labNowLoca;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.TextBox txtFree;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSaveSlu;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnShowAllProcess;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkRefresh;
        private System.Windows.Forms.PictureBox picUi;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox comScriptMode;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox chkHeight;
        private System.Windows.Forms.CheckBox chkWidth;
        private System.Windows.Forms.CheckBox chkY;
        private System.Windows.Forms.CheckBox chkX;
    }
}