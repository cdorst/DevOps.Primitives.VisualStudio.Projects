using Common.EntityFrameworkServices;
using DevOps.Primitives.Strings;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.String;

namespace DevOps.Primitives.VisualStudio.Projects
{
    [ProtoContract]
    [Table("MsBuildItems", Schema = nameof(VisualStudio))]
    public class MsBuildItem : IUniqueListRecord
    {
        public MsBuildItem() { }
        public MsBuildItem(
            in AsciiStringReference name,
            in MsBuildItemAttributeList attributeList,
            in MsBuildCondition condition = default)
        {
            ElementName = name;
            MsBuildCondition = condition;
            MsBuildItemAttributeList = attributeList;
        }
        public MsBuildItem(
            in string name,
            in MsBuildItemAttributeList attributeList,
            in MsBuildCondition condition = default)
            : this(new AsciiStringReference(in name), in attributeList, in condition)
        {
        }
        public MsBuildItem(
            in string name,
            in MsBuildItemAttributeList attributeList,
            in string condition = default)
            : this(in name, in attributeList, ConditionHelper.Create(in condition))
        {
        }

        [Key]
        [ProtoMember(1)]
        public int MsBuildItemId { get; set; }

        [ProtoMember(2)]
        public AsciiStringReference ElementName { get; set; }
        [ProtoMember(3)]
        public int ElementNameId { get; set; }

        [ProtoMember(4)]
        public MsBuildCondition MsBuildCondition { get; set; }
        [ProtoMember(5)]
        public int? MsBuildConditionId { get; set; }

        [ProtoMember(6)]
        public MsBuildItemAttributeList MsBuildItemAttributeList { get; set; }
        [ProtoMember(7)]
        public int MsBuildItemAttributeListId { get; set; }

        public string GetItem()
            => Concat("    <", ElementName.Value, MsBuildCondition?.GetCondition(), " ", MsBuildItemAttributeList.GetItemAttributes(), " />");
    }
}
