using Common.EntityFrameworkServices;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.VisualStudio.Projects
{
    [ProtoContract]
    [Table("MsBuildConditionalConstructWhenElementListAssociations", Schema = nameof(VisualStudio))]
    public class MsBuildConditionalConstructWhenElementListAssociation : IUniqueListAssociation<MsBuildConditionalConstructWhenElement>
    {
        public MsBuildConditionalConstructWhenElementListAssociation() { }
        public MsBuildConditionalConstructWhenElementListAssociation(
            in MsBuildConditionalConstructWhenElement whenElement,
            in MsBuildConditionalConstructWhenElementList whenElementList = default)
        {
            MsBuildConditionalConstructWhenElement = whenElement;
            MsBuildConditionalConstructWhenElementList = whenElementList;
        }
        public MsBuildConditionalConstructWhenElementListAssociation(
            in MsBuildCondition condition,
            in MsBuildConditionalContructItemGroupPropertyGroupSection content,
            in MsBuildConditionalConstructWhenElementList whenElementList = default)
            : this(new MsBuildConditionalConstructWhenElement(in condition, in content), in whenElementList)
        {
        }
        public MsBuildConditionalConstructWhenElementListAssociation(
            in string condition,
            in MsBuildConditionalContructItemGroupPropertyGroupSection content,
            in MsBuildConditionalConstructWhenElementList whenElementList = default)
            : this(new MsBuildCondition(in condition), in content, in whenElementList)
        {
        }

        [Key]
        [ProtoMember(1)]
        public int MsBuildConditionalConstructWhenElementListAssociationId { get; set; }

        [ProtoMember(2)]
        public MsBuildConditionalConstructWhenElement MsBuildConditionalConstructWhenElement { get; set; }
        [ProtoMember(3)]
        public int MsBuildConditionalConstructWhenElementId { get; set; }

        [ProtoMember(4)]
        public MsBuildConditionalConstructWhenElementList MsBuildConditionalConstructWhenElementList { get; set; }
        [ProtoMember(5)]
        public int MsBuildConditionalConstructWhenElementListId { get; set; }

        public MsBuildConditionalConstructWhenElement GetRecord() => MsBuildConditionalConstructWhenElement;

        public void SetRecord(in MsBuildConditionalConstructWhenElement record)
        {
            MsBuildConditionalConstructWhenElement = record;
            MsBuildConditionalConstructWhenElementId = record.MsBuildConditionalConstructWhenElementId;
        }
    }
}
