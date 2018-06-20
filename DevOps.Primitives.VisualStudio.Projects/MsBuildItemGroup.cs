using Common.EntityFrameworkServices;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.String;

namespace DevOps.Primitives.VisualStudio.Projects
{
    [ProtoContract]
    [Table("MsBuildItemGroups", Schema = nameof(VisualStudio))]
    public class MsBuildItemGroup : IUniqueListRecord
    {
        private const string Tag = "ItemGroup";

        public MsBuildItemGroup() { }
        public MsBuildItemGroup(
            in MsBuildItemList itemList,
            in MsBuildCondition condition = default)
        {
            MsBuildCondition = condition;
            MsBuildItemList = itemList;
        }
        public MsBuildItemGroup(
            in MsBuildItemList itemList,
            in string condition = default)
            : this(in itemList, ConditionHelper.Create(in condition))
        {
        }

        [Key]
        [ProtoMember(1)]
        public int MsBuildItemGroupId { get; set; }

        [ProtoMember(2)]
        public MsBuildCondition MsBuildCondition { get; set; }
        [ProtoMember(3)]
        public int? MsBuildConditionId { get; set; }

        [ProtoMember(2)]
        public MsBuildItemList MsBuildItemList { get; set; }
        [ProtoMember(3)]
        public int MsBuildItemListId { get; set; }

        public string GetItemGroup()
            => Concat("  <", Tag, MsBuildCondition?.GetCondition(), ">\r\n", MsBuildItemList.GetItems(), "\r\n  </", Tag, ">");
    }
}
