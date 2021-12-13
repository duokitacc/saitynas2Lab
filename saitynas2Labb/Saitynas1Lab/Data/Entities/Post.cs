using Saitynas1Lab.Auth.Model;
using Saitynas1Lab.Data.Dtos.Auth;
using System;
using System.ComponentModel.DataAnnotations;

namespace Saitynas1Lab.Data.Entities
{
    public class Post : IUserOwnedResource
    {
        public int Id { get; set; }
        public string GameName { get; set; }
        public int Price { get; set; }
        public string Body { get; set; }
        public DateTime CreationDateUtc { get; set; }
      


        [Required]
        public string UserId { get; set; }
        public DemoRestUser User { get; set; }
    }
}
