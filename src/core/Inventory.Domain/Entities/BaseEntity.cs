namespace Inventory.Domain.Entities;

public abstract class BaseEntity
{
    public string Id { get; set; }
    public DateTime CreateDate => DateTime.Now;
    public DateTime? UpdateDate { get; set; }
}