namespace Inventory.Application.Parameters;

public class RequestParameter
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public RequestParameter()
    {
        PageNumber = 1;
        PageSize = 10;
    }

    public RequestParameter(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}