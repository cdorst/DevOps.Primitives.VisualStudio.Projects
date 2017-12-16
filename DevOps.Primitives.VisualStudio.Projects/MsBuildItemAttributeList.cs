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
        public MsBuildItemAttributeList(List<MsBuildItemAttributeListAssociation> associations, AsciiStringReference listIdentifier = null)
        {
            MsBuildItemAttributeListAssociations = associations;
            ListIdentifier = listIdentifier;
        }
        public MsBuildItemAttributeList(MsBuildItemAttributeListAssociation associations, AsciiStringReference listIdentifier = null)
            : this(new List<MsBuildItemAttributeListAssociation> { associations }, listIdentifier)
        {
        }
        public MsBuildItemAttributeList(MsBuildItemAttribute itemAttribute, AsciiStringReference listIdentifier = null)
            : this(new MsBuildItemAttributeListAssociation(itemAttribute), listIdentifier)
        {
        }
        public MsBuildItemAttributeList(AsciiStringReference attribute, AsciiStringReference value, AsciiStringReference listIdentifier = null)
            : this(new MsBuildItemAttribute(attribute, value), listIdentifier)
        {
        }
        public MsBuildItemAttributeList(string attribute, string value, AsciiStringReference listIdentifier = null)
            : this(new MsBuildItemAttribute(attribute, value), listIdentifier)
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

        public void SetRecords(List<MsBuildItemAttribute> records)
        {
            MsBuildItemAttributeListAssociations = UniqueListAssociationsFactory<MsBuildItemAttribute, MsBuildItemAttributeListAssociation>.Create(records);
            ListIdentifier = new AsciiStringReference(
                UniqueListIdentifierFactory<MsBuildItemAttribute>.Create(records, r => r.MsBuildItemAttributeId));
        }
    }
}
