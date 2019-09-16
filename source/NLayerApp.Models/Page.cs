using NLayerApp.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NLayerApp.Models
{
    public class Page : IEntity, IEntity<int>
    {
        public int Id { get; set; }
        public string PageTitle { get; set; }
        public string Description { get; set; }
        public string Route { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
