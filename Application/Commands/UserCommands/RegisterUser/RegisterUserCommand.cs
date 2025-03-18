using MediatR;

namespace Application.Commands.UserCommands.RegisterUser;

public record RegisterUserCommand(string username , string Email , string Password) : IRequest<RegisterUserResponse>;