using Common.EntityFrameworkServices;
using DevOps.Primitives.Strings;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.VisualStudio.Projects
{
    [ProtoContract]
    [Table("MsBuildProperties", Schema = nameof(VisualStudio))]
    public class MsBuildProperty : IUniqueListRecord
    {
        public MsBuildProperty() { }
        public MsBuildProperty(AsciiStringReference name, AsciiStringReference value, MsBuildCondition condition = null)
        {
            ElementName = name;
            ElementValue = value;
            MsBuildCondition = condition;
        }
        public MsBuildProperty(string name, string value, MsBuildCondition condition = null)
            : this(new AsciiStringReference(name), new AsciiStringReference(value), condition)
        {
        }
        public MsBuildProperty(string name, string value, string condition = null)
            : this(name, value, ConditionHelper.Create(condition))
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

        public string GetProperty() => $"    <{ElementName.Value}{MsBuildCondition?.GetCondition()}>{ElementValue.Value}</{ElementName.Value}>";
    }
}
