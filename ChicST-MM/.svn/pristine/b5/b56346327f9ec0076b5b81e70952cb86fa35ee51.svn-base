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
    /// 销售订单明细
    /// </summary>
   public partial class StoreOrder_DetailsBLL:BaseService<销售_订单明细>, IStoreOrder_DetailsBLL
    {
        private IStoreOrder_DetailsDAL storeOrder_DetailsDAL;

        public StoreOrder_DetailsBLL(IStoreOrder_DetailsDAL storeOrder_DetailsDAL)
        {
            this.storeOrder_DetailsDAL = storeOrder_DetailsDAL ?? throw new ArgumentNullException(nameof(storeOrder_DetailsDAL));
            Dal = this.storeOrder_DetailsDAL;
        }
    }
}
