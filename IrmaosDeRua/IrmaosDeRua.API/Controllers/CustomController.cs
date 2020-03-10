using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IrmaosDeRua.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class CustomController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException();
        }

        /// <summary>
        /// Use for Query.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        protected async Task<TResult> QueryAsync<TResult>(IRequest<TResult> query)
        {
            return await _mediator.Send(query);
        }

        /// <summary>
        /// Use for check the response data, if the response is null then it will return the Not Found 
        /// status code of HTTP request.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        protected ActionResult<T> Single<T>(T data)
        {
            if (data == null) return NotFound();
            return Ok(data);
        }


        /// <summary>
        /// Use for Command.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="command"></param>
        /// <returns></returns>
        protected async Task<TResult> CommandAsync<TResult>(IRequest<TResult> command)
        {
            return await _mediator.Send(command);
        }
    }
}
