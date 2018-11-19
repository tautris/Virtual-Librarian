using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VirtualLibrarian.API.Core;
using VirtualLibrarian.Domain;

namespace VirtualLibrarian.API.Controllers
{
    public class UserController : ApiController
    {
        private static Library instance = null;
        private static readonly object padLock = new object();
        public UserController()
        {
            instance = Library.Instance;
        }

        [HttpGet]
        [Route("GetUsers")]
        public IHttpActionResult GetAvailableBooksSorted()
        {
            List<User> userList = instance.GetAllUsers();

            if (userList == null)
            {
                return NotFound();
            }
            return Ok(userList);
        }
    }
}
