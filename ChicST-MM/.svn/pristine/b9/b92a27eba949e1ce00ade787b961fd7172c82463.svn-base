using ChicST_MM.Model;
using ChicST_MM.IDAL;
using ChicST_MM.IBLL;
using System;

namespace ChicST_MM.BLL
{
    /// <summary>
    /// 地区档案
    /// </summary>
   public partial class RegionBLL:BaseService<地区>,IRegionBLL
    {
        private IRegionDAL regionDAL;

        public RegionBLL(IRegionDAL regionDAL)
        {
            this.regionDAL = regionDAL ?? throw new ArgumentNullException(nameof(regionDAL));
            Dal = this.regionDAL;
        }
    }
}
