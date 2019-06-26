namespace CHICERP_LogisticsClient
{
    /// <summary>
    /// 配送详情页
    /// </summary>
    partial class LogInForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogInForm));
            this.LogBox = new System.Windows.Forms.PictureBox();
            this.labelPW = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.textBox_UserNO = new System.Windows.Forms.TextBox();
            this.textBox_UserPwd = new System.Windows.Forms.TextBox();
            this.buttonLogIn = new System.Windows.Forms.Button();
            this.checkBox_pwd = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.LogBox)).BeginInit();
            this.SuspendLayout();
            // 
            // LogBox
            // 
            this.LogBox.Image = ((System.Drawing.Image)(resources.GetObject("LogBox.Image")));
            this.LogBox.Location = new System.Drawing.Point(242, 5);
            this.LogBox.Name = "LogBox";
            this.LogBox.Size = new System.Drawing.Size(100, 80);
            this.LogBox.TabIndex = 0;
            this.LogBox.TabStop = false;
            // 
            // labelPW
            // 
            this.labelPW.AutoSize = true;
            this.labelPW.Location = new System.Drawing.Point(144, 154);
            this.labelPW.Name = "labelPW";
            this.labelPW.Size = new System.Drawing.Size(35, 12);
            this.labelPW.TabIndex = 2;
            this.labelPW.Text = "密码:";
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(144, 123);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(41, 12);
            this.labelName.TabIndex = 3;
            this.labelName.Text = "编号：";
            // 
            // textBox_UserNO
            // 
            this.textBox_UserNO.Location = new System.Drawing.Point(217, 120);
            this.textBox_UserNO.Name = "textBox_UserNO";
            this.textBox_UserNO.Size = new System.Drawing.Size(148, 21);
            this.textBox_UserNO.TabIndex = 4;
            // 
            // textBox_UserPwd
            // 
            this.textBox_UserPwd.Location = new System.Drawing.Point(217, 151);
            this.textBox_UserPwd.Name = "textBox_UserPwd";
            this.textBox_UserPwd.Size = new System.Drawing.Size(148, 21);
            this.textBox_UserPwd.TabIndex = 5;
            this.textBox_UserPwd.TextChanged += new System.EventHandler(this.textBox_UserPwd_TextChanged);
            // 
            // buttonLogIn
            // 
            this.buttonLogIn.Location = new System.Drawing.Point(242, 204);
            this.buttonLogIn.Name = "buttonLogIn";
            this.buttonLogIn.Size = new System.Drawing.Size(100, 23);
            this.buttonLogIn.TabIndex = 6;
            this.buttonLogIn.Text = "登  录";
            this.buttonLogIn.UseVisualStyleBackColor = true;
            this.buttonLogIn.Click += new System.EventHandler(this.ButtonLogIn_Click);
            // 
            // checkBox_pwd
            // 
            this.checkBox_pwd.AutoSize = true;
            this.checkBox_pwd.Location = new System.Drawing.Point(383, 154);
            this.checkBox_pwd.Name = "checkBox_pwd";
            this.checkBox_pwd.Size = new System.Drawing.Size(72, 16);
            this.checkBox_pwd.TabIndex = 7;
            this.checkBox_pwd.Text = "显示密码";
            this.checkBox_pwd.UseVisualStyleBackColor = true;
            this.checkBox_pwd.CheckStateChanged += new System.EventHandler(this.CheckBox_pwd_CheckStateChanged);
           
            // 
            // LogInForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(584, 311);
            this.Controls.Add(this.checkBox_pwd);
            this.Controls.Add(this.buttonLogIn);
            this.Controls.Add(this.textBox_UserPwd);
            this.Controls.Add(this.textBox_UserNO);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.labelPW);
            this.Controls.Add(this.LogBox);
            this.Name = "LogInForm";
            this.Text = "CHIC-ERP(物流端)";
            this.Load += new System.EventHandler(this.LogInForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.LogBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox LogBox;
        private System.Windows.Forms.Label labelPW;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox textBox_UserNO;
        private System.Windows.Forms.TextBox textBox_UserPwd;
        private System.Windows.Forms.Button buttonLogIn;
        private System.Windows.Forms.CheckBox checkBox_pwd;
    }
}