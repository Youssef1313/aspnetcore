// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Wasm.Performance.Driver
{
    class BenchmarkScenarioResult
    {
        public string Name { get; set; }

        public BenchmarkDescriptor Descriptor { get; set; }

        public string ShortDescription { get; set; }

        public bool Success { get; set; }

        public int NumExecutions { get; set; }

        public double Duration { get; set; }

        public class BenchmarkDescriptor
        {
            public string Name { get; set; }

            public string Description { get; set; }
        }
    }
}