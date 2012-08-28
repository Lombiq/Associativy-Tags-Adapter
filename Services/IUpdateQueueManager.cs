using System.Collections.Generic;
using Orchard;
using Orchard.ContentManagement;

namespace Associativy.TagsAdapter.Services
{
    public interface IUpdateQueueManager : IDependency
    {
        void AddToQueue(IContent content);
        void RemoveFromQueue(IContent content);
        IEnumerable<IContent> GetPendingContents();
    }
}
