using System;
using ChicST_MM.IBLL;
using ChicST_MM.IDAL;
using ChicST_MM.Model;

namespace ChicST_MM.BLL
{
    /// <summary>
    /// 商务接待住宿报销
    /// </summary>
   public partial class BusinessRec_HotelReimBLL:BaseService<财务_接待住宿报销>,IBusinessRec_HotelReimBLL
    {
        private IBusinessRec_HotelReimDAL businessRec_HotelReimDAL;

        public BusinessRec_HotelReimBLL(IBusinessRec_HotelReimDAL businessRec_HotelReimDAL)
        {
            this.businessRec_HotelReimDAL = businessRec_HotelReimDAL ?? throw new ArgumentNullException(nameof(businessRec_HotelReimDAL));
            Dal = this.businessRec_HotelReimDAL;
        }
    }
}
