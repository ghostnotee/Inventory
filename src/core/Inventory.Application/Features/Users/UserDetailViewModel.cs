using Inventory.Domain.Entities;

namespace Inventory.Application.Features.Users;

public class UserDetailViewModel
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; }
    public string Department { get; set; }
    public List<OperationClaim> OperationClaims { get; set; }
}