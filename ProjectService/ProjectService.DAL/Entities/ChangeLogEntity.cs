using ProjectService.DAL.Entities.Base;

namespace ProjectService.DAL.Entities;

public class ChangeLogEntity : EntityWithId
{
    public Guid ProjectId { get; set; }
#nullable disable
    public string EntityName { get; set; }
    public string PropertyName { get; set; }
    public string PrimaryKeyValue { get; set; }
    public string OldValue { get; set; }
    public string NewValue { get; set; }
    public string Note { get; set; }
    public string UserId { get; set; }
#nullable enable
    public DateTime ChangeAt { get; set; }
}
