using ChicST_MM.IBLL;
using ChicST_MM.Model;
using ChicST_MM.IDAL;
using System;

namespace ChicST_MM.BLL
{
    /// <summary>
    /// 5S评审——凭据
    /// </summary>
   public partial class Review_5S_RecordProofBLL:BaseService<C5S_评审记录_凭证>, IReview_5S_RecordProofBLL
    {
        private IReview_5S_RecordProofDAL review_5S_RecordProofDAL;

        public Review_5S_RecordProofBLL(IReview_5S_RecordProofDAL review_5S_RecordProofDAL)
        {
            this.review_5S_RecordProofDAL = review_5S_RecordProofDAL ?? throw new ArgumentNullException(nameof(review_5S_RecordProofDAL));
            Dal = this.review_5S_RecordProofDAL;
        }
    }
}
