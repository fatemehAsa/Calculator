using Calculator.Data.Entities;
using Calculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Calculator.Interfaces
{
   public interface ICategoryService
    {
        tbl_Category GetCategoryById(int categoryId);
        tbl_Category UpdateCategory(tbl_Category category);
        tbl_Category DeleteCategory(int categoryId);
        IEnumerable<tbl_Category> GetAllGenre();
        tbl_Category CreateGenre(tbl_Category bookGenre);
        IEnumerable<BookListViewModel> GetAllBooks(int categoryId);
        BookListViewModel CreateBook(int cateId,tbl_Post post);
        tbl_Post GetBookById(int bookId);
        tbl_Category GetCategoryByBookId(int bookId);
        BookListViewModel UpdateBook(int bookId, tbl_Post post);
        void DeleteBook(int bookId);
    }
}
