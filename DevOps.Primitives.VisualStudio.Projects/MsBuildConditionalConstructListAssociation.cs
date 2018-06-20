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
        public MsBuildConditionalConstructListAssociation(
            in MsBuildConditionalConstruct conditionalConstruct,
            in MsBuildConditionalConstructList conditionalConstructList = default)
        {
            MsBuildConditionalConstruct = conditionalConstruct;
            MsBuildConditionalConstructList = conditionalConstructList;
        }
        public MsBuildConditionalConstructListAssociation(
            in MsBuildConditionalConstructWhenElementList whenElementList,
            in MsBuildConditionalConstructOtherwiseElement otherwiseElement = default,
            in MsBuildConditionalConstructList conditionalConstructList = default)
            : this(new MsBuildConditionalConstruct(in whenElementList, in otherwiseElement), in conditionalConstructList)
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

        public void SetRecord(in MsBuildConditionalConstruct record)
        {
            MsBuildConditionalConstruct = record;
            MsBuildConditionalConstructId = record.MsBuildConditionalConstructId;
        }
    }
}
