using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saitynas1Lab.Auth.Model
{
    public static class DemoRestUserRoles
    {
        public const string Admin = nameof(Admin);
        public const string SimpleUser = nameof(SimpleUser);

        public static readonly IReadOnlyCollection<string> All = new[] { Admin, SimpleUser };
    }
}
