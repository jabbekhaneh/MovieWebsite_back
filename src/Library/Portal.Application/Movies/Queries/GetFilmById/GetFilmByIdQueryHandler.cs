using MediatR;
using Portal.Application.Movies.Contracts;
using Portal.Extentions.Common;

namespace Portal.Application.Movies.Queries.GetFilmById;

public class GetFilmByIdQueryHandler : IRequestHandler<GetFilmByIdQuery, OperationResult<GetFilmByIdResponseDto>>
{
    private readonly FilmRepository _repository;
    public GetFilmByIdQueryHandler(FilmRepository repository)
    {
        _repository = repository;
    }
    public async Task<OperationResult<GetFilmByIdResponseDto>> Handle(GetFilmByIdQuery request, CancellationToken cancellationToken)
    {
        var  response = await _repository.GetById(request.Id); 
        return  OperationResult<GetFilmByIdResponseDto>.BuildSuccess(response);
        
    }
}
