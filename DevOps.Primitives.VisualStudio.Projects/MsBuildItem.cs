﻿using Common.EntityFrameworkServices;
using DevOps.Primitives.Strings;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.VisualStudio.Projects
{
    [ProtoContract]
    [Table("MsBuildItems", Schema = nameof(VisualStudio))]
    public class MsBuildItem : IUniqueListRecord
    {
        [Key]
        [ProtoMember(1)]
        public int MsBuildItemId { get; set; }

        [ProtoMember(2)]
        public AsciiStringReference ElementName { get; set; }
        [ProtoMember(3)]
        public int ElementNameId { get; set; }

        [ProtoMember(4)]
        public AsciiStringReference ElementValue { get; set; }
        [ProtoMember(5)]
        public int ElementValueId { get; set; }

        [ProtoMember(6)]
        public MsBuildCondition MsBuildCondition { get; set; }
        [ProtoMember(7)]
        public int? MsBuildConditionId { get; set; }

        public string GetItem() => $"<{ElementName.Value}{MsBuildCondition?.GetCondition()}>{ElementValue.Value}</{ElementName.Value}>";
    }
}