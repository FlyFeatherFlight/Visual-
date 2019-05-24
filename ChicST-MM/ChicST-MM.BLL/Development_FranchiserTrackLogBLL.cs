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
    /// 经销商追踪日志
    /// </summary>
   public partial class Development_FranchiserTrackLogBLL:BaseService<拓展_经销商_追踪日志>, IDevelopment_FranchiserTrackLogBLL

    {
        private IDevelopment_FranchiserTrackLogDAL development_FranchiserTrackLogDAL;

        public Development_FranchiserTrackLogBLL(IDevelopment_FranchiserTrackLogDAL development_FranchiserTrackLogDAL)
        {
            this.development_FranchiserTrackLogDAL = development_FranchiserTrackLogDAL ?? throw new ArgumentNullException(nameof(development_FranchiserTrackLogDAL));
            Dal = development_FranchiserTrackLogDAL;
        }
    }
}
