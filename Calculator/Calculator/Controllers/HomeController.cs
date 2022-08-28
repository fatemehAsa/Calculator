using Calculator.Data.Entities;
using Calculator.Interfaces;
using Calculator.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Calculator.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ICategoryService _categoryService;



        public HomeController(ILogger<HomeController> logger, ICategoryService categoryService)
        {
            _logger = logger;
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            var list = _categoryService.GetAllGenre();
            return View(list);
        }

        public IActionResult BookLists(int cateId)
        {
            var books = _categoryService.GetAllBooks(cateId);
            ViewData["CategoryId"] = cateId;
            return View(books);
        }

        public IActionResult Edit(int cateId)
        {
            var category = _categoryService.GetCategoryById(cateId);

            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(tbl_Category category)
        {
            _categoryService.UpdateCategory(category);
            return RedirectToAction("Index");
        }


        public IActionResult CreateBook(int id)
        {
            var cat = _categoryService.GetCategoryById(id).BookGenre;
            ViewData["Category"] = cat;
            ViewData["CategoryId"] = id;
            return View();
        }


        [HttpPost]
        public IActionResult CreateBook(int id, tbl_Post post)
        {
            var book = _categoryService.CreateBook(id, post);
            return RedirectToAction("BookLists", new { cateId = id });
        }
        public IActionResult Delete(int cateId)
        {
            _categoryService.DeleteCategory(cateId);
            return RedirectToAction("Index");

        }

        public IActionResult EditBook(int id)
        {
            var book = _categoryService.GetBookById(id);
            var catName = _categoryService.GetCategoryByBookId(id);
            int catId = _categoryService.GetCategoryByBookId(id).GenreId;
            ViewData["CategoryName"] = catName;
            ViewData["GenreId"] = catId;
            return View(book);
        }

        [HttpPost]
        public IActionResult EditBook(int id, tbl_Post post)
        {
            _categoryService.UpdateBook(id, post);
            int categoryId = _categoryService.GetCategoryByBookId(id).GenreId;
            return RedirectToAction("BookLists", new { cateId = categoryId });
        }

        public IActionResult Details(int id)
        {
            var book = _categoryService.GetBookById(id);
            var categoryName = _categoryService.GetCategoryByBookId(id).BookGenre;
            int catId = _categoryService.GetCategoryByBookId(id).GenreId;
            ViewData["GenreName"] = categoryName;
            ViewData["GenreId"] = catId;
            return View(book);
        }


        public IActionResult DeleteBook(int id)
        {
            int categoryId = _categoryService.GetCategoryByBookId(id).GenreId;
            _categoryService.DeleteBook(id);

            return RedirectToAction("BookLists", new { cateId = categoryId });
        }

       

        [HttpPost]
        public JsonResult CaculateData(string data)
        {
            ErrorClass err = new ErrorClass();
            if (data.Trim().StartsWith("/") || data.Trim().StartsWith("*"))
            {
                err.ErrorId = -1;
                err.ErrorText = "Invalid Operation";
                return Json(err);
            }

            var loDataTable = new DataTable();
            var loDataColumn = new DataColumn("Eval", typeof(double), data);
            loDataTable.Columns.Add(loDataColumn);
            loDataTable.Rows.Add(0);
            var d = (double)(loDataTable.Rows[0]["Eval"]);
            err.ErrorId = 0;
            err.ErrorText = d.ToString("0.######");
            return Json(err);
        }


        public class ErrorClass
        {
            public int ErrorId { get; set; }
            public String ErrorText { get; set; }
        }
       
    }
}
