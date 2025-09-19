namespace OnlineStore.Helpers;
using System.Net;
public class ApiResponseHelper<T>
{
    public bool Status { get; set; }
    public int StatusCode { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }

    public ApiResponseHelper(T? data, string? message = "", bool status = true, int statusCode = 200)
    {
        Status = status;
        StatusCode = statusCode;
        Message = message;
        Data = data;
    }

    // Generic static method to return a collectionresponse
    public static ApiResponseHelper<IEnumerable<T>> CollectionSuccess(IEnumerable<T> items, string? message = "")
    {
        return new ApiResponseHelper<IEnumerable<T>>(items, message, true, (int)HttpStatusCode.OK);
    }
    // Generic static method to return a collectionresponse
    public static ApiResponseHelper<T> Success(T item, string? message = "")
    {
        return new ApiResponseHelper<T>(item, message, true, (int)HttpStatusCode.OK);
    }

    // Fail Responese
    public static ApiResponseHelper<T> Fail(string? message = "", int statusCode = 400)
    {
        return new ApiResponseHelper<T>(default, message, false, statusCode);
    }
    public static ApiResponseHelper<T> Success(string? message = "") // method overloading -- polymorphism
    {
        return new ApiResponseHelper<T>(default, message, false, (int)HttpStatusCode.OK);
    }    
}
