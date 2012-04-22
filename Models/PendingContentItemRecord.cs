using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Records;

namespace Associativy.TagsAdapter.Models
{
    public class PendingContentItemRecord
    {
        public virtual int Id { get; set; }
        public virtual int ContentItemId { get; set; }
    }
}
