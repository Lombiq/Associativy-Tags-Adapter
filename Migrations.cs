using Associativy.Extensions;
using Associativy.TagsAdapter.Models;
using Orchard.ContentManagement.MetaData;
using Orchard.Data.Migration;

namespace Associativy.TagsAdapter
{
    public class Migrations : DataMigrationImpl
    {
        public int Create()
        {
            SchemaBuilder.CreateTable(typeof(PendingContentItemRecord).Name,
                table => table
                    .Column<int>("Id", column => column.PrimaryKey().Identity())
                    .Column<int>("ContentItemId")
                );

            SchemaBuilder.CreateTable(typeof(AssociativyTagNodePartRecord).Name,
                table => table
                    .ContentPartRecord()
                    .Column<int>("Tag_id")
                );

            ContentDefinitionManager.AlterTypeDefinition("AssociativyTagNode",
                cfg => cfg
                    .WithPart(typeof(AssociativyTagNodePart).Name)
                    .WithPart("CommonPart")
                    .WithLabel()
                );

            SchemaBuilder.CreateTable(typeof(AssociativyTagGraphPartRecord).Name,
                table => table
                    .ContentPartRecord()
                    .Column<bool>("IsTagGraph")
                );

            ContentDefinitionManager.AlterTypeDefinition("AssociativyGraph",
                cfg => cfg
                    .WithPart(typeof(AssociativyTagGraphPart).Name)
                );


            return 1;
        }

        //public int UpdateFrom1()
        //{

        //    return 2;
        //}
    }
}
