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
        public MsBuildConditionalConstructWhenElementList() { }
        public MsBuildConditionalConstructWhenElementList(
            in List<MsBuildConditionalConstructWhenElementListAssociation> associations,
            in AsciiStringReference listIdentifier = default)
        {
            MsBuildConditionalConstructWhenElementListAssociations = associations;
            ListIdentifier = listIdentifier;
        }
        public MsBuildConditionalConstructWhenElementList(
            in MsBuildConditionalConstructWhenElementListAssociation associations,
            in AsciiStringReference listIdentifier = default)
            : this(new List<MsBuildConditionalConstructWhenElementListAssociation> { associations }, in listIdentifier)
        {
        }
        public MsBuildConditionalConstructWhenElementList(
            in MsBuildConditionalConstructWhenElement whenElement,
            in AsciiStringReference listIdentifier = default)
            : this(new MsBuildConditionalConstructWhenElementListAssociation(in whenElement), in listIdentifier)
        {
        }
        public MsBuildConditionalConstructWhenElementList(
            in MsBuildCondition condition,
            in MsBuildConditionalContructItemGroupPropertyGroupSection content,
            in AsciiStringReference listIdentifier = default)
            : this(new MsBuildConditionalConstructWhenElement(in condition, in content), in listIdentifier)
        {
        }
        public MsBuildConditionalConstructWhenElementList(
            in string condition,
            in MsBuildConditionalContructItemGroupPropertyGroupSection content,
            in AsciiStringReference listIdentifier = default)
            : this(new MsBuildConditionalConstructWhenElement(in condition, in content), in listIdentifier)
        {
        }

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

        public string GetWhenElements()
            => string.Join("\r\n",
                GetAssociations().Select(each => each.GetRecord().GetWhenElement()));

        public void SetRecords(in List<MsBuildConditionalConstructWhenElement> records)
        {
            MsBuildConditionalConstructWhenElementListAssociations = UniqueListAssociationsFactory<MsBuildConditionalConstructWhenElement, MsBuildConditionalConstructWhenElementListAssociation>.Create(in records);
            ListIdentifier = new AsciiStringReference(
                UniqueListIdentifierFactory<MsBuildConditionalConstructWhenElement>.Create(in records, r => r.MsBuildConditionalConstructWhenElementId));
        }
    }
}
