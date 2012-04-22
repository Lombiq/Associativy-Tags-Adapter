using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Associativy.Frontends.Engines.JIT;
using Associativy.GraphDiscovery;
using Orchard.ContentManagement.Aspects;
using Orchard.ContentManagement;
using Associativy.Models;

namespace Associativy.TagsAdapter
{
    public class JITConfigurationProvider : DefaultJITConfigurationProvider
    {
        public override void Describe(JITConfigurationDescriptor descriptor)
        {
            base.Describe(descriptor);

            descriptor.GraphContext = new GraphContext { ContentTypes = new string[] { "AssociativyTagNode" } };
            descriptor.ModifyGraphQuery = (query) => { };
            descriptor.SetupViewModel =
                (node, viewModel) =>
                {
                    if (node.ContentItem.ContentType == "AssociativyTagNode")
                    {
                        viewModel.name = node.As<AssociativyNodeLabelPart>().Label;
                    }
                    else viewModel.name = node.As<ITitleAspect>().Title;
                };
        }
    }
}
