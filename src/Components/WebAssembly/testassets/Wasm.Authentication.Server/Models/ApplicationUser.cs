// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wasm.Authentication.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        public UserPreference UserPreference { get; set; }
    }
}
