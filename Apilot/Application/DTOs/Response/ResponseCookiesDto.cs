namespace Apilot.Application.DTOs.Response;

public class ResponseCookiesDto
{
    public required string CookieName { get; set; }
    public required string CookieValue { get; set; }
    public required string CookieDomain { get; set; }
    public required string CookiePath { get; set; }
    public DateTime CookieExpires { get; set; }
    public bool HttpOnly { get; set; }
    public bool Secure { get; set; }
}