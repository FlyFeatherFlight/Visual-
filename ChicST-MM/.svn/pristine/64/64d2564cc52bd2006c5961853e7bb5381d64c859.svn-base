using System;
using ChicST_MM.IBLL;
using ChicST_MM.IDAL;
using ChicST_MM.Model;

namespace ChicST_MM.BLL
{
    /// <summary>
    /// 接待报销分摊
    /// </summary>
    public partial class Reimbursement_ReceptionSharingBLL:BaseService<财务_接待报销分摊>, IReimbursement_ReceptionSharingBLL
    {
       private  IReimbursement_ReceptionSharingDAL reimbursement_ReceptionSharingDAL;

        public Reimbursement_ReceptionSharingBLL(IReimbursement_ReceptionSharingDAL reimbursement_ReceptionSharingDAL)
        {
            this.reimbursement_ReceptionSharingDAL = reimbursement_ReceptionSharingDAL ?? throw new ArgumentNullException(nameof(reimbursement_ReceptionSharingDAL));
            Dal = this.reimbursement_ReceptionSharingDAL;
        }
    }
}
