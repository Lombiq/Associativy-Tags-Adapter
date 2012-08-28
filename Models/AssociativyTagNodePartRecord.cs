using Orchard.ContentManagement.Records;
using Orchard.Tags.Models;

namespace Associativy.TagsAdapter.Models
{
    public class AssociativyTagNodePartRecord : ContentPartRecord
    {
        // Maybe?
        //[Aggregate]
        public virtual TagRecord Tag { get; set; }
    }
}
