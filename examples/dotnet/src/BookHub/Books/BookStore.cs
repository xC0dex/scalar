using Bogus;

namespace BookHub.Books;

/// <summary>
///     This is a dummy book store.
/// </summary>
public sealed class BookStore
{
    private readonly List<Book> _books;

    public BookStore()
    {
        var faker = new Faker<Book>()
            .UseSeed(69)
            .RuleFor(b => b.BookId, f => f.Random.Guid())
            .RuleFor(b => b.Title, f => f.Lorem.Sentence(3))
            .RuleFor(b => b.Description, f => f.Lorem.Paragraph(1))
            .RuleFor(b => b.Pages, f => f.Random.Int(69, 420));
        _books = faker.Generate(3);
    }

    internal IEnumerable<Book> GetAll()
    {
        return _books.ToArray();
    }

    internal Book? GetById(Guid bookId)
    {
        return _books.FirstOrDefault(x => x.BookId == bookId);
    }

    internal Book? Add(Book book)
    {
        if (_books.Any(x => x.BookId == book.BookId)) return null;

        _books.Add(book);
        return book;
    }
}