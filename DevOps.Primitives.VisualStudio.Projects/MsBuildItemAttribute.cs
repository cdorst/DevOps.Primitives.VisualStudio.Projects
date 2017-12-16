using Common.EntityFrameworkServices;
using DevOps.Primitives.Strings;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.VisualStudio.Projects
{
    [ProtoContract]
    [Table("MsBuildItemAttributes", Schema = nameof(VisualStudio))]
    public class MsBuildItemAttribute : IUniqueListRecord
    {
        public MsBuildItemAttribute() { }
        public MsBuildItemAttribute(AsciiStringReference attribute, AsciiStringReference value)
        {
            Attribute = attribute;
            Value = value;
        }
        public MsBuildItemAttribute(string attribute, string value)
            : this(new AsciiStringReference(attribute), new AsciiStringReference(value))
        {
        }

        [Key]
        [ProtoMember(1)]
        public int MsBuildItemAttributeId { get; set; }

        [ProtoMember(2)]
        public AsciiStringReference Attribute { get; set; }
        [ProtoMember(3)]
        public int AttributeId { get; set; }

        [ProtoMember(4)]
        public AsciiStringReference Value { get; set; }
        [ProtoMember(5)]
        public int ValueId { get; set; }

        public string GetItemAttribute() => $"{Attribute.Value}=\"{Value.Value}\"";
    }
}
