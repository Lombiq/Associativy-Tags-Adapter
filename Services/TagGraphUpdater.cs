using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orchard.Tasks;
using Piedone.HelpfulLibraries.Tasks;
using Associativy.Services;
using Orchard.ContentManagement;
using Orchard.Tags.Models;
using Associativy.GraphDiscovery;
using Orchard.Tags.Services;
using Associativy.TagsAdapter.Models;
using Associativy.Models;
using Associativy.Administration.Models;

namespace Associativy.TagsAdapter.Services
{
    public class TagGraphUpdater : IBackgroundTask
    {
        private readonly ILockFileManager _lockFileManager;
        private readonly IUpdateQueueManager _updaterQueueManager;
        private readonly ITagGraphManager _tagGraphManager;
        private readonly ITagService _tagService;
        private readonly IAssociativyServices _associativyServices;
        private readonly IContentManager _contentManager;

        public TagGraphUpdater(
            ILockFileManager lockFileManager,
            IUpdateQueueManager updaterQueueManager,
            ITagGraphManager tagGraphManager,
            ITagService tagService,
            IAssociativyServices associativyServices,
            IContentManager contentManager)
        {
            _lockFileManager = lockFileManager;
            _updaterQueueManager = updaterQueueManager;
            _tagGraphManager = tagGraphManager;
            _tagService = tagService;
            _associativyServices = associativyServices;
            _contentManager = contentManager;
        }

        public void Sweep()
        {
            using (var lockFile = _lockFileManager.TryAcquireLock("Associativy.TagsAdapter.Services.NodesUpdater"))
            {
                if (lockFile != null)
                {
                    var graphs = _tagGraphManager.GetTagGraphs();

                    if (graphs.Count() == 0) return;

                    var pending = _updaterQueueManager.GetPendingContents();

                    foreach (var content in pending)
                    {
                        var tags = content.ContentItem.As<TagsPart>().CurrentTags;
                        var tagNodes = new List<IContent>();
                        foreach (var tag in tags)
                        {
                            // Maybe this could be optimized by doing it in one query for all tag ids?
                            var tagNode = _contentManager.Query().Where<AssociativyTagNodePartRecord>(node => node.Tag.Id == tag.Id).List().FirstOrDefault();
                            if (tagNode == null)
                            {
                                tagNode = _contentManager.New("AssociativyTagNode");
                                tagNode.As<AssociativyTagNodePart>().Tag = tag;
                                tagNode.As<AssociativyNodeLabelPart>().Label = tag.TagName;
                                _contentManager.Create(tagNode);
                            }

                            tagNodes.Add(tagNode);

                            var taggedContents = _tagService.GetTaggedContentItems(tag.Id); // Includes the current item too
                            foreach (var taggedContent in taggedContents)
                            {
                                foreach (var graph in graphs)
                                {
                                    graph.ConnectionManager.Connect(graph.GraphContext, tagNode, taggedContent);
                                }
                            }
                        }

                        // Removing connections from the content that are not among the current tags (i.e. removed tags).
                        // This also removes any other connected contents too...
                        var tagNodeIds = tagNodes.Select(tag => tag.Id);
                        foreach (var graph in graphs)
                        {
                            foreach (var neighbourId in graph.ConnectionManager.GetNeighbourIds(graph.GraphContext, content))
                            {
                                if (!tagNodeIds.Contains(neighbourId))
                                {
                                    graph.ConnectionManager.Disconnect(graph.GraphContext, neighbourId, content.Id);
                                }
                            }
                        }

                        _updaterQueueManager.RemoveFromQueue(content);
                    }
                }
            }
        }
    }
}
