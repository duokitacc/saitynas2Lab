using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Saitynas1Lab.Data.Dtos.Posts
{
    public record CreatePostDto([Required] string GameName, int Price, [Required] string Body);
}
