using Apilot.Domain.Common;

namespace Apilot.Domain.Entities;

public class HistoryEntity : BaseEntity
{
    public DateTime TimeStamp { get; set; }
    public List<RequestHistoryEntity> Requests { get; set; } = new List<RequestHistoryEntity>();
}