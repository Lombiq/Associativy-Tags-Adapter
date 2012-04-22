using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orchard.ContentManagement.Records;
using Orchard.Tags.Models;
using Associativy.Models;
using Orchard.Data.Conventions;

namespace Associativy.TagsAdapter.Models
{
    public class AssociativyTagNodePartRecord : ContentPartRecord
    {
        // Maybe?
        //[Aggregate]
        public virtual TagRecord Tag { get; set; }
    }
}
