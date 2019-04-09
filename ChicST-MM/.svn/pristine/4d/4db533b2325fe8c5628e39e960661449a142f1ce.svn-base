using System;
using ChicST_MM.IBLL;
using ChicST_MM.IDAL;
using ChicST_MM.Model;

namespace ChicST_MM.BLL
{
    /// <summary>
    /// 出差汇报凭证
    /// </summary>
   public partial class BusinessTrip_ReportProofBLL:BaseService<HR_出差汇报凭证>, IBusinessTrip_ReportProofBLL
    {
        private IBusinessTrip_ReportProofDAL businessTrip_ReportProofDAL;

        public BusinessTrip_ReportProofBLL(IBusinessTrip_ReportProofDAL businessTrip_ReportProofDAL)
        {
            this.businessTrip_ReportProofDAL = businessTrip_ReportProofDAL ?? throw new ArgumentNullException(nameof(businessTrip_ReportProofDAL));
            Dal = this.businessTrip_ReportProofDAL;
        }
    }
}
