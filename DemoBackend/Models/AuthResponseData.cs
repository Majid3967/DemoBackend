namespace DemoBackend.Models
{
    public class AuthResponseData
    {
        public string idToken { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public string TokenExpires { get; set; } = string.Empty;

    }
}
