namespace Inventory.Domain.Entities;

public class EmailConfirmation : BaseEntity
{
    public string OldEmailAddress { get; set; }
    public string NewEmail { get; set; }
}