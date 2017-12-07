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
    [Table("MsBuildTaskAttributeLists", Schema = nameof(VisualStudio))]
    public class MsBuildTaskAttributeList : IUniqueList<MsBuildTaskAttribute, MsBuildTaskAttributeListAssociation>
    {
        [Key]
        [ProtoMember(1)]
        public int MsBuildTaskAttributeListId { get; set; }

        [ProtoMember(2)]
        public AsciiStringReference ListIdentifier { get; set; }
        [ProtoMember(3)]
        public int ListIdentifierId { get; set; }

        [ProtoMember(4)]
        public List<MsBuildTaskAttributeListAssociation> MsBuildTaskAttributeListAssociations { get; set; }

        public List<MsBuildTaskAttributeListAssociation> GetAssociations() => MsBuildTaskAttributeListAssociations;

        public string GetTaskAttributes() => string.Join(" ", GetAssociations().Select(each => each.GetRecord().GetTaskAttribute()));

        public void SetRecords(List<MsBuildTaskAttribute> records)
        {
            MsBuildTaskAttributeListAssociations = UniqueListAssociationsFactory<MsBuildTaskAttribute, MsBuildTaskAttributeListAssociation>.Create(records);
            ListIdentifier = new AsciiStringReference(
                UniqueListIdentifierFactory<MsBuildTaskAttribute>.Create(records, r => r.MsBuildTaskAttributeId));
        }
    }
}
