using Common.EntityFrameworkServices;
using DevOps.Primitives.Strings;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.VisualStudio.Projects
{
    [ProtoContract]
    [Table("MsBuildItemAttributeListAssociations", Schema = nameof(VisualStudio))]
    public class MsBuildItemAttributeListAssociation : IUniqueListAssociation<MsBuildItemAttribute>
    {
        public MsBuildItemAttributeListAssociation() { }
        public MsBuildItemAttributeListAssociation(MsBuildItemAttribute itemAttribute, MsBuildItemAttributeList itemAttributeList = null)
        {
            MsBuildItemAttribute = itemAttribute;
            MsBuildItemAttributeList = itemAttributeList;
        }
        public MsBuildItemAttributeListAssociation(AsciiStringReference attribute, AsciiStringReference value, MsBuildItemAttributeList itemAttributeList = null)
            : this(new MsBuildItemAttribute(attribute, value), itemAttributeList)
        {
        }
        public MsBuildItemAttributeListAssociation(string attribute, string value, MsBuildItemAttributeList itemAttributeList = null)
            : this(new MsBuildItemAttribute(attribute, value), itemAttributeList)
        {
        }

        [Key]
        [ProtoMember(1)]
        public int MsBuildItemAttributeListAssociationId { get; set; }

        [ProtoMember(2)]
        public MsBuildItemAttribute MsBuildItemAttribute { get; set; }
        [ProtoMember(3)]
        public int MsBuildItemAttributeId { get; set; }

        [ProtoMember(4)]
        public MsBuildItemAttributeList MsBuildItemAttributeList { get; set; }
        [ProtoMember(5)]
        public int MsBuildItemAttributeListId { get; set; }

        public MsBuildItemAttribute GetRecord() => MsBuildItemAttribute;

        public void SetRecord(MsBuildItemAttribute record)
        {
            MsBuildItemAttribute = record;
            MsBuildItemAttributeId = MsBuildItemAttribute.MsBuildItemAttributeId;
        }
    }
}
