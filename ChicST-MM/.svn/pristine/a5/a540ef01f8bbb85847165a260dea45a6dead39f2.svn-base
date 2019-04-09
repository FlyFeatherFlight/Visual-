using System;
using ChicST_MM.IBLL;
using ChicST_MM.IDAL;
using ChicST_MM.Model;

namespace ChicST_MM.BLL
{
    /// <summary>
    /// 评审记录
    /// </summary>
  public partial  class Review_5S_RcordBLL:BaseService<C5S_评审记录>, IReview_5S_RcordBLL
    {
        private IReview_5S_RcordDAL review_5S_RcordDAL;

        public Review_5S_RcordBLL(IReview_5S_RcordDAL review_5S_RcordDAL)
        {
            this.review_5S_RcordDAL = review_5S_RcordDAL ?? throw new ArgumentNullException(nameof(review_5S_RcordDAL));
            Dal = this.review_5S_RcordDAL;
        }
    }
}
