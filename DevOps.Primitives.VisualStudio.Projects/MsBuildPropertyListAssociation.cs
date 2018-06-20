using Common.EntityFrameworkServices;
using DevOps.Primitives.Strings;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.VisualStudio.Projects
{
    [ProtoContract]
    [Table("MsBuildPropertyListAssociations", Schema = nameof(VisualStudio))]
    public class MsBuildPropertyListAssociation : IUniqueListAssociation<MsBuildProperty>
    {
        public MsBuildPropertyListAssociation() { }
        public MsBuildPropertyListAssociation(
            in MsBuildProperty property,
            in MsBuildPropertyList propertyList = default)
        {
            MsBuildProperty = property;
            MsBuildPropertyList = propertyList;
        }
        public MsBuildPropertyListAssociation(
            in AsciiStringReference name,
            in AsciiStringReference value,
            in MsBuildCondition condition = default,
            in MsBuildPropertyList propertyList = default)
            : this(new MsBuildProperty(in name, in value, in condition), in propertyList)
        {
        }
        public MsBuildPropertyListAssociation(
            in string name,
            in string value,
            in MsBuildCondition condition = default,
            in MsBuildPropertyList propertyList = default)
            : this(
                  new AsciiStringReference(in name),
                  new AsciiStringReference(in value),
                  in condition,
                  in propertyList)
        {
        }
        public MsBuildPropertyListAssociation(
            in string name,
            in string value,
            in string condition = default,
            in MsBuildPropertyList propertyList = default)
            : this(
                  in name,
                  in value,
                  ConditionHelper.Create(in condition),
                  in propertyList)
        {
        }

        [Key]
        [ProtoMember(1)]
        public int MsBuildPropertyListAssociationId { get; set; }

        [ProtoMember(2)]
        public MsBuildProperty MsBuildProperty { get; set; }
        [ProtoMember(3)]
        public int MsBuildPropertyId { get; set; }

        [ProtoMember(4)]
        public MsBuildPropertyList MsBuildPropertyList { get; set; }
        [ProtoMember(5)]
        public int MsBuildPropertyListId { get; set; }

        public MsBuildProperty GetRecord() => MsBuildProperty;

        public void SetRecord(in MsBuildProperty record)
        {
            MsBuildProperty = record;
            MsBuildPropertyId = record.MsBuildPropertyId;
        }
    }
}
