using Common.EntityFrameworkServices;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.String;

namespace DevOps.Primitives.VisualStudio.Projects
{
    [ProtoContract]
    [Table("MsBuildPropertyGroups", Schema = nameof(VisualStudio))]
    public class MsBuildPropertyGroup : IUniqueListRecord
    {
        private const string Tag = "PropertyGroup";

        public MsBuildPropertyGroup() { }
        public MsBuildPropertyGroup(
            in MsBuildPropertyList propertyList,
            in MsBuildCondition condition = default)
        {
            MsBuildCondition = condition;
            MsBuildPropertyList = propertyList;
        }
        public MsBuildPropertyGroup(
            in MsBuildPropertyList propertyList,
            in string condition = default)
            : this(in propertyList, ConditionHelper.Create(in condition))
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

        public string GetPropertyGroup()
            => Concat("  <", Tag, MsBuildCondition?.GetCondition(), ">\r\n", MsBuildPropertyList.GetProperties(), "\r\n  </", Tag, ">");
    }
}
