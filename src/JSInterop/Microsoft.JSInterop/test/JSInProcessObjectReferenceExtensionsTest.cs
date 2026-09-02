// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Microsoft.JSInterop;

public class JSInProcessObjectReferenceExtensionsTest
{
    [Fact]
    public void InvokeVoid_Works()
    {
        // Arrange
        var method = "someMethod";
        var args = new[] { "a", "b" };
        var jsInProcessObjectReference = new FakeJSInProcessObjectReference
        {
            OnInvoke = (identifier, actualArgs) =>
            {
                Assert.Equal(method, identifier);
                Assert.Equal(args, actualArgs);
                return FakeJSVoidResult.Instance;
            },
        };

        // Act
        jsInProcessObjectReference.InvokeVoid(method, args);

        // Assert
        Assert.Equal(1, jsInProcessObjectReference.InvokeCallCount);
    }
}
