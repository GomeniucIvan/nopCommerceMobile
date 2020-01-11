using System;
using System.Collections.Generic;
using nopCommerceMobile.Models.Base;

namespace nopCommerceMobile.Models.News
{
    public class NewsItemModel : BaseModel
    {
        public NewsItemModel()
        {
            Comments = new List<NewsCommentModel>();
        }

        public string Title { get; set; }
        public string Short { get; set; }
        public string Full { get; set; }
        public bool AllowComments { get; set; }
        public int NumberOfComments { get; set; }
        public DateTime CreatedOn { get; set; }

        public IList<NewsCommentModel> Comments { get; set; }
    }

    #region Nested Classes

    public class NewsCommentModel : BaseModel
    {
        public int CustomerId { get; set; }

        public string CustomerName { get; set; }

        public string CustomerAvatarUrl { get; set; }

        public string CommentTitle { get; set; }

        public string CommentText { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool AllowViewingProfiles { get; set; }
    }

    #endregion
}
