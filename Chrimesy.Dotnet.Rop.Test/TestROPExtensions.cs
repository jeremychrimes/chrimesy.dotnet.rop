using System;
using Chrimesy.Dotnet.ROP;
using NSubstitute;
using Xunit;


public class TestROPExtensions
{
    [Fact]
    public void TestRopBindSuccess()
    {
        // Arrange
        Func<int, int> func = x => x * 2;
        var input = RopResult<int, string>.OnSuccess(2);
        // Act
        var result = input.Bind(func);
        // Assert
        Assert.True(result.IsSuccess());
        Assert.Equal(4, result.Success);
    }
    [Fact]
    public void TestRopBindSuccessInlineFunc()
    {
        // Arrange
        var input = RopResult<int, string>.OnSuccess(2);
        // Act
        var result = input.Bind(x => x * 2);
        // Assert
        Assert.True(result.IsSuccess());
        Assert.Equal(4, result.Success);
    }
    
    [Fact]
    public void TestRopBindFailure()
    {
        // Arrange
        Func<int, int> func = x => x * 2;
        var input = RopResult<int, string>.OnFailure("failed");
        // Act
        var result = input.Bind(func);
        // Assert
        Assert.False(result.IsSuccess());
        Assert.True(result.IsFailure());
        Assert.Equal("failed", result.Failure);
    }

    [Fact]
    public void TestTeeFailed()
    {
        // Arrange 
        var mock = Substitute.For<ITestInterface>();
        
        var input = RopResult<int, string>.OnFailure("failed");
        // Act
        var output = input.Tee(x => mock.RunMe(x));
        // Assert 
        mock.DidNotReceive().RunMe(Arg.Any<int>());
    }
    [Fact]
    public void TestTeeSuccess()
    {
        // Arrange 
        int outter = 0;
        
        var input = RopResult<int, string>.OnSuccess(4);
        // Act
        var output = input.Tee(i => outter = i);
        // Assert 
        Assert.Equal(4, output.Success);
    }
}
public interface ITestInterface
{
    public void RunMe(int a);
}