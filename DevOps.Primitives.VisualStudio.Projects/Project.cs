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
