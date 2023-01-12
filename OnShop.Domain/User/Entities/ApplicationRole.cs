using Microsoft.AspNetCore.Identity;

namespace OnShop.Domain.User.Entities
{
    public class ApplicationRole : IdentityRole<int>
    {
        public string Description { get; set; }
    }
}