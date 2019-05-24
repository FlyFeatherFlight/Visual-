using System.Drawing;
using System.Drawing.Printing;

using System.Windows.Forms;

namespace QRBuilder
{
    /// <summary>
    /// 文档操作类（打印）
    /// </summary>
    public class DocementBase : PrintDocument

    {

        //fields

        public Font Font = new Font("Verdana", 10, GraphicsUnit.Point);



        //预览打印(桌面端)

        public DialogResult showPrintPreviewDialog()

        {

            PrintPreviewDialog dialog = new PrintPreviewDialog();

            dialog.Document = this;



            return dialog.ShowDialog();

        }



        //先设置后打印(桌面端)

        public DialogResult ShowPageSettingsDialog()

        {

            PageSetupDialog dialog = new PageSetupDialog();

            dialog.Document = this;



            return dialog.ShowDialog();

        }

    }
}
