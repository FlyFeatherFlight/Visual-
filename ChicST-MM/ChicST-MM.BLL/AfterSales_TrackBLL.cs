﻿using System;
using ChicST_MM.IBLL;
using ChicST_MM.IDAL;
using ChicST_MM.Model;

namespace ChicST_MM.BLL
{
    /// <summary>
    /// 售后跟进
    /// </summary>
  public partial  class AfterSales_TrackBLL:BaseService<客服_售后跟进日志>, IAfterSales_TrackBLL
    {
        private IAfterSales_TrackDAL afterSales_TrackDAL;

        public AfterSales_TrackBLL(IAfterSales_TrackDAL afterSales_TrackDAL)
        {
            this.afterSales_TrackDAL = afterSales_TrackDAL ?? throw new ArgumentNullException(nameof(afterSales_TrackDAL));
            Dal = this.afterSales_TrackDAL;
        }
    }
}