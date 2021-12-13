using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saitynas1Lab.Data.Dtos.Reviews
{
    public record ReviewDto(int Id,string Initiator,string Title,string Body);
}
