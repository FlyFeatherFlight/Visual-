﻿using System;
using ChicST_MM.IBLL;
using ChicST_MM.IDAL;
using ChicST_MM.Model;

namespace ChicST_MM.BLL
{
    /// <summary>
    /// 出差报销详细
    /// </summary>
    public partial class TravelReimbursement_DetailsBLL:BaseService<财务_出差报销详细>,ITravelReimbursement_DetailsBLL
    {
        private ITravelReimbursement_DetailsDAL travelReimbursement_DetailsDAL;

        public TravelReimbursement_DetailsBLL(ITravelReimbursement_DetailsDAL travelReimbursement_DetailsDAL)
        {
            this.travelReimbursement_DetailsDAL = travelReimbursement_DetailsDAL ?? throw new ArgumentNullException(nameof(travelReimbursement_DetailsDAL));
            Dal = this.travelReimbursement_DetailsDAL;
        }
    }
}