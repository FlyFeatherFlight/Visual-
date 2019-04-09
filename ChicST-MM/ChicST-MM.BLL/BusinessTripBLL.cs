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
    /// <summary>
    /// 出差记录
    /// </summary>
   public partial class BusinessTripBLL:BaseService<HR_出差计划>, IBusinessTripBLL
    {
        private IBusinessTripDAL businessTripDAL;

        public BusinessTripBLL(IBusinessTripDAL businessTripDAL)
        {
            this.businessTripDAL = businessTripDAL;
            Dal = this.businessTripDAL;
        }
    }
}
