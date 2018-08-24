using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace OtherTest
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }
        //RGB
        Int32[,] XiaoRen = new Int32[9, 3] { 
            {51,53,65}, 
            {60,60,76},
            {53,58,71},
            {74,73,113},
            {51,52,64},
            {54,60,102},
            {43,43,73},
            {57,54,84},
            {58,57,99}
        };


        /// <summary> 
        /// 截取全屏幕图像 
        /// </summary> 
        /// <returns>屏幕位图</returns>         
        public Bitmap GetFullScreen()
        {
            Bitmap mimage = new Bitmap(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width, System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height);
            Graphics gp = Graphics.FromImage(mimage);
            gp.CopyFromScreen(new Point(System.Windows.Forms.Screen.PrimaryScreen.Bounds.X, System.Windows.Forms.Screen.PrimaryScreen.Bounds.Y), new Point(0, 0), mimage.Size, CopyPixelOperation.SourceCopy);
            gp.Dispose();
            return mimage;
        }

        /// <summary>
        /// 获取屏幕指定矩形的图像
        /// </summary>
        /// <param name="CatchRect"></param>
        /// <returns></returns>
        public Bitmap GetBmpFromScreen(Rectangle CatchRect)
        {
            Bitmap Bmp = new Bitmap(CatchRect.Width, CatchRect.Height);//新建一个于矩形等大的空白图片
            Graphics g = Graphics.FromImage(Bmp);
            g.DrawImage(GetFullScreen(), new Rectangle(0, 0, CatchRect.Width, CatchRect.Height), CatchRect, GraphicsUnit.Pixel);
            return Bmp;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap("d:\\tiaoyitiao1.png");
            Color cobig = bmp.GetPixel(1, 1);
            Color coend = bmp.GetPixel(bmp.Width - 1, bmp.Height - 1);
            Color cobag = Color.FromArgb((cobig.R + coend.R) / 2, (cobig.G + coend.G) / 2, (cobig.B + coend.B) / 2);

            int XunZhaoYes = 0;
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    Color co = bmp.GetPixel(i, j);

                    for (int m = 0; m < 9; m++)
                    {
                        int R = co.R - XiaoRen[m, 0];
                        int G = co.G - XiaoRen[m, 1];
                        int B = co.B - XiaoRen[m, 2];

                        if ((Math.Abs((decimal)R) + Math.Abs((decimal)G) + Math.Abs((decimal)B) <= 100))
                        {
                            bmp.SetPixel(i, j, Color.Red);
                            if (m == 8)
                            {
                                XunZhaoYes++;
                                if (XunZhaoYes < 10) bmp.SetPixel(i, j, Color.Red);
                            }
                        }
                    }
                }
            }
            bmp.Save("d:\\tiaotemo.png");
            MessageBox.Show(XunZhaoYes.ToString());
        }

        /// <summary>
        /// 获取小人的位置
        /// </summary>
        /// <param name="bmp"></param>
        /// <returns></returns>
        public Point GetXiaoRenPoint(Bitmap bmp)
        {
            Point po = new Point();
            int Num = 0;
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    Color co = bmp.GetPixel(i, j);
                    int R = co.R - XiaoRen[8, 0];
                    int G = co.G - XiaoRen[8, 1];
                    int B = co.B - XiaoRen[8, 2];

                    if ((Math.Abs((decimal)R) + Math.Abs((decimal)G) + Math.Abs((decimal)B) <= 10))
                    {
                        Num++;
                        if (Num == 10)
                        {
                            return new Point(i, j);
                        }
                    }
                }
            }
            return po;
        }

        /// <summary>
        /// 获取下一个落点的位置
        /// </summary>
        /// <param name="bmp"></param>
        /// <param name="XiaoRen"></param>
        /// <returns></returns>
        public Point GetNextTiaoPoint(Bitmap bmp, Point XiaoRen)
        {
            bool beg = false; Point NextTiaoPoint = new Point(0, 0);
            Point po01 = new Point(0, 0), po02 = new Point(0, 0);

            Color cobig = bmp.GetPixel(1, 1);
            Color coend = bmp.GetPixel(bmp.Width - 1, bmp.Height - 1);
            Color cobag = Color.FromArgb((cobig.R + coend.R) / 2, (cobig.G + coend.G) / 2, (cobig.B + coend.B) / 2);
            //30度倾斜直线
            try
            {
                bool isbenend = false;
                bool result = false;
                for (int x = 0; x < XiaoRen.X; x++)
                {
                    //150度直线
                    int y150 = (int)(0.58 * (x - XiaoRen.X) + XiaoRen.Y);

                    double xsd = (double)Math.Abs((decimal)bmp.GetPixel(x, y150).ToArgb() / cobag.ToArgb());
                    bool isBeiJing = (xsd >= 0.9 && xsd <= 1.1);

                    if (!beg && !isBeiJing)
                    {
                        continue;
                    }
                    else if (!beg && isBeiJing)
                    {
                        beg = true;
                    }
                    else if (beg && !isBeiJing)
                    {
                        if (po01.X == 0)
                        {
                            po01 = new Point(x, y150);
                            isbenend = true;
                        }
                    }
                    else if (beg && isBeiJing && isbenend)
                    {
                        if (po02.X == 0)
                        {
                            po02 = new Point(x, y150);
                            NextTiaoPoint = new Point((int)((po01.X + po02.X) / 2), (int)((po01.Y + po02.Y) / 2));
                            result = true;
                            break;
                        }
                    }
                }
                if (result) return NextTiaoPoint;

                beg = false; NextTiaoPoint = new Point(0, 0);
                po01 = new Point(0, 0); po02 = new Point(0, 0);
                isbenend = false;
                result = false;
                for (int x = XiaoRen.X + 1; x < bmp.Width; x++)
                {
                    //30度倾斜直线 
                    int y30 = (int)(-0.58 * (x - XiaoRen.X) + XiaoRen.Y);

                    double xsd = (double)Math.Abs((decimal)bmp.GetPixel(x, y30).ToArgb() / cobag.ToArgb());
                    bool isBeiJing = (xsd >= 0.9 && xsd <= 1.1);

                    if (!beg && !isBeiJing)
                    {
                        continue;
                    }
                    else if (!beg && isBeiJing)
                    {
                        beg = true;
                    }
                    else if (beg && !isBeiJing)
                    {
                        if (po01.X == 0)
                        {
                            po01 = new Point(x, y30);
                            isbenend = true;
                        }
                    }
                    else if (beg && isBeiJing && isbenend)
                    {
                        if (po02.X == 0)
                        {
                            po02 = new Point(x, y30);
                            NextTiaoPoint = new Point((int)((po01.X + po02.X + (po02.X - po01.X) / 3) / 2), (int)((po01.Y + po02.Y + (po02.Y - po01.Y) / 3) / 2));
                            result = true;
                            break;
                        }
                    }
                }
                if (result) return NextTiaoPoint;

                //开始处理两个物体靠拢的情况
                beg = false; NextTiaoPoint = new Point(0, 0);
                po01 = new Point(0, 0); po02 = new Point(0, 0);
                isbenend = false;
                result = false;
                for (int x = XiaoRen.X + 1; x < bmp.Width; x++)
                {
                    //30度倾斜直线 
                    int y30 = (int)(-0.58 * (x - XiaoRen.X) + XiaoRen.Y);

                    double xsd = (double)Math.Abs((decimal)bmp.GetPixel(x, y30).ToArgb() / cobag.ToArgb());
                    bool isBeiJing = (xsd >= 0.9 && xsd <= 1.1);

                    if (!beg && !isBeiJing)
                    {
                        continue;
                    }
                    else if (!beg && isBeiJing)
                    {
                        beg = true;
                        if (po01.X == 0)
                        {
                            po01 = new Point(x, y30);
                            NextTiaoPoint = new Point((int)((po01.X + XiaoRen.X + (po01.X - XiaoRen.X) / 3) / 2), (int)((po01.Y + XiaoRen.Y + (po01.Y - XiaoRen.Y) / 3) / 2));
                            //由于无法分辨两个盒子靠拢的时候是150度还是30度方向，所以限定盒子大小半径100以内，进行判断不同像素手机可能不同
                            if (x - XiaoRen.X <= 200) result = false;
                            else result = true;
                            isbenend = true;
                            break;
                        }
                    }
                }
                if (result) return NextTiaoPoint;

                beg = false; NextTiaoPoint = new Point(0, 0);
                po01 = new Point(0, 0); po02 = new Point(0, 0);
                isbenend = false;
                result = false;
                for (int x = XiaoRen.X - 1; x > 0; x--)
                {
                    //150度倾斜直线 
                    int y150 = (int)(0.58 * (x - XiaoRen.X) + XiaoRen.Y);

                    double xsd = (double)Math.Abs((decimal)bmp.GetPixel(x, y150).ToArgb() / cobag.ToArgb());
                    bool isBeiJing = (xsd >= 0.9 && xsd <= 1.1);

                    if (!beg && !isBeiJing)
                    {
                        continue;
                    }
                    else if (!beg && isBeiJing)
                    {
                        beg = true;
                        if (po01.X == 0)
                        {
                            po01 = new Point(x, y150);
                            NextTiaoPoint = new Point((int)((po01.X + XiaoRen.X + (po01.X - XiaoRen.X) / 3) / 2), (int)((po01.Y + XiaoRen.Y + (po01.Y - XiaoRen.Y) / 3) / 2));
                            isbenend = true;
                            result = true;
                            break;
                        }
                    }
                }
                return NextTiaoPoint;

            }
            catch (Exception ex)
            {
                return NextTiaoPoint;
            }
        }

        /// <summary>
        /// 得到两点之间的距离
        /// </summary>
        /// <param name="startPoint"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        public double GetDistance(Point startPoint, Point endPoint)
        {
            int x = System.Math.Abs(endPoint.X - startPoint.X);
            int y = System.Math.Abs(endPoint.Y - startPoint.Y);
            return Math.Sqrt(x * x + y * y);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string filePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Images\\tiaoyitiao" + textBox1.Text.Trim() + ".png";


            Rectangle CatchRect = new Rectangle(new Point(30, 70), new Size(300, 580));//保存矩形

            Bitmap bmp = new Bitmap(filePath); //GetBmpFromScreen(CatchRect); //  
            bmp.Save(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Images\\jieping.png");
            Point XiaoRen = GetXiaoRenPoint(bmp);
            Point NextTiao = GetNextTiaoPoint(bmp, XiaoRen);

            //bmp.SetPixel(XiaoRen.X, XiaoRen.Y, Color.Red);
            //30度 0.58   150度 -0.58
            Graphics g = Graphics.FromImage(bmp);

            g.DrawLine(new Pen(Color.Red), XiaoRen, NextTiao);


            //y－XiaoRen.y=0.58（x－XiaoRen.x） 30度
            int y30 = (int)(-0.58 * (bmp.Width - 1 - XiaoRen.X) + XiaoRen.Y);
            //g.DrawLine(new Pen(Color.Red), XiaoRen, new Point(bmp.Width - 1, y30));

            int y150 = (int)(0.58 * (1 - XiaoRen.X) + XiaoRen.Y);
            //g.DrawLine(new Pen(Color.Red), XiaoRen, new Point(1, y150));

            bmp.Save(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Images\\tiaoyitiao.png");
            //pictureBox1.Width = bmp.Width;
            //pictureBox1.Height = bmp.Height;

            //pictureBox1.Image = bmp;
            double line = GetDistance(XiaoRen, NextTiao);
            timeout = (int)(line / 225 * 1000); 
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string strNum = label1.Text;
            int intNum = 0;
            int.TryParse(strNum, out intNum);
            label1.Text = (++intNum).ToString();

            label2.Text = "X:" + Control.MousePosition.X + "Y:" + Control.MousePosition.Y;
        }
        BackgroundWorker bw = new BackgroundWorker();
        bool start = false;
        private void button3_Click(object sender, EventArgs e)
        {
            if (start) return;
            start = true;
            bw.DoWork += bw_DoWork;
            bw.WorkerSupportsCancellation = true;
            bw.RunWorkerCompleted += bw_RunWorkerCompleted;
            bw.RunWorkerAsync();
        }

        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            start = false;
        }

        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i < 1; )
            {
                Thread.Sleep(2000);
                string filePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Images\\tiaoyitiao" + textBox1.Text.Trim() + ".png";


                Rectangle CatchRect = new Rectangle(new Point(30, 70), new Size(300, 580));//保存矩形

                Bitmap bmp = GetBmpFromScreen(CatchRect); //  new Bitmap(filePath); //
                bmp.Save(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Images\\jieping.png");
                Point XiaoRen = GetXiaoRenPoint(bmp);
                Point NextTiao = GetNextTiaoPoint(bmp, XiaoRen);

                //bmp.SetPixel(XiaoRen.X, XiaoRen.Y, Color.Red);
                //30度 0.58   150度 -0.58
                Graphics g = Graphics.FromImage(bmp);

                g.DrawLine(new Pen(Color.Red), XiaoRen, NextTiao);


                //y－XiaoRen.y=0.58（x－XiaoRen.x） 30度
                int y30 = (int)(-0.58 * (bmp.Width - 1 - XiaoRen.X) + XiaoRen.Y);
                //g.DrawLine(new Pen(Color.Red), XiaoRen, new Point(bmp.Width - 1, y30));

                int y150 = (int)(0.58 * (1 - XiaoRen.X) + XiaoRen.Y);
                //g.DrawLine(new Pen(Color.Red), XiaoRen, new Point(1, y150));

                bmp.Save(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Images\\tiaoyitiao.png");
                //pictureBox1.Width = bmp.Width;
                //pictureBox1.Height = bmp.Height;

                //pictureBox1.Image = bmp;
                double line = GetDistance(XiaoRen, NextTiao);
                timeout = (int)(line / 225 * 1000);

                //设置鼠标的坐标
                SetCursorPos(50, 100);

                //这里模拟的是一个鼠标双击事件
                mouse_event(MouseEventFlags.LeftDown, 50, 100, 0, IntPtr.Zero);
                Thread.Sleep(timeout);
                mouse_event(MouseEventFlags.LeftUp, 50, 100, 0, IntPtr.Zero);
                i++;
                //mouse_event((int)(MouseEventFlags.LeftUp | MouseEventFlags.Absolute), 72, 40, 0, IntPtr.Zero);
                //mouse_event((int)(MouseEventFlags.LeftDown | MouseEventFlags.Absolute), 72, 40, 0, IntPtr.Zero);
            }
            bw.CancelAsync();
        }
        int timeout = 0;

        [DllImport("User32")]
        public extern static void mouse_event(MouseEventFlags dwFlags, int dx, int dy, int dwData, IntPtr dwExtraInfo);
        [DllImport("User32")]
        public extern static void SetCursorPos(int x, int y);
        [DllImport("User32")]
        public extern static bool GetCursorPos(out POINT p);
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;
        }
        public enum MouseEventFlags
        {
            Move = 0x0001, //移动鼠标
            LeftDown = 0x0002,//模拟鼠标左键按下
            LeftUp = 0x0004,//模拟鼠标左键抬起
            RightDown = 0x0008,//鼠标右键按下
            RightUp = 0x0010,//鼠标右键抬起
            MiddleDown = 0x0020,//鼠标中键按下 
            MiddleUp = 0x0040,//中键抬起
            Wheel = 0x0800,
            Absolute = 0x8000//标示是否采用绝对坐标
        }
    }
}
