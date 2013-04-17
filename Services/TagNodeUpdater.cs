using System;
using System.Linq;
using Associativy.Models;
using Associativy.Services;
using Associativy.TagsAdapter.Models;
using Orchard.ContentManagement;
using Orchard.Environment;
using Orchard.Exceptions;
using Orchard.Services;
using Orchard.Tasks.Scheduling;
using Piedone.HelpfulLibraries.Tasks;

namespace Associativy.TagsAdapter.Services
{
    public class TagNodeUpdater : IScheduledTaskHandler, IOrchardShellEvents
    {
        private readonly IContentManager _contentManager;
        private readonly IAssociativyServices _associativyServices;
        private readonly ITagGraphManager _tagGraphManager;
        private readonly IScheduledTaskManager _scheduledTaskManager;
        private readonly IClock _clock;


        public TagNodeUpdater(
            IContentManager contentManager,
            IAssociativyServices associativyServices,
            ITagGraphManager tagGraphManager,
            IScheduledTaskManager scheduledTaskManager,
            IClock clock)
        {
            _contentManager = contentManager;
            _associativyServices = associativyServices;
            _tagGraphManager = tagGraphManager;
            _scheduledTaskManager = scheduledTaskManager;
            _clock = clock;
        }


        public void Process(ScheduledTaskContext context)
        {
            if (context.Task.TaskType != "AssociativyTagNodeUpdate") return;

            Renew(true);
            
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
                catch (Exception ex)
                {
                    if (ex.IsFatal()) throw;

                    // The tag was deleted
                    _contentManager.Remove(node.ContentItem);
                    foreach (var graph in graphs)
                    {
                        graph.ConnectionManager.DeleteFromNode(node);
                    }
                }
            }
        }

        public void Activated()
        {
            Renew(false);
        }

        public void Terminating()
        {
        }


        private void Renew(bool calledFromTaskProcess)
        {
            _scheduledTaskManager.CreateTaskIfNew("AssociativyTagNodeUpdate", _clock.UtcNow.AddHours(1), null, calledFromTaskProcess);
        }
    }
}
