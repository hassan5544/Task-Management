namespace Application.Commands.UserCommands.LoginUser;

public record LoginUserResponse
{
    public string Email { get; set; }
    public string Username { get; set; }
    public string Token { get; set; }
}