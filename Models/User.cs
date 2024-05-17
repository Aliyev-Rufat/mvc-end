using Microsoft.AspNetCore.Identity;

namespace WebApplication13.Models
{
    public class User:IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
