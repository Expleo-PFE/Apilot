using Apilot.Domain.Enums;

namespace Apilot.Application.DTOs.Authentication;

public class AuthenticationDto
{
    
    public required AuthType AuthType { get; set; }
    public required Dictionary<string, string> AuthData { get; set; } = new Dictionary<string, string>();
}