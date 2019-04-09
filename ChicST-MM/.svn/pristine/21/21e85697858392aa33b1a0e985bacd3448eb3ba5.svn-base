using ChicST_MM.IBLL;
using ChicST_MM.IDAL;
using ChicST_MM.Model;

namespace ChicST_MM.BLL
{
    /// <summary>
    /// 商店信息
    /// </summary>
    public partial class StoreBLL:BaseService<销售_店铺档案>,IStoreBLL
    {
        private IStoreDAL storeDAL;

        public StoreBLL(IStoreDAL storeDAL)
        {
            this.storeDAL = storeDAL;
            base.Dal = storeDAL;
        }
    }
}
