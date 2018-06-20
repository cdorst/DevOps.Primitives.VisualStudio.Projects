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
        public MsBuildPropertyGroupListAssociation(
            in MsBuildPropertyGroup propertyGroup,
            in MsBuildPropertyGroupList propertyGroupList = default)
        {
            MsBuildPropertyGroup = propertyGroup;
            MsBuildPropertyGroupList = propertyGroupList;
        }
        public MsBuildPropertyGroupListAssociation(
            in MsBuildPropertyList propertyList,
            in MsBuildCondition condition = default,
            in MsBuildPropertyGroupList propertyGroupList = default)
            : this(new MsBuildPropertyGroup(in propertyList, in condition), in propertyGroupList)
        {
        }
        public MsBuildPropertyGroupListAssociation(
            in MsBuildPropertyList propertyList,
            in string condition = default,
            in MsBuildPropertyGroupList propertyGroupList = default)
            : this(in propertyList, ConditionHelper.Create(in condition), in propertyGroupList)
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

        public void SetRecord(in MsBuildPropertyGroup record)
        {
            MsBuildPropertyGroup = record;
            MsBuildPropertyGroupId = record.MsBuildPropertyGroupId;
        }
    }
}
