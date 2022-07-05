using Inventory.Domain.Entities;

namespace Inventory.Application.Dtos;

public class ProductViewModel
{
    public string Id { get; set; }
    public string CategoryId { get; set; }
    public string Category { get; set; }
    public string BrandId { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public string Name { get; set; }
    public string SerialNumber { get; set; }
    public string Company { get; set; }
    public string Description { get; set; }
    public DebitTicket DebitTicket { get; set; }
}