using DevOps.Primitives.Strings.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace DevOps.Primitives.VisualStudio.Projects.EntityFramework
{
    public class VisualStudioProjectsDbContext : UniqueStringsDbContext
    {
        public VisualStudioProjectsDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<MsBuildCondition> MsBuildConditions { get; set; }
        public DbSet<MsBuildConditionalConstruct> MsBuildConditionalConstructs { get; set; }
        public DbSet<MsBuildConditionalConstructList> MsBuildConditionalConstructLists { get; set; }
        public DbSet<MsBuildConditionalConstructListAssociation> MsBuildConditionalConstructListAssociations { get; set; }
        public DbSet<MsBuildConditionalConstructOtherwiseElement> MsBuildConditionalConstructOtherwiseElements { get; set; }
        public DbSet<MsBuildConditionalConstructWhenElement> MsBuildConditionalConstructWhenElements { get; set; }
        public DbSet<MsBuildConditionalConstructWhenElementList> MsBuildConditionalConstructWhenElementLists { get; set; }
        public DbSet<MsBuildConditionalConstructWhenElementListAssociation> MsBuildConditionalConstructWhenElementListAssociations { get; set; }
        public DbSet<MsBuildConditionalContructItemGroupPropertyGroupSection> MsBuildConditionalContructItemGroupPropertyGroupSections { get; set; }
        public DbSet<MsBuildItem> MsBuildItems { get; set; }
        public DbSet<MsBuildItemGroup> MsBuildItemGroups { get; set; }
        public DbSet<MsBuildItemGroupList> MsBuildItemGroupLists { get; set; }
        public DbSet<MsBuildItemGroupListAssociation> MsBuildItemGroupListAssociations { get; set; }
        public DbSet<MsBuildItemList> MsBuildItemLists { get; set; }
        public DbSet<MsBuildItemListAssociation> MsBuildItemListAssociations { get; set; }
        public DbSet<MsBuildProjectFile> MsBuildProjectFiles { get; set; }
        public DbSet<MsBuildProperty> MsBuildProperties { get; set; }
        public DbSet<MsBuildPropertyGroup> MsBuildPropertyGroups { get; set; }
        public DbSet<MsBuildPropertyGroupList> MsBuildPropertyGroupLists { get; set; }
        public DbSet<MsBuildPropertyGroupListAssociation> MsBuildPropertyGroupListAssociations { get; set; }
        public DbSet<MsBuildPropertyList> MsBuildPropertyLists { get; set; }
        public DbSet<MsBuildPropertyListAssociation> MsBuildPropertyListAssociations { get; set; }
        public DbSet<MsBuildTarget> MsBuildTargets { get; set; }
        public DbSet<MsBuildTargetList> MsBuildTargetLists { get; set; }
        public DbSet<MsBuildTargetListAssociation> MsBuildTargetListAssociations { get; set; }
        public DbSet<MsBuildTask> MsBuildTasks { get; set; }
        public DbSet<MsBuildTaskAttribute> MsBuildTaskAttributes { get; set; }
        public DbSet<MsBuildTaskAttributeList> MsBuildTaskAttributeLists { get; set; }
        public DbSet<MsBuildTaskAttributeListAssociation> MsBuildTaskAttributeListAssociations { get; set; }
        public DbSet<MsBuildTaskList> MsBuildTaskLists { get; set; }
        public DbSet<MsBuildTaskListAssociation> MsBuildTaskListAssociations { get; set; }
        public DbSet<Project> Projects { get; set; }
    }
}
