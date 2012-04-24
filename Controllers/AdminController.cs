﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orchard.UI.Admin;
using System.Web.Mvc;
using Orchard.ContentManagement;
using Orchard;
using Associativy.Administration;
using Associativy.TagsAdapter.Services;
using Associativy.Services;
using Associativy.GraphDiscovery;

namespace Associativy.TagsAdapter.Controllers
{
    [Admin]
    public class AdminController : Controller
    {
        private readonly IOrchardServices _orchardServices;
        private readonly IContentManager _contentManager;
        private readonly IGraphManager _graphManager;
        private readonly IUpdateQueueManager _updaterService;

        public AdminController(
            IOrchardServices orchardServices,
            IGraphManager graphManager,
            IUpdateQueueManager updaterService)
        {
            _orchardServices = orchardServices;
            _contentManager = orchardServices.ContentManager;
            _graphManager = graphManager;
            _updaterService = updaterService;
        }

        [HttpPost]
        public void ProcessTaggedItems(string graphName)
        {
            if (!_orchardServices.Authorizer.Authorize(Permissions.ManageAssociativyGraphs)) return;

            var graphDescriptor = _graphManager.FindGraph(new GraphContext { GraphName = graphName });

            foreach (var item in _contentManager.Query(graphDescriptor.ContentTypes.Where(type => type != "AssociativyTagNode").ToArray()).List())
            {
                _updaterService.AddToQueue(item);
            }
        }
    }
}