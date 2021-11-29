using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saitynas1Lab.Data.Dtos.Auth
{
    public class DemoRestUser : IdentityUser<Guid>
    {
        [PersonalData]
        public string AditionalInfo { get; set; }
    }
}
