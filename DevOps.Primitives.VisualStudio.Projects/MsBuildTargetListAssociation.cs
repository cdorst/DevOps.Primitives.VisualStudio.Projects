using Common.EntityFrameworkServices;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.VisualStudio.Projects
{
    [ProtoContract]
    [Table("MsBuildTargetListAssociations", Schema = nameof(VisualStudio))]
    public class MsBuildTargetListAssociation : IUniqueListAssociation<MsBuildTarget>
    {
        [Key]
        [ProtoMember(1)]
        public int MsBuildTargetListAssociationId { get; set; }

        [ProtoMember(2)]
        public MsBuildTarget MsBuildTarget { get; set; }
        [ProtoMember(3)]
        public int MsBuildTargetId { get; set; }

        [ProtoMember(4)]
        public MsBuildTargetList MsBuildTargetList { get; set; }
        [ProtoMember(5)]
        public int MsBuildTargetListId { get; set; }

        public MsBuildTarget GetRecord() => MsBuildTarget;

        public void SetRecord(MsBuildTarget record)
        {
            MsBuildTarget = record;
            MsBuildTargetId = MsBuildTarget.MsBuildTargetId;
        }
    }
}
