using Orchard.ContentManagement;

namespace Associativy.TagsAdapter.Models
{
    public class AssociativyTagGraphPart : ContentPart<AssociativyTagGraphPartRecord>
    {
        public bool IsTagGraph
        {
            get { return Retrieve(x => x.IsTagGraph); }
            set { Store(x => x.IsTagGraph, value); }
        }
    }
}
