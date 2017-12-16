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
        public MsBuildConditionalConstructWhenElementListAssociation(MsBuildConditionalConstructWhenElement whenElement, MsBuildConditionalConstructWhenElementList whenElementList = null)
        {
            MsBuildConditionalConstructWhenElement = whenElement;
            MsBuildConditionalConstructWhenElementList = whenElementList;
        }
        public MsBuildConditionalConstructWhenElementListAssociation(MsBuildCondition condition, MsBuildConditionalContructItemGroupPropertyGroupSection content, MsBuildConditionalConstructWhenElementList whenElementList = null)
            : this(new MsBuildConditionalConstructWhenElement(condition, content), whenElementList)
        {
        }
        public MsBuildConditionalConstructWhenElementListAssociation(string condition, MsBuildConditionalContructItemGroupPropertyGroupSection content, MsBuildConditionalConstructWhenElementList whenElementList = null)
            : this(new MsBuildCondition(condition), content, whenElementList)
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

        public void SetRecord(MsBuildConditionalConstructWhenElement record)
        {
            MsBuildConditionalConstructWhenElement = record;
            MsBuildConditionalConstructWhenElementId = MsBuildConditionalConstructWhenElement.MsBuildConditionalConstructWhenElementId;
        }
    }
}
