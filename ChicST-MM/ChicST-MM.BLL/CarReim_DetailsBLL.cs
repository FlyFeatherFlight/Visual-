using System;
using ChicST_MM.IBLL;
using ChicST_MM.IDAL;
using ChicST_MM.Model;

namespace ChicST_MM.BLL
{
   /// <summary>
   /// 商务接待车辆报销明细
   /// </summary>
   public partial class CarReim_DetailsBLL:BaseService<财务_车辆报销明细>, ICarReim_DetailsBLL
    {
        private ICarReim_DetailsDAL carReim_DetailsDAL;

        public CarReim_DetailsBLL(ICarReim_DetailsDAL carReim_DetailsDAL)
        {
            this.carReim_DetailsDAL = carReim_DetailsDAL ?? throw new ArgumentNullException(nameof(carReim_DetailsDAL));
            Dal = this.carReim_DetailsDAL;
        }
    }
}
