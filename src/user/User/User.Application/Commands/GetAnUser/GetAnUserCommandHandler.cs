using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using User.Domain.Constants;
using User.Domain.Contracts.Repository;

namespace User.Application.Commands.GetAnUser
{
    public class GetAnUserCommandHandler : IRequestHandler<GetAnUserCommand, GetAnUserCommandResponse>
    {
        private readonly IUserRepository _userRepository;

        public GetAnUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<GetAnUserCommandResponse> Handle(GetAnUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null)
                throw new InvalidOperationException(ExceptionMessageConstants.UserNotFound);

            return new GetAnUserCommandResponse
            {
                Email = user.Email,
                Id = user.Id.ToString(),
                Name = user.Name
            };
        }
    }
}
