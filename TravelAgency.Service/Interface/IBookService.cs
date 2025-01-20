using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace TravelAgency.Service.Interface
{
    public interface IBookService
    {
        List<Book> GetBooks();
        Book GetBook(Guid id);
    }
}
