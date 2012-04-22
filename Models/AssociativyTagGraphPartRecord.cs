using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orchard.ContentManagement.Records;

namespace Associativy.TagsAdapter.Models
{
    public class AssociativyTagGraphPartRecord : ContentPartRecord
    {
        public virtual bool IsTagGraph { get; set; }
    }
}
