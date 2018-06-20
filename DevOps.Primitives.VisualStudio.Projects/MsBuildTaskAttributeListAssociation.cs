using Common.EntityFrameworkServices;
using DevOps.Primitives.Strings;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.VisualStudio.Projects
{
    [ProtoContract]
    [Table("MsBuildTaskAttributeListAssociations", Schema = nameof(VisualStudio))]
    public class MsBuildTaskAttributeListAssociation : IUniqueListAssociation<MsBuildTaskAttribute>
    {
        public MsBuildTaskAttributeListAssociation() { }
        public MsBuildTaskAttributeListAssociation(in MsBuildTaskAttribute taskAttribute, in MsBuildTaskAttributeList taskAttributeList = default)
        {
            MsBuildTaskAttribute = taskAttribute;
            MsBuildTaskAttributeList = taskAttributeList;
        }
        public MsBuildTaskAttributeListAssociation(in AsciiStringReference attribute, in AsciiStringReference value, in MsBuildTaskAttributeList taskAttributeList = default)
            : this(new MsBuildTaskAttribute(in attribute, in value), in taskAttributeList)
        {
        }
        public MsBuildTaskAttributeListAssociation(in string attribute, in string value, in MsBuildTaskAttributeList taskAttributeList = default)
            : this(new MsBuildTaskAttribute(in attribute, in value), in taskAttributeList)
        {
        }

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

        public void SetRecord(in MsBuildTaskAttribute record)
        {
            MsBuildTaskAttribute = record;
            MsBuildTaskAttributeId = record.MsBuildTaskAttributeId;
        }
    }
}
