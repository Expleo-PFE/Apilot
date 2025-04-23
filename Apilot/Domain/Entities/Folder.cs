using Apilot.Domain.Common;

namespace Apilot.Domain.Entities;

public class Folder : BaseEntity
{
    public string Name { get; set; }
    public int CollectionId { get; set; }
    public Collection Collection { get; set; }
    
    public List<HttpRequest> HttpRequests { get; set; } = new List<HttpRequest>();
}