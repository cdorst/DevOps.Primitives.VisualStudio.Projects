using Common.EntityFrameworkServices;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.VisualStudio.Projects
{
    [ProtoContract]
    [Table("MsBuildItemGroupListAssociations", Schema = nameof(VisualStudio))]
    public class MsBuildItemGroupListAssociation : IUniqueListAssociation<MsBuildItemGroup>
    {
        public MsBuildItemGroupListAssociation() { }
        public MsBuildItemGroupListAssociation(
            in MsBuildItemGroup itemGroup,
            in MsBuildItemGroupList itemGroupList = default)
        {
            MsBuildItemGroup = itemGroup;
            MsBuildItemGroupList = itemGroupList;
        }
        public MsBuildItemGroupListAssociation(
            in MsBuildItemList itemList,
            in MsBuildCondition condition = default,
            in MsBuildItemGroupList itemGroupList = default)
            : this(new MsBuildItemGroup(in itemList, in condition), in itemGroupList)
        {
        }
        public MsBuildItemGroupListAssociation(
            MsBuildItemList itemList,
            string condition = default,
            MsBuildItemGroupList itemGroupList = default)
            : this(in itemList, ConditionHelper.Create(in condition), in itemGroupList)
        {
        }

        [Key]
        [ProtoMember(1)]
        public int MsBuildItemGroupListAssociationId { get; set; }

        [ProtoMember(2)]
        public MsBuildItemGroup MsBuildItemGroup { get; set; }
        [ProtoMember(3)]
        public int MsBuildItemGroupId { get; set; }

        [ProtoMember(4)]
        public MsBuildItemGroupList MsBuildItemGroupList { get; set; }
        [ProtoMember(5)]
        public int MsBuildItemGroupListId { get; set; }

        public MsBuildItemGroup GetRecord() => MsBuildItemGroup;

        public void SetRecord(in MsBuildItemGroup record)
        {
            MsBuildItemGroup = record;
            MsBuildItemGroupId = record.MsBuildItemGroupId;
        }
    }
}
