using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.String;

namespace DevOps.Primitives.VisualStudio.Projects
{
    [ProtoContract]
    [Table("MsBuildConditionalContructItemGroupPropertyGroupSections", Schema = nameof(VisualStudio))]
    public class MsBuildConditionalContructItemGroupPropertyGroupSection
    {
        public MsBuildConditionalContructItemGroupPropertyGroupSection() { }
        public MsBuildConditionalContructItemGroupPropertyGroupSection(
            in MsBuildPropertyGroupList propertyGroupList,
            in MsBuildItemGroupList itemGroupList,
            in MsBuildConditionalConstructList conditionalConstructList)
        {
            MsBuildConditionalConstructList = conditionalConstructList;
            MsBuildItemGroupList = itemGroupList;
            MsBuildPropertyGroupList = propertyGroupList;
        }

        [Key]
        [ProtoMember(1)]
        public int MsBuildConditionalContructItemGroupPropertyGroupSectionId { get; set; }

        [ProtoMember(2)]
        public MsBuildConditionalConstructList MsBuildConditionalConstructList { get; set; }
        [ProtoMember(3)]
        public int? MsBuildConditionalConstructListId { get; set; }

        [ProtoMember(4)]
        public MsBuildItemGroupList MsBuildItemGroupList { get; set; }
        [ProtoMember(5)]
        public int? MsBuildItemGroupListId { get; set; }

        [ProtoMember(6)]
        public MsBuildPropertyGroupList MsBuildPropertyGroupList { get; set; }
        [ProtoMember(7)]
        public int? MsBuildPropertyGroupListId { get; set; }

        public string GetSection()
        {
            var doubleReturn = "\r\n\r\n";
            var properties = MsBuildPropertyGroupList?.GetPropertyGroups();
            var items = MsBuildItemGroupList?.GetItemGroups();
            var propItemSpace = (!IsNullOrEmpty(properties) && !IsNullOrEmpty(items)) ? doubleReturn : Empty;
            return Concat(doubleReturn, properties, propItemSpace, items, MsBuildConditionalConstructList?.GetConditionalConstructs(), doubleReturn);
        }
    }
}
