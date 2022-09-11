using MediatR;

namespace User.Application.Commands.CreateAnUser
{
    public class CreateAnUserCommand : IRequest<CreateAnUserCommandResponse>
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
