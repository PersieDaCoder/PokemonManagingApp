namespace PokemonManagingApp.UseCase.DTOs;

public record ReviewDTO
{
    public required Guid Id { get; init; } 
    public required string Title { get; init; }
    public required string Text { get; init; }
    public bool Status { get; init; }
    public required Guid ReviewerId { get; init; }
    public required Guid PokemonId { get; init; }
    public PokemonDTO? Pokemon { get; set; }
    public ReviewerDTO? Reviewer { get; set; }
}