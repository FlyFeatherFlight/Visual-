using ChicST_MM.IBLL;
using ChicST_MM.IDAL;
using ChicST_MM.Model;

namespace ChicST_MM.BLL
{
    /// <summary>
    /// HR_人员信息表
    /// </summary>
    public partial class EmployeeInformationBLL:BaseService<HR_人员信息表>,IEmployeeInformationBLL
    {
        private IEmployeeInformationDAL employeeInformationDAL;

        public EmployeeInformationBLL(IEmployeeInformationDAL employeeInformationDAL)
        {
            this.employeeInformationDAL = employeeInformationDAL;
            Dal = employeeInformationDAL;
        }
    }
}
