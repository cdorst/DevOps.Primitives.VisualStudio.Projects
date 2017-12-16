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
        public MsBuildItemGroupListAssociation(MsBuildItemGroup itemGroup, MsBuildItemGroupList itemGroupList = null)
        {
            MsBuildItemGroup = itemGroup;
            MsBuildItemGroupList = itemGroupList;
        }
        public MsBuildItemGroupListAssociation(MsBuildItemList itemList, MsBuildCondition condition = null, MsBuildItemGroupList itemGroupList = null)
            : this(new MsBuildItemGroup(itemList, condition), itemGroupList)
        {
        }
        public MsBuildItemGroupListAssociation(MsBuildItemList itemList, string condition = null, MsBuildItemGroupList itemGroupList = null)
            : this(itemList, ConditionHelper.Create(condition), itemGroupList)
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

        public void SetRecord(MsBuildItemGroup record)
        {
            MsBuildItemGroup = record;
            MsBuildItemGroupId = MsBuildItemGroup.MsBuildItemGroupId;
        }
    }
}
