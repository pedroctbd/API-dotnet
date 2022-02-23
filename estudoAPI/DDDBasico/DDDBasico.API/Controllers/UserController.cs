using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDBasico.Application.Queries.Users;
using DDDBasico.Application.Users.Command;
using DDDBasico.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DDDBasico.API.Controllers
{
    [ApiController]
    [Route("api/users/")]
    public class UserController : ControllerBase
    {

        private readonly IMediator _mediator;


        public UserController(IRepositoryUser repository, IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(GetAllUsersQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, GetUserQuery query)
        {
            var request = query with { Id = id };
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateUserCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateUserCommand command)
        {
            var request = command with { Id = id};
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, DeleteUserCommand command)
        {
            var request = command with { Id = id };
            var response = await _mediator.Send(request);
            return Ok(response);
        }

     
        [HttpPost("{id:int}/drink")]
        public async Task<IActionResult> Drink(int id,DrinkUserCommand command)
        {
            var request = command with { Id = id };
            var response = await _mediator.Send(request);
            if(response!=null) return Ok(response);
            return BadRequest();

        }


    }
}