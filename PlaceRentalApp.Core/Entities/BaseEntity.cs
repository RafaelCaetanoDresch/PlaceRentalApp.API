namespace PlaceRentalApp.Core.Entities;

public abstract class BaseEntity
{
    protected BaseEntity()
    {
        CreatedAt = DateTime.UtcNow;
        IsDeleted = false;
    }
    public int Id { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public bool IsDeleted { get; set; }

    public void SetAsDeleted()
    {
        IsDeleted = true;
    }
}
