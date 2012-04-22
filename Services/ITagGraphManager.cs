using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orchard;
using Associativy.TagsAdapter.Models;

namespace Associativy.TagsAdapter.Services
{
    public interface ITagGraphManager : IDependency
    {
        IEnumerable<TagGraph> GetTagGraphs();
    }
}
