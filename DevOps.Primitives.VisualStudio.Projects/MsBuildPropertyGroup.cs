using Common.EntityFrameworkServices;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.VisualStudio.Projects
{
    [ProtoContract]
    [Table("MsBuildPropertyGroups", Schema = nameof(VisualStudio))]
    public class MsBuildPropertyGroup : IUniqueListRecord
    {
        private const string Tag = "ProjectGroup";

        public MsBuildPropertyGroup() { }
        public MsBuildPropertyGroup(MsBuildPropertyList propertyList, MsBuildCondition condition = null)
        {
            MsBuildCondition = condition;
            MsBuildPropertyList = propertyList;
        }
        public MsBuildPropertyGroup(MsBuildPropertyList propertyList, string condition = null)
            : this(propertyList, ConditionHelper.Create(condition))
        {
        }

        [Key]
        [ProtoMember(1)]
        public int MsBuildPropertyGroupId { get; set; }

        [ProtoMember(2)]
        public MsBuildCondition MsBuildCondition { get; set; }
        [ProtoMember(3)]
        public int? MsBuildConditionId { get; set; }

        [ProtoMember(4)]
        public MsBuildPropertyList MsBuildPropertyList { get; set; }
        [ProtoMember(5)]
        public int MsBuildPropertyListId { get; set; }

        public string GetPropertyGroup() => $"    <{Tag}{MsBuildCondition?.GetCondition()}>\r\n{MsBuildPropertyList.GetProperties()}\r\n    </{Tag}>";
    }
}
