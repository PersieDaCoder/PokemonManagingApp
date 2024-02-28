using System.ComponentModel.DataAnnotations;


namespace DemoAPI.Core.Models;

public class Country
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    [MaxLength(50)]
    public required string Name { get; set; }
    
    public ICollection<Owner> Owners { get; set; } = new List<Owner>();
}