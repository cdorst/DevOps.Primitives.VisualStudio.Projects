using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.VisualStudio.Projects
{
    [ProtoContract]
    [Table("MsBuildConditionalContructItemGroupPropertyGroupSections", Schema = nameof(VisualStudio))]
    public class MsBuildConditionalContructItemGroupPropertyGroupSection
    {
        [Key]
        [ProtoMember(1)]
        public int MsBuildConditionalContructItemGroupPropertyGroupSectionId { get; set; }

        [ProtoMember(2)]
        public MsBuildConditionalConstructList MsBuildConditionalConstructList { get; set; }
        [ProtoMember(3)]
        public int? MsBuildConditionalConstructListId { get; set; }

        [ProtoMember(4)]
        public MsBuildItemGroupList MsBuildItemGroupList { get; set; }
        [ProtoMember(5)]
        public int? MsBuildItemGroupListId { get; set; }

        [ProtoMember(6)]
        public MsBuildPropertyGroupList MsBuildPropertyGroupList { get; set; }
        [ProtoMember(7)]
        public int? MsBuildPropertyGroupListId { get; set; }

        public string GetSection() => $"{MsBuildPropertyGroupList?.GetPropertyGroups()}{MsBuildItemGroupList.GetItemGroups()}{MsBuildConditionalConstructList?.GetConditionalConstructs()}";
    }
}
