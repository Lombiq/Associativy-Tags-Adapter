using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orchard.Tasks;
using Orchard.ContentManagement;
using Associativy.TagsAdapter.Models;
using Associativy.Services;
using Associativy.GraphDiscovery;
using Associativy.Models;

namespace Associativy.TagsAdapter.Services
{
    // This should run e.g. every hour, not every minute
    // With static LastRunDateTime?
    public class TagNodeUpdater : IBackgroundTask
    {
        private readonly IContentManager _contentManager;
        private readonly IAssociativyServices _associativyServices;
        private readonly ITagGraphManager _tagGraphManager;

        public TagNodeUpdater(
            IContentManager contentManager,
            IAssociativyServices associativyServices,
            ITagGraphManager tagGraphManager)
        {
            _contentManager = contentManager;
            _associativyServices = associativyServices;
            _tagGraphManager = tagGraphManager;
        }

        public void Sweep()
        {
            var graphs = _tagGraphManager.GetTagGraphs();

            foreach (var node in _contentManager.Query<AssociativyTagNodePart>("AssociativyTagNode").Join<AssociativyTagNodePartRecord>().List())
            {
                try
                {
                    // This throws an exception if the tag was deleted. Is there a better way for this? This is a bit ugly...
                    if (node.Tag.TagName != node.As<AssociativyNodeLabelPart>().Label)
                    {
                        node.As<AssociativyNodeLabelPart>().Label = node.Tag.TagName;
                    }
                }
                catch (Exception)
                {
                    // The tag was deleted
                    _contentManager.Remove(node.ContentItem);
                    foreach (var graph in graphs)
                    {
                        graph.ConnectionManager.DeleteFromNode(graph.GraphContext, node); 
                    }
                }
            }
        }
    }
}
