using ProjectService.DAL.Entities.Base;
using System.ComponentModel.DataAnnotations;
using Z.EntityFramework.Plus;

namespace ProjectService.DAL.Entities;

internal class DbAuditEntry
{
    public DbAuditEntry()
    {
    }

    public DbAuditEntry(AuditEntry entry)
    {
        EntitySetName = entry.EntitySetName;
        EntityTypeName = entry.EntityTypeName;
        State = entry.State;
        StateName = entry.StateName;
        CreatedBy = entry.CreatedBy;
        CreatedDate = entry.CreatedDate;

        Properties = entry.Properties.Select(x => new DbAuditEntryProperty(x)).ToList();
        // Custom Property Value
        if (entry.Entity is EntityWithId entity)
        {
            EntityId = entity.Id;
        }
    }

    [Key]
    public int AuditEntryId { get; set; }
    public string? EntitySetName { get; set; }
    public string? EntityTypeName { get; set; }
    public AuditEntryState State { get; set; }
    public string? StateName { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public List<DbAuditEntryProperty>? Properties { get; set; }
    public Guid EntityId { get; set; }
}
