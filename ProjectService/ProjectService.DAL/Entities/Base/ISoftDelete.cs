namespace ProjectService.DAL.Entities.Base;

public interface ISoftDelete
{
    public bool IsDeleted { get; set; }

    public void Undo()
    {
        IsDeleted = false;
    }
}
