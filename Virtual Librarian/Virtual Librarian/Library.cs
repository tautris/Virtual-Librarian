﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Virtual_Librarian
{
    public sealed class Library
    {
        private static Library instance = null;
        private static readonly object padLock = new object();
        Library()
        {
            allBooks = FileReaderWriter.Instance.GetBooks();
            List<BookCopy> bookCopies = new List<BookCopy>();
            bookCopies = FileReaderWriter.Instance.GetBookCopies();
            foreach (BookCopy bookCopy in bookCopies)
            {
                allBooks.First(obj => obj.ISBN == bookCopy.book.ISBN).AddBookCopy(bookCopy);
            }
        }
        public static Library Instance
        {
            get
            {
                lock (padLock)
                {
                    if (instance == null)
                    {
                        instance = new Library();
                    }
                    return instance;
                }
            }
        }

        private List<Book> allBooks = new List<Book>();
        public void AddBook(Book book)
        {
            allBooks.Add(book);
        }
        public void AddBookCopy(BookCopy bookCopy, Book book)
        {
            if (!allBooks.Contains(book))
            {
                AddBook(book);
            }
            book.AddBookCopy(bookCopy);
        }
        public void RemoveBook(Book book)
        {
            allBooks.Remove(book);
        }
        public void RemoveBookCopy(BookCopy bookCopy, Book book)
        {
            book.RemoveBookCopy(bookCopy);
        }
        public List<Book> GetAvailableBooksList()
        {
            List<Book> availableBooks = new List<Book>();
            foreach (Book book in allBooks)
            {
                foreach (BookCopy bookCopy in book.copies)
                {
                    if (bookCopy.IsAvailable() && !availableBooks.Contains(book))
                    {
                        availableBooks.Add(book);
                    }
                }
            }
            List<Book> SortedAvailableBooks = availableBooks.OrderBy(o => o.authorSurname).ToList();
            return availableBooks;
        }

        public List<Book> GetAllBooks()    
        {
            return allBooks;
        }
        public List<BookCopy> GetAllBookCopies()
        {
            List<BookCopy> availableBookCopies = new List<BookCopy>();
            List<Book> availableBooks = new List<Book>();
            availableBooks = GetAvailableBooksList();
            foreach (Book book in availableBooks)
            {
                foreach (BookCopy bookCopy in book.copies)
                {
                    if (bookCopy.IsAvailable())
                    {
                        availableBookCopies.Add(bookCopy);
                    }
                }
            }
            return availableBookCopies;
        }
    }
}
