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
    [Table("MsBuildPropertyGroupLists", Schema = nameof(VisualStudio))]
    public class MsBuildPropertyGroupList : IUniqueList<MsBuildPropertyGroup, MsBuildPropertyGroupListAssociation>
    {
        public MsBuildPropertyGroupList() { }
        public MsBuildPropertyGroupList(
            in List<MsBuildPropertyGroupListAssociation> associations,
            in AsciiStringReference listIdentifier = default)
        {
            MsBuildPropertyGroupListAssociations = associations;
            ListIdentifier = listIdentifier;
        }
        public MsBuildPropertyGroupList(
            in MsBuildPropertyGroupListAssociation associations,
            in AsciiStringReference listIdentifier = default)
            : this(new List<MsBuildPropertyGroupListAssociation> { associations }, in listIdentifier)
        {
        }
        public MsBuildPropertyGroupList(
            in MsBuildPropertyGroup propertyGroup,
            in AsciiStringReference listIdentifier = default)
            : this(new MsBuildPropertyGroupListAssociation(in propertyGroup), in listIdentifier)
        {
        }
        public MsBuildPropertyGroupList(
            in MsBuildPropertyList propertyList,
            in MsBuildCondition condition = default,
            in AsciiStringReference listIdentifier = default)
            : this(new MsBuildPropertyGroup(in propertyList, in condition), in listIdentifier)
        {
        }
        public MsBuildPropertyGroupList(
            in MsBuildPropertyList propertyList,
            in string condition = default,
            in AsciiStringReference listIdentifier = default)
            : this(new MsBuildPropertyGroup(in propertyList, in condition), in listIdentifier)
        {
        }

        [Key]
        [ProtoMember(1)]
        public int MsBuildPropertyGroupListId { get; set; }

        [ProtoMember(2)]
        public AsciiStringReference ListIdentifier { get; set; }
        [ProtoMember(3)]
        public int ListIdentifierId { get; set; }

        [ProtoMember(4)]
        public List<MsBuildPropertyGroupListAssociation> MsBuildPropertyGroupListAssociations { get; set; }

        public List<MsBuildPropertyGroupListAssociation> GetAssociations() => MsBuildPropertyGroupListAssociations;

        public string GetPropertyGroups()
            => string.Join("\r\n\r\n",
                GetAssociations().Select(each => each.GetRecord().GetPropertyGroup()));

        public void SetRecords(in List<MsBuildPropertyGroup> records)
        {
            MsBuildPropertyGroupListAssociations = UniqueListAssociationsFactory<MsBuildPropertyGroup, MsBuildPropertyGroupListAssociation>.Create(in records);
            ListIdentifier = new AsciiStringReference(
                UniqueListIdentifierFactory<MsBuildPropertyGroup>.Create(in records, r => r.MsBuildPropertyGroupId));
        }
    }
}
