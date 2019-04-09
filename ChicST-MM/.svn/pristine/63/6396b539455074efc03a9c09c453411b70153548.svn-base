using ChicST_MM.IBLL;
using ChicST_MM.IDAL;
using ChicST_MM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChicST_MM.BLL
{
   public partial class BusinessTrip_TypeBLL:BaseService<HR_出差对象类型>, IBusinessTrip_TypeBLL
    {
        private IBusinessTrip_TypeDAL businessTrip_TypeDAL;

        public BusinessTrip_TypeBLL(IBusinessTrip_TypeDAL businessTrip_TypeDAL)
        {
            this.businessTrip_TypeDAL = businessTrip_TypeDAL ?? throw new ArgumentNullException(nameof(businessTrip_TypeDAL));
            Dal = this.businessTrip_TypeDAL;
        }
    }
}
