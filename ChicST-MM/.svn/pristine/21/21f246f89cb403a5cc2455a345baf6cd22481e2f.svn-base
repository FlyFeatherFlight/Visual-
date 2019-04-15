using System;
using ChicST_MM.IBLL;
using ChicST_MM.IDAL;
using ChicST_MM.Model;

namespace ChicST_MM.BLL
{
    /// <summary>
    /// 商务接待车辆报销分摊
    /// </summary>
  public partial  class CarReim_SharingBLL:BaseService<财务_车辆报销分摊>, ICarReim_SharingBLL
    {
        private ICarReim_SharingDAL carReim_SharingDAL;

        public CarReim_SharingBLL(ICarReim_SharingDAL carReim_SharingDAL)
        {
            this.carReim_SharingDAL = carReim_SharingDAL ?? throw new ArgumentNullException(nameof(carReim_SharingDAL));
            Dal = this.carReim_SharingDAL;
        }
    }
}
