using FluentAssertions;
using Portal.Application.Movies.Contracts;
using Portal.Application.Movies.Queries.GetAllFilms;
using Portal.Domain.Movies;
using Portal.EF;
using Portal.EF.Movies.Repositories;
using Portal.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Portal.Test.Portal.Application.Test.Movies.GetAllFilms
{
    public class GetAllFilmHandlerTest
    {
        private EFInMemoryDatabase _inMemmory;
        private EFdbApplication _context;
        private FilmRepository _repository;
        private CancellationToken _cancellationToken;
        public GetAllFilmHandlerTest()
        {
            _inMemmory = new EFInMemoryDatabase();
            _context = _inMemmory.CreateDataContext<EFdbApplication>();
            _repository = new EFFilmRepository(_context);
            _cancellationToken = new CancellationToken();
        }

        [Fact]
        private async Task Get_All_film_success()
        {
            //Arrange
            var films = new List<Film>()
             {
                 new Film()
                 {
                     Id = Guid.NewGuid(),
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

                 },
                 new Film()
                 {
                     Id = Guid.NewGuid(),
                     Body = "Dummy-body",
                     Budget = 5,
                     HomepageLink = "Dummy-link",
                     ImageUrl = "Dummy-url",
                     Tags = "tag_1,_tag_2,tag_3",
                     Title = "dummy-title-2",
                     IMDBLink = "dummy-IMDBLink",
                     ReleaseDate = DateTime.UtcNow,
                     Revenue = 52,
                     Score = 1,
                     Runtime = DateTime.Now,

                 },
                 new Film()
                 {
                     Id = Guid.NewGuid(),
                     Body = "Dummy-body",
                     Budget = 5,
                     HomepageLink = "Dummy-link",
                     ImageUrl = "Dummy-url",
                     Tags = "tag_1,_tag_2,tag_3",
                     Title = "dummy-title-3",
                     IMDBLink = "dummy-IMDBLink",
                     ReleaseDate = DateTime.UtcNow,
                     Revenue = 52,
                     Score = 1,
                     Runtime = DateTime.Now,

                 },

            };
            _context.Films.AddRange(films);
            _context.SaveChanges();
            
            //Act
            var request = new GetAllFilmsQuery { PageId = 1, Take = 20,Search="",EndDate=DateTime.Now.AddDays(1),StartDate=DateTime.Now.AddDays(-1) };
            var handler = new GetAllFilmsQueryHandler(_repository);
            var actual = await handler.Handle(request, _cancellationToken);
            //Assert
            actual.IsSuccess.Should().BeTrue();
            actual.Result.Films.Count.Should().Be(3);
        }
    }
}
