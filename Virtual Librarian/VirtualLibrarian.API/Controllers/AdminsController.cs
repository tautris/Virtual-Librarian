using System.Web.Http;
using VirtualLibrarian.API.Core;

namespace VirtualLibrarian.API.Controllers
{
    public class AdminsController : ApiController
    {
        private readonly ILibraryManager _library;
        public AdminsController(ILibraryManager library)
        {
            _library = library;
        }

        [HttpGet]
        [Route("AllAdmins")]
        public IHttpActionResult AllAdmins()
        {
            var admins = _library.GetAllAdmins();
            if (admins == null)
            {
                return NotFound();
            }
            return Ok(admins);
        }

        /*


        // GET: api/Admins
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Admins/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Admins
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Admins/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Admins/5
        public void Delete(int id)
        {
        }
        */
    }
}
