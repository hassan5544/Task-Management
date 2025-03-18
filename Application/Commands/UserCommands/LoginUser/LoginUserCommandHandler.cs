using Domain.Repositories;
using Helpers.Interfaces;
using MediatR;

namespace Application.Commands.UserCommands.LoginUser;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public LoginUserCommandHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<LoginUserResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        // Validate email existence
        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user == null)
            throw new UnauthorizedAccessException("Invalid email or password.");

        // Validate password
        var isPasswordValid = user.VerifyPassword(request.Password, _passwordHasher);
        if (!isPasswordValid)
            throw new UnauthorizedAccessException("Invalid email or password.");

        // Generate JWT token
        var token = _jwtTokenGenerator.GenerateToken(user.Id , user.Email , user.Username);

        // Return user info and token
        return new LoginUserResponse
        {
            Username = user.Username,
            Email = user.Email,
            Token = token
        };
    }
}