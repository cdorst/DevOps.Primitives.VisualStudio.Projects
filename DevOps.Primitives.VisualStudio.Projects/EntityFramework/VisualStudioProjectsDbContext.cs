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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            AddIndexes(modelBuilder);
        }

        private void AddIndexes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MsBuildCondition>()
                .HasIndex(e => new { e.ConditionId }).IsUnique();
            modelBuilder.Entity<MsBuildConditionalConstruct>()
                .HasIndex(e => new { e.MsBuildConditionalConstructOtherwiseElementId, e.MsBuildConditionalConstructWhenElementListId }).IsUnique();
            modelBuilder.Entity<MsBuildConditionalConstructList>()
                .HasIndex(e => new { e.ListIdentifierId }).IsUnique();
            modelBuilder.Entity<MsBuildConditionalConstructListAssociation>()
                .HasIndex(e => new { e.MsBuildConditionalConstructId, e.MsBuildConditionalConstructListId }).IsUnique();
            modelBuilder.Entity<MsBuildConditionalConstructOtherwiseElement>()
                .HasIndex(e => new { e.MsBuildConditionalContructItemGroupPropertyGroupSectionId }).IsUnique();
            modelBuilder.Entity<MsBuildConditionalConstructWhenElement>()
                .HasIndex(e => new { e.MsBuildConditionalContructItemGroupPropertyGroupSectionId, e.MsBuildConditionId }).IsUnique();
            modelBuilder.Entity<MsBuildConditionalConstructWhenElementList>()
                .HasIndex(e => new { e.ListIdentifierId }).IsUnique();
            modelBuilder.Entity<MsBuildConditionalConstructWhenElementListAssociation>()
                .HasIndex(e => new { e.MsBuildConditionalConstructWhenElementId, e.MsBuildConditionalConstructWhenElementListId }).IsUnique();
            modelBuilder.Entity<MsBuildConditionalContructItemGroupPropertyGroupSection>()
                .HasIndex(e => new { e.MsBuildConditionalConstructListId, e.MsBuildItemGroupListId, e.MsBuildPropertyGroupListId }).IsUnique();
            modelBuilder.Entity<MsBuildItem>()
                .HasIndex(e => new { e.ElementNameId, e.ElementValueId, e.MsBuildConditionId }).IsUnique();
            modelBuilder.Entity<MsBuildItemGroup>()
                .HasIndex(e => new { e.MsBuildConditionId, e.MsBuildItemListId }).IsUnique();
            modelBuilder.Entity<MsBuildItemGroupList>()
                .HasIndex(e => new { e.ListIdentifierId }).IsUnique();
            modelBuilder.Entity<MsBuildItemGroupListAssociation>()
                .HasIndex(e => new { e.MsBuildItemGroupId, e.MsBuildItemGroupListId }).IsUnique();
            modelBuilder.Entity<MsBuildItemList>()
                .HasIndex(e => new { e.ListIdentifierId }).IsUnique();
            modelBuilder.Entity<MsBuildItemListAssociation>()
                .HasIndex(e => new { e.MsBuildItemId, e.MsBuildItemListId }).IsUnique();
            modelBuilder.Entity<MsBuildProjectFile>()
                .HasIndex(e => new { e.MsBuildConditionalContructItemGroupPropertyGroupSectionId, e.MsBuildTargetListId, e.ProjectType }).IsUnique();
            modelBuilder.Entity<MsBuildProperty>()
                .HasIndex(e => new { e.ElementNameId, e.ElementValueId, e.MsBuildConditionId }).IsUnique();
            modelBuilder.Entity<MsBuildPropertyGroup>()
                .HasIndex(e => new { e.MsBuildConditionId, e.MsBuildPropertyListId }).IsUnique();
            modelBuilder.Entity<MsBuildPropertyGroupList>()
                .HasIndex(e => new { e.ListIdentifierId }).IsUnique();
            modelBuilder.Entity<MsBuildPropertyGroupListAssociation>()
                .HasIndex(e => new { e.MsBuildPropertyGroupId, e.MsBuildPropertyGroupListId }).IsUnique();
            modelBuilder.Entity<MsBuildPropertyList>()
                .HasIndex(e => new { e.ListIdentifierId }).IsUnique();
            modelBuilder.Entity<MsBuildPropertyListAssociation>()
                .HasIndex(e => new { e.MsBuildPropertyId, e.MsBuildPropertyListId }).IsUnique();
            modelBuilder.Entity<MsBuildTarget>()
                .HasIndex(e => new { e.MsBuildTaskListId, e.NameId, e.OutputsId }).IsUnique();
            modelBuilder.Entity<MsBuildTargetList>()
                .HasIndex(e => new { e.ListIdentifierId }).IsUnique();
            modelBuilder.Entity<MsBuildTargetListAssociation>()
                .HasIndex(e => new { e.MsBuildTargetId, e.MsBuildTargetListId }).IsUnique();
            modelBuilder.Entity<MsBuildTask>()
                .HasIndex(e => new { e.ElementId, e.MsBuildTaskAttributeListId }).IsUnique();
            modelBuilder.Entity<MsBuildTaskAttribute>()
                .HasIndex(e => new { e.AttributeId, e.ValueId }).IsUnique();
            modelBuilder.Entity<MsBuildTaskAttributeList>()
                .HasIndex(e => new { e.ListIdentifierId }).IsUnique();
            modelBuilder.Entity<MsBuildTaskAttributeListAssociation>()
                .HasIndex(e => new { e.MsBuildTaskAttributeId, e.MsBuildTaskAttributeListId }).IsUnique();
            modelBuilder.Entity<MsBuildTaskList>()
                .HasIndex(e => new { e.ListIdentifierId }).IsUnique();
            modelBuilder.Entity<MsBuildTaskListAssociation>()
                .HasIndex(e => new { e.MsBuildTaskId, e.MsBuildTaskListId }).IsUnique();
            modelBuilder.Entity<Project>()
                .HasIndex(e => new { e.MsBuildProjectFileId, e.NameId }).IsUnique();
        }
    }
}
