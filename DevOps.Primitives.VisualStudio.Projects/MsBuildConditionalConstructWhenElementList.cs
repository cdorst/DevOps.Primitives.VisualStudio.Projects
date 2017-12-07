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
    [Table("MsBuildConditionalConstructWhenElementLists", Schema = nameof(VisualStudio))]
    public class MsBuildConditionalConstructWhenElementList : IUniqueList<MsBuildConditionalConstructWhenElement, MsBuildConditionalConstructWhenElementListAssociation>
    {
        [Key]
        [ProtoMember(1)]
        public int MsBuildConditionalConstructWhenElementListId { get; set; }

        [ProtoMember(2)]
        public AsciiStringReference ListIdentifier { get; set; }
        [ProtoMember(3)]
        public int ListIdentifierId { get; set; }

        [ProtoMember(4)]
        public List<MsBuildConditionalConstructWhenElementListAssociation> MsBuildConditionalConstructWhenElementListAssociations { get; set; }

        public List<MsBuildConditionalConstructWhenElementListAssociation> GetAssociations() => MsBuildConditionalConstructWhenElementListAssociations;

        public string GetWhenElements() => string.Join(string.Empty, GetAssociations().Select(each => each.GetRecord().GetWhenElement()));

        public void SetRecords(List<MsBuildConditionalConstructWhenElement> records)
        {
            MsBuildConditionalConstructWhenElementListAssociations = UniqueListAssociationsFactory<MsBuildConditionalConstructWhenElement, MsBuildConditionalConstructWhenElementListAssociation>.Create(records);
            ListIdentifier = new AsciiStringReference(
                UniqueListIdentifierFactory<MsBuildConditionalConstructWhenElement>.Create(records, r => r.MsBuildConditionalConstructWhenElementId));
        }
    }
}
