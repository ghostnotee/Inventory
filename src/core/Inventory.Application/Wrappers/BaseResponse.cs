namespace Inventory.Application.Wrappers;

public class BaseResponse
{
    public string Id { get; set; }
    public bool IsSuccess { get; set; } = true;
    public string Message { get; set; }
}