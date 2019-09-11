using System;
using System.Reflection;
using NLayerApp.Infrastructure.Models;

namespace NLayerApp.Models
{
    public class Room : IEntity, IEntity<int>
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public string RoomName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}