using System.Data;
using System.Security;

namespace RailwayResult.Core;

public class Result<T> 
{
    public Exception? Exception { get; }
    public int StatusCode { get; }
    public T? Content { get; }

    private Result(T? content, int statusCode = 200)
    {
        Content = content;
        StatusCode = statusCode;
    }

    private Result(Exception exception)
    {
        Exception = exception;
        StatusCode = exception switch
        {
            ArgumentException => 400,
            SecurityException => 401,
            UnauthorizedAccessException => 403,
            EntryPointNotFoundException => 404,
            FileNotFoundException => 404,
            InvalidConstraintException => 409,
            _ => 500
        };
    }

    public static Result<T?> Ok(T content)
    {
        return new Result<T?>(content, content != null ? 200 : 204);
    }
    
    public static Result<T> Ko(Exception exception)
    {
        return new Result<T>(exception);
    }
}