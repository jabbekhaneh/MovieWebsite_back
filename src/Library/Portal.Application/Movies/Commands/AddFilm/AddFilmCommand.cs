using MediatR;
using Portal.Application.Common;
using Portal.Extentions.Common;

namespace Portal.Application.Movies.Commands.AddFilm;

public class AddFilmCommand : CommittableRequest, IRequest<OperationResult<AddFilmResponseDto>>
{
    public AddFilmDto Film { get; set; }
}
