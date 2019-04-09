namespace QR.Test
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.textBox = new System.Windows.Forms.TextBox();
            this.Sub = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Print = new System.Windows.Forms.Button();
            this.SubLogo = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(110, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "输入网址：";
            // 
            // textBox
            // 
            this.textBox.Location = new System.Drawing.Point(180, 61);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(142, 21);
            this.textBox.TabIndex = 1;
            // 
            // Sub
            // 
            this.Sub.Location = new System.Drawing.Point(341, 59);
            this.Sub.Name = "Sub";
            this.Sub.Size = new System.Drawing.Size(75, 23);
            this.Sub.TabIndex = 2;
            this.Sub.Text = "生成";
            this.Sub.UseVisualStyleBackColor = true;
            this.Sub.Click += new System.EventHandler(this.Sub_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(141, 88);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(881, 737);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // Print
            // 
            this.Print.Location = new System.Drawing.Point(530, 59);
            this.Print.Name = "Print";
            this.Print.Size = new System.Drawing.Size(75, 23);
            this.Print.TabIndex = 4;
            this.Print.Text = "打印";
            this.Print.UseVisualStyleBackColor = true;
            this.Print.Click += new System.EventHandler(this.Print_Click);
            // 
            // SubLogo
            // 
            this.SubLogo.Location = new System.Drawing.Point(422, 59);
            this.SubLogo.Name = "SubLogo";
            this.SubLogo.Size = new System.Drawing.Size(102, 23);
            this.SubLogo.TabIndex = 5;
            this.SubLogo.Text = "生成Logo二维码";
            this.SubLogo.UseVisualStyleBackColor = true;
            this.SubLogo.Click += new System.EventHandler(this.SubLogo_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1133, 837);
            this.Controls.Add(this.SubLogo);
            this.Controls.Add(this.Print);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.Sub);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.Button Sub;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button Print;
        private System.Windows.Forms.Button SubLogo;
    }
}

