using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Library.Repository.Interface;
using TravelAgency.Service.Interface;

namespace TravelAgency.Service.Implementation
{
    public class BookService : IBookService
    {
        private readonly IBooksRepository repository;

        public BookService(IBooksRepository repository)
        {
            this.repository = repository;
        }

        public Book GetBook(Guid id)
        {
            return repository.GetBook(id);
        }

        public List<Book> GetBooks()
        {
            return repository.GetAllBooks();
        }


    }
}
