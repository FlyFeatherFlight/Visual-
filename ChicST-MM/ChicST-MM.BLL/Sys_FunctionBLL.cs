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
    /// 系统用户
    /// </summary>
   public partial class Sys_FunctionBLL:BaseService<系统功能>, ISys_FunctionBLL
    {
        private ISys_FunctionDAL sys_FunctionDAL;

        public Sys_FunctionBLL(ISys_FunctionDAL sys_FunctionDAL)
        {
            this.sys_FunctionDAL = sys_FunctionDAL ?? throw new ArgumentNullException(nameof(sys_FunctionDAL));
            Dal = this.sys_FunctionDAL;
        }
    }
}
