using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuitarsAndMoreServerBL.Models;
using GuitarsAndMoreServer.DTO;
using System.IO;
using Microsoft.EntityFrameworkCore;

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
                Console.Write(e.Message);
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
        //Get list of all posts from DB
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
        //Get list of all models from DB
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
        //Remove or Add post to user favorites
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
                        //try to remove the post from user favorites
                        context.UserFavoritePosts.Remove(uFavPost);
                        context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        //if there was an exception - the post would be added to user favorites
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
        //Upload Image
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            bool isRegistered = false;
            User user = HttpContext.Session.GetObject<User>("theUser");
            //Check if user logged in and its ID is the same as the contact user ID
            if (user != null || isRegistered)
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
        //Add post to DB
        public Post AddPost(Post p)
        {
            User user = HttpContext.Session.GetObject<User>("theUser");
            //Check if user logged in and its ID is the same as the contact user ID
            if (user != null)
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
            return null;
        }

        [Route("EditPost")]
        [HttpPost]
        //Add post to DB
        public Post EditPost(Post p)
        {
            User user = HttpContext.Session.GetObject<User>("theUser");
            //Check if user logged in and its ID is the same as the contact user ID
            if (user != null)
            {
                try
                {
                    context.Entry(p).State = EntityState.Modified;
                    context.SaveChanges();
                    return p;
                }

                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
            }
            return null;
        }

        [Route("DeletePost")]
        [HttpDelete]
        public bool DeletePost([FromQuery] int postId)
        {

            try
            {
                User user = HttpContext.Session.GetObject<User>("theUser");
                //Check if user logged in and its ID is the same as the contact user ID
                if (user != null)
                {
                    Post p = context.Posts.Include(pp => pp.UserFavoritePosts).Where(t => t.PostId == postId).FirstOrDefault();
                    if (p != null)
                    {
                        context.ChangeTracker.Clear();
                        foreach (UserFavoritePost ufp in p.UserFavoritePosts)
                        {
                            context.Entry(ufp).State = EntityState.Deleted;
                        }
                        context.Entry(p).State = EntityState.Deleted;
                        context.SaveChanges();

                        try
                        {
                            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", $"{postId}.jpg");
                            System.IO.File.Delete(path);
                        }
                        catch { }
                        return true;
                    }
                    return false;
                }
                return false;
            }

            catch (Exception e)
            {
                return false;
                // return BadRequest();
            }
            // return Forbid();
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
        //Get gender name where genderid is the same
        public string FindGender([FromQuery] int genderId)
        {
            try
            {
                User user = HttpContext.Session.GetObject<User>("theUser");
                //Check if user logged in and its ID is the same as the contact user ID
                if (user != null)
                {
                    Gender gender = context.Genders.Where(g => g.GenderId == genderId).FirstOrDefault();
                    return gender.Gender1;
                }
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        [Route("SetManager")]
        [HttpGet]
        public bool SetManager([FromQuery] string email)
        {
            try
            {
                User user = HttpContext.Session.GetObject<User>("theUser");
                //Check if user logged in and its ID is the same as the contact user ID
                if (user != null)
                {
                    User foundUser = context.Users.Where(u => u.Email == email).FirstOrDefault();
                    if (foundUser != null)
                    {
                        foundUser.IsManager = true;
                        context.SaveChanges();
                        return true;
                    }

                }
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        [Route("CheckEmailExistance")]
        [HttpGet]
        public bool CheckEmailExistance([FromQuery] string email)
        {
            try
            {



                return context.EmailExist(email);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                return false;
            }
        }
    }
}
