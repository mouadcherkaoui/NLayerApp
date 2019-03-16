using System;

namespace NLayerAppp.Infrastructure.Models
{
    public interface IEntity
    {
        object this[string index]{ get; }
        DateTime CreatedAt { get; set; }
        DateTime ModifiedAt { get; set; }
    }
    public interface IEntity<TKey> : IEntity
    {
        TKey Id { get; set; }
    }
}
