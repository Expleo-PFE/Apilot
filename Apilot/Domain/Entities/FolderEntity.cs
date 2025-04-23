using Apilot.Domain.Common;

namespace Apilot.Domain.Entities;

public class FolderEntity : BaseEntity
{
    public string Name { get; set; }
    public int CollectionId { get; set; }
    public CollectionEntity CollectionEntity { get; set; }
    
    public List<RequestEntity> HttpRequests { get; set; } = new List<RequestEntity>();
}