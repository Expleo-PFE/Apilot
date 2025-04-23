namespace Apilot.Domain.Entities;

public class ResponseCookiesEntity
{
    public string CookieName { get; set; }
    public string CookieValue { get; set; }
    public string CookieDomain { get; set; }
    public string CookiePath { get; set; }
    public DateTime CookieExpires { get; set; }
    public bool HttpOnly { get; set; }
    public bool Secure { get; set; }
}