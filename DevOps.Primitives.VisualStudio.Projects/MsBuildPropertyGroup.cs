using Common.EntityFrameworkServices;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.VisualStudio.Projects
{
    [ProtoContract]
    [Table("MsBuildPropertyGroups", Schema = nameof(VisualStudio))]
    public class MsBuildPropertyGroup : IUniqueListRecord
    {
        [Key]
        [ProtoMember(1)]
        public int MsBuildPropertyGroupId { get; set; }

        [ProtoMember(2)]
        public MsBuildCondition MsBuildCondition { get; set; }
        [ProtoMember(3)]
        public int? MsBuildConditionId { get; set; }

        [ProtoMember(4)]
        public MsBuildPropertyList MsBuildPropertyList { get; set; }
        [ProtoMember(5)]
        public int MsBuildPropertyListId { get; set; }

        public string GetPropertyGroup() => $"<PropertyGroup{MsBuildCondition?.GetCondition()}>{MsBuildPropertyList.GetProperties()}</PropertyGroup>";
    }
}
