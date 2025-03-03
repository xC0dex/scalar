using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace BookHub.Books;

internal static class BookEndpoints
{
    internal static void MapBookEndpoints(this IEndpointRouteBuilder builder)
    {
        var apiVersionSet = builder.NewApiVersionSet()
            .HasApiVersion(new ApiVersion(1))
            .HasApiVersion(new ApiVersion(2))
            .Build();

        var books = builder.MapGroup("v{version:apiVersion}/books")
            .WithTags("minimal")
            .WithApiVersionSet(apiVersionSet)
            .MapToApiVersion(1)
            .MapToApiVersion(2);

        books
            .MapGet("/", ([FromServices] BookStore bookStore) => bookStore.GetAll())
            .Produces<Book[]>();

        books
            .MapGet("/{bookId:guid}", ([FromServices] BookStore bookStore, Guid bookId) =>
            {
                var book = bookStore.GetById(bookId);
                return book is null ? Results.NotFound() : Results.Ok(book);
            })
            .Produces<Book>()
            .Produces(StatusCodes.Status404NotFound);

        books
            .MapPost("/", ([FromServices] BookStore bookStore, Book myCustomBook) =>
            {
                var createdBook = bookStore.Add(myCustomBook);
                return createdBook is null ? Results.Conflict() : Results.Created($"/books/{createdBook.BookId}", createdBook);
            })
            .Produces<Book>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status409Conflict);
    }
}