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
using Orchard.Tasks.Scheduling;
using Orchard.Services;

namespace Associativy.TagsAdapter.Services
{
    public class TagNodeUpdater : IScheduledTaskHandler
    {
        private readonly IContentManager _contentManager;
        private readonly IAssociativyServices _associativyServices;
        private readonly ITagGraphManager _tagGraphManager;
        private readonly IUpdateTaskRenewer _updateTaskRenewer;

        public TagNodeUpdater(
            IContentManager contentManager,
            IAssociativyServices associativyServices,
            ITagGraphManager tagGraphManager,
            IUpdateTaskRenewer updateTaskRenewer)
        {
            _contentManager = contentManager;
            _associativyServices = associativyServices;
            _tagGraphManager = tagGraphManager;
            _updateTaskRenewer = updateTaskRenewer;
        }

        public void Process(ScheduledTaskContext context)
        {
            if (context.Task.TaskType != "AssociativyTagNodeUpdate") return;

            var graphs = _tagGraphManager.GetTagGraphs();

            if (graphs.Count() == 0) return;

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

            _updateTaskRenewer.RenewTagNodeUpdate();
        }
    }
}
