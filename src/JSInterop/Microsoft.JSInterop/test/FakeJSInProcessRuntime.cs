// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Microsoft.JSInterop;

// A strict test double for IJSInProcessRuntime: any call that has not been configured throws.
internal sealed class FakeJSInProcessRuntime : IJSInProcessRuntime
{
    public Func<string, object?[]?, object?>? OnInvoke { get; set; }

    public int InvokeCallCount { get; private set; }

    public TResult Invoke<TResult>(string identifier, params object?[]? args)
    {
        if (OnInvoke is null)
        {
            throw new InvalidOperationException($"Unexpected call to {nameof(Invoke)}<{typeof(TResult)}>(\"{identifier}\").");
        }

        InvokeCallCount++;
        return (TResult)OnInvoke(identifier, args)!;
    }

    public ValueTask<TValue> InvokeAsync<TValue>(string identifier, object?[]? args)
        => throw new InvalidOperationException($"Unexpected call to {nameof(InvokeAsync)}<{typeof(TValue)}>(\"{identifier}\").");

    public ValueTask<TValue> InvokeAsync<TValue>(string identifier, CancellationToken cancellationToken, object?[]? args)
        => throw new InvalidOperationException($"Unexpected call to {nameof(InvokeAsync)}<{typeof(TValue)}>(\"{identifier}\", CancellationToken, args).");
}
