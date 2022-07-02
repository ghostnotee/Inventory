namespace Inventory.Domain.Entities;

public class DeviceCategory : BaseEntity
{
    public string Name { get; set; }
    public string ParentCategoryId { get; set; }
}