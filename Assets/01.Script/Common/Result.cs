using UnityEngine;

public class Result
{
    public bool IsSuccess;
    public readonly string Message;

    public Result(bool isSuccess, string message = "")
    {
        IsSuccess = isSuccess;
        Message = message;
    }
}
