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
    [Table("MsBuildConditionalConstructLists", Schema = nameof(VisualStudio))]
    public class MsBuildConditionalConstructList : IUniqueList<MsBuildConditionalConstruct, MsBuildConditionalConstructListAssociation>
    {
        public MsBuildConditionalConstructList() { }
        public MsBuildConditionalConstructList(List<MsBuildConditionalConstructListAssociation> associations, AsciiStringReference listIdentifier = null)
        {
            MsBuildConditionalConstructListAssociations = associations;
            ListIdentifier = listIdentifier;
        }
        public MsBuildConditionalConstructList(MsBuildConditionalConstructListAssociation associations, AsciiStringReference listIdentifier = null)
            : this(new List<MsBuildConditionalConstructListAssociation> { associations }, listIdentifier)
        {
        }
        public MsBuildConditionalConstructList(MsBuildConditionalConstruct construct, AsciiStringReference listIdentifier = null)
            : this(new MsBuildConditionalConstructListAssociation(construct), listIdentifier)
        {
        }
        public MsBuildConditionalConstructList(MsBuildConditionalConstructWhenElementList whenElementList, MsBuildConditionalConstructOtherwiseElement otherwiseElement = null, AsciiStringReference listIdentifier = null)
            : this(new MsBuildConditionalConstruct(whenElementList, otherwiseElement), listIdentifier)
        {
        }

        [Key]
        [ProtoMember(1)]
        public int MsBuildConditionalConstructListId { get; set; }

        [ProtoMember(2)]
        public AsciiStringReference ListIdentifier { get; set; }
        [ProtoMember(3)]
        public int ListIdentifierId { get; set; }

        [ProtoMember(4)]
        public List<MsBuildConditionalConstructListAssociation> MsBuildConditionalConstructListAssociations { get; set; }

        public List<MsBuildConditionalConstructListAssociation> GetAssociations() => MsBuildConditionalConstructListAssociations;

        public string GetConditionalConstructs()
            => string.Join("\r\n\r\n",
                GetAssociations().Select(each => each.GetRecord().GetConditionalConstruct()));

        public void SetRecords(List<MsBuildConditionalConstruct> records)
        {
            MsBuildConditionalConstructListAssociations = UniqueListAssociationsFactory<MsBuildConditionalConstruct, MsBuildConditionalConstructListAssociation>.Create(records);
            ListIdentifier = new AsciiStringReference(
                UniqueListIdentifierFactory<MsBuildConditionalConstruct>.Create(records, r => r.MsBuildConditionalConstructId));
        }
    }
}
