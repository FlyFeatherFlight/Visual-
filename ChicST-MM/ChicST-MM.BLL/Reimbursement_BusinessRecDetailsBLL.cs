using System;
using ChicST_MM.IBLL;
using ChicST_MM.IDAL;
using ChicST_MM.Model;

namespace ChicST_MM.BLL
{
    /// <summary>
    /// 接待报销明细
    /// </summary>
   public partial class Reimbursement_BusinessRecDetailsBLL:BaseService<财务_接待报销明细>, IReimbursement_BusinessRecDetailsBLL
    {
        private IReimbursement_BusinessRecDetailsDAL reimbursement_BusinessRecDetailsDAL;

        public Reimbursement_BusinessRecDetailsBLL(IReimbursement_BusinessRecDetailsDAL reimbursement_BusinessRecDetailsDAL)
        {
            this.reimbursement_BusinessRecDetailsDAL = reimbursement_BusinessRecDetailsDAL ?? throw new ArgumentNullException(nameof(reimbursement_BusinessRecDetailsDAL));
            Dal = this.reimbursement_BusinessRecDetailsDAL;
        }
    }
}
