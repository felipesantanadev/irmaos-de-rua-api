using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IrmaosDeRua.Auth.Domain.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IrmaosDeRua.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : CustomController
    {
        public AuthController(IMediator mediator) : base(mediator) { }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserCommand command)
        {
            var result = await CommandAsync(command);
            return Ok(result);
        }
    }
}