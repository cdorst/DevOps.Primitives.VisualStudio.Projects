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
        public MsBuildConditionalConstructList(
            in List<MsBuildConditionalConstructListAssociation> associations,
            in AsciiStringReference listIdentifier = default)
        {
            MsBuildConditionalConstructListAssociations = associations;
            ListIdentifier = listIdentifier;
        }
        public MsBuildConditionalConstructList(
            in MsBuildConditionalConstructListAssociation associations,
            in AsciiStringReference listIdentifier = default)
            : this(new List<MsBuildConditionalConstructListAssociation> { associations }, in listIdentifier)
        {
        }
        public MsBuildConditionalConstructList(
            in MsBuildConditionalConstruct construct,
            in AsciiStringReference listIdentifier = default)
            : this(new MsBuildConditionalConstructListAssociation(in construct), in listIdentifier)
        {
        }
        public MsBuildConditionalConstructList(
            in MsBuildConditionalConstructWhenElementList whenElementList,
            in MsBuildConditionalConstructOtherwiseElement otherwiseElement = default,
            in AsciiStringReference listIdentifier = default)
            : this(new MsBuildConditionalConstruct(in whenElementList, in otherwiseElement), in listIdentifier)
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

        public void SetRecords(in List<MsBuildConditionalConstruct> records)
        {
            MsBuildConditionalConstructListAssociations = UniqueListAssociationsFactory<MsBuildConditionalConstruct, MsBuildConditionalConstructListAssociation>.Create(in records);
            ListIdentifier = new AsciiStringReference(
                UniqueListIdentifierFactory<MsBuildConditionalConstruct>.Create(in records, r => r.MsBuildConditionalConstructId));
        }
    }
}
