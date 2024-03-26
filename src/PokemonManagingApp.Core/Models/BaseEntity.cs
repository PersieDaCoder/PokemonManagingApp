namespace PokemonManagingApp.Core.Models;

public class BaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public bool IsDeleted { get; set; } = false;
    public void Delete()
    {
        IsDeleted = true;
    }
    public void Restore()
    {
        IsDeleted = false;
    }
}