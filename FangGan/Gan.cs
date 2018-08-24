using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FangGan
{
    public partial class Gan : Form
    {
        KeyboardHook KeyBH;
        int OperLen = 0;

        public Gan()
        {
            InitializeComponent();
        }

        private void Gan_Load(object sender, EventArgs e)
        {
            KeyBH = new KeyboardHook();
            KeyBH.SetHook();
            KeyBH.OnKeyDownEvent += KeyBH_OnKeyDownEvent;//按下按钮
            KeyBH.OnKeyUpEvent += KeyBH_OnKeyUpEvent;//离开按钮

            //窗体大小不可改变
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MinimizeBox = false;//去掉最小化按钮
            this.MaximizeBox = false;//去掉最大化按钮
        }

        private void KeyBH_OnKeyDownEvent(object sender, KeyEventArgs e)
        {
            TimeOper.Enabled = true;

            switch (e.KeyData)
            {
                case Keys.Up:
                    break;
                case Keys.Down:
                    break;
                case Keys.Left:
                    break;
                case Keys.Right:
                    break;
                default:
                    break;
            }
        }

        private void KeyBH_OnKeyUpEvent(object sender, KeyEventArgs e)
        {

            TimeOper.Enabled = false;
            TimerRun.Enabled = true;
            //OperLen = 0;
        }

        private void start_Click(object sender, EventArgs e)
        {
            RandomLen(pictureBox1);
            RandomLen(pictureBox2);
            RandomLen(pictureBox3);
            //清空原本图形
            Graphics GPic = this.CreateGraphics();
            GPic.Clear(Color.White);
            TimerRun.Enabled = false;
            OperLen = 0;
            run = 270;
        }


        //为了快速随机数字不重复，设立随机种子
        static int GetRandomSeed()
        {
            byte[] bytes = new byte[4];
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }
        int Pic1Len = 0, Pic2Len = 0;
        private void RandomLen(PictureBox PicBox)
        {
            //清空原本图形
            Graphics GPic = PicBox.CreateGraphics();
            GPic.Clear(Color.White);

            //随机宽度
            Random Ran = new Random(GetRandomSeed());
            int Len = Ran.Next(10,80);
            if (PicBox.Equals(pictureBox1)) Pic1Len = Len;
            if (PicBox.Equals(pictureBox2)) Pic2Len = Len;
            SolidBrush Bru = new SolidBrush(Color.Black);
            GPic.FillRectangle(Bru, 0, 0, Len, 200);

            GPic.Dispose();
            Bru.Dispose();
        }


        private void TimeOper_Tick(object sender, EventArgs e)
        {
            //清空原本图形
            Graphics GPic = this.CreateGraphics();
            GPic.Clear(Color.White);

            Pen pen = new Pen(Color.Black, 2);

            OperLen += 3;
            Point Start = new Point(pictureBox1.Location.X + Pic1Len-1, pictureBox1.Location.Y);
            Point End = new Point(pictureBox1.Location.X + Pic1Len - 1, pictureBox1.Location.Y - OperLen);
            GPic.DrawLine(pen, Start, End);//直线,起始(10,10),终点(100,100)

            GPic.Dispose();
            pen.Dispose();
        }

        double run = 270;
        private void TimerRun_Tick(object sender, EventArgs e)
        {
            run = run + 1;
            Point Start = new Point(pictureBox1.Location.X + Pic1Len - 2, pictureBox1.Location.Y);
            Point End = new Point((int)(pictureBox1.Location.X + Pic1Len - 2 + OperLen * Math.Cos(Math.PI / 180 * run)), (int)(pictureBox1.Location.Y + OperLen * Math.Sin(Math.PI / 180 * run)));

            Bitmap destBmp = new Bitmap(this.BackgroundImage);
            Graphics g = Graphics.FromImage(destBmp);//在刚才新建的图片上新建一个画板
            Pen pen = new Pen(Color.Black, 2);
            g.DrawLine(pen, Start, End);//直线,起始(10,10),终点(100,100)

            Graphics Gra = this.CreateGraphics();
            Gra.DrawImage(destBmp, new Point(0, 0));//将刚才所画的图片画到这个窗体上
            destBmp.Dispose();

            if (End.X - Start.X >= OperLen)
            {
                TimerRun.Enabled = false;
                OperLen = 0;
                run = 270;
                //isGameOver();
                if (End.X - pictureBox2.Location.X >= -3 && End.X - pictureBox2.Location.X <= Pic2Len)
                {
                }
                else MessageBox.Show("Game Over!");
            }
        }

        private void Gan_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void Gan_FormClosing(object sender, FormClosingEventArgs e)
        {
            KeyBH.UnHook();
        }
    }
}
