using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Library.Repository;
using TravelAgency.Service.Interface;

namespace TravelAgency.Web.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService _service;

        public BooksController(IBookService service)
        {
            _service = service;
        }

        // GET: Books
        public IActionResult Index()
        {
            return View(_service.GetBooks());
        }

        // GET: Books/Details/5
        public IActionResult Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = _service.GetBook(id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }




    }
}
