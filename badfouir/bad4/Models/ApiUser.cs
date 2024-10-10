using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace bad4.Models
{
    public class ApiUser : IdentityUser
    {
        [MaxLength(100)]
        public string? FullName { get; set; }

    }
}
