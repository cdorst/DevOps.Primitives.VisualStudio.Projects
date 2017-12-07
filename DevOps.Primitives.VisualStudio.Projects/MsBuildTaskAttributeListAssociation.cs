using Common.EntityFrameworkServices;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.VisualStudio.Projects
{
    [ProtoContract]
    [Table("MsBuildTaskAttributeListAssociations", Schema = nameof(VisualStudio))]
    public class MsBuildTaskAttributeListAssociation : IUniqueListAssociation<MsBuildTaskAttribute>
    {
        [Key]
        [ProtoMember(1)]
        public int MsBuildTaskAttributeListAssociationId { get; set; }

        [ProtoMember(2)]
        public MsBuildTaskAttribute MsBuildTaskAttribute { get; set; }
        [ProtoMember(3)]
        public int MsBuildTaskAttributeId { get; set; }

        [ProtoMember(4)]
        public MsBuildTaskAttributeList MsBuildTaskAttributeList { get; set; }
        [ProtoMember(5)]
        public int MsBuildTaskAttributeListId { get; set; }

        public MsBuildTaskAttribute GetRecord() => MsBuildTaskAttribute;

        public void SetRecord(MsBuildTaskAttribute record)
        {
            MsBuildTaskAttribute = record;
            MsBuildTaskAttributeId = MsBuildTaskAttribute.MsBuildTaskAttributeId;
        }
    }
}
