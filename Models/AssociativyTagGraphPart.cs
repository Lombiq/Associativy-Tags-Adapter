using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orchard.ContentManagement;

namespace Associativy.TagsAdapter.Models
{
    public class AssociativyTagGraphPart : ContentPart<AssociativyTagGraphPartRecord>
    {
        public bool IsTagGraph
        {
            get { return Record.IsTagGraph; }
            set { Record.IsTagGraph = value; }
        }
    }
}
