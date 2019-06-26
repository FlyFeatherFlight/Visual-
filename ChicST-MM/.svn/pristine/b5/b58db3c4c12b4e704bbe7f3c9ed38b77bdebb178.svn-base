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
    /// 系统用户权限
    /// </summary>
  public partial  class Sys_AuthorityBLL:BaseService<系统用户_权限>, ISys_AuthorityBLL
    {
        private ISys_AuthorityDAL sys_AuthorityDAL;

        public Sys_AuthorityBLL(ISys_AuthorityDAL sys_AuthorityDAL)
        {
            this.sys_AuthorityDAL = sys_AuthorityDAL ?? throw new ArgumentNullException(nameof(sys_AuthorityDAL));
            Dal = this.sys_AuthorityDAL;
        }
    }
}
