using Common.EntityFrameworkServices;
using DevOps.Primitives.Strings.EntityFramework;
using DevOps.Primitives.VisualStudio.Projects.EntityFramework.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DevOps.Primitives.VisualStudio.Projects.EntityFramework
{
    public static class AddVisualStudioProjectsServicesExtension
    {
        public static IServiceCollection AddVisualStudioProjectsServices<TDbContext>(this IServiceCollection services)
            where TDbContext : VisualStudioProjectsDbContext
            => services
                .AddUniqueStringsServices<TDbContext>()
                .AddScoped<IUpsertService<TDbContext, MsBuildCondition>, MsBuildConditionUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, MsBuildConditionalConstruct>, MsBuildConditionalConstructUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, MsBuildConditionalConstructList>, MsBuildConditionalConstructListUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, MsBuildConditionalConstructListAssociation>, MsBuildConditionalConstructListAssociationUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, MsBuildConditionalConstructOtherwiseElement>, MsBuildConditionalConstructOtherwiseElementUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, MsBuildConditionalConstructWhenElement>, MsBuildConditionalConstructWhenElementUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, MsBuildConditionalConstructWhenElementList>, MsBuildConditionalConstructWhenElementListUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, MsBuildConditionalConstructWhenElementListAssociation>, MsBuildConditionalConstructWhenElementListAssociationUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, MsBuildConditionalContructItemGroupPropertyGroupSection>, MsBuildConditionalContructItemGroupPropertyGroupSectionUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, MsBuildItem>, MsBuildItemUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, MsBuildItemAttribute>, MsBuildItemAttributeUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, MsBuildItemAttributeList>, MsBuildItemAttributeListUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, MsBuildItemAttributeListAssociation>, MsBuildItemAttributeListAssociationUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, MsBuildItemGroup>, MsBuildItemGroupUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, MsBuildItemGroupList>, MsBuildItemGroupListUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, MsBuildItemGroupListAssociation>, MsBuildItemGroupListAssociationUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, MsBuildItemList>, MsBuildItemListUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, MsBuildItemListAssociation>, MsBuildItemListAssociationUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, MsBuildProjectFile>, MsBuildProjectFileUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, MsBuildProperty>, MsBuildPropertyUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, MsBuildPropertyGroup>, MsBuildPropertyGroupUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, MsBuildPropertyGroupList>, MsBuildPropertyGroupListUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, MsBuildPropertyGroupListAssociation>, MsBuildPropertyGroupListAssociationUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, MsBuildPropertyList>, MsBuildPropertyListUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, MsBuildPropertyListAssociation>, MsBuildPropertyListAssociationUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, MsBuildTarget>, MsBuildTargetUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, MsBuildTargetList>, MsBuildTargetListUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, MsBuildTargetListAssociation>, MsBuildTargetListAssociationUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, MsBuildTask>, MsBuildTaskUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, MsBuildTaskAttribute>, MsBuildTaskAttributeUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, MsBuildTaskAttributeList>, MsBuildTaskAttributeListUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, MsBuildTaskAttributeListAssociation>, MsBuildTaskAttributeListAssociationUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, MsBuildTaskList>, MsBuildTaskListUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, MsBuildTaskListAssociation>, MsBuildTaskListAssociationUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, Project>, ProjectUpsertService<TDbContext>>();
    }
}
