using Microsoft.EntityFrameworkCore;
using ProjectService.DAL.Constants;
using ProjectService.DAL.Entities;
using ProjectService.DAL.Entities.Base;
using Z.EntityFramework.Plus;

namespace ProjectService.DAL.Contexts;

public sealed class DatabaseContext : DbContext
{
    public DbSet<ProjectEntity>? Projects { get; set; }
    public DbSet<CrowdFundRequestEntity>? CrowdFundRequests { get; set; }

    internal DbSet<DbAuditEntry>? AuditEntries { get; set; }
    internal DbSet<DbAuditEntryProperty>? AuditEntryProperties { get; set; }
    public DbSet<ChangeLogEntity>? ChangeLogs { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
        AuditManager.DefaultConfiguration
            .AuditEntryFactory = args => new AuditEntry { CreatedDate = DateTime.UtcNow };

        AuditManager.DefaultConfiguration.IgnorePropertyUnchanged = false;

        AuditManager.DefaultConfiguration.AutoSavePreAction =
            Environment.GetEnvironmentVariable(EnvironmentConstants.AspnetcoreEnvironment) != EnvironmentConstants.Production
            ? null
            : (context, audit) =>
            {
                var customAuditEntries = audit.Entries.Select(x => new DbAuditEntry(x)).ToList();
                (context as DatabaseContext)?.AuditEntries?.AddRange(customAuditEntries.Where(a => a.EntityId != Guid.Empty));
            };

        if (Database.IsRelational())
        {
            Database.Migrate();
        }
    }

    public override int SaveChanges()
    {
        OnBeforeSaving();
        var audit = new Audit
        {
            CreatedBy = AuthorConstants.CreatedByApplication
        };

        audit.PreSaveChanges(this);
        var rowsAffected = base.SaveChanges();
        audit.PostSaveChanges();

        if (audit.Configuration.AutoSavePreAction != null)
        {
            audit.Configuration.AutoSavePreAction(this, audit);
            base.SaveChanges();
        }

        return rowsAffected;
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        OnBeforeSaving();
        var audit = new Audit
        {
            CreatedBy = AuthorConstants.CreatedByApplication
        };

        audit.PreSaveChanges(this);
        var rowsAffected = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        audit.PostSaveChanges();

        if (audit.Configuration.AutoSavePreAction != null)
        {
            audit.Configuration.AutoSavePreAction(this, audit);
            await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        return rowsAffected;
    }

    public async Task<int> SaveChangesAsyncExcludingUpdatedAt(CancellationToken cancellationToken = default)
    {
        OnBeforeSaving(excludeUpdatedAt: true);
        var audit = new Audit
        {
            CreatedBy = AuthorConstants.CreatedByApplication
        };

        audit.PreSaveChanges(this);
        var rowsAffected = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        audit.PostSaveChanges();

        if (audit.Configuration.AutoSavePreAction != null)
        {
            audit.Configuration.AutoSavePreAction(this, audit);
            await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        return rowsAffected;
    }

    private void OnBeforeSaving(bool excludeUpdatedAt = false)
    {
        var entries = ChangeTracker.Entries();
        var utcNow = DateTime.UtcNow;

        foreach (var entry in entries)
        {
            if (entry.Entity is EntityWithId entity)
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        if (!excludeUpdatedAt)
                        {
                            entry.Property(AuthorConstants.CreatedAt).IsModified = false;
                            entity.UpdatedAt = utcNow;
                        }
                        break;

                    case EntityState.Added:
                        entity.CreatedAt = utcNow;
                        entity.UpdatedAt = utcNow;
                        break;
                }
            }
        }
    }
}
