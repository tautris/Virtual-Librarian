using System.Collections.Generic;
using System.Web.Http;
using VirtualLibrarian.Domain;
using VirtualLibrarian.API.Core;
using System;

namespace VirtualLibrarian.API.Controllers
{
    public class AdminsController : ApiController
    {
        private readonly ILibrary _library;
        public AdminsController(ILibrary library)
        {
            _library = library;
        }

        [HttpGet]
        [Route("GetAllAdmins")]
        public IHttpActionResult GetAllAdmins()
        {
            List<Admin> admins = _library.GetAllAdmins();
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
