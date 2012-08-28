using Associativy.Frontends.Engines.Jit;
using Associativy.Frontends.EventHandlers;
using Associativy.GraphDiscovery;
using Associativy.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Aspects;

namespace Associativy.TagsAdapter
{
    // Here other configuration handlers can be implemented too
    public class FrontendConfigurationHandler : IJitConfigurationHandler
    {
        private readonly IGraphManager _graphManager;

        public FrontendConfigurationHandler(IGraphManager graphManager)
        {
            _graphManager = graphManager;
        }

        public void SetupViewModel(FrontendContext frontendContext, IContent node, Frontends.Engines.Jit.ViewModels.NodeViewModel viewModel)
        {
            if (node.ContentItem.ContentType == "AssociativyTagNode")
            {
                viewModel.name = node.As<IAssociativyNodeLabelAspect>().Label;
            }
            else viewModel.name = node.As<ITitleAspect>().Title;
        }
    }
}
