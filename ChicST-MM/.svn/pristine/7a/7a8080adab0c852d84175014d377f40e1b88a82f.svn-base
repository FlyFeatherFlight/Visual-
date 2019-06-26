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
    /// 商品——SPU
    /// </summary>
   public partial  class Product_SPUBLL:BaseService<View__SPU>, IProduct_SPUBLL
    {
        private IProduct_SPUDAL product_SPUDAL;

        public Product_SPUBLL(IProduct_SPUDAL product_SPUDAL)
        {
            this.product_SPUDAL = product_SPUDAL ?? throw new ArgumentNullException(nameof(product_SPUDAL));
            Dal = this.product_SPUDAL;
        }
    }
}
