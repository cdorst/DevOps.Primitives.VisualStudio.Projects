using Common.EntityFrameworkServices;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.VisualStudio.Projects
{
    [ProtoContract]
    [Table("MsBuildConditionalConstructListAssociations", Schema = nameof(VisualStudio))]
    public class MsBuildConditionalConstructListAssociation : IUniqueListAssociation<MsBuildConditionalConstruct>
    {
        public MsBuildConditionalConstructListAssociation() { }
        public MsBuildConditionalConstructListAssociation(MsBuildConditionalConstruct conditionalConstruct, MsBuildConditionalConstructList conditionalConstructList = null)
        {
            MsBuildConditionalConstruct = conditionalConstruct;
            MsBuildConditionalConstructList = conditionalConstructList;
        }
        public MsBuildConditionalConstructListAssociation(MsBuildConditionalConstructWhenElementList whenElementList, MsBuildConditionalConstructOtherwiseElement otherwiseElement = null, MsBuildConditionalConstructList conditionalConstructList = null)
            : this(new MsBuildConditionalConstruct(whenElementList, otherwiseElement), conditionalConstructList)
        {
        }

        [Key]
        [ProtoMember(1)]
        public int MsBuildConditionalConstructListAssociationId { get; set; }

        [ProtoMember(2)]
        public MsBuildConditionalConstruct MsBuildConditionalConstruct { get; set; }
        [ProtoMember(3)]
        public int MsBuildConditionalConstructId { get; set; }

        [ProtoMember(4)]
        public MsBuildConditionalConstructList MsBuildConditionalConstructList { get; set; }
        [ProtoMember(5)]
        public int MsBuildConditionalConstructListId { get; set; }

        public MsBuildConditionalConstruct GetRecord() => MsBuildConditionalConstruct;

        public void SetRecord(MsBuildConditionalConstruct record)
        {
            MsBuildConditionalConstruct = record;
            MsBuildConditionalConstructId = MsBuildConditionalConstruct.MsBuildConditionalConstructId;
        }
    }
}
