using Common.EntityFrameworkServices;
using DevOps.Primitives.Strings;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.VisualStudio.Projects
{
    [ProtoContract]
    [Table("MsBuildTasks", Schema = nameof(VisualStudio))]
    public class MsBuildTask : IUniqueListRecord
    {
        public MsBuildTask() { }
        public MsBuildTask(AsciiStringReference element, MsBuildTaskAttributeList taskAttributeList)
        {
            Element = element;
            MsBuildTaskAttributeList = taskAttributeList;
        }
        public MsBuildTask(string element, MsBuildTaskAttributeList taskAttributeList)
            : this(new AsciiStringReference(element), taskAttributeList)
        {
        }

        [Key]
        [ProtoMember(1)]
        public int MsBuildTaskId { get; set; }

        [ProtoMember(2)]
        public AsciiStringReference Element { get; set; }
        [ProtoMember(3)]
        public int ElementId { get; set; }

        [ProtoMember(4)]
        public MsBuildTaskAttributeList MsBuildTaskAttributeList { get; set; }
        [ProtoMember(5)]
        public int MsBuildTaskAttributeListId { get; set; }

        public string GetTask() => $"<{Element.Value} {MsBuildTaskAttributeList.GetTaskAttributes()} />";
    }
}
