using System;
using ChicST_MM.IBLL;
using ChicST_MM.IDAL;

namespace ChicST_MM.BLL
{
    /// <summary>
    /// 5S评审记录详细
    /// </summary>
  public partial   class Review_5S_RecordDetailsBLL:BaseService<C5S_评审记录详细>, IReview_5S_RecordDetailsBLL
    {
        private IReview_5S_RecordDetailsDAL review_5S_RecordDetailsDAL;

        public Review_5S_RecordDetailsBLL(IReview_5S_RecordDetailsDAL review_5S_RecordDetailsDAL)
        {
            this.review_5S_RecordDetailsDAL = review_5S_RecordDetailsDAL ?? throw new ArgumentNullException(nameof(review_5S_RecordDetailsDAL));
            Dal = this.review_5S_RecordDetailsDAL;
        }
    }
}
