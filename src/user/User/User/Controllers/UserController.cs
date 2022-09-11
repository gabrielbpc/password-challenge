using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using User.Api.Dto.Request;
using User.Api.Dto.Response;
using User.Application.Commands.CreateAnUser;
using User.Application.Commands.GetAnUser;

namespace User.Api.Controllers
{
    /// <summary>
    /// Controller de usuarios
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("sys/v1/users")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        /// <summary>
        /// Construtor padrão
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="mapper"></param>
        public UserController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Cria um usuario
        /// </summary>
        /// <param name="request"></param>
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> CreateAnUser([FromBody] CreateAnUserRequest request)
        {
            var command = _mapper.Map<CreateAnUserCommand>(request);
            try
            {
                var result = await _mediator.Send(command);

                var response = _mapper.Map<UserResponse>(result);

                return Created(string.Empty, response);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ErrorResponse(ex.Message));
            }
        }

        /// <summary>
        /// Retorna um usuario já está cadastrado
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet("email/{email}")]
        public async Task<IActionResult> GeyByEmail([FromRoute] string email)
        {
            var command = new GetAnUserCommand { Email = email };
            try
            {
                var result = await _mediator.Send(command);

                var response = _mapper.Map<UserResponse>(result);

                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new ErrorResponse(ex.Message));
            }
        }
    }
}
