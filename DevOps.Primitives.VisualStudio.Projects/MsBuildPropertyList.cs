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
    [Table("MsBuildPropertyLists", Schema = nameof(VisualStudio))]
    public class MsBuildPropertyList : IUniqueList<MsBuildProperty, MsBuildPropertyListAssociation>
    {
        public MsBuildPropertyList() { }
        public MsBuildPropertyList(
            in List<MsBuildPropertyListAssociation> associations,
            in AsciiStringReference listIdentifier = default)
        {
            MsBuildPropertyListAssociations = associations;
            ListIdentifier = listIdentifier;
        }
        public MsBuildPropertyList(
            in MsBuildPropertyListAssociation associations,
            in AsciiStringReference listIdentifier = default)
            : this(new List<MsBuildPropertyListAssociation> { associations }, in listIdentifier)
        {
        }
        public MsBuildPropertyList(
            in MsBuildProperty propertyGroup,
            in AsciiStringReference listIdentifier = default)
            : this(new MsBuildPropertyListAssociation(in propertyGroup), in  listIdentifier)
        {
        }
        public MsBuildPropertyList(
            in AsciiStringReference name,
            in AsciiStringReference value,
            in MsBuildCondition condition = default,
            in AsciiStringReference listIdentifier = default)
            : this(new MsBuildProperty(in name, in value, in condition), in listIdentifier)
        {
        }
        public MsBuildPropertyList(
            in string name,
            in string value,
            in MsBuildCondition condition = default,
            in AsciiStringReference listIdentifier = default)
            : this(new MsBuildProperty(in name, in value, in condition), in listIdentifier)
        {
        }
        public MsBuildPropertyList(
            in string name,
            in string value,
            in string condition = default,
            in AsciiStringReference listIdentifier = default)
            : this(new MsBuildProperty(in name, in value, in condition), in listIdentifier)
        {
        }

        [Key]
        [ProtoMember(1)]
        public int MsBuildPropertyListId { get; set; }

        [ProtoMember(2)]
        public AsciiStringReference ListIdentifier { get; set; }
        [ProtoMember(3)]
        public int ListIdentifierId { get; set; }

        [ProtoMember(4)]
        public List<MsBuildPropertyListAssociation> MsBuildPropertyListAssociations { get; set; }

        public List<MsBuildPropertyListAssociation> GetAssociations() => MsBuildPropertyListAssociations;

        public string GetProperties()
            => string.Join("\r\n",
                GetAssociations().Select(each => each.GetRecord().GetProperty()));

        public void SetRecords(in List<MsBuildProperty> records)
        {
            MsBuildPropertyListAssociations = UniqueListAssociationsFactory<MsBuildProperty, MsBuildPropertyListAssociation>.Create(in records);
            ListIdentifier = new AsciiStringReference(
                UniqueListIdentifierFactory<MsBuildProperty>.Create(in records, r => r.MsBuildPropertyId));
        }
    }
}
