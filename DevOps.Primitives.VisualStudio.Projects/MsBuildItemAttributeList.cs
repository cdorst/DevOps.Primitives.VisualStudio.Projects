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
    [Table("MsBuildItemAttributeLists", Schema = nameof(VisualStudio))]
    public class MsBuildItemAttributeList : IUniqueList<MsBuildItemAttribute, MsBuildItemAttributeListAssociation>
    {
        public MsBuildItemAttributeList() { }
        public MsBuildItemAttributeList(
            in List<MsBuildItemAttributeListAssociation> associations,
            in AsciiStringReference listIdentifier = default)
        {
            MsBuildItemAttributeListAssociations = associations;
            ListIdentifier = listIdentifier;
        }
        public MsBuildItemAttributeList(
            in MsBuildItemAttributeListAssociation associations,
            in AsciiStringReference listIdentifier = default)
            : this(new List<MsBuildItemAttributeListAssociation> { associations }, in listIdentifier)
        {
        }
        public MsBuildItemAttributeList(
            in MsBuildItemAttribute itemAttribute,
            in AsciiStringReference listIdentifier = default)
            : this(new MsBuildItemAttributeListAssociation(in itemAttribute), in listIdentifier)
        {
        }
        public MsBuildItemAttributeList(
            in AsciiStringReference attribute,
            in AsciiStringReference value,
            in AsciiStringReference listIdentifier = default)
            : this(new MsBuildItemAttribute(in attribute, in value), in listIdentifier)
        {
        }
        public MsBuildItemAttributeList(
            in string attribute,
            in string value,
            in AsciiStringReference listIdentifier = default)
            : this(new MsBuildItemAttribute(in attribute, in value), in listIdentifier)
        {
        }

        [Key]
        [ProtoMember(1)]
        public int MsBuildItemAttributeListId { get; set; }

        [ProtoMember(2)]
        public AsciiStringReference ListIdentifier { get; set; }
        [ProtoMember(3)]
        public int ListIdentifierId { get; set; }

        [ProtoMember(4)]
        public List<MsBuildItemAttributeListAssociation> MsBuildItemAttributeListAssociations { get; set; }

        public List<MsBuildItemAttributeListAssociation> GetAssociations() => MsBuildItemAttributeListAssociations;

        public string GetItemAttributes()
            => string.Join(" ",
                GetAssociations().Select(each => each.GetRecord().GetItemAttribute()));

        public void SetRecords(in List<MsBuildItemAttribute> records)
        {
            MsBuildItemAttributeListAssociations = UniqueListAssociationsFactory<MsBuildItemAttribute, MsBuildItemAttributeListAssociation>.Create(in records);
            ListIdentifier = new AsciiStringReference(
                UniqueListIdentifierFactory<MsBuildItemAttribute>.Create(in records, r => r.MsBuildItemAttributeId));
        }
    }
}
