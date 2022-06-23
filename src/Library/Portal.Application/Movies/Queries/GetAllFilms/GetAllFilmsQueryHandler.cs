using MediatR;
using Portal.Application.Movies.Contracts;
using Portal.Extentions.Common;

namespace Portal.Application.Movies.Queries.GetAllFilms
{
    public class GetAllFilmsQueryHandler : IRequestHandler<GetAllFilmsQuery, OperationResult<GetAllFilmsResponseDto>>
    {
        private readonly FilmRepository _repository;
        public GetAllFilmsQueryHandler(FilmRepository repository)
        {
            _repository = repository;
        }
        public async Task<OperationResult<GetAllFilmsResponseDto>> Handle(GetAllFilmsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var resposne = new GetAllFilmsResponseDto();
                resposne = await _repository.GetAll(request.PageId, request.Take, request.Search,
                    request.StartDate, request.EndDate);
                return OperationResult<GetAllFilmsResponseDto>.BuildSuccess(resposne);
            }
            catch (Exception ex)
            {
                return OperationResult<GetAllFilmsResponseDto>.BuildFailure(ex);
            }
        }
    }
}
