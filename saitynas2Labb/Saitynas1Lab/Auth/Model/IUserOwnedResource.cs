﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saitynas1Lab.Auth.Model
{
    public interface IUserOwnedResource
    {
        string UserId { get; }
    }
}
