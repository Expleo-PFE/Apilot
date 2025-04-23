using Apilot.Domain.Common;

namespace Apilot.Domain.Entities;

public class WorkSpace : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    
    
    public List<Collection> Collections { get; set; }
    public List<Environement> Environements { get; set; }
}