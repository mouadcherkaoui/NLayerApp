using System;
using System.Reflection;
using System.Collections.Generic;
using NLayerApp.Infrastructure.Models;

namespace NLayerApp.Models
{
    public class Group : IEntity, IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual IEnumerable<GroupMembers> Members { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
