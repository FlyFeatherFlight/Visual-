using System;
using ChicST_MM.IBLL;
using ChicST_MM.IDAL;
using ChicST_MM.Model;

namespace ChicST_MM.BLL
{
    /// <summary>
    /// 商务会谈事项
    /// </summary>
    public partial class BusinessReception_MattersBLL:BaseService<营销_接待来访主要事项>,IBusinessReception_MattersBLL
    {
        private IBusinessReception_MattersDAL businessReception_MattersDAL;

        public BusinessReception_MattersBLL(IBusinessReception_MattersDAL businessReception_MattersDAL)
        {
            this.businessReception_MattersDAL = businessReception_MattersDAL ?? throw new ArgumentNullException(nameof(businessReception_MattersDAL));
            Dal = this.businessReception_MattersDAL;
        }
    }
}
