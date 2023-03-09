﻿using Microsoft.AspNetCore.Mvc;
using Mission09_adw97.Models;
using System.Diagnostics;
using System.Linq;
using Mission09_adw97.Models.ViewModels;

namespace Mission09_adw97.Controllers
{
    public class HomeController : Controller
    {
        private IBookstoreRepository repo;

        public HomeController(IBookstoreRepository temp)
        {
            repo = temp;
        }

        public IActionResult Index(string categoryType, int pageNum = 1) //1 is the default in case nothing is passed in
        {
            int pageSize = 10;

            var x = new BooksViewModel
            {
                Books = repo.Books
                .Where(p => p.Category == categoryType || categoryType == null)
                .OrderBy(prop => prop.Title)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize),

                PageInfo = new PageInfo
                {
                    TotalNumBooks = (categoryType == null ? repo.Books.Count() : repo.Books.Where(x => x.Category == categoryType).Count()),
                    BooksPerPage = pageSize,
                    CurrentPage = pageNum
                }
            };

            return View(x);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
