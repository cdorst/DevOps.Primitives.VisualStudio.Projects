using Common.EntityFrameworkServices;
using DevOps.Primitives.Strings;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.VisualStudio.Projects
{
    [ProtoContract]
    [Table("MsBuildTaskListAssociations", Schema = nameof(VisualStudio))]
    public class MsBuildTaskListAssociation : IUniqueListAssociation<MsBuildTask>
    {
        public MsBuildTaskListAssociation() { }
        public MsBuildTaskListAssociation(MsBuildTask buildTask, MsBuildTaskList buildTaskList = null)
        {
            MsBuildTask = buildTask;
            MsBuildTaskList = buildTaskList;
        }
        public MsBuildTaskListAssociation(AsciiStringReference element, MsBuildTaskAttributeList taskAttributeList, MsBuildTaskList buildTaskList = null)
            : this(new MsBuildTask(element, taskAttributeList), buildTaskList)
        {
        }
        public MsBuildTaskListAssociation(string element, MsBuildTaskAttributeList taskAttributeList, MsBuildTaskList buildTaskList = null)
            : this(new MsBuildTask(element, taskAttributeList), buildTaskList)
        {
        }

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
