using ChicST_MM.IDAL;
using ChicST_MM.IBLL;
using ChicST_MM.Model;

namespace ChicST_MM.BLL
{
    /// <summary>
    /// 员工信息
    /// </summary>
    public partial class StoreEmployeeBLL : BaseService<销售_店铺员工档案>,IStoreEmployeesBLL

    {
        private IEmployeeDAL storeEmployeesDAL;

        public StoreEmployeeBLL(IEmployeeDAL storeEmployeesDAL)
        {
            this.storeEmployeesDAL = storeEmployeesDAL;
            base.Dal = storeEmployeesDAL;
        }

    }
}
 
