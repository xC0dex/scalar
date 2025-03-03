using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace BookHub.Books;

[ApiController]
[Route("v{version:apiVersion}/books-controller")]
[ApiVersion(1)]
[ApiVersion(2)]
[Tags("controller")]
public sealed class BookController(BookStore bookStore) : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(bookStore.GetAll());
    }

    [HttpGet("{bookId:guid}")]
    public IActionResult GetById(Guid bookId)
    {
        var book = bookStore.GetById(bookId);
        return book is null ? NotFound() : Ok(book);
    }

    [HttpPost]
    public IActionResult Add(Book myCustomBook)
    {
        var createdBook = bookStore.Add(myCustomBook);
        return createdBook is null ? Conflict() : Created($"/books-controller/{createdBook.BookId}", createdBook);
    }
}