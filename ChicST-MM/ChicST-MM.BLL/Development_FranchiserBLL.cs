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
    /// 拓展_经销商档案
    /// </summary>
   public partial class Development_FranchiserBLL: BaseService<拓展_经销商档案表>, IDevelopment_FranchiserBLL
    {
        

        private IDevelopment_FranchiserDAL development_FranchiserDAL;

        public Development_FranchiserBLL(IDevelopment_FranchiserDAL development_FranchiserDAL)
        {
            this.development_FranchiserDAL = development_FranchiserDAL ?? throw new ArgumentNullException(nameof(development_FranchiserDAL));
            Dal = development_FranchiserDAL;
        }
    }
}
