using ChicST_MM.IBLL;
using ChicST_MM.IDAL;
using System;
using ChicST_MM.Model;

namespace ChicST_MM.BLL
{
    /// <summary>
    /// 商务接待报销
    /// </summary>
   public partial  class Reimbursement_BusinessReceptionBLL:BaseService<财务_接待报销>, IReimbursement_BusinessReceptionBLL

    {
        private IReimbursement_BusinessReceptionDAL reimbursement_BusinessReceptionDAL;

        public Reimbursement_BusinessReceptionBLL(IReimbursement_BusinessReceptionDAL reimbursement_BusinessReceptionDAL)
        {
            this.reimbursement_BusinessReceptionDAL = reimbursement_BusinessReceptionDAL ?? throw new ArgumentNullException(nameof(reimbursement_BusinessReceptionDAL));
            Dal = this.reimbursement_BusinessReceptionDAL;
        }
    }
}
