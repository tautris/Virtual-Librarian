using System.Web.Http;
using VirtualLibrarian.API.Core;

namespace VirtualLibrarian.API.Controllers
{
    public class UserController : ApiController
    {
        private readonly ILibraryManager _library;
        public UserController(ILibraryManager library)
        {
            _library = library;
        }

        [HttpGet]
        [Route("Users")]
        public IHttpActionResult Users()
        {
            var userList = _library.GetAllUsers();

            if (userList == null)
            {
                return NotFound();
            }
            return Ok(userList);
        }
    }
}
