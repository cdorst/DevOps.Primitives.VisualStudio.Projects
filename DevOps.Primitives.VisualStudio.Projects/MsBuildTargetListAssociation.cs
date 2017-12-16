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
        public MsBuildTargetListAssociation(MsBuildTarget target, MsBuildTargetList targetList = null)
        {
            MsBuildTarget = target;
            MsBuildTargetList = targetList;
        }
        public MsBuildTargetListAssociation(MsBuildTaskList taskList, AsciiStringReference name, AsciiStringReference outputs = null, MsBuildTargetList targetList = null)
            : this(new MsBuildTarget(taskList, name, outputs), targetList)
        {
        }
        public MsBuildTargetListAssociation(MsBuildTaskList taskList, string name, string outputs = null, MsBuildTargetList targetList = null)
            : this(new MsBuildTarget(taskList, name, outputs), targetList)
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

        public void SetRecord(MsBuildTarget record)
        {
            MsBuildTarget = record;
            MsBuildTargetId = MsBuildTarget.MsBuildTargetId;
        }
    }
}
