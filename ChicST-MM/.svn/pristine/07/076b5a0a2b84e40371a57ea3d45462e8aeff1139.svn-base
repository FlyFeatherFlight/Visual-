using System;
using ChicST_MM.IBLL;
using ChicST_MM.IDAL;
using ChicST_MM.Model;

namespace ChicST_MM.BLL
{
    /// <summary>
    /// 商务来宾信息
    /// </summary>
  public partial  class BusinessReception_GuestBLL:BaseService<营销_接待来宾信息>, IBusinessReception_GuestBLL
    {
        private IBusinessReception_GuestDAL businessReception_GuestDAL;

        public BusinessReception_GuestBLL(IBusinessReception_GuestDAL businessReception_GuestDAL)
        {
            this.businessReception_GuestDAL = businessReception_GuestDAL ?? throw new ArgumentNullException(nameof(businessReception_GuestDAL));
            Dal = this.businessReception_GuestDAL;
        }
    }
}
