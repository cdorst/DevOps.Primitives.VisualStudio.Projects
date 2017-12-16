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
        public MsBuildItemListAssociation(MsBuildItem item, MsBuildItemList itemList = null)
        {
            MsBuildItem = item;
            MsBuildItemList = itemList;
        }
        public MsBuildItemListAssociation(AsciiStringReference name, AsciiStringReference value, MsBuildCondition condition = null, MsBuildItemList itemList = null)
            : this(new MsBuildItem(name, value, condition), itemList)
        {
        }
        public MsBuildItemListAssociation(string name, string value, MsBuildCondition condition = null, MsBuildItemList itemList = null)
            : this(new AsciiStringReference(name), new AsciiStringReference(value), condition, itemList)
        {
        }
        public MsBuildItemListAssociation(string name, string value, string condition = null, MsBuildItemList itemList = null)
            : this(name, value, ConditionHelper.Create(condition), itemList)
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

        public void SetRecord(MsBuildItem record)
        {
            MsBuildItem = record;
            MsBuildItemId = MsBuildItem.MsBuildItemId;
        }
    }
}
