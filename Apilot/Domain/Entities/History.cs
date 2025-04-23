using Apilot.Domain.Common;

namespace Apilot.Domain.Entities;

public class History : BaseEntity
{
    public DateTime TimeStamp { get; set; }
    public List<HttpRequest> Requests { get; set; } = new List<HttpRequest>();
}