namespace PokemonManagingApp.UseCase.DTOs;

public record ReviewDTO
{
    public Guid Id { get; init; } 
    public string Title { get; init; } = null!;
    public string Text { get; init; } = null!;
    public bool Status { get; init; }
    public PokemonDTO? Pokemon { get; set; }
    public ReviewerDTO? Reviewer { get; set; }
}