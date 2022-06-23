using FluentAssertions;
using Portal.Application.Movies.Commands.AddFilm;
using Portal.Application.Movies.Contracts;
using Portal.EF;
using Portal.EF.Movies.Repositories;
using Portal.Extentions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Portal.Test.Portal.Application.Test.Movies.AddFilm;

public class AddMoviesCommandHandlerTest
{
    private EFInMemoryDatabase _InMemory;
    private EFdbApplication _context;
    private FilmRepository _repository;
    private CancellationToken _cancellation;
    public AddMoviesCommandHandlerTest()
    {
        _InMemory = new EFInMemoryDatabase();
        _context = _InMemory.CreateDataContext<EFdbApplication>();
        _repository = new EFFilmRepository(_context);
        _cancellation = new CancellationToken();
    }

    [Fact]
    private async Task Add_film_success()
    {
        //Arrange
        var generateAddFilmDto = new AddFilmDto
        {
            Body = "Dummy-body",
            Budget = 5,
            HomepageLink = "Dummy-link",
            ImageUrl = "Dummy-url",
            Tags = "tag_1,_tag_2,tag_3",
            Title = "dummy-title",
            IMDBLink = "dummy-IMDBLink",
            ReleaseDate = DateTime.UtcNow,
            Revenue = 52,
            Score = 1,
            Runtime = DateTime.Now,
        };
        //Act
        var request = new AddFilmCommand() { Film = generateAddFilmDto };
        var handler = new AddFilmCommandHandler(_repository);
        var actual= await handler.Handle(request, _cancellation);
        _context.SaveChanges();
        //Assert
        actual.IsSuccess.Should().BeTrue();
        var expected =_context.Films.First();
        expected.Title.Should().Be(generateAddFilmDto.Title);
        expected.Runtime.Should().Be(generateAddFilmDto.Runtime);
        expected.Body.Should().Be(generateAddFilmDto.Body);
        expected.Tags.Should().Be(generateAddFilmDto.Tags);

    }
}
