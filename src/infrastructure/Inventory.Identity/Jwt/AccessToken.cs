namespace Inventory.Identity.Jwt;

public class AccessToken
{
    public string Token { get; set; }
    public DateTime TokenExpiration { get; set; }
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpiration { get; set; }
}