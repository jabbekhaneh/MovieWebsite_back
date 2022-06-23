using MediatR;
using Portal.Application.Movies.Contracts;
using Portal.Domain.Movies;
using Portal.Extentions.Common;

namespace Portal.Application.Movies.Commands.AddFilm;

public class AddFilmCommandHandler : IRequestHandler<AddFilmCommand, OperationResult<AddFilmResponseDto>>
{
    private readonly FilmRepository _repository;

    public AddFilmCommandHandler(FilmRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResult<AddFilmResponseDto>> Handle(AddFilmCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var response=new AddFilmResponseDto();
            var newFile = new Film
            {
                Id = Guid.NewGuid(),
                Body = request.Film.Body,
                Budget = request.Film.Budget,
                HomepageLink = request.Film.HomepageLink,
                ImageUrl = request.Film.ImageUrl,
                IMDBLink = request.Film.IMDBLink,
                ReleaseDate = request.Film.ReleaseDate,
                Revenue = request.Film.Revenue,
                Runtime = request.Film.Runtime,
                Score = request.Film.Score,
                Tags = request.Film.Tags,
                Title = request.Film.Title,

            };
            await _repository.Add(newFile);
            response.Id=newFile.Id;
            return OperationResult<AddFilmResponseDto>.BuildSuccess(response);  
        }
        catch (Exception ex)
        {
            return OperationResult<AddFilmResponseDto>.BuildFailure(ex);
        }
    }
}
