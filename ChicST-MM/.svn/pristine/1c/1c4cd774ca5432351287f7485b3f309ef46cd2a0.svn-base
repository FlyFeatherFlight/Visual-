using ChicST_MM.IBLL;
using ChicST_MM.Model;
using ChicST_MM.IDAL;
using System;

namespace ChicST_MM.BLL
{
    /// <summary>
    /// 售后发起
    /// </summary>
    public partial class AfterSalesBLL:BaseService<客服_售后发起单>, IAfterSalesBLL
    {
        private IAfterSalesDAL afterSalesDAL;

        public AfterSalesBLL(IAfterSalesDAL afterSalesDAL)
        {
            this.afterSalesDAL = afterSalesDAL ?? throw new ArgumentNullException(nameof(afterSalesDAL));
            Dal = this.afterSalesDAL;
        }
    }
}
