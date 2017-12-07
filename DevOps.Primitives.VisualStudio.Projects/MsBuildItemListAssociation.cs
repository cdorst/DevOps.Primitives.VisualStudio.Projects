using Common.EntityFrameworkServices;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.VisualStudio.Projects
{
    [ProtoContract]
    [Table("MsBuildItemListAssociations", Schema = nameof(VisualStudio))]
    public class MsBuildItemListAssociation : IUniqueListAssociation<MsBuildItem>
    {
        [Key]
        [ProtoMember(1)]
        public int MsBuildItemListAssociationId { get; set; }

        [ProtoMember(2)]
        public MsBuildItem MsBuildItem { get; set; }
        [ProtoMember(3)]
        public int MsBuildItemId { get; set; }

        [ProtoMember(4)]
        public MsBuildItemList MsBuildItemList { get; set; }
        [ProtoMember(5)]
        public int MsBuildItemListId { get; set; }

        public MsBuildItem GetRecord() => MsBuildItem;

        public void SetRecord(MsBuildItem record)
        {
            MsBuildItem = record;
            MsBuildItemId = MsBuildItem.MsBuildItemId;
        }
    }
}
