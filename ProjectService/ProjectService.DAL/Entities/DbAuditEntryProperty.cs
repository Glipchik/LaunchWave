using System.ComponentModel.DataAnnotations;
using Z.EntityFramework.Plus;

namespace ProjectService.DAL.Entities;

internal class DbAuditEntryProperty
{
    public DbAuditEntryProperty()
    {
    }
    public DbAuditEntryProperty(AuditEntryProperty property)
    {
        RelationName = property.RelationName;
        PropertyName = property.PropertyName;
        OldValue = property.OldValueFormatted;
        NewValue = property.NewValueFormatted;
    }

    [Key]
    public int AuditEntryPropertyId { get; set; }
    public string? RelationName { get; set; }
    public string? PropertyName { get; set; }
    public string? OldValue { get; set; }
    public string? NewValue { get; set; }
}
