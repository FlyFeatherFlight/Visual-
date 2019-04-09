using System;
using ChicST_MM.IBLL;
using ChicST_MM.IDAL;
using ChicST_MM.Model;

namespace ChicST_MM.BLL
{
    /// <summary>
    /// HR_出差汇报
    /// </summary>
   public partial  class BusinessTrip_ReportBLL:BaseService<HR_出差汇报>, IBusinessTrip_ReportBLL
    {
        private IBusinessTrip_ReportDAL businessTrip_ReportDAL;

        public BusinessTrip_ReportBLL(IBusinessTrip_ReportDAL businessTrip_ReportDAL)
        {
            this.businessTrip_ReportDAL = businessTrip_ReportDAL ?? throw new ArgumentNullException(nameof(businessTrip_ReportDAL));
            Dal = this.businessTrip_ReportDAL;
        }
    }
}
