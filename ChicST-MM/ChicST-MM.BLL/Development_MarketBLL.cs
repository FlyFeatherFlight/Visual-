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
    /// 拓展_商场档案
    /// </summary>
  public partial  class Development_MarketBLL:BaseService<拓展_商场档案表>, IDevelopment_MarketBLL
    {
        private IDevelopment_MarketDAL development_MarketDAL;

        public Development_MarketBLL(IDevelopment_MarketDAL development_MarketDAL)
        {
            this.development_MarketDAL = development_MarketDAL ?? throw new ArgumentNullException(nameof(development_MarketDAL));
            Dal = development_MarketDAL;
        }
    }
}
