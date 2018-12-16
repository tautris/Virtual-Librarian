using System;
using System.Collections.Generic;

namespace VirtualLibrarian.Domain.Models
{
    public class Book : Base
    {
        public string Author { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? Likes { get; set; }
        public string Pdf { get; set; }
        public string Image { get; set; }
        public string ISBN { get; set;}
        public DateTime DateWritten { get; set; }
        public ICollection<BookCopy> Copies { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public int ReviewerCount { get; set; }
        public double Star { get; set; }
    }
}
