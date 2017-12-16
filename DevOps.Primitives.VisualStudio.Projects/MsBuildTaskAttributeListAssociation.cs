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
        public MsBuildTaskAttributeListAssociation(MsBuildTaskAttribute taskAttribute, MsBuildTaskAttributeList taskAttributeList = null)
        {
            MsBuildTaskAttribute = taskAttribute;
            MsBuildTaskAttributeList = taskAttributeList;
        }
        public MsBuildTaskAttributeListAssociation(AsciiStringReference attribute, AsciiStringReference value, MsBuildTaskAttributeList taskAttributeList = null)
            : this(new MsBuildTaskAttribute(attribute, value), taskAttributeList)
        {
        }
        public MsBuildTaskAttributeListAssociation(string attribute, string value, MsBuildTaskAttributeList taskAttributeList = null)
            : this(new MsBuildTaskAttribute(attribute, value), taskAttributeList)
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

        public void SetRecord(MsBuildTaskAttribute record)
        {
            MsBuildTaskAttribute = record;
            MsBuildTaskAttributeId = MsBuildTaskAttribute.MsBuildTaskAttributeId;
        }
    }
}
