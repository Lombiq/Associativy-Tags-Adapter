using System.Collections.Generic;
using Associativy.TagsAdapter.Models;
using Orchard;

namespace Associativy.TagsAdapter.Services
{
    public interface ITagGraphManager : IDependency
    {
        IEnumerable<TagGraph> GetTagGraphs();
    }
}
