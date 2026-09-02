// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Microsoft.JSInterop;

public class JSRuntimeExtensionsTest
{
    [Fact]
    public async Task InvokeAsync_WithParamsArgs()
    {
        // Arrange
        var method = "someMethod";
        var expected = new[] { "a", "b" };
        var jsRuntime = new FakeJSRuntime
        {
            OnInvokeAsync = (identifier, args) =>
            {
                Assert.Equal(method, identifier);
                Assert.Equal(expected, args);
                return "Hello";
            },
        };

        // Act
        var result = await jsRuntime.InvokeAsync<string>(method, "a", "b");

        // Assert
        Assert.Equal("Hello", result);
        Assert.Equal(1, jsRuntime.InvokeAsyncCallCount);
    }

    [Fact]
    public async Task InvokeAsync_WithParamsArgsAndCancellationToken()
    {
        // Arrange
        var method = "someMethod";
        var expected = new[] { "a", "b" };
        var cancellationToken = new CancellationToken();
        var jsRuntime = new FakeJSRuntime
        {
            OnInvokeAsyncWithCancellationToken = (identifier, token, args) =>
            {
                Assert.Equal(method, identifier);
                Assert.Equal(cancellationToken, token);
                Assert.Equal(expected, args);
                return "Hello";
            },
        };

        // Act
        var result = await jsRuntime.InvokeAsync<string>(method, cancellationToken, "a", "b");

        // Assert
        Assert.Equal("Hello", result);
        Assert.Equal(1, jsRuntime.InvokeAsyncCallCount);
    }

    [Fact]
    public async Task InvokeVoidAsync_WithoutCancellationToken()
    {
        // Arrange
        var method = "someMethod";
        var args = new[] { "a", "b" };
        var jsRuntime = new FakeJSRuntime
        {
            OnInvokeAsync = (identifier, actualArgs) =>
            {
                Assert.Equal(method, identifier);
                Assert.Equal(args, actualArgs);
                return FakeJSVoidResult.Instance;
            },
        };

        // Act
        await jsRuntime.InvokeVoidAsync(method, args);

        // Assert
        Assert.Equal(1, jsRuntime.InvokeAsyncCallCount);
    }

    [Fact]
    public async Task InvokeVoidAsync_WithCancellationToken()
    {
        // Arrange
        var method = "someMethod";
        var args = new[] { "a", "b" };
        var jsRuntime = new FakeJSRuntime
        {
            OnInvokeAsyncWithCancellationToken = (identifier, token, actualArgs) =>
            {
                Assert.Equal(method, identifier);
                Assert.Equal(args, actualArgs);
                return FakeJSVoidResult.Instance;
            },
        };

        // Act
        await jsRuntime.InvokeVoidAsync(method, new CancellationToken(), args);

        // Assert
        Assert.Equal(1, jsRuntime.InvokeAsyncCallCount);
    }

    [Fact]
    public async Task InvokeAsync_WithTimeout()
    {
        // Arrange
        var expected = "Hello";
        var method = "someMethod";
        var args = new[] { "a", "b" };
        var jsRuntime = new FakeJSRuntime
        {
            OnInvokeAsyncWithCancellationToken = (identifier, token, actualArgs) =>
            {
                Assert.Equal(method, identifier);
                Assert.Equal(args, actualArgs);

                // There isn't a very good way to test when the cts will cancel. We'll just verify that
                // it'll get cancelled eventually.
                Assert.True(token.CanBeCanceled);
                return expected;
            },
        };

        // Act
        var result = await jsRuntime.InvokeAsync<string>(method, TimeSpan.FromMinutes(5), args);

        // Assert
        Assert.Equal(expected, result);
        Assert.Equal(1, jsRuntime.InvokeAsyncCallCount);
    }

    [Fact]
    public async Task InvokeAsync_WithInfiniteTimeout()
    {
        // Arrange
        var expected = "Hello";
        var method = "someMethod";
        var args = new[] { "a", "b" };
        var jsRuntime = new FakeJSRuntime
        {
            OnInvokeAsyncWithCancellationToken = (identifier, token, actualArgs) =>
            {
                Assert.Equal(method, identifier);
                Assert.Equal(args, actualArgs);
                Assert.False(token.CanBeCanceled);
                Assert.True(token == CancellationToken.None);
                return expected;
            },
        };

        // Act
        var result = await jsRuntime.InvokeAsync<string>(method, Timeout.InfiniteTimeSpan, args);

        // Assert
        Assert.Equal(expected, result);
        Assert.Equal(1, jsRuntime.InvokeAsyncCallCount);
    }

    [Fact]
    public async Task InvokeVoidAsync_WithTimeout()
    {
        // Arrange
        var method = "someMethod";
        var args = new[] { "a", "b" };
        var jsRuntime = new FakeJSRuntime
        {
            OnInvokeAsyncWithCancellationToken = (identifier, token, actualArgs) =>
            {
                Assert.Equal(method, identifier);
                Assert.Equal(args, actualArgs);

                // There isn't a very good way to test when the cts will cancel. We'll just verify that
                // it'll get cancelled eventually.
                Assert.True(token.CanBeCanceled);
                return FakeJSVoidResult.Instance;
            },
        };

        // Act
        await jsRuntime.InvokeVoidAsync(method, TimeSpan.FromMinutes(5), args);

        // Assert
        Assert.Equal(1, jsRuntime.InvokeAsyncCallCount);
    }

    [Fact]
    public async Task InvokeVoidAsync_WithInfiniteTimeout()
    {
        // Arrange
        var method = "someMethod";
        var args = new[] { "a", "b" };
        var jsRuntime = new FakeJSRuntime
        {
            OnInvokeAsyncWithCancellationToken = (identifier, token, actualArgs) =>
            {
                Assert.Equal(method, identifier);
                Assert.Equal(args, actualArgs);
                Assert.False(token.CanBeCanceled);
                Assert.True(token == CancellationToken.None);
                return FakeJSVoidResult.Instance;
            },
        };

        // Act
        await jsRuntime.InvokeVoidAsync(method, Timeout.InfiniteTimeSpan, args);

        // Assert
        Assert.Equal(1, jsRuntime.InvokeAsyncCallCount);
    }
}
