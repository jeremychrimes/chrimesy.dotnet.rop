using System.Runtime;

namespace Chrimesy.Dotnet.ROP;

public static class ROPExtensions
{
    public static RopResult<TSuccessOut, TFailure> Bind<TSuccessIn, TFailure, TSuccessOut>(
        this RopResult<TSuccessIn, TFailure> input, Func<TSuccessIn, TSuccessOut> func)
    {
        return input.IsSuccess() 
            ? RopResult<TSuccessOut, TFailure>.OnSuccess(func(input.Success)) 
            : RopResult<TSuccessOut, TFailure>.OnFailure(input.Failure);
    }

    public static RopResult<TSuccessOut, TFailure> Bind<TSuccessIn, TFailure, TSuccessOut>(
        this RopResult<TSuccessIn, TFailure> input,
        Func<RopResult<TSuccessIn, TFailure>, RopResult<TSuccessOut, TFailure>> func)
    {
        return input.IsSuccess() 
            ? func(input)
            : RopResult<TSuccessOut, TFailure>.OnFailure(input.Failure);
        
    }

    public static RopResult<TSuccess, TFailure> Tee<TSuccess, TFailure>(
        this RopResult<TSuccess, TFailure> input, Action<TSuccess> func)
    {
        if (input.IsFailure()) return RopResult<TSuccess, TFailure>.OnFailure(input.Failure);
        func(input.Success);
        return RopResult<TSuccess, TFailure>.OnSuccess(input.Success);
    }
}