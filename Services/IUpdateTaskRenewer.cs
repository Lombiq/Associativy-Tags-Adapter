using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orchard;

namespace Associativy.TagsAdapter.Services
{
    public interface IUpdateTaskRenewer : IDependency
    {
        void RenewTagNodeUpdate();
    }
}
