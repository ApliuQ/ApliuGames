using Random_1_n_Not.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Random_1_n_Not
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //绑定图片资源
        Bitmap[] Bip=new Bitmap[8];

        private void buttonReset_Click(object sender, EventArgs e)
        {
            int Num = 16;
            int[] Random = new int[Num];
            Random = Random_1_n(Num);
            textBox1.Text = "";
            for (int i = 0; i < Random.Length; i++)
            {
                textBox1.Text += Random[i].ToString() + "-";
            }
            //textBox1.Text += Environment.NewLine;

            //重新显示界面
            foreach (Control control in this.Controls)
            {
                if (control is Button)
                {
                    ((Button)control).Visible=true;
                }
            }

            //绑定图片资源
            //Resources.ResourceManager.GetObject("");
            Bip[0] = Random_1_n_Not.Properties.Resources.aim;
            Bip[1] = Random_1_n_Not.Properties.Resources.behance;
            Bip[2] = Random_1_n_Not.Properties.Resources.dribbble;
            Bip[3] = Random_1_n_Not.Properties.Resources.feed;
            Bip[4] = Random_1_n_Not.Properties.Resources.google;
            Bip[5] = Random_1_n_Not.Properties.Resources.mail;
            Bip[6] = Random_1_n_Not.Properties.Resources.rdio;
            Bip[7] = Random_1_n_Not.Properties.Resources.spotify;
            

            //为Button赋值
            for (int i = 0, j = 0; i < this.Controls.Count; i++)
            {
                if (Controls[i] is Button && Controls[i] != button1)
                {
                    ((Button)Controls[i]).Text = Random[Num-1-j].ToString();

                    int BipId = Random[Num - 1 - j];
                    if (BipId > 7) BipId = 15 - BipId;
                    ((Button)Controls[i]).BackgroundImage = Bip[BipId];
                    ((Button)Controls[i]).BackgroundImageLayout = ImageLayout.Center;
                    ((Button)Controls[i]).Font = new Font("宋体", 1F);
                    ((Button)Controls[i]).ForeColor = SystemColors.Control;
                    //this.button1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    j++;
                }
            }

            //更新二维数组
            VirtualAll = Transform(Random);

            foreach (var VirItem in VirtualAll)
            {
                textBox1.Text +="-"+ VirItem.ToString();
            }
        }

        private int[,] Transform(int[] ArrNO1)
        {
            int len = (int)Math.Sqrt(ArrNO1.Length);
            int[,] ArrNO2 = new int[len, len];
            for (int i = 0, j = -1, k = 0; i < ArrNO1.Length; i++, k++)
            {
                if (i % len==0)
                {
                    j = j + 1;
                    k = 0;
                }
                ArrNO2[j, k] = ArrNO1[i];
            }
            return ArrNO2;
        }

        //ran.Next(100,999);
        private int[] Random_1_n(int n)
        {
            int[] Random = new int[n];
            List<int> list = new List<int>();
            Random ran = new Random();

            for (int i = 0; i < n; i++) list.Add(i);

            for (int j = 0; j < Random.Length; j++)
            {
                int id = ran.Next(0, list.Count);
                Random[j] = list[id];
                list.RemoveAt(id);
            }
            return Random;
        }

        //标记寻找折点
        Point LastMake, CurrentMake;
        Point MakeNO1 = new Point(-2, -2), MakeNO2 = new Point(-2, -2);

        Button LastButton = null;
        int[,] VirtualAll = new int[,] 
            { { 12, 13,  6, 2 }, 
            {    1, 15, 10, 8 }, 
            {    7,  4,  5, 9 }, 
            {   11, 14,  0, 3 } };


        private void buttonAll_Click(object sender, EventArgs e)
        {
            if (LastButton == null) LastButton = sender as Button;
            else
            {
                Button CurrentButton = sender as Button;
                if (Run(int.Parse(LastButton.Text), int.Parse(CurrentButton.Text)))
                {
                    Graphics GraForm = pictureBox1.CreateGraphics(); //创建画板,这里的画板是由Form提供的.
                    Pen pen = new Pen(Color.Blue, 2);//定义了一个蓝色,宽度为的画笔

                    if (MakeNO2.X == -2)
                    {
                        if (MakeNO1.X == -2) GraForm.DrawLine(pen, (int)27.75 * (LastMake.Y * 2 + 1), (int)22.2 * (LastMake.X * 2 + 1), (int)27.75 * (CurrentMake.Y * 2 + 1), (int)22.2 * (CurrentMake.X * 2 + 1));
                        else
                        {
                            GraForm.DrawLine(pen, (int)27.75 * (LastMake.Y * 2 + 1), (int)22.2 * (LastMake.X * 2 + 1), (int)27.75 * (MakeNO1.Y * 2 + 1), (int)22.2 * (MakeNO1.X * 2 + 1));
                            GraForm.DrawLine(pen, (int)27.75 * (MakeNO1.Y * 2 + 1), (int)22.2 * (MakeNO1.X * 2 + 1), (int)27.75 * (CurrentMake.Y * 2 + 1), (int)22.2 * (CurrentMake.X * 2 + 1));
                        }
                    }
                    else
                    {
                        GraForm.DrawLine(pen, (int)27.75 * (LastMake.Y * 2 + 1), (int)22.2 * (LastMake.X * 2 + 1), (int)27.75 * (MakeNO2.Y * 2 + 1), (int)22.2 * (MakeNO2.X * 2 + 1));
                        GraForm.DrawLine(pen, (int)27.75 * (MakeNO1.Y * 2 + 1), (int)22.2 * (MakeNO1.X * 2 + 1), (int)27.75 * (MakeNO2.Y * 2 + 1), (int)22.2 * (MakeNO2.X * 2 + 1));
                        GraForm.DrawLine(pen, (int)27.75 * (MakeNO1.Y * 2 + 1), (int)22.2 * (MakeNO1.X * 2 + 1), (int)27.75 * (CurrentMake.Y * 2 + 1), (int)22.2 * (CurrentMake.X * 2 + 1));
                    }
                    Thread.Sleep(500);
                    GraForm.Clear(pictureBox1.BackColor);
                    LastButton.Visible = false;
                    CurrentButton.Visible = false;
                    MakeNO1 = new Point(-2, -2);
                    MakeNO2 = new Point(-2, -2);
                }
                button1.Focus();
                LastButton = null;
            }
        }

        private bool Run(int IntLast,int IntCurrent)
        {
            if (IntLast + IntCurrent != 15 || IntLast == IntCurrent) return false;
            Point PointLast = new Point(-2, -2), PointCurrent = new Point(-2, -2);
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (IntLast==VirtualAll[i,j]) PointLast=new Point(i,j);
                    if (IntCurrent == VirtualAll[i, j]) PointCurrent = new Point(i, j);
                }
            }
            LastMake = PointLast;
            CurrentMake = PointCurrent;
            #region NO.1-直线相连
            if (PointLast.X == PointCurrent.X || PointLast.Y == PointCurrent.Y)
            {
                if (LineTORF(PointLast, PointCurrent))
                {
                    VirtualAll[PointLast.X, PointLast.Y] = -1;
                    VirtualAll[PointCurrent.X, PointCurrent.Y] = -1;
                    return true;
                }
            }
            #endregion

            #region NO.2-折线一次相连
            if (FoldOneTORF(PointLast, PointCurrent))
            {
                VirtualAll[PointLast.X, PointLast.Y] = -1;
                VirtualAll[PointCurrent.X, PointCurrent.Y] = -1;
                return true;
            }
            #endregion

            #region NO.3-折线二次相连
            if (FoldTwoTORF(PointLast, PointCurrent))
            {
                VirtualAll[PointLast.X, PointLast.Y] = -1;
                VirtualAll[PointCurrent.X, PointCurrent.Y] = -1;
                return true;
            }
            #endregion


            return false;
        }

        #region NO.1-直线相连T or F
        private bool LineTORF(Point NO1,Point NO2)
        {
            Point FinalNO1 = NO1;
            //MessageBox.Show("COME ON");
            for (int Xup = 0; Xup < 3; Xup++)
            {
                NO1.X++;
                if (NO1.X > 3) break;
                if (NO1.Equals(NO2)) return true;
                if (VirtualAll[NO1.X , NO1.Y] != -1) break;
            } NO1 = FinalNO1;

            for (int Xdown = 0; Xdown < 3; Xdown++)
            {
                NO1.X--;
                if (NO1.X < 0) break;
                if (NO1.Equals(NO2)) return true;
                if (VirtualAll[NO1.X , NO1.Y] != -1) break;
            } NO1 = FinalNO1;

            for (int Yup = 0; Yup < 3; Yup++)
            {
                NO1.Y++;
                if (NO1.Y > 3) break;
                if (NO1.Equals(NO2)) return true;
                if (VirtualAll[NO1.X, NO1.Y] != -1) break;
            } NO1 = FinalNO1;

            for (int Ydown = 0; Ydown < 3; Ydown++)
            {
                NO1.Y--;
                if (NO1.Y < 0) break;
                if (NO1.Equals(NO2)) return true;
                if (VirtualAll[NO1.X, NO1.Y] != -1) break;
            }
            return false;
        }
        #endregion
        /*
         * 忘记了，每次的对X,Y的操作，忘记初始化原本的点！！！
         */
        #region NO.2-折线一次相连T or F
        private bool FoldOneTORF(Point NO1, Point NO2)
        {
            Point FinalNO1 = NO1;
            for (int Xup = 0; Xup < 3; Xup++)
            {
                NO1.X++;
                if (NO1.X > 3) break;
                if (VirtualAll[NO1.X, NO1.Y] == -1)
                {
                    if (NO1.X != NO2.X && NO1.Y != NO2.Y) continue;
                    if (LineTORF(new Point(NO1.X, NO1.Y), NO2) == true)
                    {
                        MakeNO1 = NO1;
                        label1.Text = "Xup: X=" + NO1.X.ToString() + " ,Y=" + NO1.Y.ToString();
                        return true;
                    }
                }
                else break;
            } NO1 = FinalNO1;

            for (int Xdown = 0; Xdown < 3; Xdown++)
            {
                NO1.X--;
                if (NO1.X < 0) break;
                if (VirtualAll[NO1.X, NO1.Y] == -1)
                {
                    if (NO1.X != NO2.X && NO1.Y != NO2.Y) continue;
                    if (LineTORF(new Point(NO1.X, NO1.Y), NO2) == true)
                    {
                        MakeNO1 = NO1;
                        label1.Text = "Xdown: X=" + NO1.X.ToString() + " ,Y=" + NO1.Y.ToString();
                        return true;
                    }
                }
                else break;
            } NO1 = FinalNO1;

            for (int Yup = 0; Yup < 3; Yup++)
            {
                NO1.Y++;
                if (NO1.Y > 3) break;
                if (VirtualAll[NO1.X, NO1.Y] == -1)
                {
                    if (NO1.X != NO2.X && NO1.Y != NO2.Y) continue;
                    if (LineTORF(new Point(NO1.X, NO1.Y), NO2) == true)
                    {
                        MakeNO1 = NO1;
                        label1.Text = "Yup: X=" + NO1.X.ToString() + " ,Y=" + NO1.Y.ToString();
                        return true;
                    }
                }
                else break;
            } NO1 = FinalNO1;

            for (int Ydown = 0; Ydown < 3; Ydown++)
            {
                NO1.Y--;
                if (NO1.Y < 0) break;
                if (VirtualAll[NO1.X, NO1.Y] == -1)
                {
                    if (NO1.X != NO2.X && NO1.Y != NO2.Y) continue;
                    if (LineTORF(new Point(NO1.X, NO1.Y), NO2) == true)
                    {
                        MakeNO1 = NO1;
                        label1.Text = "Ydown: X=" + NO1.X.ToString() + " ,Y=" + NO1.Y.ToString();
                        return true;
                    }
                }
                else break;
            }
            return false;
        }
        #endregion

        #region NO.3-折线二次相连T or F
        private bool FoldTwoTORF(Point NO1, Point NO2)
        {
            Point FinalNO1 = NO1;
            for (int Xup = 0; Xup < 3; Xup++)
            {
                NO1.X++;
                if (NO1.X > 3) break;
                if (VirtualAll[NO1.X, NO1.Y] == -1)
                {
                    if (FoldOneTORF(new Point(NO1.X, NO1.Y), NO2) == true)
                    {
                        MakeNO2 = NO1;
                        label2.Text = "Xup: X=" + NO1.X.ToString() + " ,Y=" + NO1.Y.ToString();
                        return true;
                    }
                }
                else break;
            } NO1 = FinalNO1;

            for (int Xdown = 0; Xdown < 3; Xdown++)
            {
                NO1.X--;
                if (NO1.X < 0) break;
                if (VirtualAll[NO1.X, NO1.Y] == -1)
                {
                    if (FoldOneTORF(new Point(NO1.X, NO1.Y), NO2) == true)
                    {
                        MakeNO2 = NO1;
                        label2.Text = "Xdown: X=" + NO1.X.ToString() + " ,Y=" + NO1.Y.ToString();
                        return true;
                    }
                }
                else break;
            } NO1 = FinalNO1;

            for (int Yup = 0; Yup < 3; Yup++)
            {
                NO1.Y++;
                if (NO1.Y > 3) break;
                if (VirtualAll[NO1.X, NO1.Y] == -1)
                {
                    if (FoldOneTORF(new Point(NO1.X, NO1.Y), NO2) == true)
                    {
                        MakeNO2 = NO1;
                        label2.Text = "Yup: X=" + NO1.X.ToString() + " ,Y=" + NO1.Y.ToString();
                        return true;
                    }
                }
                else break;
            } NO1 = FinalNO1;

            for (int Ydown = 0; Ydown < 3; Ydown++)
            {
                NO1.Y--;
                if (NO1.Y < 0) break;
                if (VirtualAll[NO1.X, NO1.Y] == -1)
                {
                    if (FoldOneTORF(new Point(NO1.X, NO1.Y), NO2) == true)
                    {
                        MakeNO2 = NO1;
                        label2.Text = "Ydown: X=" + NO1.X.ToString() + " ,Y=" + NO1.Y.ToString();
                        return true;
                    }
                }
                else break;
            }
            return false;
        }
        #endregion

        //private void picBackground_Paint(object sender, PaintEventArgs e)
        //{
        //    foreach (Control C in this.Controls)
        //    {
        //        if (C is Label)
        //        {

        //            Label L = (Label)C;

        //            L.Visible = false;

        //            //设置绘制文字的格式  
        //            StringFormat strFmt = new System.Drawing.StringFormat();
        //            strFmt.Alignment = StringAlignment.Center; //文本垂直居中  
        //            strFmt.LineAlignment = StringAlignment.Center; //文本水平居中  
        //            e.Graphics.DrawString(L.Text, this.Font, new SolidBrush(this.ForeColor), new RectangleF(L.Left - pictureBox1.Left, L.Top - pictureBox1.Top, L.Width, L.Height), strFmt);

        //        }
        //        else if (C is PictureBox)
        //        {
        //            PictureBox L = (PictureBox)C;
        //            if (!L.Name.Equals("picBackground"))
        //            {
        //                L.Visible = false;
        //                ImageAttributes attrib = new ImageAttributes();
        //                //Bitmap img = new Bitmap(L.Image);
        //                Color color = Color.Transparent;
        //                attrib.SetColorKey(color, color);
        //                e.Graphics.DrawImage(L.Image, new Rectangle(L.Left - pictureBox1.Left, L.Top - pictureBox1.Top, L.Width, L.Height), 0, 0, L.Image.Width, L.Image.Height, GraphicsUnit.Pixel, attrib);
        //            }
        //        }
        //    }
        //}
        private void Form1_Load(object sender, EventArgs e)
        {
            //button6.Visible = false;
            //btn.BringToFront();//将控件放置所有控件最前端  
            pictureBox1.SendToBack();//将控件放置所有控件最底端 
            buttonReset_Click(button1, null);
        }

        private void TextOK()
        {
            int buttontextisint = -1;
            int[,] TextAry = VirtualAll.Clone() as int[,];//第一种复制方法，克隆函数
            //VirtualAll.CopyTo(TextAry, VirtualAll.Length);//第二种复制方法，复制函数
            //Array.Copy(VirtualAll, TextAry, VirtualAll.Length);//第三种复制方法，数组的静态复制函数
            //TextAry=VirtualAll;则是引用其地址，而不是复制。当后者改变，前者一起改变。引用类型。
            bool TextIsOk = true;

            for (int k = 0; k < this.Controls.Count; k++)
            {
                for (int i = 0; i < this.Controls.Count; i++)
                {
                    if (Controls[i] is Button && Controls[i].Visible && int.TryParse(Controls[i].Text, out buttontextisint) && buttontextisint >= 0 && buttontextisint < 16)
                    {
                        for (int j = 0; j < this.Controls.Count; j++)
                        {
                            if (Controls[j] is Button && Controls[j].Visible && int.TryParse(Controls[j].Text, out buttontextisint) && buttontextisint >= 0 && buttontextisint < 16)
                            {
                                if (TextRun((Button)Controls[i], (Button)Controls[j])) ;
                                //buttonAll_Click(Controls[i], null);
                                //buttonAll_Click(Controls[j], null);
                            }
                        }
                    }
                }
            }
            foreach (Control item in Controls)
            {
                if (item is Button && int.TryParse(item.Text, out buttontextisint) && buttontextisint >= 0 && buttontextisint < 16)
                {
                    if (item.Visible) TextIsOk = false;
                }
            }

            MessageBox.Show(TextIsOk.ToString());
            if (TextIsOk)
            {
                foreach (Control item in Controls)
                {
                    if (item is Button && int.TryParse(item.Text, out buttontextisint) && buttontextisint >= 0 && buttontextisint < 16)
                    {
                        item.Visible = true;
                    }
                }
                VirtualAll = TextAry;
            }
            else
            {
                buttonReset_Click(button1, null);
            }

            //foreach (Control itemlast in Controls)
            //{
            //    if (itemlast is Button && int.TryParse(itemlast.Text, out buttontextisint) && buttontextisint >= 0 && buttontextisint < 16)
            //    {
            //        foreach (Control itemcurrent in Controls)
            //        {
            //            if (itemcurrent is Button && int.TryParse(itemcurrent.Text, out buttontextisint) && buttontextisint >= 0 && buttontextisint < 16)
            //            {
            //                //if (TextRun((Button)itemlast, (Button)itemcurrent)) return;
            //                buttonAll_Click(itemlast, null);
            //                buttonAll_Click(itemcurrent, null);
            //            }
            //        }
            //    }
            //}
        }
        private bool TextRun(Button last, Button current)
        {
            LastButton = last;
            Button CurrentButton = current;
            if (Run(int.Parse(LastButton.Text), int.Parse(CurrentButton.Text)))
            {
                Graphics GraForm = pictureBox1.CreateGraphics(); //创建画板,这里的画板是由Form提供的.
                Pen pen = new Pen(Color.Blue, 2);//定义了一个蓝色,宽度为的画笔

                if (MakeNO2.X == -2)
                {
                    if (MakeNO1.X == -2) GraForm.DrawLine(pen, (int)27.75 * (LastMake.Y * 2 + 1), (int)22.2 * (LastMake.X * 2 + 1), (int)27.75 * (CurrentMake.Y * 2 + 1), (int)22.2 * (CurrentMake.X * 2 + 1));
                    else
                    {
                        GraForm.DrawLine(pen, (int)27.75 * (LastMake.Y * 2 + 1), (int)22.2 * (LastMake.X * 2 + 1), (int)27.75 * (MakeNO1.Y * 2 + 1), (int)22.2 * (MakeNO1.X * 2 + 1));
                        GraForm.DrawLine(pen, (int)27.75 * (MakeNO1.Y * 2 + 1), (int)22.2 * (MakeNO1.X * 2 + 1), (int)27.75 * (CurrentMake.Y * 2 + 1), (int)22.2 * (CurrentMake.X * 2 + 1));
                    }
                }
                else
                {
                    GraForm.DrawLine(pen, (int)27.75 * (LastMake.Y * 2 + 1), (int)22.2 * (LastMake.X * 2 + 1), (int)27.75 * (MakeNO2.Y * 2 + 1), (int)22.2 * (MakeNO2.X * 2 + 1));
                    GraForm.DrawLine(pen, (int)27.75 * (MakeNO1.Y * 2 + 1), (int)22.2 * (MakeNO1.X * 2 + 1), (int)27.75 * (MakeNO2.Y * 2 + 1), (int)22.2 * (MakeNO2.X * 2 + 1));
                    GraForm.DrawLine(pen, (int)27.75 * (MakeNO1.Y * 2 + 1), (int)22.2 * (MakeNO1.X * 2 + 1), (int)27.75 * (CurrentMake.Y * 2 + 1), (int)22.2 * (CurrentMake.X * 2 + 1));
                }
                int intSleepTime;
                if (!int.TryParse(SleepTime.Text, out intSleepTime) || intSleepTime<100) intSleepTime = 800;
                Thread.Sleep(intSleepTime);
                LastButton.Visible = false;
                CurrentButton.Visible = false;
                GraForm.Clear(pictureBox1.BackColor);
                MakeNO1 = new Point(-2, -2);
                MakeNO2 = new Point(-2, -2);
                return true;
            }
            button1.Focus();
            LastButton = null;
            return false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.CheckState == CheckState.Checked)
            {
                TextOK();
            }
        }
    }
}
