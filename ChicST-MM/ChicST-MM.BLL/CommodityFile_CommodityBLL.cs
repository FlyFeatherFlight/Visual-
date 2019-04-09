using ChicST_MM.IBLL;
using ChicST_MM.Model;

using ChicST_MM.IDAL;
using System;

namespace ChicST_MM.BLL
{
    /// <summary>
    /// 商品档案_商品
    /// </summary>
   public partial class CommodityFile_CommodityBLL:BaseService<商品档案_商品>, ICommodityFile_CommodityBLL
    {
        private ICommodityFile_CommodityDAL commodityFile_CommodityDAL;

        public CommodityFile_CommodityBLL(ICommodityFile_CommodityDAL commodityFile_CommodityDAL)
        {
            this.commodityFile_CommodityDAL = commodityFile_CommodityDAL ?? throw new ArgumentNullException(nameof(commodityFile_CommodityDAL));
            Dal = this.commodityFile_CommodityDAL;
        }
    }
}
