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
    /// 经销商_联系人档案
    /// </summary>
    public partial class Development_FranchiserContactsBLL : BaseService<拓展_经销商_联系人>, IDevelopment_FranchiserContactsBLL
    {
        private IDevelopment_FranchiserContactsDAL development_FranchiserContactsDAL;

        public Development_FranchiserContactsBLL(IDevelopment_FranchiserContactsDAL development_FranchiserContactsDAL)
        {
            this.development_FranchiserContactsDAL = development_FranchiserContactsDAL ?? throw new ArgumentNullException(nameof(development_FranchiserContactsDAL));
            Dal = development_FranchiserContactsDAL;
        }
    }
}
