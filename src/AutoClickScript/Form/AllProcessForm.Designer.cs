namespace AutoClickScript
{
    partial class AllProcessForm
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
            this.grid = new System.Windows.Forms.DataGridView();
            this.ColName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColHandle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColChoice = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
            // 
            // grid
            // 
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColName,
            this.ColTitle,
            this.ColHandle,
            this.ColChoice});
            this.grid.Location = new System.Drawing.Point(14, 88);
            this.grid.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grid.Name = "grid";
            this.grid.RowHeadersWidth = 51;
            this.grid.RowTemplate.Height = 27;
            this.grid.Size = new System.Drawing.Size(1143, 792);
            this.grid.TabIndex = 0;
            this.grid.CellContentClick += Grid_CellContentClick;
            // 
            // ColName
            // 
            this.ColName.HeaderText = "程序名";
            this.ColName.MinimumWidth = 6;
            this.ColName.Name = "ColName";
            this.ColName.Width = 250;
            // 
            // ColTitle
            // 
            this.ColTitle.HeaderText = "标题";
            this.ColTitle.MinimumWidth = 6;
            this.ColTitle.Name = "ColTitle";
            this.ColTitle.Width = 500;
            // 
            // ColHandle
            // 
            this.ColHandle.HeaderText = "句柄";
            this.ColHandle.MinimumWidth = 6;
            this.ColHandle.Name = "ColHandle";
            this.ColHandle.Width = 150;
            // 
            // ColChoice
            // 
            this.ColChoice.HeaderText = "选择";
            this.ColChoice.MinimumWidth = 6;
            this.ColChoice.Name = "ColChoice";
            this.ColChoice.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColChoice.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ColChoice.Width = 125;
            // 
            // AllProcessForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1182, 897);
            this.Controls.Add(this.grid);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "AllProcessForm";
            this.Text = "AllProcessForm";
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColHandle;
        private System.Windows.Forms.DataGridViewButtonColumn ColChoice;
    }
}