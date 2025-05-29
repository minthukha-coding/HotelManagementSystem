using Newtonsoft.Json.Linq;

namespace HotelManagementSystem.Shared;

public class Result<T>
{
    public bool IsSuccess { get; set; }
    public bool IsError => !IsSuccess;
    public string? Message { get; set; }
    public T? Data { get; set; }

    public static Result<T> SuccessResult(T data, string message = "Operation successful.")
    {
        return new Result<T> { IsSuccess = true, Data = data, Message = message };
    }

    public static Result<T> SuccessResult(string message = "Operation successful.")
    {
        return new Result<T> { IsSuccess = true, Message = message };
    }

    public static Result<T> FailureResult(string message = "Operation Fail.")
    {
        return new Result<T> { IsSuccess = false, Message = message };
    }

    public static Result<T> FailureResult(object obj)
    {
        JObject jObj = JObject.FromObject(obj);
        return new Result<T>
        {
            IsSuccess = jObj["Success"]!.Value<bool>(),
            Message = jObj["Message"]!.ToString()
        };
    }

    public static Result<T> FailureResult(Exception ex)
    {
        return new Result<T> { IsSuccess = false, Message = ex.ToString() };
    }

    public static Result<T> ExecuteResult(int result)
    {
        return result > 0 ? SuccessResult() : FailureResult();
    }

    public static Result<T> Success<T>(string message)
    {
        return new Result<T>() { IsSuccess = true, Message = message };
    }
    public static Result<T> Success<T>(string message, T? data = default)
    {
        return new Result<T>() { IsSuccess = true, Message = message, Data = data };
    }

    public static Result<T> Fail<T>(string message, T? data = default)
    {
        return new Result<T>() { IsSuccess = false, Message = message, Data = data };
    }

    public static Result<T> Exception<T>(string message, Exception? ex)
    {
        return new Result<T>() { IsSuccess = false, Message = ex.ToString() };
    }
}