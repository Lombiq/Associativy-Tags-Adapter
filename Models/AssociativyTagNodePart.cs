using Orchard.ContentManagement;
using Orchard.Tags.Models;

namespace Associativy.TagsAdapter.Models
{
    public class AssociativyTagNodePart : ContentPart<AssociativyTagNodePartRecord>
    {
        public TagRecord Tag
        {
            get { return Record.Tag; }
            set { Record.Tag = value; }
        }
    }
}
