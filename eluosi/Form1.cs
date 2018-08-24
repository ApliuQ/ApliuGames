using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace eluosi
{
    enum Direction
    {
        Up = 1,
        Down = 2,
        Left = 3,
        Right = 4,
    }

    enum Tetromino
    {
        S = 1,
        Z = 2,
        L = 3,
        J = 4,
        I = 5,
        O = 6,
        T = 7,
    }

    enum Status
    {
        A = 1,
        B = 2,
        C = 3,
        D = 4,
    }

    public partial class Form1 : Form
    {
        const int H = 20;
        const int W = 10;
        PictureBox[] PicBox = new PictureBox[W * H];//主界面图形
        PictureBox[] NextPic = new PictureBox[4 * 4];//下一个方块显示界面
        int[,] GameArry = new int[H, W];//主界面数字模拟
        int[] CurrentArry = new int[4];//当前游戏方块数字模拟
        int[] LastNext = null;//为了防止重绘闪屏
        Tetromino NextTetromino;//下一个方块样式
        Tetromino CurrentTetromino;//当前方块样式
        Direction CurrentDir = Direction.Up;//按钮触发（方向）
        KeyboardHook KeyDown;
        Status DefaultStatus = Status.A;//当前方块状态
        bool eluosIsRun = false;//游戏是否正在进行
        int Score = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            KeyDown = new KeyboardHook();
            KeyDown.SetHook();
            KeyDown.OnKeyDownEvent += kh_OnKeyDownEvent;

            //初始化背景、数组
            for (int i = 0; i < W * H; i++)
            {
                PicBox[i] = new PictureBox();
                PicBox[i].Size = new Size(20, 20);
                PicBox[i].BackColor = Color.Black;
                PicBox[i].Margin = new Padding(0); //new Padding(left, top, right, bottom); 
                MainPanel.Controls.Add(PicBox[i]);
            }
            for (int i = 0; i < H; i++)
            {
                for (int j = 0; j < W; j++)
                {
                    GameArry[i, j] = 0;
                }
            }
            for (int i = 0; i < 4 * 4; i++)
            {
                NextPic[i] = new PictureBox();
                NextPic[i].Size = new Size(20, 20);
                NextPic[i].BackColor = Color.Black;
                NextPic[i].Margin = new Padding(0);
                //NextPic[i].BorderStyle = BorderStyle.FixedSingle;
                NextTetris.Controls.Add(NextPic[i]);
            }

            //窗体大小不可改变
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MinimizeBox = false;//去掉最小化按钮
            this.MaximizeBox = false;//去掉最大化按钮

            //窗体图标
            this.Icon = eluosi.Properties.Resources.apple7;
        }

        void kh_OnKeyDownEvent(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Up:
                    CurrentDir = Direction.Up; GameChange(CurrentTetromino);
                    break;
                case Keys.Down:
                    CurrentDir = Direction.Down; GameDown();
                    break;
                case Keys.Left:
                    CurrentDir = Direction.Left; GameLeft();
                    break;
                case Keys.Right:
                    CurrentDir = Direction.Right; GameRight();
                    break;
                default: break;
            }
        }

        private void TimerRun_Tick(object sender, EventArgs e)
        {
            GameRun();
        }

        private void NextTetrisRandom()
        {
            if (LastNext != null)
            {
                for (int i = 0; i < LastNext.Length; i++)
                {
                    NextPic[LastNext[i]].BackColor = Color.Black;
                    NextPic[LastNext[i]].BorderStyle = BorderStyle.None;
                }
            }
            int[][] AllTetris = new int[][]
            {
                new int[]{ 5, 6, 8, 9 },          //S = 1,
                new int[]{ 4, 5, 9, 10 },         //Z = 2,
                new int[]{ 6, 8, 9, 10 },         //L = 3,
                new int[]{ 4, 8, 9, 10 },         //J = 4,
                new int[]{ 8, 9, 10, 11 },        //I = 5,
                new int[]{ 5, 6, 9, 10 },         //O = 6,
                new int[]{ 5, 8, 9, 10 }          //T = 7,
            };

            Random NextRan = new Random();
            int Next = NextRan.Next(0, 7);
            LastNext = AllTetris[Next];
            foreach (int item in LastNext)
            {
                NextPic[item].BackColor = Color.Red;
                NextPic[item].BorderStyle = BorderStyle.FixedSingle;
            }
            NextTetromino = (Tetromino)(Next + 1);
            #region 交错数组（数组的数组）
            //int[][] c1 = new int[3][];
            //c1[0] = new int[3];
            //c1[1] = new int[2];
            //c1[2] = new int[1];
            //int[][] c2 = new int[][]  
            //{  
            //    new int[] {1,2,3},  
            //    new int[] {4,5,6},  
            //    new int[] {7,8,9}  
            //};
            //            int[][] c3 =  
            //{  
            //    new int[] {1,2,3},  
            //    new int[] {4,5,6},  
            //    new int[] {7,8,9}  
            //}; 
            #endregion
        }

        private void GameRun()
        {
            //判断是否掉落到底,或者碰撞到物体--temporary临时
            int[] TemporaryCurrentArry = CurrentArry.Clone() as int[];
            int[,] TemporaryGameArry = GameArry.Clone() as int[,];
            bool IsRun = false;
            for (int i = 0; i < TemporaryCurrentArry.Length; i++)
            {
                if (TemporaryCurrentArry[i] >= 190)
                {
                    Create();
                    return;
                }
                else
                {
                    TemporaryCurrentArry[i] += 10;
                }
            }
            foreach (int item in CurrentArry) TemporaryGameArry[item / 10, item % 10] = 0;
            foreach (int item in TemporaryCurrentArry)
                if (TemporaryGameArry[item / 10, item % 10] == 1)
                {
                    Create();
                    return;
                }

            //清除当前状态
            foreach (int item in CurrentArry) GameArry[item / 10, item % 10] = 0;
            for (int i = 0; i < CurrentArry.Length; i++)
            {
                PicBox[CurrentArry[i]].BackColor = Color.Black;
                PicBox[CurrentArry[i]].BorderStyle = BorderStyle.None;
            }

            //增加下一状态
            for (int i = 0; i < CurrentArry.Length; i++) CurrentArry[i] += 10;

            //进行下一状态
            foreach (int item in CurrentArry) GameArry[item / 10, item % 10] = 1;
            for (int i = 0; i < CurrentArry.Length; i++)
            {
                PicBox[CurrentArry[i]].BackColor = Color.Red;
                PicBox[CurrentArry[i]].BorderStyle = BorderStyle.FixedSingle;
            }

            #region
            ////初始背景，再根据数组重新生成
            //for (int i = 0; i < W * H; i++)
            //{
            //    PicBox[i].BackColor = Color.Black;
            //    PicBox[i].BorderStyle = BorderStyle.None;
            //}
            //for (int i = 0; i < H; i++)
            //{
            //    for (int j = 0; j < W; j++)
            //    {
            //        if (GameArry[i, j] == 1)
            //        {
            //            PicBox[i * 10 + j].BackColor = Color.Red;
            //            PicBox[i * 10 + j].BorderStyle = BorderStyle.FixedSingle;
            //        }
            //    }
            //}
            #endregion
        }

        private void Clear()
        {
            int IntClear = 0;
            bool isClear = true;
            for (int i = 0; i < H; i++)
            {
                //是否有消除行
                isClear = true;
                for (int j = 0; j < W; j++)
                {
                    if (GameArry[i, j] == 0) isClear = false;
                }

                if (isClear)
                {
                    //有消除行，则将行进行消除，初始化，并增加消除行计数
                    for (int k = 0; k < W; k++)
                    {
                        GameArry[i, k] = 0;
                        PicBox[i * 10 + k].BackColor = Color.Black;
                        PicBox[i * 10 + k].BorderStyle = BorderStyle.None;
                    }
                    IntClear++;
                    //消除行上方的所有方块，往下落消除的行数
                    for (int m = i - 1; m >= 0; m--)
                    {
                        for (int n = W - 1; n >= 0; n--)
                        {
                            if (GameArry[m, n] == 1)
                            {
                                GameArry[m, n] = 0;
                                GameArry[m + 1, n] = 1;
                            }
                        }
                    }
                    for (int x = 10 * i - 1; x >= 0; x--)
                    {
                        if (PicBox[x].BackColor.Equals(Color.Red))
                        {
                            PicBox[x].BackColor = Color.Black;
                            PicBox[x].BorderStyle = BorderStyle.None;

                            PicBox[x + 10].BackColor = Color.Red;
                            PicBox[x + 10].BorderStyle = BorderStyle.FixedSingle;
                        }
                    }
                }
            }
            switch (IntClear)
            {
                case 0:
                    break;
                case 1: Score += 2;
                    break;
                case 2: Score += 5;
                    break;
                case 3: Score += 9;
                    break;
                case 4: Score += 14;
                    break;
                default: break;
            }
            ScoreLabel.Text = Score.ToString();
        }

        private void GameLeft()
        {
            //判断是否掉落到底,或者碰撞到物体--temporary临时
            int[] TemporaryCurrentArry = CurrentArry.Clone() as int[];
            int[,] TemporaryGameArry = GameArry.Clone() as int[,];
            for (int i = 0; i < TemporaryCurrentArry.Length; i++)
            {
                if (TemporaryCurrentArry[i] % 10 == 0) return;
                else TemporaryCurrentArry[i] -= 1;
            }
            foreach (int item in CurrentArry) TemporaryGameArry[item / 10, item % 10] = 0;
            foreach (int item in TemporaryCurrentArry)
                if (TemporaryGameArry[item / 10, item % 10] == 1) return;

            //清除当前状态
            foreach (int item in CurrentArry) GameArry[item / 10, item % 10] = 0;
            for (int i = 0; i < CurrentArry.Length; i++)
            {
                PicBox[CurrentArry[i]].BackColor = Color.Black;
                PicBox[CurrentArry[i]].BorderStyle = BorderStyle.None;
            }

            //增加下一状态
            for (int i = 0; i < CurrentArry.Length; i++) CurrentArry[i] -= 1;

            //进行下一状态
            foreach (int item in CurrentArry) GameArry[item / 10, item % 10] = 1;
            for (int i = 0; i < CurrentArry.Length; i++)
            {
                PicBox[CurrentArry[i]].BackColor = Color.Red;
                PicBox[CurrentArry[i]].BorderStyle = BorderStyle.FixedSingle;
            }
        }

        private void GameRight()
        {
            //判断是否掉落到底,或者碰撞到物体--temporary临时
            int[] TemporaryCurrentArry = CurrentArry.Clone() as int[];
            int[,] TemporaryGameArry = GameArry.Clone() as int[,];
            for (int i = 0; i < TemporaryCurrentArry.Length; i++)
            {
                if (TemporaryCurrentArry[i] % 10 == 9) return;
                else TemporaryCurrentArry[i] += 1;
            }
            foreach (int item in CurrentArry) TemporaryGameArry[item / 10, item % 10] = 0;
            foreach (int item in TemporaryCurrentArry)
                if (TemporaryGameArry[item / 10, item % 10] == 1) return;

            //清除当前状态
            foreach (int item in CurrentArry) GameArry[item / 10, item % 10] = 0;
            for (int i = 0; i < CurrentArry.Length; i++)
            {
                PicBox[CurrentArry[i]].BackColor = Color.Black;
                PicBox[CurrentArry[i]].BorderStyle = BorderStyle.None;
            }

            //增加下一状态
            for (int i = 0; i < CurrentArry.Length; i++) CurrentArry[i] += 1;

            //进行下一状态
            foreach (int item in CurrentArry) GameArry[item / 10, item % 10] = 1;
            for (int i = 0; i < CurrentArry.Length; i++)
            {
                PicBox[CurrentArry[i]].BackColor = Color.Red;
                PicBox[CurrentArry[i]].BorderStyle = BorderStyle.FixedSingle;
            }
        }

        private void GameDown()
        {
            TimerRun.Interval = 25;
        }

        //一切转向为顺时针方向转
        private void GameChange(Tetromino Tetromino)
        {
            if (Tetromino.Equals(Tetromino.O)) return;
            //清除当前状态
            foreach (int item in CurrentArry) GameArry[item / 10, item % 10] = 0;
            for (int i = 0; i < CurrentArry.Length; i++)
            {
                PicBox[CurrentArry[i]].BackColor = Color.Black;
                PicBox[CurrentArry[i]].BorderStyle = BorderStyle.None;
            }

            //进行变换
            switch (Tetromino)
            {
                case Tetromino.S: ChangeS();
                    break;
                case Tetromino.Z: ChangeZ();
                    break;
                case Tetromino.L: ChangeL();
                    break;
                case Tetromino.J: ChangeJ();
                    break;
                case Tetromino.I: ChangeI();
                    break;
                case Tetromino.O:
                    break;
                case Tetromino.T: ChangeT();
                    break;
                default: break;
            }

            //进行下一状态
            foreach (int item in CurrentArry) GameArry[item / 10, item % 10] = 1;
            for (int i = 0; i < CurrentArry.Length; i++)
            {
                PicBox[CurrentArry[i]].BackColor = Color.Red;
                PicBox[CurrentArry[i]].BorderStyle = BorderStyle.FixedSingle;
            }
        }

        #region 方块变换具体方法
        private void ChangeI()
        {
            int[] TemporaryCurrentArry = CurrentArry.Clone() as int[];
            Status CurrrntS = Status.A;
            switch (DefaultStatus)
            {
                case Status.A://(32,33,34,35)->(3,13,23,33)
                    int IntA = TemporaryCurrentArry[1];
                    TemporaryCurrentArry[0] = IntA - 30;
                    TemporaryCurrentArry[1] = IntA - 20;
                    TemporaryCurrentArry[2] = IntA - 10;
                    TemporaryCurrentArry[3] = IntA;
                    CurrrntS = Status.B;
                    break;
                case Status.B://(3,13,23,33)->(32,33,34,35)
                    int IntB = TemporaryCurrentArry[3];
                    switch (IntB % 10)
                    {
                        case 0: IntB += 1;
                            break;
                        case 8: IntB -= 1;
                            break;
                        case 9: IntB -= 2;
                            break;
                        default: break;
                    }
                    TemporaryCurrentArry[0] = IntB - 1;
                    TemporaryCurrentArry[1] = IntB;
                    TemporaryCurrentArry[2] = IntB + 1;
                    TemporaryCurrentArry[3] = IntB + 2;
                    CurrrntS = Status.A;
                    break;
                default: break;
            }

            //判断是否掉落到底,或者碰撞到物体--temporary临时
            foreach (int item in TemporaryCurrentArry) if (item < 0 || item > 199) return;

            foreach (int item in TemporaryCurrentArry)
                if (GameArry[item / 10, item % 10] == 1) return;

            //检测通过后，则进行实际赋值
            CurrentArry = TemporaryCurrentArry.Clone() as int[];
            DefaultStatus = CurrrntS;
        }

        private void ChangeJ()
        {
            int[] TemporaryCurrentArry = CurrentArry.Clone() as int[];
            Status CurrrntS = Status.A;
            switch (DefaultStatus)
            {
                case Status.A://(13,23,24,25)->(4,5,14,24)
                    int IntA = TemporaryCurrentArry[2];
                    TemporaryCurrentArry[0] = IntA - 20;
                    TemporaryCurrentArry[1] = IntA - 20 + 1;
                    TemporaryCurrentArry[2] = IntA - 10;
                    TemporaryCurrentArry[3] = IntA;
                    CurrrntS = Status.B;
                    break;
                case Status.B://(4,5,14,24)->(13,14,15,25)
                    int IntB = TemporaryCurrentArry[2];
                    switch (IntB % 10)
                    {
                        case 0: IntB += 1;
                            break;
                        default: break;
                    }
                    TemporaryCurrentArry[0] = IntB - 1;
                    TemporaryCurrentArry[1] = IntB;
                    TemporaryCurrentArry[2] = IntB + 1;
                    TemporaryCurrentArry[3] = IntB + 1 + 10;
                    CurrrntS = Status.C;
                    break;
                case Status.C://(13,14,15,25)->(4,14,23,24)
                    int IntC = TemporaryCurrentArry[1];
                    TemporaryCurrentArry[0] = IntC - 10;
                    TemporaryCurrentArry[1] = IntC;
                    TemporaryCurrentArry[2] = IntC + 10 - 1;
                    TemporaryCurrentArry[3] = IntC + 10;
                    CurrrntS = Status.D;
                    break;
                case Status.D://(4,14,23,24)->(13,23,24,25)
                    int IntD = TemporaryCurrentArry[3];
                    switch (IntD % 10)
                    {
                        case 9: IntD -= 1;
                            break;
                        default: break;
                    }
                    TemporaryCurrentArry[0] = IntD - 1 - 10;
                    TemporaryCurrentArry[1] = IntD - 1;
                    TemporaryCurrentArry[2] = IntD;
                    TemporaryCurrentArry[3] = IntD + 1;
                    CurrrntS = Status.A;
                    break;
                default: break;
            }

            //判断是否掉落到底,或者碰撞到物体--temporary临时
            foreach (int item in TemporaryCurrentArry) if (item < 0 || item > 199) return;

            foreach (int item in TemporaryCurrentArry)
                if (GameArry[item / 10, item % 10] == 1) return;

            //检测通过后，则进行实际赋值
            CurrentArry = TemporaryCurrentArry.Clone() as int[];
            DefaultStatus = CurrrntS;
        }

        private void ChangeL()
        {
            int[] TemporaryCurrentArry = CurrentArry.Clone() as int[];
            Status CurrrntS = Status.A;
            switch (DefaultStatus)
            {
                case Status.A://(15,23,24,25)->(4,14,24,25)
                    int IntA = TemporaryCurrentArry[2];
                    TemporaryCurrentArry[0] = IntA - 20;
                    TemporaryCurrentArry[1] = IntA - 10;
                    TemporaryCurrentArry[2] = IntA;
                    TemporaryCurrentArry[3] = IntA + 1;
                    CurrrntS = Status.B;
                    break;
                case Status.B://(4,14,24,25)->(13,14,15,23)
                    int IntB = TemporaryCurrentArry[1];
                    switch (IntB % 10)
                    {
                        case 0: IntB += 1;
                            break;
                        default: break;
                    }
                    TemporaryCurrentArry[0] = IntB - 1;
                    TemporaryCurrentArry[1] = IntB;
                    TemporaryCurrentArry[2] = IntB + 1;
                    TemporaryCurrentArry[3] = IntB + 10 - 1;
                    CurrrntS = Status.C;
                    break;
                case Status.C://(13,14,15,23)->(3,4,14,24)
                    int IntC = TemporaryCurrentArry[1];
                    TemporaryCurrentArry[0] = IntC - 10 - 1;
                    TemporaryCurrentArry[1] = IntC - 10;
                    TemporaryCurrentArry[2] = IntC;
                    TemporaryCurrentArry[3] = IntC + 10;
                    CurrrntS = Status.D;
                    break;
                case Status.D://(3,4,14,24)->(15,23,24,25)
                    int IntD = TemporaryCurrentArry[3];
                    switch (IntD % 10)
                    {
                        case 9: IntD -= 1;
                            break;
                        default: break;
                    }
                    TemporaryCurrentArry[0] = IntD + 1 - 10;
                    TemporaryCurrentArry[1] = IntD - 1;
                    TemporaryCurrentArry[2] = IntD;
                    TemporaryCurrentArry[3] = IntD + 1;
                    CurrrntS = Status.A;
                    break;
                default: break;
            }

            //判断是否掉落到底,或者碰撞到物体--temporary临时
            foreach (int item in TemporaryCurrentArry) if (item < 0 || item > 199) return;

            foreach (int item in TemporaryCurrentArry)
                if (GameArry[item / 10, item % 10] == 1) return;

            //检测通过后，则进行实际赋值
            CurrentArry = TemporaryCurrentArry.Clone() as int[];
            DefaultStatus = CurrrntS;
        }

        private void ChangeS()
        {
            int[] TemporaryCurrentArry = CurrentArry.Clone() as int[];
            Status CurrrntS = Status.A;
            switch (DefaultStatus)
            {
                case Status.A://(13,14,22,23)->(3,13,14,24)
                    int IntA = TemporaryCurrentArry[0];
                    TemporaryCurrentArry[0] = IntA - 10;
                    TemporaryCurrentArry[1] = IntA;
                    TemporaryCurrentArry[2] = IntA + 1;
                    TemporaryCurrentArry[3] = IntA + 1 + 10;
                    CurrrntS = Status.B;
                    break;
                case Status.B://(3,13,14,24)->(13,14,22,23))
                    int IntB = TemporaryCurrentArry[1];
                    switch (IntB % 10)
                    {
                        case 0: IntB += 1;
                            break;
                        default: break;
                    }
                    TemporaryCurrentArry[0] = IntB;
                    TemporaryCurrentArry[1] = IntB + 1;
                    TemporaryCurrentArry[2] = IntB + 10 - 1;
                    TemporaryCurrentArry[3] = IntB + 10;
                    CurrrntS = Status.A;
                    break;
                default: break;
            }

            //判断是否掉落到底,或者碰撞到物体--temporary临时
            foreach (int item in TemporaryCurrentArry) if (item < 0 || item > 199) return;

            foreach (int item in TemporaryCurrentArry)
                if (GameArry[item / 10, item % 10] == 1) return;

            //检测通过后，则进行实际赋值
            CurrentArry = TemporaryCurrentArry.Clone() as int[];
            DefaultStatus = CurrrntS;
        }

        private void ChangeZ()
        {
            int[] TemporaryCurrentArry = CurrentArry.Clone() as int[];
            Status CurrrntS = Status.A;
            switch (DefaultStatus)
            {
                case Status.A://(12,13,23,24)->(3,12,13,22)
                    int IntA = TemporaryCurrentArry[0];
                    TemporaryCurrentArry[0] = IntA + 1 - 10;
                    TemporaryCurrentArry[1] = IntA;
                    TemporaryCurrentArry[2] = IntA + 1;
                    TemporaryCurrentArry[3] = IntA + 10;
                    CurrrntS = Status.B;
                    break;
                case Status.B://(3,12,13,22)->(12,13,23,24))
                    int IntB = TemporaryCurrentArry[2];
                    switch (IntB % 10)
                    {
                        case 9: IntB -= 1;
                            break;
                        default: break;
                    }
                    TemporaryCurrentArry[0] = IntB - 1;
                    TemporaryCurrentArry[1] = IntB;
                    TemporaryCurrentArry[2] = IntB + 10;
                    TemporaryCurrentArry[3] = IntB + 10 + 1;
                    CurrrntS = Status.A;
                    break;
                default: break;
            }

            //判断是否掉落到底,或者碰撞到物体--temporary临时
            foreach (int item in TemporaryCurrentArry) if (item < 0 || item > 199) return;

            foreach (int item in TemporaryCurrentArry)
                if (GameArry[item / 10, item % 10] == 1) return;

            //检测通过后，则进行实际赋值
            CurrentArry = TemporaryCurrentArry.Clone() as int[];
            DefaultStatus = CurrrntS;
        }

        private void ChangeT()
        {
            int[] TemporaryCurrentArry = CurrentArry.Clone() as int[];
            Status CurrrntS = Status.A;
            switch (DefaultStatus)
            {
                case Status.A://(13,22,23,24)->(3,13,14,23)
                    int IntA = TemporaryCurrentArry[0];
                    TemporaryCurrentArry[0] = IntA - 10;
                    TemporaryCurrentArry[1] = IntA;
                    TemporaryCurrentArry[2] = IntA + 1;
                    TemporaryCurrentArry[3] = IntA + 10;
                    CurrrntS = Status.B;
                    break;
                case Status.B://(3,13,14,23)->(12,13,14,23)
                    int IntB = TemporaryCurrentArry[1];
                    switch (IntB % 10)
                    {
                        case 0: IntB += 1;
                            break;
                        default: break;
                    }
                    TemporaryCurrentArry[0] = IntB - 1;
                    TemporaryCurrentArry[1] = IntB;
                    TemporaryCurrentArry[2] = IntB + 1;
                    TemporaryCurrentArry[3] = IntB + 10;
                    CurrrntS = Status.C;
                    break;
                case Status.C://(12,13,14,23)->(3,12,13,23)
                    int IntC = TemporaryCurrentArry[1];
                    TemporaryCurrentArry[0] = IntC - 10;
                    TemporaryCurrentArry[1] = IntC - 1;
                    TemporaryCurrentArry[2] = IntC;
                    TemporaryCurrentArry[3] = IntC + 10;
                    CurrrntS = Status.D;
                    break;
                case Status.D://(3,12,13,23)->(13,22,23,24)
                    int IntD = TemporaryCurrentArry[2];
                    switch (IntD % 10)
                    {
                        case 9: IntD -= 1;
                            break;
                        default: break;
                    }
                    TemporaryCurrentArry[0] = IntD;
                    TemporaryCurrentArry[1] = IntD + 10 - 1;
                    TemporaryCurrentArry[2] = IntD + 10;
                    TemporaryCurrentArry[3] = IntD + 10 + 1;
                    CurrrntS = Status.A;
                    break;
                default: break;
            }

            //判断是否掉落到底,或者碰撞到物体--temporary临时
            foreach (int item in TemporaryCurrentArry) if (item < 0 || item > 199) return;

            foreach (int item in TemporaryCurrentArry)
                if (GameArry[item / 10, item % 10] == 1) return;

            //检测通过后，则进行实际赋值
            CurrentArry = TemporaryCurrentArry.Clone() as int[];
            DefaultStatus = CurrrntS;
        }
        #endregion

        private void Create()
        {

            Clear();//创造方块之前，先进行消除操作
            DefaultStatus = Status.A;//创造方块时，进行方块状态初始化

            //创造方块时，速度回复初始速度
            int speed = 100 * (SpeedComBox.SelectedIndex + 1);
            TimerRun.Interval = speed;

            if (NextTetromino == 0) NextTetrisRandom();
            CurrentTetromino = NextTetromino;

            int[][] AllTetris = new int[][]
            {
                new int[]{ 4, 5, 13, 14 },          //S = 1,
                new int[]{ 3, 4, 14, 15 },         //Z = 2,
                new int[]{ 5, 13, 14, 15 },         //L = 3,
                new int[]{ 3, 13, 14, 15 },         //J = 4,
                new int[]{ 3, 4, 5, 6 },        //I = 5,
                new int[]{ 4, 5, 14, 15 },         //O = 6,
                new int[]{ 4, 13, 14, 15 }          //T = 7,
            };

            CurrentArry = AllTetris[(int)NextTetromino - 1];

            //先进行游戏是否结束判断
            foreach (var item in CurrentArry)
            {
                //是否游戏结束
                if (GameArry[item / 10, item % 10] == 1)
                {
                    TimerRun.Enabled = false;
                    GameLabel.Text = "Game Over!";
                    GameLabel.ForeColor = Color.Red;
                    SpeedComBox.Enabled = true;
                    eluosIsRun = false;
                    return;
                }
            }

            foreach (int item in CurrentArry)
            {
                PicBox[item].BackColor = Color.Red;
                PicBox[item].BorderStyle = BorderStyle.FixedSingle;
                GameArry[item / 10, item % 10] = 1;
            }
            NextTetrisRandom();
        }

        private void start_Click(object sender, EventArgs e)
        {
            //初始化背景、数组
            GameLabel.Text = "Come On!";
            GameLabel.ForeColor = Color.Lime;
            for (int i = 0; i < W * H; i++)
            {
                PicBox[i].BackColor = Color.Black;
                PicBox[i].BorderStyle = BorderStyle.None;
            }
            for (int i = 0; i < H; i++)
            {
                for (int j = 0; j < W; j++)
                {
                    GameArry[i, j] = 0;
                }
            }
            for (int i = 0; i < 4 * 4; i++)
            {
                NextPic[i].BackColor = Color.Black;
                NextPic[i].BorderStyle = BorderStyle.None;
            }

            //speed
            int speed = 100 * (SpeedComBox.SelectedIndex + 1);
            TimerRun.Interval = speed;
            TimerRun.Start();

            //score
            Score = 0;
            ScoreLabel.Text = Score.ToString();

            //禁用comboBox1
            SpeedComBox.Enabled = false;

            //下一个方块清空，重新创造，并且运行
            NextTetromino = 0;
            Create();
            TimerRun.Enabled = true;
            eluosIsRun = true;


        }

        private void pause_Click(object sender, EventArgs e)
        {
            if (!eluosIsRun) return;
            if (TimerRun.Enabled)
            {
                TimerRun.Stop();
                pause.Text = "|>";
            }
            else
            {
                TimerRun.Start();
                pause.Text = "||";
            }
        }
    }
}
