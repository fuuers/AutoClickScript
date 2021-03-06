﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoClickScript
{
    public partial class UiControl : UserControl
    {
        public delegate void FixImage(UiControl uiCtrl);
        public FixImage DoFixImage = null;
        public FixImage DoDeleteImage = null;
        public Image UiImage
        {
            set
            {
                this.pictureBox1.Image = value;
            }
        }

        public UiInfo uiInfo { get; set; }

        public string TextLoca
        {
            set { this.txtLoca.Text = value; }
        }

        public UiControl()
        {
            InitializeComponent();
        }


        private void btnFix_Click(object sender, EventArgs e)
        {
            if (DoFixImage != null)
            {
                DoFixImage(this);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (DoDeleteImage != null)
            {
                DoDeleteImage(this);
            }
        }
    }
}
