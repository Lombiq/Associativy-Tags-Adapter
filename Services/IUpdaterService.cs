using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orchard;
using Orchard.ContentManagement;

namespace Associativy.TagsAdapter.Services
{
    public interface IUpdaterService : IDependency
    {
        void AddToQueue(IContent content);
        void RemoveFromQueue(IContent content);
        IEnumerable<IContent> GetPendingContents();
    }
}
