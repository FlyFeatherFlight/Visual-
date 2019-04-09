﻿using ChicST_MM.IBLL;
using ChicST_MM.IDAL;
using ChicST_MM.Model;

namespace ChicST_MM.BLL
{
    /// <summary>
    /// 系统用户
    /// </summary>
    public partial class AccountBLL:BaseService<系统用户>,IAccountBLL
    {
        private IAccountDAL accountDAL;

        public AccountBLL(IAccountDAL accountDAL)
        {
            this.accountDAL = accountDAL;
            Dal = accountDAL;
        }
    }
}
