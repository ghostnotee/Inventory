namespace Inventory.Identity.Jwt;

public class AccessToken
{
    public string Token { get; set; }
    public DateTime Expiration { get; set; }
}