using Common.EntityFrameworkServices;
using Common.StringExtensions.Conditionals;
using DevOps.Primitives.Strings;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.VisualStudio.Projects
{
    [ProtoContract]
    [Table("MsBuildTargets", Schema = nameof(VisualStudio))]
    public class MsBuildTarget : IUniqueListRecord
    {
        [Key]
        [ProtoMember(1)]
        public int MsBuildTargetId { get; set; }

        [ProtoMember(2)]
        public MsBuildTaskList MsBuildTaskList { get; set; }
        [ProtoMember(3)]
        public int MsBuildTaskListId { get; set; }

        [ProtoMember(4)]
        public AsciiStringReference Name { get; set; }
        [ProtoMember(5)]
        public int NameId { get; set; }

        [ProtoMember(6)]
        public AsciiStringReference Outputs { get; set; }
        [ProtoMember(7)]
        public int? OutputsId { get; set; }

        public string GetTarget() => $"<Target Name=\"{Name.Value}\"{GetOutputs()}>{MsBuildTaskList.GetTasks()}</Target>";

        private string GetOutputs() => $" Outputs=\"{Outputs.Value}\"".When(Outputs != null);
    }
}
