using System;
using MyClass;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace CHICERP_LogisticsClient
{
    public partial class LogisticsCenterForm : Form
    {
        public LogisticsCenterForm()
        {
            InitializeComponent();
        }

        private void LogisticsCenterForm_Load(object sender, EventArgs e)
        {

            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
            }

            SqlConnection con = MyClass.SQL.GetSqlConnect(MyClass.AppSetting.GetAppConnectString());
            con.Open();
            MyClass.AppVar.SysCon = con;
            string msg = "";
            BuildLogiticsData(null, null, null, out msg);
            MyClass.Msg.Show(msg);
        }

        /// <summary>
        /// 日期查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_DateSelect_Click(object sender, EventArgs e)
        {
            string msg = "";
            DateTime startDate = dateTimePicker_start.Value;
            DateTime endDate = dateTimePicker_end.Value;
            if (startDate == null || endDate == null)
            {
                MyClass.Msg.Show("日期查询条件不能为空！");
                return;
            }
            try
            {
                DataTable table = BuildLogiticsData(startDate, endDate, null, out msg);

                dataGridView1.DataSource = table;
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;//所有列填充最大
                }
                dataGridView1.Columns[0].Visible = false;
                MyClass.Msg.Show(msg);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                MyClass.Msg.Show(msg); ;
            }


        }

        /// <summary>
        /// 订单查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_OrderSelect_Click(object sender, EventArgs e)
        {
            string msg;
            string _orderNo = textBox_OrderNo.Text;

            if (string.IsNullOrEmpty(_orderNo))
            {
                MyClass.Msg.Show("订单编号查询条件不能为空！");
                return;
            }
            else
            {
                try
                {
                    DataTable table = BuildLogiticsData(null, null, _orderNo, out msg);
                    dataGridView1.DataSource = table;
                    foreach (DataGridViewColumn column in dataGridView1.Columns)
                    {
                        column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;//所有列填充最大
                    }
                    dataGridView1.Columns[0].Visible = false;
                    MyClass.Msg.Show(msg);
                }
                catch (Exception ex)
                {
                    msg = ex.Message;
                    MyClass.Msg.Show(msg);

                }

            }
        }



        /// <summary>
        /// 删除单条配送数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Delete_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string msg = "";
            if (dataGridView1.SelectedRows.Count > 0)
            {
                //得到选中行的索引
                int intRow = dataGridView1.SelectedRows[0].Cells[0].RowIndex;
                string id = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();


                //得到选中行某列的值
                //string str = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                MessageBox.Show(intRow + "*" + id);
                var a = DeletLogiticsData(id, out msg);
                if (a != -1)
                {
                    dataGridView1.Rows.RemoveAt(intRow);
                }
              //  MyClass.Msg.Show(a + "||" + msg);
            }
        }
        private void Button_SelectPrint_Click(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// 查询配送单列表
        /// </summary>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="orderNo">订单编号</param>
        /// <param name="msg">返回的信息</param>
        /// <returns></returns>
        private DataTable BuildLogiticsData(DateTime? startDate, DateTime? endDate, string orderNo, out string msg)
        {

            string cmd_str = "";


            if (startDate == null && endDate == null)
            {
                DateTime date = DateTime.Now;
                startDate = date.AddMonths(-1);
                endDate = date;

            }

            cmd_str = "select *from dbo.[SH_配送单] as a WHERE a.[单据日期]>='" + startDate.Value.ToShortDateString() + "' and a.[单据日期]<='" + endDate.Value.ToShortDateString() + "'";


            if (!string.IsNullOrEmpty(orderNo))
            {
                cmd_str = "select *from dbo.[SH_配送单] as a WHERE a.[单据编号]='" + orderNo + "'";

            }



            SqlCommand cmd = MyClass.SQL.NewSqlCommand(cmd_str);
            //cmd.Parameters.Add("@code", SqlDbType.NVarChar);
            //cmd.Parameters.Add("@pwd", SqlDbType.NVarChar);
            //cmd.Parameters["@code"].Value = code;
            //cmd.Parameters["@pwd"].Value = str_pwd;
            SqlDataAdapter r = MyClass.SQL.GetSqlDataAdater(cmd);
            DataSet myDataSet = new DataSet();
            r.Fill(myDataSet, "LogiticsList");
            DataTable logiticsTable = myDataSet.Tables["LogiticsList"];
            if (logiticsTable.Rows.Count > 0)
            {
                msg = "查询成功！";

                return logiticsTable;
            }
            else
            {
                msg = "查询为空！";

                return logiticsTable;
            }

        }

        /// <summary>
        /// 删除配送
        /// </summary>
        /// <param name="id">配送单ID</param>
        /// <param name="msg">返回消息</param>
        /// <returns></returns>
        private int DeletLogiticsData(string id, out string msg)
        {
            string cmd_str = "DELETE  from dbo.[SH_配送单] WHERE ID = '" + id + "'";
            int a = -1;
            try
            {
                SqlCommand cmd = MyClass.SQL.NewSqlCommand(cmd_str);
                a = cmd.ExecuteNonQuery();
                msg = "删除成功!";
            }
            catch (Exception e)
            {

                msg = "删除失败!" + e.Message;
            }

            return a;


        }


    }
}
