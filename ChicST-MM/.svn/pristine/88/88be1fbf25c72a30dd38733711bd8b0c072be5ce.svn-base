using ChicST_MM.IBLL;
using ChicST_MM.IDAL;
using System;
using ChicST_MM.Model;
namespace ChicST_MM.BLL
{
    /// <summary>
    /// 商务接待计划明细
    /// </summary>
   public partial class BusinessReception_PlansBLL:BaseService<营销_接待计划明细>, IBusinessReception_PlansBLL
    {
        private IBusinessReception_PlansDAL businessReception_PlansDAL;

        public BusinessReception_PlansBLL(IBusinessReception_PlansDAL businessReception_PlansDAL)
        {
            this.businessReception_PlansDAL = businessReception_PlansDAL ?? throw new ArgumentNullException(nameof(businessReception_PlansDAL));
            Dal = this.businessReception_PlansDAL;
        }
    }
}
