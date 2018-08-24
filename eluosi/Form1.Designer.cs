namespace eluosi
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.MainPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.start = new System.Windows.Forms.Button();
            this.TimerRun = new System.Windows.Forms.Timer(this.components);
            this.NextTetris = new System.Windows.Forms.FlowLayoutPanel();
            this.NextLabel = new System.Windows.Forms.Label();
            this.GameLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ScoreLabel = new System.Windows.Forms.Label();
            this.SpeedComBox = new System.Windows.Forms.ComboBox();
            this.pause = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // MainPanel
            // 
            this.MainPanel.Location = new System.Drawing.Point(3, 51);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(200, 400);
            this.MainPanel.TabIndex = 0;
            // 
            // start
            // 
            this.start.Location = new System.Drawing.Point(3, 12);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(75, 23);
            this.start.TabIndex = 1;
            this.start.Text = "start";
            this.start.UseVisualStyleBackColor = true;
            this.start.Click += new System.EventHandler(this.start_Click);
            // 
            // TimerRun
            // 
            this.TimerRun.Interval = 200;
            this.TimerRun.Tick += new System.EventHandler(this.TimerRun_Tick);
            // 
            // NextTetris
            // 
            this.NextTetris.Location = new System.Drawing.Point(234, 87);
            this.NextTetris.Name = "NextTetris";
            this.NextTetris.Size = new System.Drawing.Size(80, 80);
            this.NextTetris.TabIndex = 2;
            // 
            // NextLabel
            // 
            this.NextLabel.AutoSize = true;
            this.NextLabel.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.NextLabel.Location = new System.Drawing.Point(251, 65);
            this.NextLabel.Name = "NextLabel";
            this.NextLabel.Size = new System.Drawing.Size(40, 16);
            this.NextLabel.TabIndex = 4;
            this.NextLabel.Text = "Next";
            // 
            // GameLabel
            // 
            this.GameLabel.AutoSize = true;
            this.GameLabel.Font = new System.Drawing.Font("Cambria", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GameLabel.ForeColor = System.Drawing.Color.Lime;
            this.GameLabel.Location = new System.Drawing.Point(115, 11);
            this.GameLabel.Name = "GameLabel";
            this.GameLabel.Size = new System.Drawing.Size(88, 22);
            this.GameLabel.TabIndex = 5;
            this.GameLabel.Text = "Come On!";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(243, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "score:";
            // 
            // ScoreLabel
            // 
            this.ScoreLabel.AutoSize = true;
            this.ScoreLabel.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ScoreLabel.Location = new System.Drawing.Point(300, 16);
            this.ScoreLabel.Name = "ScoreLabel";
            this.ScoreLabel.Size = new System.Drawing.Size(16, 16);
            this.ScoreLabel.TabIndex = 7;
            this.ScoreLabel.Text = "0";
            // 
            // SpeedComBox
            // 
            this.SpeedComBox.FormattingEnabled = true;
            this.SpeedComBox.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.SpeedComBox.Location = new System.Drawing.Point(293, 188);
            this.SpeedComBox.Name = "SpeedComBox";
            this.SpeedComBox.Size = new System.Drawing.Size(41, 20);
            this.SpeedComBox.TabIndex = 8;
            this.SpeedComBox.Text = "10";
            // 
            // pause
            // 
            this.pause.Location = new System.Drawing.Point(246, 228);
            this.pause.Name = "pause";
            this.pause.Size = new System.Drawing.Size(50, 23);
            this.pause.TabIndex = 9;
            this.pause.Text = "||";
            this.pause.UseVisualStyleBackColor = true;
            this.pause.Click += new System.EventHandler(this.pause_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(231, 188);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 16);
            this.label2.TabIndex = 10;
            this.label2.Text = "speed:";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("楷体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox1.Location = new System.Drawing.Point(209, 275);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(125, 99);
            this.textBox1.TabIndex = 11;
            this.textBox1.Text = "操作方法（键盘方向键）：\r\n↑  ：形状变换\r\n↓  ：快速下落\r\n← ：左移动\r\n→ ：右移动";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(346, 454);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pause);
            this.Controls.Add(this.SpeedComBox);
            this.Controls.Add(this.ScoreLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.GameLabel);
            this.Controls.Add(this.NextLabel);
            this.Controls.Add(this.NextTetris);
            this.Controls.Add(this.start);
            this.Controls.Add(this.MainPanel);
            this.Name = "Form1";
            this.Text = "eluosi";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel MainPanel;
        private System.Windows.Forms.Button start;
        private System.Windows.Forms.Timer TimerRun;
        private System.Windows.Forms.FlowLayoutPanel NextTetris;
        private System.Windows.Forms.Label NextLabel;
        private System.Windows.Forms.Label GameLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label ScoreLabel;
        private System.Windows.Forms.ComboBox SpeedComBox;
        private System.Windows.Forms.Button pause;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
    }
}

