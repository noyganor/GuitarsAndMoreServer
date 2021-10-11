using System;
using System.Collections.Generic;

#nullable disable

namespace GuitarsAndMoreServerBL.Models
{
    public partial class Post
    {
        public Post()
        {
            UserFavoritePosts = new HashSet<UserFavoritePost>();
        }

        public int PostId { get; set; }
        public int? ReviewId { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
        public int? ModelId { get; set; }
        public int TownId { get; set; }
        public double Price { get; set; }
        public string Pdescription { get; set; }
        public string Link { get; set; }

        public virtual Category Category { get; set; }
        public virtual Model Model { get; set; }
        public virtual UserReview Review { get; set; }
        public virtual Town Town { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<UserFavoritePost> UserFavoritePosts { get; set; }
    }
}
