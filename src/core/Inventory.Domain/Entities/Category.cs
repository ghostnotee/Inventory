namespace Inventory.Domain.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; }
    public string ParentCategoryId { get; set; }
}