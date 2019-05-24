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
    /// 商场联系人
    /// </summary>
   public partial class Development_MarketContactBLL:BaseService<拓展_商场联系人>, IDevelopment_MarketContactBLL
    {
        private IDevelopment_MarketContactDAL development_MarketContactDAL;

        public Development_MarketContactBLL(IDevelopment_MarketContactDAL development_MarketContactDAL)
        {
            this.development_MarketContactDAL = development_MarketContactDAL ?? throw new ArgumentNullException(nameof(development_MarketContactDAL));
            Dal = development_MarketContactDAL;
        }
    }
}
