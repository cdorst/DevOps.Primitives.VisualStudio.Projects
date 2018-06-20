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
    [Table("MsBuildItemLists", Schema = nameof(VisualStudio))]
    public class MsBuildItemList : IUniqueList<MsBuildItem, MsBuildItemListAssociation>
    {
        public MsBuildItemList() { }
        public MsBuildItemList(
            in List<MsBuildItemListAssociation> associations,
            in AsciiStringReference listIdentifier = default)
        {
            MsBuildItemListAssociations = associations;
            ListIdentifier = listIdentifier;
        }
        public MsBuildItemList(
            in MsBuildItemListAssociation associations,
            in AsciiStringReference listIdentifier = default)
            : this(new List<MsBuildItemListAssociation> { associations }, in listIdentifier)
        {
        }
        public MsBuildItemList(
            in MsBuildItem itemGroup,
            in AsciiStringReference listIdentifier = default)
            : this(new MsBuildItemListAssociation(in itemGroup), in listIdentifier)
        {
        }
        public MsBuildItemList(
            in AsciiStringReference name,
            in MsBuildItemAttributeList attributes,
            in MsBuildCondition condition = default,
            in AsciiStringReference listIdentifier = default)
            : this(new MsBuildItem(in name, in attributes, in condition), in listIdentifier)
        {
        }
        public MsBuildItemList(
            in string name,
            in MsBuildItemAttributeList attributes,
            in MsBuildCondition condition = default,
            in AsciiStringReference listIdentifier = default)
            : this(new MsBuildItem(in name, in attributes, in condition), in listIdentifier)
        {
        }
        public MsBuildItemList(
            in string name,
            in MsBuildItemAttributeList attributes,
            in string condition = default,
            in AsciiStringReference listIdentifier = default)
            : this(new MsBuildItem(in name, in attributes, in condition), in listIdentifier)
        {
        }

        [Key]
        [ProtoMember(1)]
        public int MsBuildItemListId { get; set; }

        [ProtoMember(2)]
        public AsciiStringReference ListIdentifier { get; set; }
        [ProtoMember(3)]
        public int ListIdentifierId { get; set; }

        [ProtoMember(4)]
        public List<MsBuildItemListAssociation> MsBuildItemListAssociations { get; set; }

        public List<MsBuildItemListAssociation> GetAssociations() => MsBuildItemListAssociations;

        public string GetItems()
            => string.Join("\r\n",
                GetAssociations().Select(each => each.GetRecord().GetItem()));

        public void SetRecords(in List<MsBuildItem> records)
        {
            MsBuildItemListAssociations = UniqueListAssociationsFactory<MsBuildItem, MsBuildItemListAssociation>.Create(in records);
            ListIdentifier = new AsciiStringReference(
                UniqueListIdentifierFactory<MsBuildItem>.Create(in records, r => r.MsBuildItemId));
        }
    }
}
