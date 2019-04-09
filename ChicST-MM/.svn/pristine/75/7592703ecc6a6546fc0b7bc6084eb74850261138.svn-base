using System;
using ChicST_MM.IBLL;
using ChicST_MM.IDAL;
using ChicST_MM.Model;

namespace ChicST_MM.BLL
{
    /// <summary>
    /// 财务机票详细
    /// </summary>
   public partial  class AirFaresBLL:BaseService<财务_机票明细>, IAirFaresBLL
    {
        private IAirFaresDAL airFaresDAL;

        public AirFaresBLL(IAirFaresDAL airFaresDAL)
        {
            this.airFaresDAL = airFaresDAL ?? throw new ArgumentNullException(nameof(airFaresDAL));
            Dal = this.airFaresDAL;
        }
    }
}
