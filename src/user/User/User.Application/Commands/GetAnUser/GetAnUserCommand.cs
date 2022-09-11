using MediatR;

namespace User.Application.Commands.GetAnUser
{
    public class GetAnUserCommand : IRequest<GetAnUserCommandResponse>
    {
        public string Email { get; set; }
    }
}
