using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuitarsAndMoreServerBL.Models;

namespace GuitarsAndMoreServer.DTO
{
    public class LookUpTables
    {
        public List<Gender> Genders { get; set; }
        public List<Area> Areas { get; set; }
        public List<Town> Towns { get; set; }
        public List<Producer> Producers { get; set; }
        public List<Model> Models { get; set; }
        public List<Category> Categories { get; set; }
        public List<ModelReview> ModelReviews { get; set; }
        public List<UserFavoritePost> UserFavoritePosts { get; set; }
    }
}
