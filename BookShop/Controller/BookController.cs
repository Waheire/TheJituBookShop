using BookShop.Helpers;
using BookShop.Models;
using BookShop.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace BookShop.Controller
{
    internal class BookController
    {
        BookService bookService = new BookService();
        //communicate with service
        public async static Task Init () 
        {
            Console.WriteLine("Hello Welcome to theJitu BookShop");
            Console.WriteLine("1. Add a Book");
            Console.WriteLine("2. View Books");
            Console.WriteLine("3. Update a Book");
            Console.WriteLine("4. Delete a Book");

            var input = Console.ReadLine();
           var validateResults =  Validation.Validate(new List<string> { input });
            if (!validateResults)
            {
                await BookController.Init();
            }
            else
            {
               await new BookController().MenuRedirect(input);
            }
        }
        public async Task MenuRedirect (string id) 
        {
            switch (id) 
            {
                case "1":
                    await AddnewBook();
                    break;
                    case "2":   
                    await ViewBooks();
                    break;
                    case "3":
                   await  UpdateBook();
                    break;
                    case "4":
                    await DeleteBook();
                    break;
                    default: 
                    await BookController.Init();
                    break;
            }
        }

        public async Task ViewBooks()
        {
            try 
            {
                var books = await bookService.GetAllBooksAsync();
                foreach (var book in books) 
                {
                    await Console.Out.WriteLineAsync($"{book.Id}. {book.Title}");
                    Console.WriteLine("View one of the above books ");
                    var id = Console.ReadLine();
                    await ViewOneBook(id);
                }
            } catch (Exception ex) { }
     

        }

        public async Task ViewOneBook(string id)
        {
            try
            {
                var res = await bookService.GetBookAsync(id);
                Console.WriteLine(res.Title);
                Console.WriteLine(res.Description);
                Console.WriteLine(res.Author);
                Console.WriteLine(res.Price);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }


        }

        public async Task AddnewBook()
        {
            Console.WriteLine("Enter book Title");
            var bookTitle = Console.ReadLine();
            Console.WriteLine("Enter book Description");
            var bookDescription = Console.ReadLine();
            Console.WriteLine("Enter book Author");
            var bookAuthor = Console.ReadLine();
            Console.WriteLine("Enter book Price");
            var bookPrice = Console.ReadLine();

            //create an instance 
            var newBook = new AddBook()
            {
                Title = bookTitle,
                Description = bookDescription,
                Author = bookAuthor,
                Price = bookPrice
            };

            //call service 
            try
            {
                //if it goes right
                var res = await bookService.CreateBookAsync(newBook);
                Console.WriteLine(res.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public async  Task UpdateBook()
        {
            await ViewBooks();
            Console.WriteLine("Enter ID of book you want to update: ");
            var Id = Console.ReadLine();
            Console.WriteLine("Enter book Title");
            var bookTitle = Console.ReadLine();
            Console.WriteLine("Enter book Description");
            var bookDescription = Console.ReadLine();
            Console.WriteLine("Enter book Author");
            var bookAuthor = Console.ReadLine();
            Console.WriteLine("Enter book Price");
            var bookPrice = Console.ReadLine();

            //create an instance 
            var updatedBook = new Book()
            {
                Id = Id,
                Title = bookTitle,
                Description = bookDescription,
                Author = bookAuthor,
                Price = bookPrice
            };

            try 
            {
                var res = await bookService.UpdateBookAsync(updatedBook);
                Console.WriteLine(res.Message);
            } catch(Exception ex) 
            {
                Console.WriteLine(ex.Message);

            }
        }
        public async Task DeleteBook()
        {
            await ViewBooks();
            Console.WriteLine("Enter ID of book you want to delete: ");
            var id = Console.ReadLine();
            try
            {
                var res = await bookService.DeleteBookAsync(id);
                Console.WriteLine(res.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }


        }
    }
}
