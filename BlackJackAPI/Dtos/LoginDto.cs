namespace BlackJackAPI.Dtos
{
    public class PlayerLoginDto
    {
        public string UsernameOrEmail { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
