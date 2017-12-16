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
        public MsBuildPropertyList(List<MsBuildPropertyListAssociation> associations, AsciiStringReference listIdentifier = null)
        {
            MsBuildPropertyListAssociations = associations;
            ListIdentifier = listIdentifier;
        }
        public MsBuildPropertyList(MsBuildPropertyListAssociation associations, AsciiStringReference listIdentifier = null)
            : this(new List<MsBuildPropertyListAssociation> { associations }, listIdentifier)
        {
        }
        public MsBuildPropertyList(MsBuildProperty propertyGroup, AsciiStringReference listIdentifier = null)
            : this(new MsBuildPropertyListAssociation(propertyGroup), listIdentifier)
        {
        }
        public MsBuildPropertyList(AsciiStringReference name, AsciiStringReference value, MsBuildCondition condition = null, AsciiStringReference listIdentifier = null)
            : this(new MsBuildProperty(name, value, condition), listIdentifier)
        {
        }
        public MsBuildPropertyList(string name, string value, MsBuildCondition condition = null, AsciiStringReference listIdentifier = null)
            : this(new MsBuildProperty(name, value, condition), listIdentifier)
        {
        }
        public MsBuildPropertyList(string name, string value, string condition = null, AsciiStringReference listIdentifier = null)
            : this(new MsBuildProperty(name, value, condition), listIdentifier)
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
            => string.Join(string.Empty,
                GetAssociations().Select(each => each.GetRecord().GetProperty()));

        public void SetRecords(List<MsBuildProperty> records)
        {
            MsBuildPropertyListAssociations = UniqueListAssociationsFactory<MsBuildProperty, MsBuildPropertyListAssociation>.Create(records);
            ListIdentifier = new AsciiStringReference(
                UniqueListIdentifierFactory<MsBuildProperty>.Create(records, r => r.MsBuildPropertyId));
        }
    }
}
