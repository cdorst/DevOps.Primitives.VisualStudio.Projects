using Common.EntityFrameworkServices;
using Common.EntityFrameworkServices.Factories;
using DevOps.Primitives.Strings;
using ProtoBuf;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using static System.String;

namespace DevOps.Primitives.VisualStudio.Projects
{
    [ProtoContract]
    [Table("MsBuildTaskLists", Schema = nameof(VisualStudio))]
    public class MsBuildTaskList : IUniqueList<MsBuildTask, MsBuildTaskListAssociation>
    {
        public MsBuildTaskList() { }
        public MsBuildTaskList(in List<MsBuildTaskListAssociation> associations, in AsciiStringReference listIdentifier = default)
        {
            MsBuildTaskListAssociations = associations;
            ListIdentifier = listIdentifier;
        }
        public MsBuildTaskList(in MsBuildTaskListAssociation associations, in AsciiStringReference listIdentifier = default)
            : this(new List<MsBuildTaskListAssociation> { associations }, in listIdentifier)
        {
        }
        public MsBuildTaskList(in MsBuildTask task, in AsciiStringReference listIdentifier = default)
            : this(new MsBuildTaskListAssociation(in task), in listIdentifier)
        {
        }
        public MsBuildTaskList(in AsciiStringReference element, in MsBuildTaskAttributeList taskAttributeList, in AsciiStringReference listIdentifier = default)
            : this(new MsBuildTask(in element, in taskAttributeList), in listIdentifier)
        {
        }

        [Key]
        [ProtoMember(1)]
        public int MsBuildTaskListId { get; set; }

        [ProtoMember(2)]
        public AsciiStringReference ListIdentifier { get; set; }
        [ProtoMember(3)]
        public int ListIdentifierId { get; set; }

        [ProtoMember(4)]
        public List<MsBuildTaskListAssociation> MsBuildTaskListAssociations { get; set; }

        public List<MsBuildTaskListAssociation> GetAssociations() => MsBuildTaskListAssociations;

        public string GetTasks()
            => Join(Empty, GetAssociations().Select(each => each.GetRecord().GetTask()));

        public void SetRecords(in List<MsBuildTask> records)
        {
            MsBuildTaskListAssociations = UniqueListAssociationsFactory<MsBuildTask, MsBuildTaskListAssociation>.Create(in records);
            ListIdentifier = new AsciiStringReference(
                UniqueListIdentifierFactory<MsBuildTask>.Create(in records, r => r.MsBuildTaskId));
        }
    }
}
