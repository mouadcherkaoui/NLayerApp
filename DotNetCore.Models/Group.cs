using System;
using System.Reflection;
using System.Collections.Generic;
using DotNetCore.Infrastructure.Models;

namespace DotNetCore.Models
{
    public class Group : IEntity, IEntity<int>
    {
        public object this[string index] => this.GetType().GetProperty(index).GetValue(this, null);

        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Member> Members { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
