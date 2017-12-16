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
    [Table("MsBuildTargetLists", Schema = nameof(VisualStudio))]
    public class MsBuildTargetList : IUniqueList<MsBuildTarget, MsBuildTargetListAssociation>
    {
        public MsBuildTargetList() { }
        public MsBuildTargetList(List<MsBuildTargetListAssociation> associations, AsciiStringReference listIdentifier = null)
        {
            MsBuildTargetListAssociations = associations;
            ListIdentifier = listIdentifier;
        }
        public MsBuildTargetList(MsBuildTargetListAssociation associations, AsciiStringReference listIdentifier = null)
            : this(new List<MsBuildTargetListAssociation> { associations }, listIdentifier)
        {
        }
        public MsBuildTargetList(MsBuildTarget target, AsciiStringReference listIdentifier = null)
            : this(new MsBuildTargetListAssociation(target), listIdentifier)
        {
        }
        public MsBuildTargetList(MsBuildTaskList taskList, AsciiStringReference name, AsciiStringReference outputs = null, AsciiStringReference listIdentifier = null)
            : this(new MsBuildTarget(taskList, name, outputs), listIdentifier)
        {
        }
        public MsBuildTargetList(MsBuildTaskList taskList, string name, string outputs = null, AsciiStringReference listIdentifier = null)
            : this(new MsBuildTarget(taskList, name, outputs), listIdentifier)
        {
        }

        [Key]
        [ProtoMember(1)]
        public int MsBuildTargetListId { get; set; }

        [ProtoMember(2)]
        public AsciiStringReference ListIdentifier { get; set; }
        [ProtoMember(3)]
        public int ListIdentifierId { get; set; }

        [ProtoMember(4)]
        public List<MsBuildTargetListAssociation> MsBuildTargetListAssociations { get; set; }

        public List<MsBuildTargetListAssociation> GetAssociations() => MsBuildTargetListAssociations;

        public string GetTargets()
            => string.Join(string.Empty,
                GetAssociations().Select(each => each.GetRecord().GetTarget()));

        public void SetRecords(List<MsBuildTarget> records)
        {
            MsBuildTargetListAssociations = UniqueListAssociationsFactory<MsBuildTarget, MsBuildTargetListAssociation>.Create(records);
            ListIdentifier = new AsciiStringReference(
                UniqueListIdentifierFactory<MsBuildTarget>.Create(records, r => r.MsBuildTargetId));
        }
    }
}
