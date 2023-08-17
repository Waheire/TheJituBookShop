using BookShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Service.IServices
{
    internal interface IBookInterface
    {
       //write the structure if all the methods I will have on my service
       //Add a book to DB
        Task<SuccessMessage> CreateBookAsync(AddBook book);
        //Update a Book
        Task<SuccessMessage> UpdateBookAsync(Book book);
        //Delete a Book
        Task<SuccessMessage> DeleteBookAsync(string id);
        //Get one Book
        Task<Book> GetBookAsync(string id);
        //Get all Books
        Task<List<Book>> GetAllBooksAsync();


    }
}
