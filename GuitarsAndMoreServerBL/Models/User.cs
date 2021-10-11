using System;
using System.Collections.Generic;

#nullable disable

namespace GuitarsAndMoreServerBL.Models
{
    public partial class User
    {
        public User()
        {
            ModelReviews = new HashSet<ModelReview>();
            Posts = new HashSet<Post>();
            UserFavoritePosts = new HashSet<UserFavoritePost>();
            UserReviewSellers = new HashSet<UserReview>();
            UserReviewUsers = new HashSet<UserReview>();
        }

        public int UserId { get; set; }
        public string Email { get; set; }
        public string Nickname { get; set; }
        public string Pass { get; set; }
        public string VerPassword { get; set; }
        public string PhoneNum { get; set; }
        public int GenderId { get; set; }
        public string FavBand { get; set; }
        public DateTime JoinDate { get; set; }

        public virtual Gender Gender { get; set; }
        public virtual ICollection<ModelReview> ModelReviews { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<UserFavoritePost> UserFavoritePosts { get; set; }
        public virtual ICollection<UserReview> UserReviewSellers { get; set; }
        public virtual ICollection<UserReview> UserReviewUsers { get; set; }
    }
}
