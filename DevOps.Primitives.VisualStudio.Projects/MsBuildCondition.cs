using DevOps.Primitives.Strings;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.VisualStudio.Projects
{
    [ProtoContract]
    [Table("VisualStudioProjects", Schema = nameof(VisualStudio))]
    public class MsBuildCondition
    {
        public MsBuildCondition() { }
        public MsBuildCondition(AsciiStringReference condition) { Condition = condition; }
        public MsBuildCondition(string condition) : this(new AsciiStringReference(condition)) { }

        [Key]
        [ProtoMember(1)]
        public int MsBuildConditionId { get; set; }

        [ProtoMember(2)]
        public AsciiStringReference Condition { get; set; }
        [ProtoMember(3)]
        public int ConditionId { get; set; }

        public string GetCondition() => $" Condition=\"{Condition.Value}\"";
    }
}
