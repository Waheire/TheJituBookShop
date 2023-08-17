using BookShop.Models;
using BookShop.Service.IServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BookShop.Service
{
    internal class BookService : IBookInterface
    {
        private readonly HttpClient _httpClient;
        private readonly string  _url  = "http://localhost:3000/books";
        public BookService()
        {
           _httpClient = new HttpClient();
        }
        //Communicating with database
        public async Task<SuccessMessage> CreateBookAsync(AddBook book)
        {
            var content  = JsonConvert.SerializeObject(book);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_url, bodyContent);

            if (response.IsSuccessStatusCode) {
                return new SuccessMessage { Message = "Book created successfully" };
            }
            throw new NotImplementedException();
        }

        public async Task<SuccessMessage> DeleteBookAsync(string id)
        {
            var response = await _httpClient.DeleteAsync(_url + "/"+id);
            if (response.IsSuccessStatusCode)
            {
                return new SuccessMessage { Message = "Book Deleted successfully" };
            }
            throw new Exception("Failed to delete book");

        }

        public async Task<List<Book>> GetAllBooksAsync()
        {
            var response = await _httpClient.GetAsync(_url);
            var books = JsonConvert.DeserializeObject<List<Book>>(await response.Content.ReadAsStringAsync());
            if (response.IsSuccessStatusCode)
            {
                return books;
            }
            throw new Exception("Failed to get books");
        }

        public async Task<Book> GetBookAsync(string id)
        {
            var response = await _httpClient.GetAsync(_url + "/" + id);
            var book = JsonConvert.DeserializeObject<Book>(await response.Content.ReadAsStringAsync());
            if (response.IsSuccessStatusCode)
            {
                return book;
            }
            throw new Exception("Failed to get a book");
        }

        public async Task<SuccessMessage> UpdateBookAsync(Book book)
        {
            var content = JsonConvert.SerializeObject(book);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(_url + "/" + book.Id, bodyContent);
            if (response.IsSuccessStatusCode)
            {
                return new SuccessMessage { Message = "Book Updated successfully" };
            }
            throw new Exception("Failed to update book");
        }
    }
}
