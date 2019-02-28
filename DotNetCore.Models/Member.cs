﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using DotNetCore.Infrastructure.Models;

namespace DotNetCore.Models
{
    public class Member : IEntity, IEntity<int>
    {
        public object this[string index] => this.GetType().GetProperty(index).GetValue(this, null);
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [EmailAddress]
        public string EmailAddress { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
