namespace Chrimesy.Dotnet.ROP;

public class RopResult<TSuccess, TFailure>
{
    private bool _successful;

    private RopResult()
    {
    }

    public TSuccess? Success { get; private init; }
    public TFailure? Failure { get; private init; }

    public bool IsSuccess()
    {
        return _successful;
    }

    public bool IsFailure()
    {
        return !IsSuccess();
    }

    public static RopResult<TSuccess, TFailure> OnSuccess(TSuccess success)
    {
        return new()
        {
            Success = success,
            Failure = default,
            _successful = true
        };
    }

    public static RopResult<TSuccess, TFailure> OnFailure(TFailure failure)
    {
        return new()
        {
            Success = default,
            Failure = failure,
            _successful = false
        };
    }
}