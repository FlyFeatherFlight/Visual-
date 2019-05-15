using ChicST_MM.IBLL;
using ChicST_MM.IDAL;
using System;
using ChicST_MM.Model;

namespace ChicST_MM.BLL
{
    /// <summary>
    /// 拓展_城市档案
    /// </summary>
 public partial  class Development_CityBLL:BaseService<拓展_城市档案>,IDevelopment_CityBLL
    {
        private IDevelopment_CityDAL development_CityDAL;

        public Development_CityBLL(IDevelopment_CityDAL development_CityDAL)
        {
            this.development_CityDAL = development_CityDAL ?? throw new ArgumentNullException(nameof(development_CityDAL));
            Dal = development_CityDAL;
        }
    }
}
