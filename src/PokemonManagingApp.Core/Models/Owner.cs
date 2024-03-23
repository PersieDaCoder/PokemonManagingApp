using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace PokemonManagingApp.Core.Models;

public class Owner : IdentityUser
{
    public Owner() : base()
    {
        Id = Guid.NewGuid().ToString();
        SecurityStamp = Guid.NewGuid().ToString();
    }
    [MaxLength(50)]
    public string Name { get; set; } = "Username";
    [MaxLength(50)]
    public string Gym { get; set; } = "Gym";
    [ForeignKey(nameof(Country))]
    public Guid CountryId { get; set; } = Guid.Empty;
    public bool Status { get; set; } = true;

    public Country Country { get; set; } = null!;
    public ICollection<PokemonOwner> PokemonOwners { get; set; } = new List<PokemonOwner>();
}