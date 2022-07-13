using Inventory.Domain.Entities;

namespace Inventory.Identity.Jwt;

public interface ITokenHelper
{
    AccessToken CreateToken(User user, List<OperationClaim> operationClaims);
}