using Apilot.Domain.Common;

namespace Apilot.Domain.Entities;

public class Collection : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    
    public int WorkSpaceId { get; set; }
    public WorkSpace WorkSpace { get; set; }

    public List<Folder> Folders { get; set; } = new List<Folder>();
    public List<HttpRequest> HttpRequests { get; set; } = new List<HttpRequest>();
}