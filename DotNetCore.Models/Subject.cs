using System;
using System.Reflection;
using DotNetCore.Infrastructure.Models;

namespace DotNetCore.Models
{
    public class Subject : IEntity, IEntity<int>
    {
        public object this[string index] => this.GetType().GetProperty(index).GetValue(this, null);
        public int Id { get; set; }
        public string SubjectName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
