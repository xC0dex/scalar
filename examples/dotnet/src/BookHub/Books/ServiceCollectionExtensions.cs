namespace BookHub.Books;

internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddBookHub(this IServiceCollection services)
    {
        // Register the dummy book store.
        return services.AddSingleton<BookStore>();
    }
}