using Portal.Application.Movies.Contracts;
using Portal.Domain.Movies;

namespace Portal.EF.Movies.Repositories;

public class EFFilmRepository : FilmRepository
{
    private readonly EFdbApplication _context;

    public EFFilmRepository(EFdbApplication context)
    {
        _context = context;
    }

    public async Task Add(Film film)
    {
        await _context.Films.AddAsync(film);
    }
}
