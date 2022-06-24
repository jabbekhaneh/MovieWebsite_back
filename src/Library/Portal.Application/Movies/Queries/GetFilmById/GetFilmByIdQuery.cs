using MediatR;
using Portal.Extentions.Common;

namespace Portal.Application.Movies.Queries.GetFilmById;

public class GetFilmByIdQuery : IRequest<OperationResult<GetFilmByIdResponseDto>>
{
    public Guid Id { get; set; }
}
