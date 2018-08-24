using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tanchishe
{
    enum Direction
    {
        Up = 1,
        Down = 2,
        Left = 3,
        Right = 4,
    }

    public partial class Form1 : Form
    {
        const int n = 20;
        Point Food = new Point(0, 0);
        List<Point> Snake = new List<Point> { };
        PictureBox[] PicBox = new PictureBox[n * n];
        Direction CurrentDir = Direction.Right;
        KeyboardHook KeyDown;
        int Score = 0;
        bool snakeisrun = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            KeyDown = new KeyboardHook();
            KeyDown.SetHook();
            KeyDown.OnKeyDownEvent += kh_OnKeyDownEvent;

            for (int i = 0; i < n * n; i++)
            {
                PicBox[i] = new PictureBox();
                PicBox[i].Size = new Size(15, 15);
                PicBox[i].BackColor = Color.Black;
                //Btn[i].Enabled = false;
                PicBox[i].Margin = new Padding(0); //new Padding(left, top, right, bottom); 
                MainPanel.Controls.Add(PicBox[i]);
            }

            //窗体大小不可改变
            this.FormBorderStyle =FormBorderStyle.FixedDialog;
            this.MinimizeBox = false;//去掉最小化按钮
            this.MaximizeBox = false;//去掉最大化按钮

            //窗体图标
            this.Icon = tanchishe.Properties.Resources.bird1934;
        }

        void kh_OnKeyDownEvent(object sender, KeyEventArgs e)
        {
            //if (e.KeyData == (Keys.S | Keys.Control)) { this.Show(); }//Ctrl+S显示窗口
            //if (e.KeyData == (Keys.H | Keys.Control)) { this.Hide(); }//Ctrl+H隐藏窗口
            //if (e.KeyData == (Keys.C | Keys.Control)) { this.Close(); }//Ctrl+C 关闭窗口 
            //if (e.KeyData == (Keys.A | Keys.Control | Keys.Alt)) { this.Text = "你发现了什么？"; }//Ctrl+Alt+A
            switch (e.KeyData)
            {
                case Keys.Up:
                    CurrentDir = Direction.Up;
                    break;
                case Keys.Down:
                    CurrentDir = Direction.Down;
                    break;
                case Keys.Left:
                    CurrentDir = Direction.Left;
                    break;
                case Keys.Right:
                    CurrentDir = Direction.Right;
                    break;
                default: break;
            }

        }

        private void Start_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < n * n; i++)
            {
                PicBox[i].BackColor = Color.Black;
            }

            //food point
            GreadFood();

            //snake list
            Snake.Clear();
            Snake.Add(new Point(10,10));
            SnakeColor(Color.Red);

            //speed
            int speed = 100 * (comboBox1.SelectedIndex + 1);
            Timer.Interval = speed;
            Timer.Start();

            //score
            Score = 0;

            //禁用comboBox1
            comboBox1.Enabled = false;

            snakeisrun = true;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            SnakeRun();
        }

        private void SnakeRun()
        {
            Point SnakeEnd = Snake[Snake.Count-1];
            SnakeColor(Color.Black);
            switch (CurrentDir)
            {
                case Direction.Up:
                    for (int i = Snake.Count-1; i > 0; i--)
                    {
                        Snake[i] = Snake[i - 1];
                    }
                    Snake[0] = new Point(Snake[0].X - 1, Snake[0].Y);
                    break;

                case Direction.Down:
                    for (int i = Snake.Count - 1; i > 0; i--)
                    {
                        Snake[i] = Snake[i - 1];
                    }
                    Snake[0] = new Point(Snake[0].X + 1, Snake[0].Y);
                    break;

                case Direction.Left:
                    for (int i = Snake.Count - 1; i > 0; i--)
                    {
                        Snake[i] = Snake[i - 1];
                    }
                    Snake[0] = new Point(Snake[0].X, Snake[0].Y - 1);
                    break;

                case Direction.Right:
                    for (int i = Snake.Count - 1; i > 0; i--)
                    {
                        Snake[i] = Snake[i - 1];
                    }
                    Snake[0] = new Point(Snake[0].X, Snake[0].Y + 1);
                    break;
                default: break;
            }

            if (Snake[0].Equals(Food))
            {
                GreadFood(); 
                Score++;
                labelscore.Text = Score.ToString();
                Snake.Add(SnakeEnd);
            }
            SnakeColor(Color.Red);
        }

        private void SnakeColor(Color CurCol)
        {
            //可以在此判断，是否可以撞自己的身体
            if (Snake[0].X >= 20 || Snake[0].Y >= 20 || Snake[0].X < 0 || Snake[0].Y < 0)
            {
                Timer.Stop();
                MessageBox.Show("You are failer");
                comboBox1.Enabled = true;
                snakeisrun = false;
                return;
            }
            foreach (Point item in Snake)
            {
                PicBox[20 * item.X + item.Y].BackColor = CurCol;
            }
            //head  circle
            //int head = 20 * Snake[0].X + Snake[0].Y;
            //PicBox[head].BackColor = Color.Black;
            //Graphics Gra = PicBox[head].CreateGraphics();
            //SolidBrush Bru = new SolidBrush(CurCol);
            //Gra.FillEllipse(Bru, 0, 0, 10, 10);//在画板上画椭圆,起始坐标为(10,10),外接矩形的宽为,高为
            //System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);//画刷
            //formGraphics.FillEllipse(myBrush, new Rectangle(0, 0, 100, 200));//画实心椭圆
        }

        private void GreadFood()
        {
            do{
                Random CurrentRan = new Random();
                int IntX = CurrentRan.Next(0, n);
                int IntY = CurrentRan.Next(0, n);
                Food = new Point(IntX, IntY);
            } while (Snake_Food());
            
            PicBox[20 * Food.X + Food.Y].BackColor = Color.Red;
        }

        private bool Snake_Food()
        {
            foreach (var item in Snake)
            {
                if (item.Equals(Food)) return true;
            }
            return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!snakeisrun) return;
            if (Timer.Enabled){
                Timer.Stop();
                button1.Text = "|>";
            }
            else{
                Timer.Start();
                button1.Text = "||";
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            KeyDown.UnHook();
        }
    }
}
