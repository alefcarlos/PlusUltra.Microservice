using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Flunt.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlusUltra.WebApi.Controllers;

namespace PlusUltra.Microservice.Controllers.v1
{
    [Authorize(JwtBearerDefaults.AuthenticationScheme, Roles = "PlusUltra.Microservice.Api")]
    [Route("[controller]")]
    public class ValuesController : WebApiController
    {
        public ValuesController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        private readonly IMediator mediator;

        /// <summary>
        /// Exemplo de Get
        /// </summary>
        /// <param name="id">ID</param>
        [HttpGet("{id}")]
        [Authorize(Roles = "PlusUltra.Microservice.Api")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(List<Notification>), (int)HttpStatusCode.UnprocessableEntity)]
        [Produces("application/json")]
        public async Task<IActionResult> Get(Guid id)
        {
            return NoContent();
        }
    }
}
