using FluentAssertions;
using Portal.Application.Movies.Contracts;
using Portal.Application.Movies.Queries.GetFilmById;
using Portal.Domain.Movies;
using Portal.EF;
using Portal.EF.Movies.Repositories;
using Portal.Extentions;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Portal.Test.Portal.Application.Test.Movies.GetFilmById;

public class GetFilmByIdHandlerTest
{
    private EFInMemoryDatabase _InMemory;
    private EFdbApplication _context;
    private FilmRepository _repository;
    private CancellationToken _cancellationToken;
    public GetFilmByIdHandlerTest()
    {
        _InMemory = new EFInMemoryDatabase();
        _context = _InMemory.CreateDataContext<EFdbApplication>();
        _repository = new EFFilmRepository(_context);
    }

    [Fact]
    private async Task Get_film_by_id_success()
    {
        //Arrange
        var filmId = Guid.NewGuid();
        var film = new Film()
        {
            Id = filmId,
            Body = "Dummy-body",
            Budget = 5,
            HomepageLink = "Dummy-link",
            ImageUrl = "Dummy-url",
            Tags = "tag_1,_tag_2,tag_3",
            Title = "dummy-title-1",
            IMDBLink = "dummy-IMDBLink",
            ReleaseDate = DateTime.UtcNow,
            Revenue = 52,
            Score = 1,
            Runtime = DateTime.Now,
        };
        _context.Films.Add(film);
        _context.SaveChanges();
        //Act
        var request = new GetFilmByIdQuery { Id = filmId };
        var handler = new GetFilmByIdQueryHandler(_repository);
        var actual= await handler.Handle(request, _cancellationToken);
        //Assert
        actual.IsSuccess.Should().BeTrue();
        actual.Result.Title.Should().Be(film.Title);
        actual.Result.Body.Should().Be(film.Body);
        actual.Result.Runtime.Should().Be(film.Runtime);

    }
}
