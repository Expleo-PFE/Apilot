using Apilot.Domain.Common;

namespace Apilot.Domain.Entities;

public class HistoryEntity : BaseEntity
{
    public DateTime TimeStamp { get; set; }
    public List<HistoryRequestEntity> Requests { get; set; } = new List<HistoryRequestEntity>();
}