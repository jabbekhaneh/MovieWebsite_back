using MediatR;
using Portal.Extentions.Common;

namespace Portal.Application.Movies.Queries.GetAllFilms
{
    public class GetAllFilmsQuery :IRequest<OperationResult<GetAllFilmsResponseDto>>
    {
        public int PageId { get; set; }
        public int Take { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Search { get; set; }
    }
}
