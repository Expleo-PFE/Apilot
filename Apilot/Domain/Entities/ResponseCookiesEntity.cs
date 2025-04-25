namespace Apilot.Domain.Entities;

public class ResponseCookiesEntity
{
    public required string CookieName { get; set; }
    public required string CookieValue { get; set; }
    public required string CookieDomain { get; set; }
    public required string CookiePath { get; set; }
    public DateTime CookieExpires { get; set; }
    public bool HttpOnly { get; set; }
    public bool Secure { get; set; }
}