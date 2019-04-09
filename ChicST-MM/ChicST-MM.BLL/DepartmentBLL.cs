using ChicST_MM.IBLL;
using ChicST_MM.IDAL;
using ChicST_MM.Model;

namespace ChicST_MM.BLL
{
    /// <summary>
    /// 系统_部门
    /// </summary>
    public partial class DepartmentBLL:BaseService<系统_部门>,IDepartmentBLL
    {
        private IDepartmentDAL departmentDAL;

        public DepartmentBLL(IDepartmentDAL departmentDAL)
        {
            this.departmentDAL = departmentDAL;
            Dal = departmentDAL;
        }
    }
}
