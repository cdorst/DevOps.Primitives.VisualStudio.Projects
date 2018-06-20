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
    [Table("MsBuildItemGroupLists", Schema = nameof(VisualStudio))]
    public class MsBuildItemGroupList : IUniqueList<MsBuildItemGroup, MsBuildItemGroupListAssociation>
    {
        public MsBuildItemGroupList() { }
        public MsBuildItemGroupList(
            in List<MsBuildItemGroupListAssociation> associations,
            in AsciiStringReference listIdentifier = default)
        {
            MsBuildItemGroupListAssociations = associations;
            ListIdentifier = listIdentifier;
        }
        public MsBuildItemGroupList(
            in MsBuildItemGroupListAssociation associations,
            in AsciiStringReference listIdentifier = default)
            : this(new List<MsBuildItemGroupListAssociation> { associations }, in listIdentifier)
        {
        }
        public MsBuildItemGroupList(
            in MsBuildItemGroup itemGroup,
            in AsciiStringReference listIdentifier = default)
            : this(new MsBuildItemGroupListAssociation(in itemGroup), in listIdentifier)
        {
        }
        public MsBuildItemGroupList(
            in MsBuildItemList itemList,
            in MsBuildCondition condition = default,
            in AsciiStringReference listIdentifier = default)
            : this(new MsBuildItemGroup(in itemList, in condition), in listIdentifier)
        {
        }
        public MsBuildItemGroupList(
            in MsBuildItemList itemList,
            in string condition = default,
            in AsciiStringReference listIdentifier = default)
            : this(new MsBuildItemGroup(in itemList, in condition), in listIdentifier)
        {
        }

        [Key]
        [ProtoMember(1)]
        public int MsBuildItemGroupListId { get; set; }

        [ProtoMember(2)]
        public AsciiStringReference ListIdentifier { get; set; }
        [ProtoMember(3)]
        public int ListIdentifierId { get; set; }

        [ProtoMember(4)]
        public List<MsBuildItemGroupListAssociation> MsBuildItemGroupListAssociations { get; set; }

        public List<MsBuildItemGroupListAssociation> GetAssociations() => MsBuildItemGroupListAssociations;

        public string GetItemGroups()
            => string.Join("\r\n\r\n",
                GetAssociations().Select(each => each.GetRecord().GetItemGroup()));

        public void SetRecords(in List<MsBuildItemGroup> records)
        {
            MsBuildItemGroupListAssociations = UniqueListAssociationsFactory<MsBuildItemGroup, MsBuildItemGroupListAssociation>.Create(in records);
            ListIdentifier = new AsciiStringReference(
                UniqueListIdentifierFactory<MsBuildItemGroup>.Create(in records, r => r.MsBuildItemGroupId));
        }
    }
}
