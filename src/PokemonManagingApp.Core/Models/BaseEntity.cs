namespace PokemonManagingApp.Core.Models;

public class BaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public bool IsDeleted { get; set; } = false;
    public DateTime? DeletedAt { get; set; }
    public void Delete()
    {
        IsDeleted = true;
        DeletedAt = DateTime.Now;
    }
    public void Restore()
    {
        IsDeleted = false;
        DeletedAt = null;
    }
}