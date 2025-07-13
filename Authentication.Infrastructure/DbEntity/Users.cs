using System.Data;

namespace Authentication.Infrastructure.DbEntity
{
    public class Users
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string UserName { get; set; } = null!;

        public string? Email { get; set; } = null!;

        public bool EmailConfirmed { get; set; } = false;

        public string? PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; } = false;

        public string PasswordHash { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        public string? FirstName { get; set; }

        public string? LastName { get; set; }
        public int? TokenExpirationInMinutes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }
    }
}
