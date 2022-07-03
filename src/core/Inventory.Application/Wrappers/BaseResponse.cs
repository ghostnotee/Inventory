namespace Inventory.Application.Wrappers;

public class BaseResponse
{
    public string Id { get; set; }
    public bool Success { get; set; }
    public string Message { get; set; }
}