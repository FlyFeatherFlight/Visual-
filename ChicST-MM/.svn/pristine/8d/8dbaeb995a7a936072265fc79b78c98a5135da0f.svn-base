using ChicST_MM.IBLL;
using ChicST_MM.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChicST_MM.BLL
{
    /// <summary>
    /// 出差拜访对象
    /// </summary>
   public partial class BusinessTrip_VisitSubjectBLL:BaseService<HR_出差_拜访对象>, IBusinessTrip_VisitSubjectBLL
    {
        private IBusinessTrip_VisitSubjectDAL businessTrip_VisitSubjectDAL;

        public BusinessTrip_VisitSubjectBLL(IBusinessTrip_VisitSubjectDAL businessTrip_VisitSubjectDAL)
        {
            this.businessTrip_VisitSubjectDAL = businessTrip_VisitSubjectDAL ?? throw new ArgumentNullException(nameof(businessTrip_VisitSubjectDAL));
            Dal = businessTrip_VisitSubjectDAL;
        }
    }
}
