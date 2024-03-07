using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PokemonManagingApp.Core.Models;

public class Reviewer
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    [MaxLength(50)]
    public required string FirstName { get; set; }
    [MaxLength(50)]
    public required string LastName { get; set; }
    public bool Status { get; set; } = true;

    public ICollection<Review> Reviews { get; set; } = new List<Review>();
}