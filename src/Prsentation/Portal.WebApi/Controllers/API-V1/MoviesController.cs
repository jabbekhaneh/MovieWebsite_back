using MediatR;
using Microsoft.AspNetCore.Mvc;
using Portal.Application.Movies.Commands.AddFilm;
using Portal.Application.Movies.Queries.GetAllFilms;
using Portal.Application.Movies.Queries.GetFilmById;
using Portal.Extentions.Common;

namespace Portal.WebApi.Controllers.API_V1
{
    public class MoviesController : BaseController
    {
        public MoviesController(IMediator mediator) : base(mediator)
        {

        }

        [HttpPost]
        public async Task<OperationResult<AddFilmResponseDto>> Add(AddFilmDto dto)
        {
            return await _mediator.Send(new AddFilmCommand { Film = dto });
        }
        [HttpGet]
        public async Task<OperationResult<GetAllFilmsResponseDto>> GetAll(int pageId = 1, int take = 20, string search = "", string startDate = "", string endDate = "")
        {
            DateTime dt = new DateTime();
            if (!DateTime.TryParse(startDate, null, System.Globalization.DateTimeStyles.RoundtripKind, out dt))
            {
                return OperationResult<GetAllFilmsResponseDto>
                    .BuildFailure(new OperationException("incorrect input", ExceptionStatusCodeType.BadRequest));

            }
            if (!DateTime.TryParse(endDate, null, System.Globalization.DateTimeStyles.RoundtripKind, out dt))
            {
                return OperationResult<GetAllFilmsResponseDto>
                    .BuildFailure(new OperationException("incorrect input", ExceptionStatusCodeType.BadRequest));

            }
            return await _mediator.Send(new GetAllFilmsQuery
            {
                PageId = pageId,
                Take = take,
                Search = search,
                StartDate = DateTime.Parse(startDate),
                EndDate = DateTime.Parse(endDate)
            });
        }
        [HttpGet("id")]
        public async Task<OperationResult<GetFilmByIdResponseDto>> Get(Guid id)
        {

            return await _mediator.Send(new GetFilmByIdQuery
            {
                Id = id
            });
        }
    }
}
