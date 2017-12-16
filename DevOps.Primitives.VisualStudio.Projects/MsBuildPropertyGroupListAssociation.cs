using Common.EntityFrameworkServices;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.VisualStudio.Projects
{
    [ProtoContract]
    [Table("MsBuildPropertyGroupListAssociations", Schema = nameof(VisualStudio))]
    public class MsBuildPropertyGroupListAssociation : IUniqueListAssociation<MsBuildPropertyGroup>
    {
        public MsBuildPropertyGroupListAssociation() { }
        public MsBuildPropertyGroupListAssociation(MsBuildPropertyGroup propertyGroup, MsBuildPropertyGroupList propertyGroupList = null)
        {
            MsBuildPropertyGroup = propertyGroup;
            MsBuildPropertyGroupList = propertyGroupList;
        }
        public MsBuildPropertyGroupListAssociation(MsBuildPropertyList propertyList, MsBuildCondition condition = null, MsBuildPropertyGroupList propertyGroupList = null)
            : this(new MsBuildPropertyGroup(propertyList, condition), propertyGroupList)
        {
        }
        public MsBuildPropertyGroupListAssociation(MsBuildPropertyList propertyList, string condition = null, MsBuildPropertyGroupList propertyGroupList = null)
            : this(propertyList, ConditionHelper.Create(condition), propertyGroupList)
        {
        }

        [Key]
        [ProtoMember(1)]
        public int MsBuildPropertyGroupListAssociationId { get; set; }

        [ProtoMember(2)]
        public MsBuildPropertyGroup MsBuildPropertyGroup { get; set; }
        [ProtoMember(3)]
        public int MsBuildPropertyGroupId { get; set; }

        [ProtoMember(4)]
        public MsBuildPropertyGroupList MsBuildPropertyGroupList { get; set; }
        [ProtoMember(5)]
        public int MsBuildPropertyGroupListId { get; set; }

        public MsBuildPropertyGroup GetRecord() => MsBuildPropertyGroup;

        public void SetRecord(MsBuildPropertyGroup record)
        {
            MsBuildPropertyGroup = record;
            MsBuildPropertyGroupId = MsBuildPropertyGroup.MsBuildPropertyGroupId;
        }
    }
}
