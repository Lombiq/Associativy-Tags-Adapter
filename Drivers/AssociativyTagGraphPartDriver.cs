using Associativy.TagsAdapter.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.Handlers;

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

        protected override void Exporting(AssociativyTagGraphPart part, ExportContentContext context)
        {
            context.Element(part.PartDefinition.Name).SetAttributeValue("IsTagGraph", part.IsTagGraph);
        }

        protected override void Importing(AssociativyTagGraphPart part, ImportContentContext context)
        {
            context.ImportAttribute(part.PartDefinition.Name, "IsTagGraph", value => part.IsTagGraph = bool.Parse(value));
        }
    }
}
