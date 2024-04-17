using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokemonManagingApp.UseCases.Helpers;

public static class ConvertRoleIntToString
{
    public static string ConvertIntToString(this int role)
    {
        return role switch
        {
            1 => "Admin",
            2 => "User",
            0 => "Guest",
            _ => throw new ArgumentException("Invalid role")
        };
    }
}