using ChicST_MM.IBLL;
using ChicST_MM.IDAL;
using System;
using ChicST_MM.Model;

namespace ChicST_MM.BLL
{
    /// <summary>
    /// 销售成交记录
    /// </summary>
  public partial  class SalesRecordBLL:BaseService<销售_接待成交单>, ISalesRecordBLL
    {
        private ISalesRecordDAL salesRecordDAL;

        public SalesRecordBLL(ISalesRecordDAL salesRecordDAL)
        {
            this.salesRecordDAL = salesRecordDAL ?? throw new ArgumentNullException(nameof(salesRecordDAL));
            Dal = this.salesRecordDAL;
        }
    }
}
