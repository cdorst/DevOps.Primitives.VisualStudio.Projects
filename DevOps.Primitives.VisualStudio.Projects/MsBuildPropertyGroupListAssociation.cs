﻿using Common.EntityFrameworkServices;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.VisualStudio.Projects
{
    [ProtoContract]
    [Table("MsBuildPropertyGroupListAssociations", Schema = nameof(VisualStudio))]
    public class MsBuildPropertyGroupListAssociation : IUniqueListAssociation<MsBuildPropertyGroup>
    {
        [Key]
        [ProtoMember(1)]
        public int MsBuildPropertyGroupListAssociationId { get; set; }

        [ProtoMember(2)]
        public MsBuildPropertyGroup MsBuildPropertyGroup { get; set; }
        [ProtoMember(3)]
        public int MsBuildPropertyGroupId { get; set; }

        [ProtoMember(4)]
        public MsBuildPropertyGroupList MsBuildPropertyGroupList { get; set; }
        [ProtoMember(5)]
        public int MsBuildPropertyGroupListId { get; set; }

        public MsBuildPropertyGroup GetRecord() => MsBuildPropertyGroup;

        public void SetRecord(MsBuildPropertyGroup record)
        {
            MsBuildPropertyGroup = record;
            MsBuildPropertyGroupId = MsBuildPropertyGroup.MsBuildPropertyGroupId;
        }
    }
}