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
    /// 门店月报数据
    /// </summary>
   public partial  class StoreMonth_ReportBLL:BaseService<门店月报数据_Result>, IStoreMonth_ReportBLL
    {
        private IStoreMonth_ReportDAL storeMonth_ReportDAL;

        public StoreMonth_ReportBLL(IStoreMonth_ReportDAL storeMonth_ReportDAL)
        {
            this.storeMonth_ReportDAL = storeMonth_ReportDAL ?? throw new ArgumentNullException(nameof(storeMonth_ReportDAL));
            Dal = this.storeMonth_ReportDAL;
        }
    }
}
