using System;
using System.ComponentModel.DataAnnotations;
using NLayerAppp.Controllers.Attributes;
using NLayerAppp.Infrastructure.Models;

namespace NLayerAppp.MvcApp.Models
{
    [GeneratedController("api/v1/members")]
    public class Member : IEntity, IEntity<int>
    {
        public object this[string index] => this.GetType().GetProperty(index).GetValue(this, null);
        public Member(int id, string firstName, string lastName, string emailAddress, DateTime createdAt, DateTime modifiedAt) 
        {
            this.Id = id;
                this.FirstName = firstName;
                this.LastName = lastName;
                this.EmailAddress = emailAddress;
                this.CreatedAt = createdAt;
                this.ModifiedAt = modifiedAt;
               
        }
                public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [EmailAddress]
        public string EmailAddress { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}