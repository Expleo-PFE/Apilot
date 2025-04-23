using Apilot.Domain.Common;

namespace Apilot.Domain.Entities;

public class EnvironementEntity : BaseEntity
{
    public string Name { get; set; }
    public int WorkSpaceId { get; set; }
    public WorkSpaceEntity WorkSpaceEntity { get; set; }

    public Dictionary<string, string> Variables { get; set; } = new Dictionary<string, string>();
}