using System;
using MyClass;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using EncryptionHelper;

namespace CHICERP_LogisticsClient
{
    public partial class LogInForm : Form
    {
        public LogInForm()
        {
            InitializeComponent();
        }

        private void ButtonLogIn_Click(object sender, EventArgs e)
        {

            string msg;

            var LogStatus = CheckLogIn(textBox_UserNO.Text, textBox_UserPwd.Text, out msg);

            MyClass.Msg.Show(msg);
            if (LogStatus)
            {
                LogisticsCenterForm logisticsCenterForm = new LogisticsCenterForm();
                this.Visible = false;
                logisticsCenterForm.ShowDialog();//此处不可用Show()
                this.Dispose();
                this.Close();
            }
           
        }
        /// <summary>
        /// 用户验证
        /// </summary>
        /// <param name="code">用户编号</param>
        /// <param name="pwd">密码</param>
        /// <param name="msg">返回错误消息</param>
        /// <returns></returns>
        public bool CheckLogIn(string code, string pwd , out string msg) {
            string str_pwd = MyClass.Encrypt.MD5Encrypt(pwd);
            string cmd_str = "select *from dbo.[系统用户] as a where a.[编号]='"+code+"' and a.[密码]='"+str_pwd+"' ";
            SqlCommand cmd = MyClass.SQL.NewSqlCommand(cmd_str);
            //cmd.Parameters.Add("@code", SqlDbType.NVarChar);
            //cmd.Parameters.Add("@pwd", SqlDbType.NVarChar);
            //cmd.Parameters["@code"].Value = code;
            //cmd.Parameters["@pwd"].Value = str_pwd;
            SqlDataReader r = MyClass.SQL.GetSqlDataReader(cmd);
            if (r.HasRows)
            {
                msg = "登陆成功";
                r.Close();
                return true;
            }
            else
            {
                msg = "登陆失败";
                r.Close();
                return false;
            }
           
            
          
        }

        private void LogInForm_Load(object sender, EventArgs e)
        {
            SqlConnection con = MyClass.SQL.GetSqlConnect(MyClass.AppSetting.GetAppConnectString());
            con.Open();
            MyClass.AppVar.SysCon = con;
        }

        private void textBox_UserPwd_TextChanged(object sender, EventArgs e)
        {
            if (checkBox_pwd.Checked)
            {
                //复选框被勾选，明文显示
                textBox_UserPwd.PasswordChar = new char();
            }
            else
            {
                //复选框被取消勾选，密文显示
                textBox_UserPwd.PasswordChar = '*';
            }
        }

      

        private void CheckBox_pwd_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox_pwd.Checked)
            {
                
                //复选框被勾选，明文显示
                textBox_UserPwd.PasswordChar = new char();
               
            }
            else
            {
                //复选框被取消勾选，密文显示
                textBox_UserPwd.PasswordChar = '*';
            }
        }
    }
}
