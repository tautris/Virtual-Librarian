using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using VirtualLibrarian.Domain;

namespace VirtualLibrarian.API.Services
{
    public class BookService
    {
        public Book book { get; set; }

        public void ReviewBook(string comment, double star, Book book)
        {
            this.book = book;

            ParameterizedThreadStart staring = new ParameterizedThreadStart(CountStars);
            ParameterizedThreadStart commenting = new ParameterizedThreadStart(AddComment);

            Thread thread1 = new Thread(staring);
            Thread thread2 = new Thread(commenting);

            thread1.Start(star);
            thread2.Start(comment);

            thread1.Join();
            thread2.Join();
        }

        public void CountStars(object star)
        {
            double stars = book.stars;
            int reviewers = book.reviewers;

            stars = stars * reviewers;
            reviewers++;
            stars = stars + Convert.ToInt32(star);
            stars = stars / reviewers;

            book.stars = stars ;
            book.reviewers = reviewers;
        }

        public void AddComment(object comment)
        {
            book.comments.Add(comment.ToString());
        }
    }
}