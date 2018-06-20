using Common.EntityFrameworkServices;
using DevOps.Primitives.Strings;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.String;

namespace DevOps.Primitives.VisualStudio.Projects
{
    [ProtoContract]
    [Table("MsBuildTaskAttributes", Schema = nameof(VisualStudio))]
    public class MsBuildTaskAttribute : IUniqueListRecord
    {
        public MsBuildTaskAttribute() { }
        public MsBuildTaskAttribute(in AsciiStringReference attribute, in AsciiStringReference value)
        {
            Attribute = attribute;
            Value = value;
        }
        public MsBuildTaskAttribute(in string attribute, in string value)
            : this(new AsciiStringReference(in attribute), new AsciiStringReference(in value))
        {
        }

        [Key]
        [ProtoMember(1)]
        public int MsBuildTaskAttributeId { get; set; }

        [ProtoMember(2)]
        public AsciiStringReference Attribute { get; set; }
        [ProtoMember(3)]
        public int AttributeId { get; set; }

        [ProtoMember(4)]
        public AsciiStringReference Value { get; set; }
        [ProtoMember(5)]
        public int ValueId { get; set; }

        public string GetTaskAttribute() => Concat(Attribute.Value, "=\"", Value.Value, "\"");
    }
}
