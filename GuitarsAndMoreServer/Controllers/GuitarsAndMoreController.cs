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

            catch(Exception e)
            {
                return null;
            }
            
        }

        [Route("GetLookUpTables")]
        [HttpGet]
        public LookUpTables GetLookUpTable()
        {
            LookUpTables tables = new LookUpTables()
            {
                Genders = context.Genders.ToList(),
                Areas = context.Areas.ToList(),
                Models = context.Models.ToList(),
                Producers = context.Producers.ToList(),
                Towns = context.Towns.ToList()
            };

            return tables;
        }
    }
}
