using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using Associativy.TagsAdapter.Models;
using Associativy.Administration.Models;
using Orchard.ContentManagement;

namespace Associativy.TagsAdapter.Handlers
{
    public class AssociativyTagGraphPartHandler : ContentHandler
    {
        public AssociativyTagGraphPartHandler(IRepository<AssociativyTagGraphPartRecord> repository)
        {
            Filters.Add(StorageFilter.For(repository));

            OnUpdated<AssociativyGraphPart>((context, part) =>
                {
                    if (!part.As<AssociativyTagGraphPart>().IsTagGraph) return;

                    if (!part.ContainedContentTypes.Contains("AssociativyTagNode"))
                    {
                        part.ContainedContentTypes.Add("AssociativyTagNode");
                        // This is required as serialization only runs when the setter runs
                        part.ContainedContentTypes = part.ContainedContentTypes;
                    }
                });
        }
    }
}