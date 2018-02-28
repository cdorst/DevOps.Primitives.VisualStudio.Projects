using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.VisualStudio.Projects
{
    [ProtoContract]
    [Table("MsBuildConditionalContructItemGroupPropertyGroupSections", Schema = nameof(VisualStudio))]
    public class MsBuildConditionalContructItemGroupPropertyGroupSection
    {
        public MsBuildConditionalContructItemGroupPropertyGroupSection() { }
        public MsBuildConditionalContructItemGroupPropertyGroupSection(MsBuildPropertyGroupList propertyGroupList, MsBuildItemGroupList itemGroupList, MsBuildConditionalConstructList conditionalConstructList)
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
            var propItemSpace = (!string.IsNullOrEmpty(properties) && !string.IsNullOrEmpty(items)) ? doubleReturn : string.Empty;
            return $"{doubleReturn}{properties}{items}{propItemSpace}{MsBuildConditionalConstructList?.GetConditionalConstructs()}{doubleReturn}";
        }
    }
}
