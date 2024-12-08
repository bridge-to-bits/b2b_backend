namespace Core.Interfaces.Auth
{
    public class JwtOptions
    {
        public string SecretKey { get; set; }
        public int ExpiresHours { get; set; }
    }
}
