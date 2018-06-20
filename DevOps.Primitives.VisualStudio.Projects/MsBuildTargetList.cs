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
    [Table("MsBuildTargetLists", Schema = nameof(VisualStudio))]
    public class MsBuildTargetList : IUniqueList<MsBuildTarget, MsBuildTargetListAssociation>
    {
        public MsBuildTargetList() { }
        public MsBuildTargetList(in List<MsBuildTargetListAssociation> associations, in AsciiStringReference listIdentifier = default)
        {
            MsBuildTargetListAssociations = associations;
            ListIdentifier = listIdentifier;
        }
        public MsBuildTargetList(in MsBuildTargetListAssociation associations, in AsciiStringReference listIdentifier = default)
            : this(new List<MsBuildTargetListAssociation> { associations }, in listIdentifier)
        {
        }
        public MsBuildTargetList(in MsBuildTarget target, in AsciiStringReference listIdentifier = default)
            : this(new MsBuildTargetListAssociation(in target), in listIdentifier)
        {
        }
        public MsBuildTargetList(in MsBuildTaskList taskList, in AsciiStringReference name, in AsciiStringReference outputs = default, in AsciiStringReference listIdentifier = default)
            : this(new MsBuildTarget(in taskList, in name, in outputs), in listIdentifier)
        {
        }
        public MsBuildTargetList(in MsBuildTaskList taskList, in string name, in string outputs = default, in AsciiStringReference listIdentifier = default)
            : this(new MsBuildTarget(in taskList, in name, in outputs), in listIdentifier)
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
            => Join(Empty, GetAssociations().Select(each => each.GetRecord().GetTarget()));

        public void SetRecords(in List<MsBuildTarget> records)
        {
            MsBuildTargetListAssociations = UniqueListAssociationsFactory<MsBuildTarget, MsBuildTargetListAssociation>.Create(in records);
            ListIdentifier = new AsciiStringReference(
                UniqueListIdentifierFactory<MsBuildTarget>.Create(in records, r => r.MsBuildTargetId));
        }
    }
}
