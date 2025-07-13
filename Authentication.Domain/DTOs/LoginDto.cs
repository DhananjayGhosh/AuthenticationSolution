namespace Authentication.Domain.DTOs
{
    public class LoginDto
    {
        public string userName { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
