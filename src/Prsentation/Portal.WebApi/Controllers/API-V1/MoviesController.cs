using MediatR;
using Microsoft.AspNetCore.Mvc;
using Portal.Application.Movies.Commands.AddFilm;
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

    }
}
