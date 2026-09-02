// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.JSInterop.Infrastructure;

namespace Microsoft.JSInterop;

internal sealed class FakeJSVoidResult : IJSVoidResult
{
    public static readonly FakeJSVoidResult Instance = new();
}
