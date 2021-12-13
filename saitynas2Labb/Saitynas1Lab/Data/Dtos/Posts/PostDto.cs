using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saitynas1Lab.Data.Dtos.Posts
{
    public record PostDto(int Id, string GameName,int Price, string Body,string UserId);
}
