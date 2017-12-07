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
    [Table("MsBuildPropertyGroupLists", Schema = nameof(VisualStudio))]
    public class MsBuildPropertyGroupList : IUniqueList<MsBuildPropertyGroup, MsBuildPropertyGroupListAssociation>
    {
        [Key]
        [ProtoMember(1)]
        public int MsBuildPropertyGroupListId { get; set; }

        [ProtoMember(2)]
        public AsciiStringReference ListIdentifier { get; set; }
        [ProtoMember(3)]
        public int ListIdentifierId { get; set; }

        [ProtoMember(4)]
        public List<MsBuildPropertyGroupListAssociation> MsBuildPropertyGroupListAssociations { get; set; }

        public List<MsBuildPropertyGroupListAssociation> GetAssociations() => MsBuildPropertyGroupListAssociations;

        public string GetPropertyGroups() => string.Join(string.Empty, GetAssociations().Select(each => each.GetRecord().GetPropertyGroup()));

        public void SetRecords(List<MsBuildPropertyGroup> records)
        {
            MsBuildPropertyGroupListAssociations = UniqueListAssociationsFactory<MsBuildPropertyGroup, MsBuildPropertyGroupListAssociation>.Create(records);
            ListIdentifier = new AsciiStringReference(
                UniqueListIdentifierFactory<MsBuildPropertyGroup>.Create(records, r => r.MsBuildPropertyGroupId));
        }
    }
}
