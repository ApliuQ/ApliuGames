using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _7XI_YBB
{
    public partial class Form1 : Form
    {
        KeyboardHook KeyDown;
        PictureBox[] PicBox = new PictureBox[10];//主界面图形
        Bitmap[] Bmp=new Bitmap[10];
        

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Bmp[0] = Properties.Resources.qixi1;
            Bmp[1] = Properties.Resources.qixi2;
            Bmp[2] = Properties.Resources.qixi3;
            Bmp[3] = Properties.Resources.qixi4;
            Bmp[4] = Properties.Resources.qixi5;
            Bmp[5] = Properties.Resources.qixi6;
            Bmp[6] = Properties.Resources.qixi7;
            Bmp[7] = Properties.Resources.qixi8;
            Bmp[8] = Properties.Resources.qixi9;
            Bmp[9] = Properties.Resources.qixi10;

            KeyDown = new KeyboardHook();
            KeyDown.SetHook();
            KeyDown.OnKeyDownEvent += kh_OnKeyDownEvent;

            //初始化背景、数组
            for (int i = 0; i < 10; i++)
            {
                PicBox[i] = new PictureBox();
                //this.Controls.Add(PicBox[i]);
            }

            //this.Opacity = 0.5D;//窗口可见度
            this.TransparencyKey = SystemColors.Control;
            
        }

        void kh_OnKeyDownEvent(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Escape: this.Close();
                    break;
                case Keys.Q: timer1.Enabled=true;
                    break;
                default: break;
            }
        }

        int BmpNum = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (BmpNum >= 9) BmpNum = 0;
            pictureBox1.BackgroundImage = Bmp[BmpNum];
            BmpNum++;
        }
    }
}
