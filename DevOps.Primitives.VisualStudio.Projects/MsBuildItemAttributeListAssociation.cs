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
        public MsBuildItemAttributeListAssociation(
            in MsBuildItemAttribute itemAttribute,
            in MsBuildItemAttributeList itemAttributeList = default)
        {
            MsBuildItemAttribute = itemAttribute;
            MsBuildItemAttributeList = itemAttributeList;
        }
        public MsBuildItemAttributeListAssociation(
            in AsciiStringReference attribute,
            in AsciiStringReference value,
            in MsBuildItemAttributeList itemAttributeList = default)
            : this(new MsBuildItemAttribute(in attribute, in value), in itemAttributeList)
        {
        }
        public MsBuildItemAttributeListAssociation(
            in string attribute,
            in string value,
            in MsBuildItemAttributeList itemAttributeList = default)
            : this(new MsBuildItemAttribute(in attribute, in value), in itemAttributeList)
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

        public void SetRecord(in MsBuildItemAttribute record)
        {
            MsBuildItemAttribute = record;
            MsBuildItemAttributeId = record.MsBuildItemAttributeId;
        }
    }
}
