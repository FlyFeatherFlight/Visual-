using ChicST_MM.Model;
using ChicST_MM.IDAL;
using ChicST_MM.IBLL;
using System;

namespace ChicST_MM.BLL
{
    /// <summary>
    /// 商务接待住宿报销分摊
    /// </summary>
  public partial  class HotelReimSharingBLL:BaseService<财务_接待住宿报销分摊>, IHotelReimSharingBLL
    {

        private IHotelReimSharingDAL hotelReimSharingDAL;

        public HotelReimSharingBLL(IHotelReimSharingDAL hotelReimSharingDAL)
        {
            this.hotelReimSharingDAL = hotelReimSharingDAL ?? throw new ArgumentNullException(nameof(hotelReimSharingDAL));
            Dal = this.hotelReimSharingDAL;
        }
    }
}
