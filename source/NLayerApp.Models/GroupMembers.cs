using System;
using System.Reflection;
using System.Collections.Generic;
using NLayerApp.Infrastructure.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NLayerApp.Models
{
    public class GroupMembers : IEntity, IManyToManyEntity<Group, int>
    {
        public object this[string index] => this.GetType().GetProperty(index).GetValue(this, null);
        [NotMapped]
        public int Id { get; set; }
        [ForeignKey("GroupId")]
        public int GroupId { get; set; }
        public Group Group { get; set; }
        [ForeignKey("MemberId")]
        public int MemberId { get; set; }
        public Member Member { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
