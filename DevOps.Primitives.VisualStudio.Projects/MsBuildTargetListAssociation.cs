using Common.EntityFrameworkServices;
using DevOps.Primitives.Strings;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.VisualStudio.Projects
{
    [ProtoContract]
    [Table("MsBuildTargetListAssociations", Schema = nameof(VisualStudio))]
    public class MsBuildTargetListAssociation : IUniqueListAssociation<MsBuildTarget>
    {
        public MsBuildTargetListAssociation() { }
        public MsBuildTargetListAssociation(in MsBuildTarget target, in MsBuildTargetList targetList = default)
        {
            MsBuildTarget = target;
            MsBuildTargetList = targetList;
        }
        public MsBuildTargetListAssociation(in MsBuildTaskList taskList, in AsciiStringReference name, in AsciiStringReference outputs = default, in MsBuildTargetList targetList = default)
            : this(new MsBuildTarget(in taskList, in name, in outputs), in targetList)
        {
        }
        public MsBuildTargetListAssociation(in MsBuildTaskList taskList, in string name, in string outputs = default, in MsBuildTargetList targetList = default)
            : this(new MsBuildTarget(in taskList, in name, in outputs), in targetList)
        {
        }

        [Key]
        [ProtoMember(1)]
        public int MsBuildTargetListAssociationId { get; set; }

        [ProtoMember(2)]
        public MsBuildTarget MsBuildTarget { get; set; }
        [ProtoMember(3)]
        public int MsBuildTargetId { get; set; }

        [ProtoMember(4)]
        public MsBuildTargetList MsBuildTargetList { get; set; }
        [ProtoMember(5)]
        public int MsBuildTargetListId { get; set; }

        public MsBuildTarget GetRecord() => MsBuildTarget;

        public void SetRecord(in MsBuildTarget record)
        {
            MsBuildTarget = record;
            MsBuildTargetId = record.MsBuildTargetId;
        }
    }
}
