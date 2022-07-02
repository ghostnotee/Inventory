using Inventory.Domain.Enums;

namespace Inventory.Domain.Entities;

public class Product : BaseEntity
{
    public string DeviceCategoryId { get; set; }
    public DeviceCategory DeviceCategory { get; set; }
    public string BrandId { get; set; }
    public Brand Brand { get; set; }
    public string Model { get; set; }
    public string Name { get; set; }
    public string SerialNumber { get; set; }
    public Companies Company { get; set; }
    public string Description { get; set; }
    public DebitTicket DebitTicket { get; set; }
}