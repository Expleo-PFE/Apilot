using Apilot.Domain.Common;

namespace Apilot.Domain.Entities;

public class WorkSpaceEntity : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    
    
    public List<CollectionEntity> Collections { get; set; }
    public List<EnvironmentEntity> Environements { get; set; }
    public List<HistoryEntity> Histories { get; set; }
}