using Apilot.Domain.Common;

namespace Apilot.Domain.Entities;

public class Environement : BaseEntity
{
    public string Name { get; set; }
    public int WorkSpaceId { get; set; }
    public WorkSpace WorkSpace { get; set; }

    public Dictionary<string, string> Variables { get; set; } = new Dictionary<string, string>();
}