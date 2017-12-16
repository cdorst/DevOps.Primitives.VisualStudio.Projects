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
        public MsBuildPropertyListAssociation(MsBuildProperty property, MsBuildPropertyList propertyList = null)
        {
            MsBuildProperty = property;
            MsBuildPropertyList = propertyList;
        }
        public MsBuildPropertyListAssociation(AsciiStringReference name, AsciiStringReference value, MsBuildCondition condition = null, MsBuildPropertyList propertyList = null)
            : this(new MsBuildProperty(name, value, condition), propertyList)
        {
        }
        public MsBuildPropertyListAssociation(string name, string value, MsBuildCondition condition = null, MsBuildPropertyList propertyList = null)
            : this(new AsciiStringReference(name), new AsciiStringReference(value), condition, propertyList)
        {
        }
        public MsBuildPropertyListAssociation(string name, string value, string condition = null, MsBuildPropertyList propertyList = null)
            : this(name, value, ConditionHelper.Create(condition), propertyList)
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

        public void SetRecord(MsBuildProperty record)
        {
            MsBuildProperty = record;
            MsBuildPropertyId = MsBuildProperty.MsBuildPropertyId;
        }
    }
}
