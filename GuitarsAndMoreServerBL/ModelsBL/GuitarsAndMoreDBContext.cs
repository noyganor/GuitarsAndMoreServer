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
                .Include(g => g.Gender)
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

        public List<Model> GetListOfModels()
        {
            List<Model> p = Models.ToList();
            //.Include(p => p.ProducerId)
            //.Include(m => m.ModelId)
            //.Include(u => u.ModelName).ToList();
            return p;
        }
        public User UpdateUserDetalis(User user, User updatedUser)
        {
            try
            {
                User currentUser = this.Users
                 .Include(us => us.Posts)
                .Include(uc => uc.UserFavoritePosts)
                .Include(us => us.UserReviewSellers)
                .Include(us => us.UserReviewUsers)
                .Include(g => g.Gender)
                .Where(u => u.UserId == user.UserId).FirstOrDefault();

                currentUser.Pass = updatedUser.Pass;
                currentUser.Email = currentUser.Email;
                currentUser.PhoneNum = updatedUser.PhoneNum;
                currentUser.Nickname = updatedUser.Nickname;
                currentUser.Gender = updatedUser.Gender;
                currentUser.FavBand = updatedUser.FavBand;

                this.ChangeTracker.Clear();
                this.Entry(currentUser).State = EntityState.Modified;
                this.SaveChanges();
                return currentUser;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
