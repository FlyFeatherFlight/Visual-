using System;
using ChicST_MM.IBLL;
using ChicST_MM.IDAL;
using ChicST_MM.Model;

namespace ChicST_MM.BLL
{
    /// <summary>
    /// 其他报销
    /// </summary>
 public partial   class OtherReimburseBLL:BaseService<财务_其它报销>, IOtherReimburseBLL
    {
        private IOtherReimburseDAL otherReimburseDAL;

        public OtherReimburseBLL(IOtherReimburseDAL otherReimburseDAL)
        {
            this.otherReimburseDAL = otherReimburseDAL ?? throw new ArgumentNullException(nameof(otherReimburseDAL));
            Dal = this.otherReimburseDAL;
        }
    }
}
