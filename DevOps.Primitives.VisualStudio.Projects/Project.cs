using DevOps.Primitives.Strings;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.VisualStudio.Projects
{
    [ProtoContract]
    [Table("Projects", Schema = nameof(VisualStudio))]
    public class Project
    {
        public Project() { }
        public Project(AsciiStringReference name, MsBuildProjectFile projectFile)
        {
            Name = name;
            MsBuildProjectFile = projectFile;
        }
        public Project(string name, MsBuildProjectFile projectFile)
            : this(new AsciiStringReference(name), projectFile)
        {
        }

        [Key]
        [ProtoMember(1)]
        public int ProjectId { get; set; }

        [ProtoMember(2)]
        public MsBuildProjectFile MsBuildProjectFile { get; set; }
        [ProtoMember(3)]
        public int MsBuildProjectFileId { get; set; }

        [ProtoMember(4)]
        public AsciiStringReference Name { get; set; }
        [ProtoMember(5)]
        public int NameId { get; set; }
    }
}
