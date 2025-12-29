using MVC.Models.Author;
using MVC.Models.Genre;

namespace MVC.Models.Book;

public class BookListViewModel : PagedViewModel<BookViewModel>
{
    public List<GenreSimplifiedViewModel> Genres { get; set; } = [];
    public int? SelectedGenreId { get; set; }
    public List<AuthorSimplifiedViewModel> Authors { get; set; } = [];
    public int? SelectedAuthorId { get; set; }
}
