namespace game2048
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
            this.TabelLP = new System.Windows.Forms.TableLayoutPanel();
            this.reset = new System.Windows.Forms.Button();
            this.scorelabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.gameon = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TabelLP
            // 
            this.TabelLP.ColumnCount = 4;
            this.TabelLP.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.TabelLP.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.TabelLP.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.TabelLP.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.TabelLP.Location = new System.Drawing.Point(2, 35);
            this.TabelLP.Name = "TabelLP";
            this.TabelLP.RowCount = 4;
            this.TabelLP.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.TabelLP.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.TabelLP.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.TabelLP.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.TabelLP.Size = new System.Drawing.Size(400, 400);
            this.TabelLP.TabIndex = 0;
            // 
            // reset
            // 
            this.reset.Location = new System.Drawing.Point(12, 6);
            this.reset.Name = "reset";
            this.reset.Size = new System.Drawing.Size(75, 23);
            this.reset.TabIndex = 1;
            this.reset.Text = "reset";
            this.reset.UseVisualStyleBackColor = true;
            this.reset.Click += new System.EventHandler(this.reset_Click);
            // 
            // scorelabel
            // 
            this.scorelabel.AutoSize = true;
            this.scorelabel.Location = new System.Drawing.Point(362, 11);
            this.scorelabel.Name = "scorelabel";
            this.scorelabel.Size = new System.Drawing.Size(11, 12);
            this.scorelabel.TabIndex = 2;
            this.scorelabel.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(306, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "Score:";
            // 
            // gameon
            // 
            this.gameon.AutoSize = true;
            this.gameon.Font = new System.Drawing.Font("楷体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gameon.ForeColor = System.Drawing.Color.Lime;
            this.gameon.Location = new System.Drawing.Point(141, 6);
            this.gameon.Name = "gameon";
            this.gameon.Size = new System.Drawing.Size(97, 19);
            this.gameon.TabIndex = 4;
            this.gameon.Text = "Come On!";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 436);
            this.Controls.Add(this.gameon);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.scorelabel);
            this.Controls.Add(this.reset);
            this.Controls.Add(this.TabelLP);
            this.Name = "Form1";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "2048";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel TabelLP;
        private System.Windows.Forms.Button reset;
        private System.Windows.Forms.Label scorelabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label gameon;



    }
}

