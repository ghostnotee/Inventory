namespace Inventory.Domain.Entities;

public class UserOperationClaim : BaseEntity
{
    public string UserId { get; set; }
    public string OperationClaimId { get; set; }
}