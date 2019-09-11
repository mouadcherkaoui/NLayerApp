using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using NLayerApp.Infrastructure.Models;

namespace NLayerApp.Models
{
    public class Member : IEntity, IEntity<int>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [EmailAddress]
        public string EmailAddress { get; set; }
        public virtual IEnumerable<GroupMembers> Groups { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
