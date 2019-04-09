using ChicST_MM.Model;
using ChicST_MM.IDAL;
using ChicST_MM.IBLL;
using System;

namespace ChicST_MM.BLL
{
    /// <summary>
    /// 经销商档案
    /// </summary>
    public partial class DistributorBLL:BaseService<销售_经销商档案>,IDistributorBLL
    {
        private IDistributorDAL distributorDAL;

        public DistributorBLL(IDistributorDAL distributorDAL)
        {
            this.distributorDAL = distributorDAL ?? throw new ArgumentNullException(nameof(distributorDAL));
            Dal = this.distributorDAL;
        }
    }
}
