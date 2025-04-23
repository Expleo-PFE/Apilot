using Apilot.Domain.Enums;

namespace Apilot.Domain.Entities;

public class AuthenticationEntity
{
    public AuthType AuthType { get; set; }
    public Dictionary<string, string> AuthData { get; set; } = new Dictionary<string, string>();
}