using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace xiaoxiaole
{
    public partial class Form1 : Form
    {
        const int M = 10, N = 10;
        ShapeButton[] SpeBtu = new ShapeButton[M * N];
        List<Color> AllColor = new List<Color>() { Color.Red, Color.Black, Color.Blue, Color.Gold };
        Button LastBtn = null;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < SpeBtu.Length; i++)
            {
                SpeBtu[i] = new ShapeButton();
                SpeBtu[i].BackColor = RandomColor();
                SpeBtu[i].Click += new System.EventHandler(All_Click);
                GamePanel.Controls.Add(SpeBtu[i]);
            }
        }

        //为了快速随机数字不重复，设立随机种子
        static int GetRandomSeed()
        {
            byte[] bytes = new byte[4];
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }

        private Color RandomColor()
        {
            Random Ran = new Random(GetRandomSeed());
            int RanColor = Ran.Next(0, AllColor.Count);
            return AllColor[RanColor];
        }

        private void start_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < SpeBtu.Length; i++)
            {
                Color NextColor = RandomColor();
                SpeBtu[i].BackColor = NextColor;
            }
            for (int i = 0; i < 10; i++)
            {
                Random Ran = new Random(GetRandomSeed());
                int RanColor = Ran.Next(0, 10);
                textBox1.Text += RanColor.ToString();
            }
        }

        private void All_Click(object sender, EventArgs e)
        {
            if (LastBtn == null)
            {
                LastBtn = sender as Button;
                return;
            }
            Button CurrentBtn = sender as Button;
            Color ChangeColor = LastBtn.BackColor;
            LastBtn.BackColor = CurrentBtn.BackColor;
            CurrentBtn.BackColor = ChangeColor;

            LastBtn = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text += "OK";
        }
    }


    public partial class ShapeButton : Button
    {
        public ShapeButton() { this.Circle = true; }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        bool flag;
        [Description(" 获取或设置按钮椭圆效果。"), DefaultValue(false)]
        public bool Circle
        {
            set
            {
                flag = value;
                GraphicsPath gp = new GraphicsPath();
                this.Size = new Size(30,30);
                gp.AddEllipse(this.ClientRectangle);//以按钮矩形为内接画圆形
                this.Margin = new Padding(0);//控件之间的距离
                this.Region = new Region(gp);
                FlatAppearance.BorderSize = 0;//去掉边框
                FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                BackColor = SystemColors.Control;//System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));//背景颜色
                this.Invalidate();
            }
            get { return flag; }
        }
    }
}