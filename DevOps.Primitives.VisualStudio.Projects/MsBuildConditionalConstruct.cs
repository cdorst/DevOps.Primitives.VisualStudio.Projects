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
        private const string Tag = "Choose";

        public MsBuildConditionalConstruct() { }
        public MsBuildConditionalConstruct(MsBuildConditionalConstructWhenElementList whenElementList, MsBuildConditionalConstructOtherwiseElement otherwiseElement = null)
        {
            MsBuildConditionalConstructWhenElementList = whenElementList;
            MsBuildConditionalConstructOtherwiseElement = otherwiseElement;
        }

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

        public string GetConditionalConstruct() => $"    <{Tag}>{MsBuildConditionalConstructWhenElementList.GetWhenElements()}{MsBuildConditionalConstructOtherwiseElement?.GetOtherwiseElement()}</{Tag}>";
    }
}
