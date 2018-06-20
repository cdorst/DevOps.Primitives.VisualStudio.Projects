using Common.EntityFrameworkServices;
using DevOps.Primitives.Strings;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.VisualStudio.Projects
{
    [ProtoContract]
    [Table("MsBuildItemListAssociations", Schema = nameof(VisualStudio))]
    public class MsBuildItemListAssociation : IUniqueListAssociation<MsBuildItem>
    {
        public MsBuildItemListAssociation() { }
        public MsBuildItemListAssociation(
            in MsBuildItem item,
            in MsBuildItemList itemList = default)
        {
            MsBuildItem = item;
            MsBuildItemList = itemList;
        }
        public MsBuildItemListAssociation(
            in AsciiStringReference name,
            in MsBuildItemAttributeList attributes,
            in MsBuildCondition condition = default,
            in MsBuildItemList itemList = default)
            : this(new MsBuildItem(in name, in attributes, in condition), in itemList)
        {
        }
        public MsBuildItemListAssociation(
            in string name,
            in MsBuildItemAttributeList attributes,
            in MsBuildCondition condition = default,
            in MsBuildItemList itemList = default)
            : this(new AsciiStringReference(in name), in attributes, in condition, in itemList)
        {
        }
        public MsBuildItemListAssociation(
            in string name,
            in MsBuildItemAttributeList attributes,
            in string condition = default,
            in MsBuildItemList itemList = default)
            : this(in name, in attributes, ConditionHelper.Create(in condition), in itemList)
        {
        }

        [Key]
        [ProtoMember(1)]
        public int MsBuildItemListAssociationId { get; set; }

        [ProtoMember(2)]
        public MsBuildItem MsBuildItem { get; set; }
        [ProtoMember(3)]
        public int MsBuildItemId { get; set; }

        [ProtoMember(4)]
        public MsBuildItemList MsBuildItemList { get; set; }
        [ProtoMember(5)]
        public int MsBuildItemListId { get; set; }

        public MsBuildItem GetRecord() => MsBuildItem;

        public void SetRecord(in MsBuildItem record)
        {
            MsBuildItem = record;
            MsBuildItemId = record.MsBuildItemId;
        }
    }
}
