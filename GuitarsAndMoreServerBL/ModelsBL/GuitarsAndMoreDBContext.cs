using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;


#nullable disable

namespace GuitarsAndMoreServerBL.Models
{
    public partial class GuitarsAndMoreDBContext : DbContext
    {
        public User Login(string email, string pswd)
        {
            User user = this.Users
                .Include(us => us.Posts)
                .Include(uc => uc.UserFavoritePosts)
                .Include(us => us.UserReviewSellers)
                .Include(us => us.UserReviewUsers)
                .Where(u => u.Email == email && u.Pass == pswd).FirstOrDefault();

            return user;
        }

        public List<Post> GetListOfPosts()
        {
            List<Post> p = Posts
                           .Include(c => c.Category)
                           .Include(u => u.User).ToList();
            return p;
        }
    }
}
