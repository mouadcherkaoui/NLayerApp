using System;

namespace NLayerApp.Infrastructure.Models
{
    public interface IEntity
    {
        DateTime CreatedAt { get; set; }
        DateTime ModifiedAt { get; set; }
    }
    public interface IEntity<TKey> : IEntity
    {
        TKey Id { get; set; }
    }

    public interface IManyToManyEntity<TType, TKey> : IEntity<TKey>, IEntity
    {
    }
}
