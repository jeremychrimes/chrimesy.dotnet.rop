using Chrimesy.Dotnet.ROP;
using Xunit;

public class UnitTest1
{
    [Fact]
    public void TestSuccess()
    {
        var result = RopResult<int, int>.OnSuccess(365);
        Assert.True(result.IsSuccess());
        Assert.False(result.IsFailure());
        Assert.Equal(365, result.Success);
    }

    [Fact]
    public void TestFailure()
    {
        var result = RopResult<int, int>.OnFailure(42);
        Assert.False(result.IsSuccess());
        Assert.True(result.IsFailure());
        Assert.Equal(42, result.Failure);
    }
}