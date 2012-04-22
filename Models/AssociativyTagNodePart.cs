using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orchard.ContentManagement;
using Associativy.Models;
using Orchard.Tags.Models;
using Orchard.ContentManagement.Aspects;

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
