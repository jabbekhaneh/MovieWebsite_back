using Portal.Application.Movies.Queries.GetAllFilms;
using Portal.Application.Movies.Queries.GetFilmById;
using Portal.Domain.Movies;

namespace Portal.Application.Movies.Contracts;

public interface FilmRepository 
{
    Task Add(Film film);
    Task<GetAllFilmsResponseDto> GetAll(int pageId, int take, string search, DateTime startDate, DateTime endDate);
    Task<GetFilmByIdResponseDto> GetById(Guid id);
}
