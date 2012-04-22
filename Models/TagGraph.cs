using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Associativy.GraphDiscovery;
using Associativy.Services;

namespace Associativy.TagsAdapter.Models
{
    public abstract class TagGraph
    {
        public IGraphContext GraphContext { get; set; }
        public IConnectionManager ConnectionManager { get; set; }
    }
}
