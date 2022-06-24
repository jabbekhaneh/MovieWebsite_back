using Mapster;
using Microsoft.EntityFrameworkCore;
using Portal.Application.Movies.Contracts;
using Portal.Application.Movies.Queries.GetAllFilms;
using Portal.Application.Movies.Queries.GetFilmById;
using Portal.Domain.Movies;
using Portal.Extentions;

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

    public async Task<GetAllFilmsResponseDto> GetAll(int pageId, int take, string search, DateTime startDate, DateTime endDate)
    {
        var response = new GetAllFilmsResponseDto();
        IQueryable<Film> films = _context.Films;
        if (!string.IsNullOrEmpty(search))
            films = films.Where(_ => _.Title.Contains(search) &&
                                    _.Body.Contains(search));
        if (startDate != null && endDate != null)
            films = films.Where(_ => _.ReleaseDate >= startDate && _.ReleaseDate <= endDate);
        var pagination = films.Pagination<Film>(take, pageId);
        response.Films = await pagination.Query.ProjectToType<FilmDto>().ToListAsync();
        response.PageSize = pagination.PageSize;
        response.PageId = pageId;
        return response;
    }

    public async Task<GetFilmByIdResponseDto> GetById(Guid id)
    {
        return await _context.Films
            .Where(_ => _.Id == id)
            .ProjectToType<GetFilmByIdResponseDto>()
            .FirstOrDefaultAsync();
    }
}
