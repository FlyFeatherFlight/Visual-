using System;
using ChicST_MM.IBLL;
using ChicST_MM.IDAL;
using ChicST_MM.Model;

namespace ChicST_MM.BLL
{
    /// <summary>
    /// 客服售后费用分摊
    /// </summary>
 public partial   class AfterSales_CostComputationBLL:BaseService<客服_售后费用分摊>, IAfterSales_CostComputationBLL
    {
        private IAfterSales_CostComputationDAL afterSales_CostComputationDAL;

        public AfterSales_CostComputationBLL(IAfterSales_CostComputationDAL afterSales_CostComputationDAL)
        {
            this.afterSales_CostComputationDAL = afterSales_CostComputationDAL ?? throw new ArgumentNullException(nameof(afterSales_CostComputationDAL));
            Dal = this.afterSales_CostComputationDAL;
        }
    }
}
