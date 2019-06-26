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
    /// 商品_SKU
    /// </summary>
    public partial class Product_SKUBLL:BaseService<商品档案_SKU>, IProduct_SKUBLL
    {
        private IProduct_SKUDAL product_SKUDAL;

        public Product_SKUBLL(IProduct_SKUDAL product_SKUDAL)
        {
            this.product_SKUDAL = product_SKUDAL ?? throw new ArgumentNullException(nameof(product_SKUDAL));
            Dal = this.product_SKUDAL;
        }
    }
}
