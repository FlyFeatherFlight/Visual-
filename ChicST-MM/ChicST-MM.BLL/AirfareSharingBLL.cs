using System;
using ChicST_MM.IBLL;
using ChicST_MM.IDAL;
using ChicST_MM.Model;
namespace ChicST_MM.BLL
{
    /// <summary>
    /// 财务_机票明细分摊
    /// </summary>
    public partial  class AirfareSharingBLL:BaseService<财务_机票明细分摊>, IAirfareSharingBLL
    {
        private IAirfareSharingDAL airfareSharingDAL;

        public AirfareSharingBLL(IAirfareSharingDAL airfareSharingDAL)
        {
            this.airfareSharingDAL = airfareSharingDAL ?? throw new ArgumentNullException(nameof(airfareSharingDAL));
            Dal = this.airfareSharingDAL;
        }
    }
}
