using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saitynas1Lab.Data.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public string Initiator { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }

        public DateTime CreationDateUtc { get; set; }

        
        public int PostId { get; set; }



    }
}
