using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuitarsAndMoreServerBL.Models;
using GuitarsAndMoreServer.DTO;

namespace GuitarsAndMoreServer.Controllers
{
    [Route("GuitarsAndMoreAPI")]
    [ApiController]
    public class GuitarsAndMoreController : ControllerBase
    {
        #region Add connection to the db context using dependency injection
        GuitarsAndMoreDBContext context;
        public GuitarsAndMoreController(GuitarsAndMoreDBContext context)
        {
            this.context = context;
        }
        #endregion

        [Route("Login")]
        [HttpGet]
        public User Login([FromQuery] string email, [FromQuery] string pass)
        {
            User user = context.Login(email, pass);

            //Check user name and password
            if (user != null)
            {
                HttpContext.Session.SetObject("theUser", user);

                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;

                //Important! Due to the Lazy Loading, the user will be returned with all of its contects!!
                return user;
            }
            else
            {

                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return null;
            }
        }

        [Route("SignUp")]
        [HttpPost]
        public User SignUp(User u)
        {
            try
            {
                context.Users.Add(u);
                context.SaveChanges();
                return u;
            }

            catch (Exception e)
            {
                return null;
            }
        }

        [Route("GetLookUpTables")]
        [HttpGet]
        public LookUpTables GetLookUpTable()
        {
            try
            {
                LookUpTables tables = new LookUpTables()
                {
                    Genders = context.Genders.ToList(),
                    Areas = context.Areas.ToList(),
                    Models = context.Models.ToList(),
                    ModelReviews = context.ModelReviews.ToList(),
                    Categories = context.Categories.ToList(),
                    Producers = context.Producers.ToList(),
                    Towns = context.Towns.ToList()
                };

                return tables;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            
        }


        [Route("GetPosts")]
        [HttpGet]
        public List<Post> GetListOfPosts()
        {
            try
            {
                return context.GetListOfPosts();
            }

            catch(Exception e)
            {
                return null;
            }
        }

        [Route("AddPostToFavorites")]
        [HttpPost]
        public bool AddPostToUserFavorites([FromBody] Post p, [FromQuery] bool isFavorite)
        {
            
            try
            {
                User theUser;
                theUser = HttpContext.Session.GetObject<User>("theUser");

                if (theUser != null)
                {
                    UserFavoritePost uFavPost = new UserFavoritePost();
                    uFavPost.UserId = theUser.UserId;
                    uFavPost.PostId = p.PostId;
                    
                    if (isFavorite)
                    {
                        context.UserFavoritePosts.Add(uFavPost);
                        
                    }
                    else
                    {
                        context.UserFavoritePosts.Remove(uFavPost);
                    }
                    context.SaveChanges();
                    return true;

                }
                else
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                    return false;
                }
                
            }

            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    
    }
}
