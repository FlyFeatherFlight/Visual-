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
    /// 其它报销详细
    /// </summary>
   public partial    class OtherReimburse_DetailsBLL:BaseService<财务_其他报销_副表>, IOtherReimburse_DetailsBLL
    {
        private IOtherReimburse_DetailsDAL otherReimburse_DetailsDAL;

        public OtherReimburse_DetailsBLL(IOtherReimburse_DetailsDAL otherReimburse_DetailsDAL)
        {
            this.otherReimburse_DetailsDAL = otherReimburse_DetailsDAL ?? throw new ArgumentNullException(nameof(otherReimburse_DetailsDAL));
            Dal = this.otherReimburse_DetailsDAL;
        }
    }
}
