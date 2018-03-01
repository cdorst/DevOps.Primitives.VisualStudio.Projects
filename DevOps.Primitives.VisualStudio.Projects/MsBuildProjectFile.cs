using Common.EnumStringValues;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.VisualStudio.Projects
{
    [ProtoContract]
    [Table("MsBuildProjectFiles", Schema = nameof(VisualStudio))]
    public class MsBuildProjectFile
    {
        private const string Tag = "Project";
        private const string XmlHeader = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n";

        public MsBuildProjectFile() { }
        public MsBuildProjectFile(ProjectType projectType, MsBuildConditionalContructItemGroupPropertyGroupSection content, MsBuildTargetList targetList)
        {
            ProjectType = projectType;
            MsBuildConditionalContructItemGroupPropertyGroupSection = content;
            MsBuildTargetList = targetList;
        }

        [Key]
        [ProtoMember(1)]
        public int MsBuildProjectFileId { get; set; }

        [ProtoMember(2)]
        public ProjectType ProjectType { get; set; }

        [ProtoMember(3)]
        public MsBuildConditionalContructItemGroupPropertyGroupSection MsBuildConditionalContructItemGroupPropertyGroupSection { get; set; }
        [ProtoMember(4)]
        public int MsBuildConditionalContructItemGroupPropertyGroupSectionId { get; set; }

        [ProtoMember(5)]
        public MsBuildTargetList MsBuildTargetList { get; set; }
        [ProtoMember(6)]
        public int? MsBuildTargetListId { get; set; }

        public string GetProjectFile()
            => ProjectType == ProjectType.MicrosoftNetSdk
                ? GetProjectNode()
                : $"{XmlHeader}{GetProjectNode()}";

        private string GetProjectNode()
            => $"<{Tag} {GetToolsVersion()}{ProjectType.GetStringValue()}>{MsBuildConditionalContructItemGroupPropertyGroupSection.GetSection()}{MsBuildTargetList?.GetTargets()}</{Tag}>\r\n";

        private string GetToolsVersion() => ProjectType == ProjectType.MicrosoftNetSdk ? string.Empty : "ToolsVersion=\"15.0\" ";
    }
}
