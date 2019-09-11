using System;
using System.Reflection;
using NLayerApp.Infrastructure.Models;

namespace NLayerApp.Models
{
    public class Subject : IEntity, IEntity<int>
    {
        public int Id { get; set; }
        public string SubjectName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
