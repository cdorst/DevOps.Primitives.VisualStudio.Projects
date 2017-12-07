using Common.EntityFrameworkServices;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.VisualStudio.Projects
{
    [ProtoContract]
    [Table("MsBuildConditionalConstructs", Schema = nameof(VisualStudio))]
    public class MsBuildConditionalConstruct : IUniqueListRecord
    {
        [Key]
        [ProtoMember(1)]
        public int MsBuildConditionalConstructId { get; set; }

        [ProtoMember(2)]
        public MsBuildConditionalConstructOtherwiseElement MsBuildConditionalConstructOtherwiseElement { get; set; }
        [ProtoMember(3)]
        public int? MsBuildConditionalConstructOtherwiseElementId { get; set; }

        [ProtoMember(4)]
        public MsBuildConditionalConstructWhenElementList MsBuildConditionalConstructWhenElementList { get; set; }
        [ProtoMember(5)]
        public int MsBuildConditionalConstructWhenElementListId { get; set; }

        public string GetConditionalConstruct() => $"<Choose>{MsBuildConditionalConstructWhenElementList.GetWhenElements()}{MsBuildConditionalConstructOtherwiseElement?.GetOtherwiseElement()}</Choose>";
    }
}
