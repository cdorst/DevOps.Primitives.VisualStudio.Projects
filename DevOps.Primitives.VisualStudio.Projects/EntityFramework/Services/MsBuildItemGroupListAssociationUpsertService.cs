using Common.EntityFrameworkServices;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DevOps.Primitives.VisualStudio.Projects.EntityFramework.Services
{
    public class MsBuildItemGroupListAssociationUpsertService<TDbContext> : UpsertService<TDbContext, MsBuildItemGroupListAssociation>
        where TDbContext : VisualStudioProjectsDbContext
    {
        public MsBuildItemGroupListAssociationUpsertService(ICacheService<MsBuildItemGroupListAssociation> cache, TDbContext database, ILogger<UpsertService<TDbContext, MsBuildItemGroupListAssociation>> logger)
            : base(cache, database, logger, database.MsBuildItemGroupListAssociations)
        {
            CacheKey = record => $"{nameof(VisualStudio)}.{nameof(MsBuildItemGroupListAssociation)}={record.MsBuildItemGroupId}:{record.MsBuildItemGroupListId}";
        }

        protected override IEnumerable<object> EnumerateReferences(MsBuildItemGroupListAssociation record)
        {
            yield return record.MsBuildItemGroup;
            yield return record.MsBuildItemGroupList;
        }

        protected override Expression<Func<MsBuildItemGroupListAssociation, bool>> FindExisting(MsBuildItemGroupListAssociation record)
            => existing
                => existing.MsBuildItemGroupId == record.MsBuildItemGroupId
                && existing.MsBuildItemGroupListId == record.MsBuildItemGroupListId;
    }
}
