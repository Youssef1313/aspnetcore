// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Microsoft.JSInterop;

// A strict test double for IJSInProcessObjectReference: any call that has not been configured throws.
internal sealed class FakeJSInProcessObjectReference : IJSInProcessObjectReference
{
    public Func<string, object?[]?, object?>? OnInvoke { get; set; }

    public int InvokeCallCount { get; private set; }

    public TValue Invoke<TValue>(string identifier, params object?[]? args)
    {
        if (OnInvoke is null)
        {
            throw new InvalidOperationException($"Unexpected call to {nameof(Invoke)}<{typeof(TValue)}>(\"{identifier}\").");
        }

        InvokeCallCount++;
        return (TValue)OnInvoke(identifier, args)!;
    }

    public ValueTask<TValue> InvokeAsync<TValue>(string identifier, object?[]? args)
        => throw new InvalidOperationException($"Unexpected call to {nameof(InvokeAsync)}<{typeof(TValue)}>(\"{identifier}\").");

    public ValueTask<TValue> InvokeAsync<TValue>(string identifier, CancellationToken cancellationToken, object?[]? args)
        => throw new InvalidOperationException($"Unexpected call to {nameof(InvokeAsync)}<{typeof(TValue)}>(\"{identifier}\", CancellationToken, args).");

    public void Dispose()
    {
    }

    public ValueTask DisposeAsync() => ValueTask.CompletedTask;
}
