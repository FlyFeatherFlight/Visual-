using ChicST_MM.IBLL;
using ChicST_MM.Model;
using ChicST_MM.IDAL;
using System;

namespace ChicST_MM.BLL
{
    /// <summary>
    /// 商务接待预算
    /// </summary>
   public partial class BusinessReception_BudgetBLL:BaseService<营销_接待计划费用预估>, IBusinessReception_BudgetBLL
    {
        private IBusinessReception_BudgetDAL businessReception_BudgetDAL;

        public BusinessReception_BudgetBLL(IBusinessReception_BudgetDAL businessReception_BudgetDAL)
        {
            this.businessReception_BudgetDAL = businessReception_BudgetDAL ?? throw new ArgumentNullException(nameof(businessReception_BudgetDAL));
            Dal = this.businessReception_BudgetDAL;
        }
    }
}
