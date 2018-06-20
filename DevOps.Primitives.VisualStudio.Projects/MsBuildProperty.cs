using Common.EntityFrameworkServices;
using DevOps.Primitives.Strings;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.String;

namespace DevOps.Primitives.VisualStudio.Projects
{
    [ProtoContract]
    [Table("MsBuildProperties", Schema = nameof(VisualStudio))]
    public class MsBuildProperty : IUniqueListRecord
    {
        public MsBuildProperty() { }
        public MsBuildProperty(
            in AsciiStringReference name,
            in AsciiStringReference value,
            in MsBuildCondition condition = default)
        {
            ElementName = name;
            ElementValue = value;
            MsBuildCondition = condition;
        }
        public MsBuildProperty(
            in string name,
            in string value,
            in MsBuildCondition condition = default)
            : this(new AsciiStringReference(in name), new AsciiStringReference(in value), in condition)
        {
        }
        public MsBuildProperty(
            in string name,
            in string value,
            in string condition = default)
            : this(in name, in value, ConditionHelper.Create(in condition))
        {
        }

        [Key]
        [ProtoMember(1)]
        public int MsBuildPropertyId { get; set; }

        [ProtoMember(2)]
        public MsBuildCondition MsBuildCondition { get; set; }
        [ProtoMember(3)]
        public int? MsBuildConditionId { get; set; }

        [ProtoMember(4)]
        public AsciiStringReference ElementName { get; set; }
        [ProtoMember(5)]
        public int ElementNameId { get; set; }

        [ProtoMember(6)]
        public AsciiStringReference ElementValue { get; set; }
        [ProtoMember(7)]
        public int ElementValueId { get; set; }

        public string GetProperty()
            => Concat("    <", ElementName.Value, MsBuildCondition?.GetCondition(), ">", ElementValue.Value, "</", ElementName.Value, ">");
    }
}
