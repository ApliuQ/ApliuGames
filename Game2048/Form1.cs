using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace game2048
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
        const int n = 4;
        KeyboardHook KeyBH;
        PictureBox[] PicBox = new PictureBox[n * n];
        Bitmap[] BitMap=new Bitmap[14];
        int score = 0;
        bool congratulations = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            #region 布局添加实例
            //TableLayoutPanel TabelLP = new TableLayoutPanel();
            ////TabelLP.Name = "tableLayoutPanel1";
            //TabelLP.RowCount = 4;
            //TabelLP.ColumnCount = 4;
            //TabelLP.Controls.Add(btn1);
            ////this.tableLayoutPanel1.Controls.Add(this.button2, 1, 0);
            #endregion

            #region 加载图片资源
            BitMap[0] = game2048.Properties.Resources.game0;
            BitMap[1] = game2048.Properties.Resources.game2;
            BitMap[2] = game2048.Properties.Resources.game4;
            BitMap[3] = game2048.Properties.Resources.game8;
            BitMap[4] = game2048.Properties.Resources.game16;
            BitMap[5] = game2048.Properties.Resources.game32;
            BitMap[6] = game2048.Properties.Resources.game64;
            BitMap[7] = game2048.Properties.Resources.game128;
            BitMap[8] = game2048.Properties.Resources.game256;
            BitMap[9] = game2048.Properties.Resources.game512;
            BitMap[10] = game2048.Properties.Resources.game1024;
            BitMap[11] = game2048.Properties.Resources.game2048;
            BitMap[12] = game2048.Properties.Resources.game4096;
            BitMap[13] = game2048.Properties.Resources.game8192;
            #endregion

            for (int i = 0; i < n * n; i++)
            {
                PicBox[i] = new PictureBox();
                PicBox[i].Size = new Size(94, 94);
                PicBox[i].BackgroundImage = BitMap[0];
                PicBox[i].BackgroundImageLayout = ImageLayout.Zoom;
                TabelLP.Controls.Add(PicBox[i]);
            }


            //窗体大小不可改变
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MinimizeBox = false;//去掉最小化按钮
            this.MaximizeBox = false;//去掉最大化按钮

            this.Icon = game2048.Properties.Resources.apple9;

            KeyBH = new KeyboardHook();
            KeyBH.SetHook();
            KeyBH.OnKeyDownEvent += KeyBH_OnKeyDownEvent;

            Random();
        }

        private void reset_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < n * n; i++) PicBox[i].BackgroundImage = BitMap[0];
            Random();
            gameon.Text = "Come On!";
            gameon.ForeColor = Color.Lime;
            congratulations = false;
        }


        void KeyBH_OnKeyDownEvent(object sender, KeyEventArgs e)
        {
            #region 按钮实例
            //if (e.KeyData == (Keys.Q)) { label1.Text += "Q"; }//Ctrl+S显示窗口
            //if (e.KeyData == (Keys.S | Keys.Control)) { this.Show(); }//Ctrl+S显示窗口
            //if (e.KeyData == (Keys.H | Keys.Control)) { this.Hide(); }//Ctrl+H隐藏窗口
            //if (e.KeyData == (Keys.C | Keys.Control)) { this.Close(); }//Ctrl+C 关闭窗口 
            //if (e.KeyData == (Keys.A | Keys.Control | Keys.Alt)) { this.Text = "你发现了什么？"; }//Ctrl+Alt+A
            #endregion

            switch (e.KeyData)
            {
                case Keys.Up: Run2048(Direction.Up);
                    break;
                case Keys.Down: Run2048(Direction.Down);
                    break;
                case Keys.Left: Run2048(Direction.Left);
                    break;
                case Keys.Right: Run2048(Direction.Right);
                    break;
                default:
                    break;
            }
        }

        private void Run2048(Direction keydown)
        {
            if (congratulations) return;

            #region  UP
            if (keydown == Direction.Up)
            {
                for (int i = n * n - 1; i >= 0; i--)
                {
                    //寻找不为空的pic
                    if (!PicBox[i].BackgroundImage.Equals(BitMap[0]))
                    {
                        if (i - 4 < 0) continue;
                        //如果上一级也不为空
                        if (!PicBox[i - 4].BackgroundImage.Equals(BitMap[0]))
                        {
                            if (PicBox[i - 4].BackgroundImage.Equals(PicBox[i].BackgroundImage))
                            {
                                //在此处可以判断是否胜利。
                                int picid = IndexOf(PicBox[i].BackgroundImage as Bitmap);
                                PicBox[i - 4].BackgroundImage = BitMap[picid + 1];
                                PicBox[i].BackgroundImage = BitMap[0];
                                if (picid + 1 == 13)
                                {
                                    congratulations = true;
                                    gameon.Text = "Congratulations！";
                                }
                            }
                        }
                        else //如果上一级为空
                        {
                            PicBox[i - 4].BackgroundImage = PicBox[i].BackgroundImage;
                            PicBox[i].BackgroundImage = BitMap[0];
                        }
                    }
                }
            }
            #endregion

            #region  DOWN
            if (keydown == Direction.Down)
            {
                for (int i = 0; i < n * n; i++)
                {
                    //寻找不为空的pic
                    if (!PicBox[i].BackgroundImage.Equals(BitMap[0]))
                    {
                        if (i + 4 > n * n - 1) continue;
                        //如果上一级也不为空
                        if (!PicBox[i + 4].BackgroundImage.Equals(BitMap[0]))
                        {
                            if (PicBox[i + 4].BackgroundImage.Equals(PicBox[i].BackgroundImage))
                            {
                                //在此处可以判断是否胜利。
                                int picid = IndexOf(PicBox[i].BackgroundImage as Bitmap);
                                PicBox[i + 4].BackgroundImage = BitMap[picid + 1];
                                PicBox[i].BackgroundImage = BitMap[0];
                                if (picid + 1 == 13)
                                {
                                    congratulations = true;
                                    gameon.Text = "Congratulations！";
                                }
                            }
                        }
                        else //如果上一级为空
                        {
                            PicBox[i + 4].BackgroundImage = PicBox[i].BackgroundImage;
                            PicBox[i].BackgroundImage = BitMap[0];
                        }
                    }
                }
            }
            #endregion

            #region  LEFT
            if (keydown == Direction.Left)
            {
                for (int i = n * n - 1; i >= 0; i = i - 4)
                {
                    //如果是靠墙的，则不作处理(其实只要判断是否等于12即可)
                    if (i == 0 || i == 4 || i == 8 || i == 12) continue;
                    //寻找不为空的pic
                    if (!PicBox[i].BackgroundImage.Equals(BitMap[0]))
                    {
                        //如果上一级也不为空
                        if (!PicBox[i - 1].BackgroundImage.Equals(BitMap[0]))
                        {
                            if (PicBox[i - 1].BackgroundImage.Equals(PicBox[i].BackgroundImage))
                            {
                                //在此处可以判断是否胜利。
                                int picid = IndexOf(PicBox[i].BackgroundImage as Bitmap);
                                PicBox[i - 1].BackgroundImage = BitMap[picid + 1];
                                PicBox[i].BackgroundImage = BitMap[0];
                                if (picid + 1 == 13)
                                {
                                    congratulations = true;
                                    gameon.Text = "Congratulations！";
                                }
                            }
                        }
                        else //如果上一级为空
                        {
                            PicBox[i - 1].BackgroundImage = PicBox[i].BackgroundImage;
                            PicBox[i].BackgroundImage = BitMap[0];
                        }
                    }
                    //判断是否到顶，到顶则前一列（其实只要判断i<=3即可）因为循环会减掉4，所以要多加4；
                    if (i == 1 || i == 2 || i == 3) i = i + 15;
                }
            }
            #endregion

            #region  RIGHT
            if (keydown == Direction.Right)
            {
                for (int i = 0; i < n * n; i = i + 4)
                {
                    //如果是靠墙的，则不作处理(其实只要判断是否等于3即可)
                    if (i == 3 || i == 7 || i == 11 || i == 15) continue;
                    //寻找不为空的pic
                    if (!PicBox[i].BackgroundImage.Equals(BitMap[0]))
                    {
                        //如果上一级也不为空
                        if (!PicBox[i + 1].BackgroundImage.Equals(BitMap[0]))
                        {
                            if (PicBox[i + 1].BackgroundImage.Equals(PicBox[i].BackgroundImage))
                            {
                                //在此处可以判断是否胜利。
                                int picid = IndexOf(PicBox[i].BackgroundImage as Bitmap);
                                PicBox[i + 1].BackgroundImage = BitMap[picid + 1];
                                PicBox[i].BackgroundImage = BitMap[0];
                                if (picid + 1 == 13)
                                {
                                    congratulations = true;
                                    gameon.Text = "Congratulations！";
                                }
                            }
                        }
                        else //如果上一级为空
                        {
                            PicBox[i + 1].BackgroundImage = PicBox[i].BackgroundImage;
                            PicBox[i].BackgroundImage = BitMap[0];
                        }
                    }
                    //判断是否到第，到顶则下一列（其实只要判断i>=12即可）因循环会自动加上4，所以得多加4；
                    if (i == 12 || i == 13 || i == 14) i = i - 15;
                }
            }
            #endregion

            //计算分数
            score = 0;
            foreach (PictureBox item in PicBox)
            {
                int idbitmap=IndexOf(item.BackgroundImage as Bitmap);
                //if (score < idbitmap) score = idbitmap;
                if(idbitmap!=0)
                    score += (int)Math.Pow(2, idbitmap);
            }
            scorelabel.Text = score.ToString();

            if (!Random())
            {
                gameon.Text = "Game Over";
                gameon.ForeColor = Color.Red;
                //MessageBox.Show("Game Over!");
            }
        }

        private int IndexOf(Bitmap newbitmap)
        {
            for (int i = 0; i < BitMap.Length; i++)
            {
                if (BitMap[i].Equals(newbitmap)) return i;
            }
            return -1;
        }

        private bool Random()
        {
            List<int> picisnull = new List<int> { };
            for (int i = 0; i < n * n; i++)
            {
                if (PicBox[i].BackgroundImage.Equals(BitMap[0])) picisnull.Add(i);
            }
            if (picisnull.Count <= 1) return false;

            Random ran = new Random();
            int last, current;
            do
            {
                last = ran.Next(0, picisnull.Count);
                current = ran.Next(0, picisnull.Count);
            } while (last == current);

            PicBox[picisnull[last]].BackgroundImage = BitMap[1];
            PicBox[picisnull[current]].BackgroundImage = BitMap[1];

            return true;
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            KeyBH.UnHook();
        }

    }
}
