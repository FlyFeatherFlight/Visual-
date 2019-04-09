using System;
using ChicST_MM.IBLL;
using ChicST_MM.IDAL;
using ChicST_MM.Model;

namespace ChicST_MM.BLL
{
    /// <summary>
    /// 客服_售后分类
    /// </summary>
    public partial class AfterSales_ClassifyBLL:BaseService<客服_售后分类>, IAfterSales_ClassifyBLL
    {
        private IAfterSales_ClassifyDAL afterSales_ClassifyDAL;

        public AfterSales_ClassifyBLL(IAfterSales_ClassifyDAL afterSales_ClassifyDAL)
        {
            this.afterSales_ClassifyDAL = afterSales_ClassifyDAL ?? throw new ArgumentNullException(nameof(afterSales_ClassifyDAL));
            Dal = this.afterSales_ClassifyDAL; 
        }
    }
}
