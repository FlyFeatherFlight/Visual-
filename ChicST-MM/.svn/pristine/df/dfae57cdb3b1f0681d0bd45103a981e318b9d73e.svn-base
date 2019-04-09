using ChicST_MM.IBLL;
using ChicST_MM.IDAL;
using ChicST_MM.Model;

namespace ChicST_MM.BLL
{
    public partial class BusinessTrip_DetailsBLL:BaseService<HR_出差计划详细>, IBusinessTrip_DetailsBLL
    {
        private IBusinessTrip_DetailsDAL businessTrip_DetailsDAL;

        public BusinessTrip_DetailsBLL(IBusinessTrip_DetailsDAL businessTrip_DetailsDAL)
        {
            this.businessTrip_DetailsDAL = businessTrip_DetailsDAL;
            Dal = this.businessTrip_DetailsDAL;
        }
    }
}
