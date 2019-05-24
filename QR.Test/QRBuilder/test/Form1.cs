using QRBuilder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QRBuilder;
namespace test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox.Text))
            {
                MessageBox.Show("请输入文本");
                return;
            }
            Image map = BarcodeHelper.GenerateLogoQRcode(textBox.Text, 150, 150,1300,600);

            pictureBox1.Image = map;
        }

        private DocementBase _docement;
        private void PrintBtn_Click(object sender, EventArgs e)
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
    }
}
