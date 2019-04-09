using ChicST_MM.Model;
using ChicST_MM.IBLL;
using ChicST_MM.IDAL;
using System;

namespace ChicST_MM.BLL
{
    /// <summary>
    /// C5S_评审标准
    /// </summary>
    public partial class Review_5SBLL:BaseService<C5S_评审标准>,IReview_5SBLL
    {
        private IReview_5SDAL review_5SDAL;

        public Review_5SBLL(IReview_5SDAL review_5SDAL)
        {
            this.review_5SDAL = review_5SDAL ?? throw new ArgumentNullException(nameof(review_5SDAL));
            Dal = this.review_5SDAL;
        }
    }
}
