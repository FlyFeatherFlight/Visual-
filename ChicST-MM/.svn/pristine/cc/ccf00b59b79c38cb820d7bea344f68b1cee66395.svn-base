using ChicST_MM.Model;
using ChicST_MM.IBLL;
using ChicST_MM.IDAL;

namespace ChicST_MM.BLL
{
    /// <summary>
    /// 意向客户追踪
    /// </summary>
    public partial class CustomerTrackingBLL:BaseService<销售_意向追踪日志>,ICustomerTrackingBLL
    {
        private ICustomerTrackingLogDAL customerTackingLogDAL;

        public CustomerTrackingBLL(ICustomerTrackingLogDAL customerTackingLogDAL)
        {

            this.customerTackingLogDAL = customerTackingLogDAL;
            base.Dal = customerTackingLogDAL;
        }
    }
}
