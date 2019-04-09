using ChicST_MM.IBLL;
using ChicST_MM.Model;
using ChicST_MM.IDAL;
using System;

namespace ChicST_MM.BLL
{
    /// <summary>
    /// 客服_售后发起单凭证
    /// </summary>
    public partial class AfterSales_ProofBLL:BaseService<客服_售后发起单凭证>, IAfterSales_ProofBLL
    {
        private IAfterSales_ProofDAL afterSales_ProofDAL;

        public AfterSales_ProofBLL(IAfterSales_ProofDAL afterSales_ProofDAL)
        {
            this.afterSales_ProofDAL = afterSales_ProofDAL ?? throw new ArgumentNullException(nameof(afterSales_ProofDAL));
            Dal = this.afterSales_ProofDAL;
        }
    }
}
