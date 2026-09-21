using AuthService.Application.Interfaces;
using AuthService.Core.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Application.Commands
{
    public record loginCommand(string Username, string Password) : IRequest<string>;

    public class HandlerLoginCommand : IRequestHandler<loginCommand, string>
    {
        private readonly IPasswordHash _passwordHashcheck;
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public HandlerLoginCommand(IUserRepository userRepository, IPasswordHash passwordHashcheck, IJwtTokenGenerator jwtTokenGenerator)
        {
            _passwordHashcheck = passwordHashcheck;
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<string> Handle(loginCommand request, CancellationToken cancellationToken)
        {
            var verifiedUser = await _userRepository.FindUserByUsername(request.Username);

            if(verifiedUser == null)
            {
                return "User is not found!";
            }

            var Verified = _passwordHashcheck.verifyPassword(request.Password, verifiedUser.PasswordHash);

            if (Verified)
            {
                string token = _jwtTokenGenerator.GenerateJwtToken(verifiedUser);
                return $"User is successfully login Token: {token}";
                
            }

            return "Enter Valid Password";
        }
    }
}
