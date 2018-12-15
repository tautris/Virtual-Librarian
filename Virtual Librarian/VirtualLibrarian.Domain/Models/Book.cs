using System;
using System.Collections.Generic;

namespace VirtualLibrarian.Domain.Models
{
    public class Book : Base
    {
        public string Author { get; }
        public string Title { get; }
        public string Description { get; }
        public int Likes { get; set; }
        public string Pdf { get; set; }
        public string Image { get; set; }
        public string ISBN { get; }
        public DateTimeOffset Date { get; set; }
        public ICollection<BookCopy> Copies { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public int ReviewerCount { get; set; }
        public double Star { get; set; }
    }
}
