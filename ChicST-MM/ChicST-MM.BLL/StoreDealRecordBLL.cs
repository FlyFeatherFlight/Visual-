using ChicST_MM.IBLL;
using ChicST_MM.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChicST_MM.BLL
{
    /// <summary>
    /// 门店销售成交记录
    /// </summary>
   public partial class StoreDealRecordBLL:BaseService<View_销售成交记录>, IStoreDealRecordBLL
    {
        private IStoreDealRecordDAL storeDealRecordDAL;

        public StoreDealRecordBLL(IStoreDealRecordDAL storeDealRecordDAL)
        {
            this.storeDealRecordDAL = storeDealRecordDAL ?? throw new ArgumentNullException(nameof(storeDealRecordDAL));
            Dal = this.storeDealRecordDAL;
        }
    }
}
