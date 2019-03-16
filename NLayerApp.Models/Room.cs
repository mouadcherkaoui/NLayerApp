using System;
using System.Reflection;
using NLayerAppp.Infrastructure.Models;

namespace NLayerAppp.Models
{
    public class Room : IEntity, IEntity<int>
    {
        public object this[string index] => this.GetType().GetProperty(index).GetValue(this, null);
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public string RoomName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}