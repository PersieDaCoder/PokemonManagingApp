using System.ComponentModel.DataAnnotations.Schema;

namespace PokemonManagingApp.Core.Models;

public class OwnerReview
{
    // Properties
    public Guid OwnerId { get; set; }
    public Guid ReviewId { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool Status { get; set; } = true;
    // Navigation properties
    [ForeignKey(nameof(OwnerId))]
    public Owner Owner { get; set; } = null!;
    [ForeignKey(nameof(ReviewId))]
    public Review Review { get; set; } = null!;
}