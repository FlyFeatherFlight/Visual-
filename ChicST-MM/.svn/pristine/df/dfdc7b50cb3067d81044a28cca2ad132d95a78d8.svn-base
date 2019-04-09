using System;
using ChicST_MM.IBLL;
using ChicST_MM.IDAL;
using ChicST_MM.Model;

namespace ChicST_MM.BLL
{
    /// <summary>
    /// 商务洽谈计划
    /// </summary>
  public partial   class BusinessReceptionBLL:BaseService<营销_接待计划>, IBusinessReceptionBLL
    {
        private IBusinessReceptionDAL businessReceptionDAL;

        public BusinessReceptionBLL(IBusinessReceptionDAL businessReceptionDAL)
        {
            this.businessReceptionDAL = businessReceptionDAL ?? throw new ArgumentNullException(nameof(businessReceptionDAL));
            Dal = this.businessReceptionDAL;
        }
    }
}
