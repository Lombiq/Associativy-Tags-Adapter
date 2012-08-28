using Associativy.TagsAdapter.Models;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;

namespace Associativy.TagsAdapter.Handlers
{
    public class AssociativyTagNodePartHandler : ContentHandler
    {
        public AssociativyTagNodePartHandler(IRepository<AssociativyTagNodePartRecord> repository)
        {
            Filters.Add(StorageFilter.For(repository));
        }
    }
}
