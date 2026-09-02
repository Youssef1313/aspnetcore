// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Microsoft.JSInterop;

// A strict test double for IJSRuntime: any call that has not been configured throws.
internal sealed class FakeJSRuntime : IJSRuntime
{
    public Func<string, object?[]?, object?>? OnInvokeAsync { get; set; }

    public Func<string, CancellationToken, object?[]?, object?>? OnInvokeAsyncWithCancellationToken { get; set; }

    public int InvokeAsyncCallCount { get; private set; }

    public ValueTask<TValue> InvokeAsync<TValue>(string identifier, object?[]? args)
    {
        if (OnInvokeAsync is null)
        {
            throw new InvalidOperationException($"Unexpected call to {nameof(InvokeAsync)}<{typeof(TValue)}>(\"{identifier}\").");
        }

        InvokeAsyncCallCount++;
        return new ValueTask<TValue>((TValue)OnInvokeAsync(identifier, args)!);
    }

    public ValueTask<TValue> InvokeAsync<TValue>(string identifier, CancellationToken cancellationToken, object?[]? args)
    {
        if (OnInvokeAsyncWithCancellationToken is null)
        {
            throw new InvalidOperationException($"Unexpected call to {nameof(InvokeAsync)}<{typeof(TValue)}>(\"{identifier}\", CancellationToken, args).");
        }

        InvokeAsyncCallCount++;
        return new ValueTask<TValue>((TValue)OnInvokeAsyncWithCancellationToken(identifier, cancellationToken, args)!);
    }
}
