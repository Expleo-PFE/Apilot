using Apilot.Domain.Enums;

namespace Apilot.Domain.Entities;

public class HistoryRequestEntity
{
    public string Name { get; set; }
    public ApiHttpMethod HttpMethod { get; set; }
    public string Url { get; set; }
    public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
    public AuthenticationEntity Authentication { get; set; }
    public string Body { get; set; }
}