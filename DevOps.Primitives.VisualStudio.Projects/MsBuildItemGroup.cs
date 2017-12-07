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

        public string GetItemGroup() => $"<ItemGroup{MsBuildCondition?.GetCondition()}>{MsBuildItemList.GetItems()}</ItemGroup>";
    }
}
