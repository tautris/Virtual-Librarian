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
        private readonly ILibrary _library;
        public UserController(ILibrary library)
        {
            _library = library;
        }

        [HttpGet]
        [Route("GetUsers")]
        public IHttpActionResult GetAvailableBooksSorted()
        {
            List<User> userList = _library.GetAllUsers();

            if (userList == null)
            {
                return NotFound();
            }
            return Ok(userList);
        }
    }
}
