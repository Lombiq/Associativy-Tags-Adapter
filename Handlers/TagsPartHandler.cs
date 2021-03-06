﻿using Associativy.TagsAdapter.Services;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Aspects;
using Orchard.ContentManagement.Handlers;
using Orchard.Core.Contents.Settings;
using Orchard.Tags.Models;
using Orchard.Tags.Services;

namespace Associativy.TagsAdapter.Handlers
{
    public class TagsPartHandler : ContentHandler
    {
        public TagsPartHandler(
            ITagService tagService,
            IUpdateQueueManager updaterService)
        {
            OnUpdated<TagsPart>(
                (context, part) =>
                {
                    // If a content type is not draftable OnPublished() will not run, therefore we do the same here.
                    // With draftables we only deal with tag nodes after publishing.
                    if (!context.ContentItem.Has<IPublishingControlAspect>() && !context.ContentItem.TypeDefinition.Settings.GetModel<ContentTypeSettings>().Draftable)
                    {
                        updaterService.AddToQueue(context.ContentItem);
                    }
                });

            OnPublished<TagsPart>(
                (context, part) =>
                {
                    updaterService.AddToQueue(context.ContentItem);
                });
        }
    }
}
