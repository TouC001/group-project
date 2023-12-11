using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SoftwareBookList.Data;
using SoftwareBookList.Models;
using System.Net;

public class BookHub : Hub
{
    private readonly DataContext _context;

    public BookHub(DataContext context)
    {
        _context = context;
    }

    public async Task SendBookRemovedNotification(int bookID)
    {
        // Send a message to all connected clients
        await Clients.All.SendAsync("BookRemoved", bookID);
    }

    public async Task RemoveButton(int bookID, int bookListID)
    {
        BookInList bookInList = await _context.BookInLists.FirstOrDefaultAsync(b => b.BookID == bookID && b.BookListID == bookListID);

        if (bookInList != null)
        {
            _context.BookInLists.Remove(bookInList);

            await _context.SaveChangesAsync();

            await _context.RefreshBookRating(bookInList.BookID);

        }

        await Clients.All.SendAsync("ReceiveBookUpdate", bookID, bookListID);
    }
}
