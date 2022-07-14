using Inventory.Identity.Jwt;

namespace Inventory.Application.Features.Users;

public class LoginUserViewModel
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; }
    public string Token { get; set; }
    public DateTime TokenExpiration { get; set; }
    public string RefreshToken { get; set; }
}