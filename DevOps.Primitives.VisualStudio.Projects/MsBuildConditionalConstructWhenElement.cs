using Common.EntityFrameworkServices;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.String;

namespace DevOps.Primitives.VisualStudio.Projects
{
    [ProtoContract]
    [Table("MsBuildConditionalConstructWhenElements", Schema = nameof(VisualStudio))]
    public class MsBuildConditionalConstructWhenElement : IUniqueListRecord
    {
        private const string Tag = "When";

        public MsBuildConditionalConstructWhenElement() { }
        public MsBuildConditionalConstructWhenElement(
            in MsBuildCondition condition,
            in MsBuildConditionalContructItemGroupPropertyGroupSection content)
        {
            MsBuildCondition = condition;
            MsBuildConditionalContructItemGroupPropertyGroupSection = content;
        }
        public MsBuildConditionalConstructWhenElement(
            in string condition,
            in MsBuildConditionalContructItemGroupPropertyGroupSection content)
            : this(new MsBuildCondition(in condition), in content)
        {
        }

        [Key]
        [ProtoMember(1)]
        public int MsBuildConditionalConstructWhenElementId { get; set; }

        [ProtoMember(2)]
        public MsBuildCondition MsBuildCondition { get; set; }
        [ProtoMember(3)]
        public int MsBuildConditionId { get; set; }

        [ProtoMember(4)]
        public MsBuildConditionalContructItemGroupPropertyGroupSection MsBuildConditionalContructItemGroupPropertyGroupSection { get; set; }
        [ProtoMember(5)]
        public int MsBuildConditionalContructItemGroupPropertyGroupSectionId { get; set; }

        public string GetWhenElement()
            => Concat("        <", Tag, MsBuildCondition.GetCondition(), ">", MsBuildConditionalContructItemGroupPropertyGroupSection.GetSection(), "</", Tag, ">");
    }
}
