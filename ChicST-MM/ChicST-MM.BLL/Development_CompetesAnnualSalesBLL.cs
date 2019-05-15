using ChicST_MM.IBLL;
using ChicST_MM.IDAL;
using System;
using ChicST_MM.Model;

namespace ChicST_MM.BLL
{
    /// <summary>
    /// 拓展_竞品_年度销售档案
    /// </summary>
   public partial class Development_CompetesAnnualSalesBLL:BaseService<拓展_竞品档案_年销售档案表>, IDevelopment_CompetesAnnualSalesBLL
    {

        private IDevelopment_CompetesAnnualSalesDAL development_CompetesAnnualSalesDAL;

        public Development_CompetesAnnualSalesBLL(IDevelopment_CompetesAnnualSalesDAL development_CompetesAnnualSalesDAL)
        {
            this.development_CompetesAnnualSalesDAL = development_CompetesAnnualSalesDAL ?? throw new ArgumentNullException(nameof(development_CompetesAnnualSalesDAL));
            Dal = development_CompetesAnnualSalesDAL;
        }
    }
}
