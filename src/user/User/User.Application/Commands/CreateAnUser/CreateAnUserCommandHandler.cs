using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using User.Domain.Constants;
using User.Domain.Contracts.Repository;

namespace User.Application.Commands.CreateAnUser
{
    public class CreateAnUserCommandHandler : IRequestHandler<CreateAnUserCommand, CreateAnUserCommandResponse>
    {
        private readonly IUserRepository _userRepository;

        public CreateAnUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<CreateAnUserCommandResponse> Handle(CreateAnUserCommand request, CancellationToken cancellationToken)
        {
            if (await _userRepository.ExistsAsync(request.Email))
                throw new InvalidOperationException(ExceptionMessageConstants.UserAlreadyCreated);
            var newUser = new Domain.Entities.User
            {
                Email = request.Email,
                Name = request.Name
            };
            var id = await _userRepository.SaveAsync(newUser);

            return new CreateAnUserCommandResponse
            {
                Name = newUser.Name,
                Email = newUser.Email,
                Id = id
            };
        }
    }
}
