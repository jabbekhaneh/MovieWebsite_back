﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Portal.Application.Users.Commands.AddUser;
using Portal.Application.Users.Commands.EditUser;
using Portal.Application.Users.Queries.GetUserById;
using Portal.Application.Users.Queries.GetUsers;
using Portal.Extentions.Common;

namespace Portal.WebApi.Controllers.Manager
{
    public class ManagerUsersController : BaseController
    {
        public ManagerUsersController(IMediator mediator) : base(mediator)
        {
        }


        [HttpGet]
        public async Task<OperationResult<GetUsersDto>> GetAll(string search = "", int take = 40, int pageId = 1)
        {
            return await _mediator.Send(new GetUsersQuery { PageId = pageId, Search = search, Take = take});
        }
        [HttpGet("id")]
        public async Task<OperationResult<GetUserByIdDto>> Get(Guid id)
        {
            return await _mediator.Send(new GetUserByIdQuery { UserId = id });
        }

        [HttpPost]
        public async Task<OperationResult<AddUserResponseDto>> AddUser(AddUserDto user)
        {
            return await _mediator.Send(new AddUserCommand { User=user});
        }

        [HttpPut]
        public async Task<OperationResult<EditUserResponseDto>> EditUser(Guid id, EditUserDto user)
        {
            return await _mediator.Send(new EditUserCommand(id, user));
        }

    }
}
