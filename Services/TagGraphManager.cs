﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Associativy.TagsAdapter.Models;
using Orchard.ContentManagement;
using Associativy.Services;
using Associativy.Administration.Models;
using Associativy.GraphDiscovery;

namespace Associativy.TagsAdapter.Services
{
    public class TagGraphManager : ITagGraphManager
    {
        private readonly IContentManager _contentManager;
        private readonly IAssociativyServices _associativyServices;

        public TagGraphManager(
            IContentManager contentManager,
            IAssociativyServices associativyServices)
        {
            _contentManager = contentManager;
            _associativyServices = associativyServices;
        }

        public IEnumerable<TagGraph> GetTagGraphs()
        {
            var graphs = _contentManager.Query<AssociativyGraphPart>("AssociativyGraph").Where<AssociativyTagGraphPartRecord>(record => record.IsTagGraph).List();
            var graphInfos = new List<TagGraph>();
            foreach (var graph in graphs)
            {
                var graphContext = new GraphContext { GraphName = graph.GraphName };
                graphInfos.Add(new TagGraphImpl
                {
                    GraphContext = graphContext,
                    ConnectionManager = _associativyServices.GraphManager.FindGraph(graphContext).ConnectionManager
                });
            }

            return graphInfos;
        }

        private class TagGraphImpl : TagGraph
        {
        }
    }
}