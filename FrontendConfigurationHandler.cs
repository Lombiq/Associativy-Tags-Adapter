using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Associativy.Frontends.Engines.JIT;
using Associativy.Frontends.Engines;
using Associativy.GraphDiscovery;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Aspects;
using Associativy.Models;

namespace Associativy.TagsAdapter
{
    // Here other configuration handler can be implemented too
    public class FrontendConfigurationHandler : IJITConfigurationHandler
    {
        public void SetupViewModel(IEngineContext engineContext, IGraphContext graphContext, IContent node, Frontends.Engines.JIT.ViewModels.NodeViewModel viewModel)
        {
            if (node.ContentItem.ContentType == "AssociativyTagNode")
            {
                viewModel.name = node.As<IAssociativyNodeLabelAspect>().Label;
            }
            else viewModel.name = node.As<ITitleAspect>().Title;
        }
    }
}
