using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuitarsAndMoreServerBL.Models;
using GuitarsAndMoreServer.DTO;
using System.IO;

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
                HttpContext.Session.SetObject("theUser", u);
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
                    //UserFavoritePosts = context.UserFavoritePosts.ToList(),
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

            catch (Exception e)
            {
                return null;
            }
        }

        [Route("GetModels")]
        [HttpGet]
        public List<Model> GetListOfModels()
        {
            try
            {
                return context.GetListOfModels();
            }

            catch (Exception e)
            {
                return null;
            }
        }

        [Route("AddPostToFavorites")]
        [HttpGet]
        public bool AddPostToUserFavorites([FromQuery] int postID)
        {

            try
            {
                User theUser;
                theUser = HttpContext.Session.GetObject<User>("theUser");

                if (theUser != null)
                {
                    UserFavoritePost uFavPost = new UserFavoritePost();
                    uFavPost.UserId = theUser.UserId;
                    uFavPost.PostId = postID;

                    try
                    {
                        
                        context.UserFavoritePosts.Remove(uFavPost);
                        context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        context.UserFavoritePosts.Add(uFavPost);
                        context.SaveChanges();
                    }
                    return true;

                }
                else
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                    return false;
                }

            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        [Route("UploadImage")]
        [HttpPost]

        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            User user = HttpContext.Session.GetObject<User>("theUser");
            //Check if user logged in and its ID is the same as the contact user ID
            if (user != null)
            {
                if (file == null)
                {
                    return BadRequest();
                }

                try
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", file.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }


                    return Ok(new { length = file.Length, name = file.FileName });
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return BadRequest();
                }
            }
            return Forbid();
        }

        [Route("AddPost")]
        [HttpPost]
        public Post AddPost(Post p)
        {
            try
            {
                context.Posts.Add(p);
                context.SaveChanges();
                return p;
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        [Route("DeletePost")]
        [HttpDelete]
        public bool DeletePost([FromQuery] int postId)
        {
            try
            {

                Post p = context.Posts.Where(t => t.PostId == postId).FirstOrDefault();
                if (p != null)
                {
                    context.Posts.Remove(p);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }

            catch (Exception e)
            {
                return false;
            }
        }

        [Route("UpdateUserDetails")]
        [HttpPost]
        public User UpdateUser([FromBody] User user)
        {
            try
            {
                //If user is null the request is bad
                if (user == null)
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                    return null;
                }

                User currentUser = HttpContext.Session.GetObject<User>("theUser");
                //Check if user logged in and its ID is the same as the contact user ID
                if (currentUser != null && currentUser.UserId == user.UserId)
                {
                    User updatedUser = context.UpdateUserDetalis(currentUser, user);

                    if (updatedUser == null)
                    {
                        Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                        return null;
                    }

                    Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                    return updatedUser;


                }
                else
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                    return null;
                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        [Route("FindGender")]
        [HttpGet]
        public string FindGender([FromQuery] int genderId)
        {
            try
            {
                Gender gender = context.Genders.Where(g => g.GenderId == genderId).FirstOrDefault();
                return gender.Gender1;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
