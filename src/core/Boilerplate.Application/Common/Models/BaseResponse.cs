namespace Boilerplate.Application.Common.Models;

public class BaseResponse<T>
{
    public BaseResponse()
    {
        Errors = new List<string>();
    }

    public bool HasError => Errors.Any();
    public List<string> Errors { get; set; }
    public int Total { get; set; }
    public T Result { get; set; }
}