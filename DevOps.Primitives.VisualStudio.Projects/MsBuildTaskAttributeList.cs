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
    [Table("MsBuildTaskAttributeLists", Schema = nameof(VisualStudio))]
    public class MsBuildTaskAttributeList : IUniqueList<MsBuildTaskAttribute, MsBuildTaskAttributeListAssociation>
    {
        public MsBuildTaskAttributeList() { }
        public MsBuildTaskAttributeList(in List<MsBuildTaskAttributeListAssociation> associations, in AsciiStringReference listIdentifier = default)
        {
            MsBuildTaskAttributeListAssociations = associations;
            ListIdentifier = listIdentifier;
        }
        public MsBuildTaskAttributeList(in MsBuildTaskAttributeListAssociation associations, in AsciiStringReference listIdentifier = default)
            : this(new List<MsBuildTaskAttributeListAssociation> { associations }, in listIdentifier)
        {
        }
        public MsBuildTaskAttributeList(in MsBuildTaskAttribute taskAttribute, in AsciiStringReference listIdentifier = default)
            : this(new MsBuildTaskAttributeListAssociation(in taskAttribute), in listIdentifier)
        {
        }
        public MsBuildTaskAttributeList(in AsciiStringReference attribute, in AsciiStringReference value, in AsciiStringReference listIdentifier = default)
            : this(new MsBuildTaskAttribute(in attribute, in value), in listIdentifier)
        {
        }
        public MsBuildTaskAttributeList(in string attribute, in string value, in AsciiStringReference listIdentifier = default)
            : this(new MsBuildTaskAttribute(in attribute, in value), in listIdentifier)
        {
        }

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

        public string GetTaskAttributes()
            => Join(" ", GetAssociations().Select(each => each.GetRecord().GetTaskAttribute()));

        public void SetRecords(in List<MsBuildTaskAttribute> records)
        {
            MsBuildTaskAttributeListAssociations = UniqueListAssociationsFactory<MsBuildTaskAttribute, MsBuildTaskAttributeListAssociation>.Create(in records);
            ListIdentifier = new AsciiStringReference(
                UniqueListIdentifierFactory<MsBuildTaskAttribute>.Create(in records, r => r.MsBuildTaskAttributeId));
        }
    }
}
