using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Library.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Library.Repository.Implementation
{
    public class BookRepository : IBooksRepository
    {
        private readonly LibraryDbContext _context;

        public BookRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public List<Book> GetAllBooks()
        {
            return _context.Books?.Include(s => s.Publisher)?.Include(s => s.Author)?.AsEnumerable()?.ToList();
        }

        public Book GetBook(Guid id)
        {
            return _context.Books?.Include(s => s.Publisher)?.Include(s => s.Author)?.First(s => s.Id == id);
        }
    }
}
