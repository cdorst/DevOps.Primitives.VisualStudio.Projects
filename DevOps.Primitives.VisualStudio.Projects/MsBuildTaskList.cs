using Common.EntityFrameworkServices;
using Common.EntityFrameworkServices.Factories;
using DevOps.Primitives.Strings;
using ProtoBuf;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DevOps.Primitives.VisualStudio.Projects
{
    [ProtoContract]
    [Table("MsBuildTaskLists", Schema = nameof(VisualStudio))]
    public class MsBuildTaskList : IUniqueList<MsBuildTask, MsBuildTaskListAssociation>
    {
        public MsBuildTaskList() { }
        public MsBuildTaskList(List<MsBuildTaskListAssociation> associations, AsciiStringReference listIdentifier = null)
        {
            MsBuildTaskListAssociations = associations;
            ListIdentifier = listIdentifier;
        }
        public MsBuildTaskList(MsBuildTaskListAssociation associations, AsciiStringReference listIdentifier = null)
            : this(new List<MsBuildTaskListAssociation> { associations }, listIdentifier)
        {
        }
        public MsBuildTaskList(MsBuildTask task, AsciiStringReference listIdentifier = null)
            : this(new MsBuildTaskListAssociation(task), listIdentifier)
        {
        }
        public MsBuildTaskList(AsciiStringReference element, MsBuildTaskAttributeList taskAttributeList, AsciiStringReference listIdentifier = null)
            : this(new MsBuildTask(element, taskAttributeList), listIdentifier)
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
            => string.Join(string.Empty,
                GetAssociations().Select(each => each.GetRecord().GetTask()));

        public void SetRecords(List<MsBuildTask> records)
        {
            MsBuildTaskListAssociations = UniqueListAssociationsFactory<MsBuildTask, MsBuildTaskListAssociation>.Create(records);
            ListIdentifier = new AsciiStringReference(
                UniqueListIdentifierFactory<MsBuildTask>.Create(records, r => r.MsBuildTaskId));
        }
    }
}
