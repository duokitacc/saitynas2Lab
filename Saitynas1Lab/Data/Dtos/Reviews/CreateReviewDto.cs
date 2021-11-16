using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Saitynas1Lab.Data.Dtos.Reviews
{
    public record CreateReviewDto([Required] string Title,[Required]string Body);
}
