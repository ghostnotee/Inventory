namespace Inventory.Application.Features.Users;

public class AccessTokenViewModel
{
    public string Token { get; set; }
    public DateTime TokenExpiration { get; set; }
    public string RefreshToken { get; set; }
}