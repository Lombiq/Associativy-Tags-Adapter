﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orchard.ContentManagement.Drivers;
using Associativy.TagsAdapter.Models;
using Orchard.ContentManagement;

namespace Associativy.TagsAdapter.Drivers
{
    public class AssociativyTagGraphPartDriver : ContentPartDriver<AssociativyTagGraphPart>
    {
        protected override string Prefix
        {
            get { return "Associativy.TagsAdapter.AssociativyTagGraphPartDriver"; }
        }

        // GET
        protected override DriverResult Editor(AssociativyTagGraphPart part, dynamic shapeHelper)
        {
            return ContentShape("Parts_AssociativyTagGraph_Edit",
                () => shapeHelper.EditorTemplate(
                        TemplateName: "Parts.AssociativyTagGraph",
                        Model: part,
                        Prefix: Prefix));
        }

        // POST
        protected override DriverResult Editor(AssociativyTagGraphPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            updater.TryUpdateModel(part, Prefix, null, null);

            return Editor(part, shapeHelper);
        }
    }
}