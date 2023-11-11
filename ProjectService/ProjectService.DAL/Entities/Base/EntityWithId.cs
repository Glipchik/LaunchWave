namespace ProjectService.DAL.Entities.Base;

public class EntityWithId
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; } = default;
}
