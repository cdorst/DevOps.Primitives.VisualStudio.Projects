using Common.EntityFrameworkServices;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.VisualStudio.Projects
{
    [ProtoContract]
    [Table("MsBuildTaskListAssociations", Schema = nameof(VisualStudio))]
    public class MsBuildTaskListAssociation : IUniqueListAssociation<MsBuildTask>
    {
        [Key]
        [ProtoMember(1)]
        public int MsBuildTaskListAssociationId { get; set; }

        [ProtoMember(2)]
        public MsBuildTask MsBuildTask { get; set; }
        [ProtoMember(3)]
        public int MsBuildTaskId { get; set; }

        [ProtoMember(4)]
        public MsBuildTaskList MsBuildTaskList { get; set; }
        [ProtoMember(5)]
        public int MsBuildTaskListId { get; set; }

        public MsBuildTask GetRecord() => MsBuildTask;

        public void SetRecord(MsBuildTask record)
        {
            MsBuildTask = record;
            MsBuildTaskId = MsBuildTask.MsBuildTaskId;
        }
    }
}
