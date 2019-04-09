﻿using System;
using ChicST_MM.IBLL;
using ChicST_MM.IDAL;
using ChicST_MM.Model;

namespace ChicST_MM.BLL
{
    /// <summary>
    /// 出差报销
    /// </summary>
    public partial  class TravelReimbursementBLL:BaseService<财务_出差报销单>, ITravelReimbursementBLL
    {

        private ITravelReimbursementDAL travelReimbursementDAL;

        public TravelReimbursementBLL(ITravelReimbursementDAL travelReimbursementDAL)
        {
            this.travelReimbursementDAL = travelReimbursementDAL ?? throw new ArgumentNullException(nameof(travelReimbursementDAL));
            Dal = this.travelReimbursementDAL;
        }
    }
}
