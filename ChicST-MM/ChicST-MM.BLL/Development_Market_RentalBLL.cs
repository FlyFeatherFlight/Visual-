using ChicST_MM.IBLL;
using ChicST_MM.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChicST_MM.BLL
{
    /// <summary>
    /// 拓展_商场租金档案
    /// </summary>
  public partial  class Development_Market_RentalBLL:BaseService<拓展_商场_租金档案>, IDevelopment_Market_RentalBLL
    {
        private IDevelopment_Market_RentalDAL development_Market_RentalDAL;

        public Development_Market_RentalBLL(IDevelopment_Market_RentalDAL development_Market_RentalDAL)
        {
            this.development_Market_RentalDAL = development_Market_RentalDAL ?? throw new ArgumentNullException(nameof(development_Market_RentalDAL));
            Dal = development_Market_RentalDAL;
        }
    }
}
