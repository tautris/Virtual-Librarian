using Newtonsoft.Json;
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
    public class BooksController : ApiController
    {
        private static Library instance = null;
        private static readonly object padLock = new object();
        public BooksController()
        {
            instance = Library.Instance;
        }

        [HttpGet]
        [Route("GetAvailableBooksSorted")]
        public IHttpActionResult GetAvailableBooksSorted()
        {
            List<Book> sortedAvailableBooks = instance.GetAvailableBooksSorted();

            if(sortedAvailableBooks == null)
            {
                return NotFound();
            }
            return Ok(sortedAvailableBooks);
        }

        [HttpGet]
        [Route("GetAvailableBookCopies")]
        public IHttpActionResult GetAvailableBookCopies()
        {
            List<BookCopy> availableBookCopies = instance.GetAvailableBookCopies();
            if(availableBookCopies == null)
            {
                return NotFound();
            }

            return Ok(availableBookCopies);
        }

        [HttpGet]
        [Route("GetAllBooks")]
        public IHttpActionResult GetAllBooks()
        {
            List<Book> allBookEntities = instance.GetAllBooks();
            if (allBookEntities == null)
            {
                return NotFound();
            }
            return Ok(allBookEntities);
        }
    }
}
