using Common.EntityFrameworkServices;
using DevOps.Primitives.Strings;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.VisualStudio.Projects
{
    [ProtoContract]
    [Table("MsBuildItems", Schema = nameof(VisualStudio))]
    public class MsBuildItem : IUniqueListRecord
    {
        public MsBuildItem() { }
        public MsBuildItem(AsciiStringReference name, MsBuildItemAttributeList attributeList, MsBuildCondition condition = null)
        {
            ElementName = name;
            MsBuildCondition = condition;
            MsBuildItemAttributeList = attributeList;
        }
        public MsBuildItem(string name, MsBuildItemAttributeList attributeList, MsBuildCondition condition = null)
            : this(new AsciiStringReference(name), attributeList, condition)
        {
        }
        public MsBuildItem(string name, MsBuildItemAttributeList attributeList, string condition = null)
            : this(name, attributeList, ConditionHelper.Create(condition))
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

        public string GetItem() => $"<{ElementName.Value}{MsBuildCondition?.GetCondition()} {MsBuildItemAttributeList.GetItemAttributes()} />";
    }
}
