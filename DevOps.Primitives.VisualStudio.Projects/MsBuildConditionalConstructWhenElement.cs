using Common.EntityFrameworkServices;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.VisualStudio.Projects
{
    [ProtoContract]
    [Table("MsBuildConditionalConstructWhenElements", Schema = nameof(VisualStudio))]
    public class MsBuildConditionalConstructWhenElement : IUniqueListRecord
    {
        [Key]
        [ProtoMember(1)]
        public int MsBuildConditionalConstructWhenElementId { get; set; }

        [ProtoMember(2)]
        public MsBuildCondition MsBuildCondition { get; set; }
        [ProtoMember(3)]
        public int MsBuildConditionId { get; set; }

        [ProtoMember(4)]
        public MsBuildConditionalContructItemGroupPropertyGroupSection MsBuildConditionalContructItemGroupPropertyGroupSection { get; set; }
        [ProtoMember(5)]
        public int MsBuildConditionalContructItemGroupPropertyGroupSectionId { get; set; }

        public string GetWhenElement() => $"<When{MsBuildCondition.GetCondition()}>{MsBuildConditionalContructItemGroupPropertyGroupSection.GetSection()}</When>";
    }
}
