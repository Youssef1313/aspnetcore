// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TestServer
{
    public class ResourceRequestLog
    {
        private List<string> _requestPaths = new List<string>();

        public IReadOnlyCollection<string> RequestPaths => _requestPaths;

        public void AddRequest(HttpRequest request)
        {
            _requestPaths.Add(request.Path);
        }

        public void Clear()
        {
            _requestPaths.Clear();
        }
    }
}
