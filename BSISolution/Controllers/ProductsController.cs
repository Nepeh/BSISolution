using BSISolution.Data;
using BSISolutions.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSISolution.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ProductsController(ApplicationDbContext context)
        {
            _db = context;
        }
        public IActionResult Index()
        {
            var resulList = _db.Products.ToList();
            return View(resulList);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                var resulList =  _db.Products.ToList();
            }

            return RedirectToAction("Index", new {  });
        }
    }
}
