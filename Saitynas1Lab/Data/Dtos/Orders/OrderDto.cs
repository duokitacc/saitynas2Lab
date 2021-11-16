using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saitynas1Lab.Data.Dtos.Orders
{
    public record OrderDto(int Id, string GameName, int Price, string Body);
}
