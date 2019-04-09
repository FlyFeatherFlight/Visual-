using System;
using ChicST_MM.IBLL;
using ChicST_MM.IDAL;
using ChicST_MM.Model;

namespace ChicST_MM.BLL
{
    /// <summary>
    /// 收款单
    /// </summary>
    public partial class ReceiptBLL:BaseService<财务_收款单>,IReceiptBLL
    {
        private IReceiptDAL receiptDAL;

        public ReceiptBLL(IReceiptDAL receiptDAL)
        {
            this.receiptDAL = receiptDAL ?? throw new ArgumentNullException(nameof(receiptDAL));
            Dal = this.receiptDAL;
        }
    }
}
