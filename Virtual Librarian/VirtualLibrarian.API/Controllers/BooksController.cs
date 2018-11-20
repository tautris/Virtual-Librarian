﻿using Newtonsoft.Json;
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
        private static Library library = null;
        private static readonly object padLock = new object();
        public BooksController()
        {
            library = Library.Instance;
        }

        [HttpGet]
        [Route("GetAvailableBooksSorted")]
        public IHttpActionResult GetAvailableBooksSorted()
        {
            List<Book> sortedAvailableBooks = library.GetAvailableBooksSorted();

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
            List<BookCopy> availableBookCopies = library.GetAvailableBookCopies();
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
            List<Book> allBookEntities = library.GetAllBooks();
            if (allBookEntities == null)
            {
                return NotFound();
            }
            return Ok(allBookEntities);
        }

        [HttpPut]
        [Route("LikeBook/{id}")]
         public IHttpActionResult LikeBook(int id)
        {
            Book book = library.LikeBook(id);
            if (book != null)
            {
                return Ok(book);
            } 
            return BadRequest("Bad book id, book not found :(");
        }
    }
}
