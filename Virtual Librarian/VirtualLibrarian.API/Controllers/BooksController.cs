using System.Web.Http;
using VirtualLibrarian.API.Core;

namespace VirtualLibrarian.API.Controllers
{
    public class BooksController : ApiController
    {
        private readonly ILibraryManager _library;
        public BooksController(ILibraryManager library)
        {
            _library = library;
        }

        [HttpGet]
        [Route("AvailableBooksSorted")]
        public IHttpActionResult AvailableBooksSorted()
        {
            var sortedAvailableBooks = _library.GetAvailableBooksSorted();

            if (sortedAvailableBooks == null)
            {
                return NotFound();
            }
            return Ok(sortedAvailableBooks);
        }

        [HttpGet]
        [Route("AvailableBookCopies")]
        public IHttpActionResult AvailableBookCopies()
        {
            var availableBookCopies = _library.GetAvailableBookCopies();
            if (availableBookCopies == null)
            {
                return NotFound();
            }

            return Ok(availableBookCopies);
        }

        [HttpGet]
        [Route("AllBooks")]
        public IHttpActionResult AllBooks()
        {
            var allBookEntities = _library.GetAllBooks();
            if (allBookEntities == null)
            {
                return NotFound();
            }
            return Ok(allBookEntities);
        }
        [HttpGet]
        [Route("Book/{id}")]
        public IHttpActionResult Book(int id)
        {
            var book = _library.GetBook(id);
            if (book != null)
            {
                return Ok(book);
            }
            return BadRequest("Bad book id, book not found :(");
        }
        [HttpPut]
        [Route("LikeBook/{id}")]
        public IHttpActionResult LikeBook(int id)
        {
            var book = _library.LikeBook(id);
            if (book != null)
            {
                return Ok(book);
            }
            return BadRequest("Bad book id, book not found :(");
        }
    }
}
