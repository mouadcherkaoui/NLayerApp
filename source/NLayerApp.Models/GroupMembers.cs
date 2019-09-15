using System;
using System.Reflection;
using System.Collections.Generic;
using NLayerApp.Infrastructure.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using NLayerApp.Models.Configurations;
using NLayerApp.Infrastructure.DataAccessLayer;
using Newtonsoft.Json;

namespace NLayerApp.Models
{
    [TypeConfiguration(typeof(GroupMembersConfiguration))]
    public class GroupMembers : IEntity, IEntity<object>
    {
        public object this[string index] => this.GetType().GetProperty(index).GetValue(this, null);
        private object _id = new { MemberId=0, GroupId=0 };
        [NotMapped]
        public object Id { get => _id = new { MemberId, GroupId }; set => _id = value; }

        public int GroupId { get; set; }
        [JsonIgnore]
        public Group Group { get; set; }
        public int MemberId { get; set; }
        [JsonIgnore]
        public Member Member { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
