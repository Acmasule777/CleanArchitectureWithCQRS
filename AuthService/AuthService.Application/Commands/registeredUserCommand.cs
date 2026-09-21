using AuthService.Application.Interfaces;
using AuthService.Core.DTOs;
using AuthService.Core.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Application.Commands
{
    public record registeredUserCommand(string Username, string password, string role) : IRequest<string>;

    public class handlerRegisteredCommand : IRequestHandler<registeredUserCommand, string>
    {
        private readonly IPasswordHash _password;
        private readonly IUserRepository _userRepository;
        public handlerRegisteredCommand(IPasswordHash password, IUserRepository userRepository)
        {
            _password = password;
            _userRepository = userRepository;
        }

        public async Task<string> Handle(registeredUserCommand request, CancellationToken cancellationToken)
        {

            var HashedPassword = _password.hashPassword(request.password);

            var User = new User
            {
                Username = request.Username,
                PasswordHash = HashedPassword,
                Role = request.role
            };

            return await _userRepository.registerUser(User, cancellationToken);

            
        }

    }
}
