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

        public string GetItems() => string.Join(string.Empty, GetAssociations().Select(each => each.GetRecord().GetItem()));

        public void SetRecords(List<MsBuildItem> records)
        {
            MsBuildItemListAssociations = UniqueListAssociationsFactory<MsBuildItem, MsBuildItemListAssociation>.Create(records);
            ListIdentifier = new AsciiStringReference(
                UniqueListIdentifierFactory<MsBuildItem>.Create(records, r => r.MsBuildItemId));
        }
    }
}
