using Apilot.Domain.Common;

namespace Apilot.Domain.Entities;

public class CollectionEntity : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    
    public int WorkSpaceId { get; set; }
    public WorkSpaceEntity WorkSpaceEntity { get; set; }

    public List<FolderEntity> Folders { get; set; } = new List<FolderEntity>();
    public List<RequestEntity> HttpRequests { get; set; } = new List<RequestEntity>();
}