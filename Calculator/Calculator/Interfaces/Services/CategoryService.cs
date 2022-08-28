using Calculator.Data.Entities;
using Calculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calculator.Interfaces.Services
{


    public class CategoryService : ICategoryService
    {
        private CmsContext _context;
        public CategoryService(CmsContext context)
        {
            _context = context;
        }

        public BookListViewModel CreateBook(int cateId, tbl_Post post)
        {
            _context.tbl_Posts.Add(post);
            _context.SaveChanges();
            var genre = _context.tbl_Categories.Find(cateId).BookGenre;
            var bookvm = new BookListViewModel()
            {
                BookDescription = post.BookDescription,
                BookTitle = post.BookTitle,
                GenreTitle = genre
            };
            return bookvm;
        }

        public tbl_Category CreateGenre(tbl_Category bookGenre)
        {
            _context.tbl_Categories.Add(bookGenre);
            _context.SaveChanges();
            return bookGenre;
        }

        public tbl_Category DeleteCategory(int categoryId)
        {
            var category = _context.tbl_Categories.Find(categoryId);
            _context.tbl_Categories.Remove(category);
            _context.SaveChanges();
            return category;
        }

        public IEnumerable<BookListViewModel> GetAllBooks(int categoryId)
        {

            var bookList = _context.tbl_Posts.Where(b => b.GenreId == categoryId).ToList();
            var genreTitle = _context.tbl_Categories.FirstOrDefault(c => c.GenreId == categoryId).BookGenre;
            var list = new List<BookListViewModel>();
            foreach (var item in bookList)
            {
                list.Add(new BookListViewModel()
                {
                    BookId=item.BookId,
                    BookDescription = item.BookDescription,
                    BookTitle = item.BookTitle,
                    GenreTitle = genreTitle
                });
            }

            return list;
        }

        public IEnumerable<tbl_Category> GetAllGenre()
        {
            return _context.tbl_Categories.ToList();
        }

        public tbl_Post GetBookById(int bookId)
        {
            return _context.tbl_Posts.FirstOrDefault(b=>b.BookId==bookId);
        }

        public tbl_Category GetCategoryById(int categoryId)
        {
            return _context.tbl_Categories.FirstOrDefault(c => c.GenreId == categoryId);
        }

        public tbl_Category GetCategoryByBookId(int bookId)
        {
            var catId = _context.tbl_Posts.FirstOrDefault(b=>b.BookId==bookId).GenreId;
            var category = _context.tbl_Categories.Find(catId);
            return category;
        }

        public BookListViewModel UpdateBook(int bookId, tbl_Post post)
        {
            _context.Entry(post).State=Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            var genre = GetCategoryByBookId(bookId).BookGenre;
            var bookvm = new BookListViewModel()
            {
                BookId=post.BookId,
                BookDescription = post.BookDescription,
                BookTitle = post.BookTitle,
                GenreTitle = genre
            };
            return bookvm;
        }

        public tbl_Category UpdateCategory(tbl_Category category)
        {
            _context.tbl_Categories.Update(category);
            _context.SaveChanges();
            return category;

        }

        public void DeleteBook(int bookId)
        {
            var book = GetBookById(bookId);
            _context.tbl_Posts.Remove(book);
            _context.SaveChanges();

        }
    }
}
