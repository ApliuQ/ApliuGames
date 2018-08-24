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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        KeyboardHook kh;
        private void Form2_Load(object sender, EventArgs e)
        {


            kh = new KeyboardHook();

            kh.SetHook();

            kh.OnKeyDownEvent += kh_OnKeyDownEvent;

        }

        void kh_OnKeyDownEvent(object sender, KeyEventArgs e)

        {
            if (e.KeyData == (Keys.Q)) { label1.Text += "Q"; }//Ctrl+S显示窗口

            if (e.KeyData == (Keys.S | Keys.Control)) { this.Show(); }//Ctrl+S显示窗口

            if (e.KeyData == (Keys.H | Keys.Control)) { this.Hide(); }//Ctrl+H隐藏窗口

            if (e.KeyData == (Keys.C | Keys.Control)) { this.Close(); }//Ctrl+C 关闭窗口 

            if (e.KeyData == (Keys.A | Keys.Control | Keys.Alt)) { this.Text = "你发现了什么？"; }//Ctrl+Alt+A

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)

        {

            kh.UnHook();

        }

    }
}
