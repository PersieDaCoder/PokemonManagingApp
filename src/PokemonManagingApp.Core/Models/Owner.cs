using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokemonManagingApp.Core.Models;

public class Owner
{
    [Key]
    public Guid Id { get; init; } = Guid.NewGuid();
    [MaxLength(50)]
    public required string Email { get; set; }
    [MaxLength(50)]
    public required string Password { get; set; }
    [MaxLength(50)]
    public required string UserName { get; set; }
    [Required]
    public int Role { get; set; }
    [Required]
    public bool Status { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    [MaxLength(50)]
    public Guid GymId { get; set; }
    public required Guid CountryId { get; set; }
    // Navigation properties
    [ForeignKey(nameof(CountryId))]
    public Country Country { get; set; } = null!;
    [ForeignKey(nameof(GymId))]
    public Gym Gym { get; set; } = null!;
    public ICollection<PokemonOwner> PokemonOwners { get; set; } = new List<PokemonOwner>();
    public ICollection<OwnerReview> OwnerReviews { get; set; } = new List<OwnerReview>();
}