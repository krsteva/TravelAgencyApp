using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Library.Repository.Interface
{
    public interface IBooksRepository
    {
        List<Book> GetAllBooks();
        Book GetBook(Guid id);
    }
}
