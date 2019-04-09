using QRBuilder;
using System;
using System.Windows.Forms;

namespace QR.Test
{
    public partial class Form1 : Form
    {
       
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Sub_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox.Text))
            {
                MessageBox.Show("请输入文本");
                return;
            }
            System.Drawing.Bitmap map = BarcodeHelper.GenerateQRcode(textBox.Text, 250, 250);

            pictureBox1.Image = map;

        }
        private DocementBase _docement;
        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Print_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)

            {

                MessageBox.Show("没有图片!");

                return;

            }

            else

            {

                _docement = new ImageDocument(pictureBox1.Image);

            }

            _docement.showPrintPreviewDialog();

       
        }

        private void SubLogo_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox.Text))
            {
                MessageBox.Show("请输入文本");
                return;
            }
            System.Drawing.Bitmap map = BarcodeHelper.GenerateLogoQRcode(textBox.Text, 500, 500);

            pictureBox1.Image = map;
        }
    }
}