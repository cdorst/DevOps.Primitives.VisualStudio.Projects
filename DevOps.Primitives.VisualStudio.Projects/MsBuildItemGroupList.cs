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
        public MsBuildItemGroupList(List<MsBuildItemGroupListAssociation> associations, AsciiStringReference listIdentifier = null)
        {
            MsBuildItemGroupListAssociations = associations;
            ListIdentifier = listIdentifier;
        }
        public MsBuildItemGroupList(MsBuildItemGroupListAssociation associations, AsciiStringReference listIdentifier = null)
            : this(new List<MsBuildItemGroupListAssociation> { associations }, listIdentifier)
        {
        }
        public MsBuildItemGroupList(MsBuildItemGroup itemGroup, AsciiStringReference listIdentifier = null)
            : this(new MsBuildItemGroupListAssociation(itemGroup), listIdentifier)
        {
        }
        public MsBuildItemGroupList(MsBuildItemList itemList, MsBuildCondition condition = null, AsciiStringReference listIdentifier = null)
            : this(new MsBuildItemGroup(itemList, condition), listIdentifier)
        {
        }
        public MsBuildItemGroupList(MsBuildItemList itemList, string condition = null, AsciiStringReference listIdentifier = null)
            : this(new MsBuildItemGroup(itemList, condition), listIdentifier)
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

        public void SetRecords(List<MsBuildItemGroup> records)
        {
            MsBuildItemGroupListAssociations = UniqueListAssociationsFactory<MsBuildItemGroup, MsBuildItemGroupListAssociation>.Create(records);
            ListIdentifier = new AsciiStringReference(
                UniqueListIdentifierFactory<MsBuildItemGroup>.Create(records, r => r.MsBuildItemGroupId));
        }
    }
}
