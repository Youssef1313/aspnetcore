// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Wasm.Authentication.Server.Models
{
    public class UserPreference
    {
        public string Id { get; set; }

        public string ApplicationUserId { get; set; }

        public string Color { get; set; }
    }
}
