using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PokemonManagingApp.Core.Models;

public class Pokemon
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    [MaxLength(50)]
    public required string Name { get; set; }
    public required DateTime BirthDate { get; set; }
    
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public ICollection<PokemonOwner> PokemonOwners { get; set; } = new List<PokemonOwner>();
    public ICollection<PokemonCategory> PokemonCategories { get; set; } = new List<PokemonCategory>();

}