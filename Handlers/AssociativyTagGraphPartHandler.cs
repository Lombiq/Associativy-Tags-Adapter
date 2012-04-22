using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using Associativy.TagsAdapter.Models;

namespace Associativy.TagsAdapter.Handlers
{
    public class AssociativyTagGraphPartHandler : ContentHandler
    {
        public AssociativyTagGraphPartHandler(IRepository<AssociativyTagGraphPartRecord> repository)
        {
            Filters.Add(StorageFilter.For(repository));
        }
    }
}