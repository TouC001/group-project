﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftwareBookList.Data;
using SoftwareBookList.GoogleBooks;
using SoftwareBookList.Model_View;
using SoftwareBookList.Models;
using SoftwareBookList.Services;
using System.Net;
using System.Security.Claims;

namespace SoftwareBookList.Controllers
{
    public class BooksController : Controller
    {
        private readonly DataContext _context;

        private readonly GoogleBooksService _googleBooksService;
        private readonly BookMappingService _bookMappingService;
        private readonly AddBooksService _addBooksService;

        public BooksController(DataContext context, GoogleBooksService googleBooksService, BookMappingService bookMappingService)
        {
            _context = context;
            _googleBooksService = googleBooksService;
            _bookMappingService = bookMappingService;
            _addBooksService = new AddBooksService(context);
        }

        public bool CheckIfBookIsAdded(int bookID)
        {
            // Check if a record with the given bookID and user ID exists in the BookInList table
            bool isAdded = _context.BookInLists.Any(bil => bil.BookID == bookID);

            return isAdded;
        }

        public IActionResult Books(int page = 1)
        {

            int pageSize = 50; // Display 50 books per page

            IQueryable<Book> allBooksQuery = _context.Books.AsQueryable();
            // This orders the book on the page by the ratings.
            allBooksQuery = allBooksQuery.Include(b => b.BookInLists).Include(b => b.Reviews).OrderByDescending(b => b.TotalScore());
            // Apply any filtering or sorting operations you need here
            // Example: allBooksQuery = allBooksQuery.Where(b => b.Title.Contains("Software")).OrderBy(b => b.Title);
            // Create a paginated list
            BookPaginatedList<Book> paginatedList = new BookPaginatedList<Book>(allBooksQuery, page, pageSize);
            // Get the list of books for the current page
            List<Book> books = paginatedList.Books.ToList();
            // Creating a Dictionary to store whether each book is already added
            Dictionary<int, bool> bookAlreadyAddedMap = new Dictionary<int, bool>();
            // Loop through the list of books and check if each one is already added to the user's list
            foreach (var book in books)
            {
                int bookID = book.BookID;
                bool isBookAlreadyAdded = CheckIfBookIsAdded(bookID);
                // Store the result in the dictionary, using the bookID as the key
                bookAlreadyAddedMap[bookID] = isBookAlreadyAdded;
            }
            // Pass the dictionary to the view using ViewData, so it can be available in the Razor view
            ViewData["BookAlreadyAddedMap"] = bookAlreadyAddedMap;
            // Return the paginated list to the view
            return View(paginatedList);
        }

        public async Task<IActionResult> BookDetails(string googleID)
        {
            GoogleBook googleBook = await _googleBooksService.GetSingleBookAsync(googleID);

            if (googleBook == null)
            {
                return NotFound();
            }

            return View(googleBook);
        }

        [HttpPost("AddToList")]
        public IActionResult AddBook(AddBookViewModel addBookViewModel)
        {

            if (!ModelState.IsValid)
            {
                return View(addBookViewModel);
            }


            if (User.Identity.IsAuthenticated)
            {
                int userID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

                int bookListID = _addBooksService.GetBookListIDForUser(userID);

                if (bookListID != 0)
                {

                    BookInList bookInList = new BookInList
                (
                    addBookViewModel.BookID,
                    addBookViewModel.StatusID,
                    bookListID,
                    addBookViewModel.RatingValue
                );

                    try
                    {
                        _context.BookInLists.Add(bookInList);

                        _context.SaveChanges();


                        return RedirectToAction("Books");
                    }
                    catch (Exception ex)
                    {
                        return RedirectToAction("Books");
                    }

                }
            }
            return RedirectToAction("Books");
        }

        [HttpPost("EditBook")]
        public IActionResult EditBook(EditBookViewModel editBookViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(editBookViewModel);
            }

            if (User.Identity.IsAuthenticated)
            {
                int userID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

                int bookListID = _addBooksService.GetBookListIDForUser(userID);

                BookInList oldBookInList = _context.BookInLists.FirstOrDefault(bil => bil.BookID == editBookViewModel.BookID);

                if (oldBookInList != null)
                {

                    //Creating a New BookInList Object with updated Status and RatingValue
                    BookInList newBookInList = new BookInList
                    (
                        oldBookInList.BookID,
                        editBookViewModel.StatusID,
                        bookListID,
                        editBookViewModel.RatingValue

                    );

                    _context.BookInLists.Remove(oldBookInList);

                    _context.BookInLists.Add(newBookInList);

                    _context.SaveChanges();

                    return Redirect("Account");
                }

            }

            return View(editBookViewModel);
        }

        [HttpPost("RemoveBook")]
        public IActionResult RemoveBook(int bookID)
        {
            if (!ModelState.IsValid)
            {
                return View("Account");
            }

            if (User.Identity.IsAuthenticated)
            {
                BookInList bookInList = _context.BookInLists.FirstOrDefault(bil => bil.BookID == bookID);

                if (bookInList != null)
                {
                    _context.BookInLists.Remove(bookInList);

                    _context.SaveChanges();

                    return Redirect("Account");
                }
            }

            return View("Account");

        }
    }
}
