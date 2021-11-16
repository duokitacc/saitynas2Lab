using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saitynas1Lab.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string GameName { get; set; }
        public int Price { get; set; }
        public string Body { get; set; }
        public DateTime CreationDateUtc { get; set; }
        

    }
}
