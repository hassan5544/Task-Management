using Application.Commands.UserCommands.RegisterUser;
using Domain.Entities;
using Domain.Repositories;
using Helpers.Interfaces;
using MediatR;

namespace Application.Commands.UserCommands.RegisterUser;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegisterUserResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUnitOfWork _unitOfWork;
    public RegisterUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, IJwtTokenGenerator jwtTokenGenerator, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
        _unitOfWork = unitOfWork;
    }

    public async Task<RegisterUserResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if(await _userRepository.EmailExistsAsync(request.Email))
        {
            throw new Exception("Email already exists");   
        }
        var user = Domain.Entities.User.Create(request.username, request.Email, request.Password, _passwordHasher);
        
        await _userRepository.AddUserAsync(user , cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);
        
        string token = _jwtTokenGenerator.GenerateToken(user.Id , user.Email,user.Username);
        RegisterUserResponse response = new RegisterUserResponse
        {
            UserName = user.Username,
            Email = user.Email,
            Token = token
        };
        return response;
    }
}