using System.Data;

namespace Authentication.Domain.Entities
{
    public class UserEntity
    {
            public Guid Id { get; set; }
            public string UserName { get; set; } = null!;
            public string Email { get; set; } = null!;  
            public string? FirstName { get; set; }
            public string? LastName { get; set; }
            public int TokenExpirationInMinutes { get; set; }
            public string? PhoneNum { get; set; }
        
    }

}
