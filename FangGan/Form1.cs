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
    public partial class Form1 : Form
    {
        KeyboardHook KeyBH;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            KeyBH = new KeyboardHook();
            KeyBH.SetHook();
            KeyBH.OnKeyDownEvent += KeyBH_OnKeyDownEvent;
            KeyBH.OnKeyUpEvent += KeyBH_OnKeyUpEvent;
        }

        private void GDIpint()
        {
            Graphics Gra = pictureBox1.CreateGraphics();
            Pen pen = new Pen(Color.Blue, 2);
            SolidBrush Bru = new SolidBrush(Color.Blue);
            Gra.FillEllipse(Bru, 100, 100, 100, 100);//椭圆,起始坐标(10,10),外接矩形(100,100)
        }

        private void KeyBH_OnKeyDownEvent(object sender, KeyEventArgs e)
        {
            TimerRun.Enabled = true;

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
            
            TimerRun.Enabled = true;
        }

        Point Start = new Point(150, 150);
        double run = 0;
        //(x－a)²+(y－b)²=r²

        private void TimerRun_Tick(object sender, EventArgs e)
        {

            run = run + 1;
            Point End = new Point((int)(150 + 100 * Math.Cos(Math.PI / 180 * run)), (int)(150 + 100 * Math.Sin(Math.PI / 180 * run)));

            Bitmap destBmp = new Bitmap(pictureBox1.BackgroundImage);
            Graphics g = Graphics.FromImage(destBmp);//在刚才新建的图片上新建一个画板
            Pen pen = new Pen(Color.Blue, 2);
            g.DrawLine(pen, Start, End);//直线,起始(10,10),终点(100,100)
            

            Graphics Gra = pictureBox1.CreateGraphics();
            //Gra.DrawLine(pen, Start, End);//直线,起始(10,10),终点(100,100)
            Gra.DrawImage(destBmp, new Point(0, 0));//将刚才所画的图片画到这个窗体上
            destBmp.Dispose();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int Score = 0;
            Score += (int)Math.Pow(10, 1);
            label1.Text = Score.ToString();
        }

        private void Gan_Click(object sender, EventArgs e)
        {
            Gan myfrmMain = new Gan();//实例化窗体
            myfrmMain.Show();//将窗体显示出来
            //this.Hide();//当前窗体隐藏
        }
    }
}
