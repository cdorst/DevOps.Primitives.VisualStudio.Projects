﻿using Common.EntityFrameworkServices;
using DevOps.Primitives.Strings;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.String;

namespace DevOps.Primitives.VisualStudio.Projects
{
    [ProtoContract]
    [Table("MsBuildTargets", Schema = nameof(VisualStudio))]
    public class MsBuildTarget : IUniqueListRecord
    {
        public MsBuildTarget() { }
        public MsBuildTarget(in MsBuildTaskList taskList, in AsciiStringReference name, in AsciiStringReference outputs = default)
        {
            MsBuildTaskList = taskList;
            Name = name;
            Outputs = outputs;
        }
        public MsBuildTarget(in MsBuildTaskList taskList, in string name, in string outputs = default)
            : this(in taskList, new AsciiStringReference(in name), !IsNullOrWhiteSpace(outputs) ? new AsciiStringReference(in outputs) : null)
        {
        }

        [Key]
        [ProtoMember(1)]
        public int MsBuildTargetId { get; set; }

        [ProtoMember(2)]
        public MsBuildTaskList MsBuildTaskList { get; set; }
        [ProtoMember(3)]
        public int MsBuildTaskListId { get; set; }

        [ProtoMember(4)]
        public AsciiStringReference Name { get; set; }
        [ProtoMember(5)]
        public int NameId { get; set; }

        [ProtoMember(6)]
        public AsciiStringReference Outputs { get; set; }
        [ProtoMember(7)]
        public int? OutputsId { get; set; }

        public string GetTarget() => Concat("<Target Name=\"", Name.Value, "\"", GetOutputs(), ">", MsBuildTaskList.GetTasks(), "</Target>");

        private string GetOutputs() => Outputs == null ? Empty : Concat(" Outputs=\"", Outputs.Value, "\"");
    }
}
