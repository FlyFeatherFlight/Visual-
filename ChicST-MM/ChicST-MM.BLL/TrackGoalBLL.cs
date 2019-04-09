using ChicST_MM.IBLL;
using ChicST_MM.IDAL;
using ChicST_MM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChicST_MM.BLL
{
    /// <summary>
    /// 跟进目标数申请表
    /// </summary>
   public partial class TrackGoalBLL:BaseService<销售_跟进目标数申请表>,ITrackGoalBLL
    {
        private ITrackGoalDAL trackGoalDAL;

        public TrackGoalBLL(ITrackGoalDAL trackGoalDAL)
        {
            this.trackGoalDAL = trackGoalDAL;
            Dal = trackGoalDAL;
        }
    }
}
