using MediatR;
using Microsoft.AspNetCore.Mvc;
using Portal.EF;

namespace Portal.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public abstract class BaseController : ControllerBase
{
    protected readonly IMediator _mediator;

    public BaseController(IMediator mediator)
    {
        _mediator = mediator;
        
    }

}
