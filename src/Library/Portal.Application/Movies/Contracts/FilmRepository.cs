using Portal.Domain.Movies;

namespace Portal.Application.Movies.Contracts;

public interface FilmRepository 
{
    Task Add(Film film);
}
