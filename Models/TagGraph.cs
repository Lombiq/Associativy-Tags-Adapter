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
