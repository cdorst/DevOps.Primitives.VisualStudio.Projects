using Common.EntityFrameworkServices;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.VisualStudio.Projects
{
    [ProtoContract]
    [Table("MsBuildItemGroups", Schema = nameof(VisualStudio))]
    public class MsBuildItemGroup : IUniqueListRecord
    {
        private const string Tag = "ItemGroup";

        public MsBuildItemGroup() { }
        public MsBuildItemGroup(MsBuildItemList itemList, MsBuildCondition condition = null)
        {
            MsBuildCondition = condition;
            MsBuildItemList = itemList;
        }
        public MsBuildItemGroup(MsBuildItemList itemList, string condition = null)
            : this(itemList, ConditionHelper.Create(condition))
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

        public string GetItemGroup() => $"    <{Tag}{MsBuildCondition?.GetCondition()}>{MsBuildItemList.GetItems()}\r\n    </{Tag}>";
    }
}
