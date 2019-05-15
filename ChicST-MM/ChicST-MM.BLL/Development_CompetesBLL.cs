using ChicST_MM.IBLL;
using ChicST_MM.IDAL;
using System;
using ChicST_MM.Model;
namespace ChicST_MM.BLL
{
    /// <summary>
    /// 拓展_竞品档案
    /// </summary>
    public partial class Development_CompetesBLL:BaseService<拓展_竞品档案表>, IDevelopment_CompetesBLL
    {
        private IDevelopment_CompetesDAL development_CompetesDAL;

        public Development_CompetesBLL(IDevelopment_CompetesDAL development_CompetesDAL)
        {
            this.development_CompetesDAL = development_CompetesDAL ?? throw new ArgumentNullException(nameof(development_CompetesDAL));
            Dal = development_CompetesDAL;
        }
    }
}
