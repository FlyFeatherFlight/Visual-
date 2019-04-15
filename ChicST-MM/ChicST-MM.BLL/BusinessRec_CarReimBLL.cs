using ChicST_MM.IBLL;
using ChicST_MM.IDAL;
using System;
using ChicST_MM.Model;

namespace ChicST_MM.BLL
{
    /// <summary>
    /// 商务接待车辆报销
    /// </summary>
   public partial class BusinessRec_CarReimBLL:BaseService<财务_接待车辆报销>, IBusinessRec_CarReimBLL
    {
        private IBusinessRec_CarReimDAL businessRec_CarReimDAL;

        public BusinessRec_CarReimBLL(IBusinessRec_CarReimDAL businessRec_CarReimDAL)
        {
            this.businessRec_CarReimDAL = businessRec_CarReimDAL ?? throw new ArgumentNullException(nameof(businessRec_CarReimDAL));
            Dal = this.businessRec_CarReimDAL;
        }
    }
}
