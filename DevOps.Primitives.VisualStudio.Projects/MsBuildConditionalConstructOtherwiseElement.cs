using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.String;

namespace DevOps.Primitives.VisualStudio.Projects
{
    [ProtoContract]
    [Table("MsBuildConditionalConstructOtherwiseElements", Schema = nameof(VisualStudio))]
    public class MsBuildConditionalConstructOtherwiseElement
    {
        private const string Tag = "Otherwise";

        public MsBuildConditionalConstructOtherwiseElement() { }
        public MsBuildConditionalConstructOtherwiseElement(in MsBuildConditionalContructItemGroupPropertyGroupSection content)
        {
            MsBuildConditionalContructItemGroupPropertyGroupSection = content;
        }

        [Key]
        [ProtoMember(1)]
        public int MsBuildConditionalConstructOtherwiseElementId { get; set; }

        [ProtoMember(2)]
        public MsBuildConditionalContructItemGroupPropertyGroupSection MsBuildConditionalContructItemGroupPropertyGroupSection { get; set; }
        [ProtoMember(3)]
        public int MsBuildConditionalContructItemGroupPropertyGroupSectionId { get; set; }

        public string GetOtherwiseElement()
            => Concat("<", Tag, ">", MsBuildConditionalContructItemGroupPropertyGroupSection.GetSection(), "</", Tag, ">");
    }
}
