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
    /// 门店销售订单
    /// </summary>
   public partial class StoreOrderBLL:BaseService<销售_订单>, IStoreOrderBLL
    {
        private IStoreOrderDAL storeOrderDAL;

        public StoreOrderBLL(IStoreOrderDAL storeOrderDAL)
        {
            this.storeOrderDAL = storeOrderDAL ?? throw new ArgumentNullException(nameof(storeOrderDAL));
            Dal = this.storeOrderDAL;
        }
    }
}
