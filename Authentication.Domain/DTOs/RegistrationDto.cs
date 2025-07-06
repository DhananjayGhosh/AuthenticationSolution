namespace Authentication.Domain.DTOs
{
    public class RegistrationDto
    {
        public string UserName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? PhoneNumber { get; set; }

        public string Password { get; set; } = null!;

        public string? FirstName { get; set; }

        public string? LastName { get; set; }
        public int TokenExpirationInMinutes { get; set; }
    }
}
