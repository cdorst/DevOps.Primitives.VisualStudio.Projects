using Common.EntityFrameworkServices;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DevOps.Primitives.VisualStudio.Projects.EntityFramework.Services
{
    public class MsBuildPropertyGroupListAssociationUpsertService<TDbContext> : UpsertService<TDbContext, MsBuildPropertyGroupListAssociation>
        where TDbContext : VisualStudioProjectsDbContext
    {
        public MsBuildPropertyGroupListAssociationUpsertService(ICacheService<MsBuildPropertyGroupListAssociation> cache, TDbContext database, ILogger<UpsertService<TDbContext, MsBuildPropertyGroupListAssociation>> logger)
            : base(cache, database, logger, database.MsBuildPropertyGroupListAssociations)
        {
            CacheKey = record => $"{nameof(VisualStudio)}.{nameof(MsBuildPropertyGroupListAssociation)}={record.MsBuildPropertyGroupId}:{record.MsBuildPropertyGroupListId}";
        }

        protected override IEnumerable<object> EnumerateReferences(MsBuildPropertyGroupListAssociation record)
        {
            yield return record.MsBuildPropertyGroup;
            yield return record.MsBuildPropertyGroupList;
        }

        protected override Expression<Func<MsBuildPropertyGroupListAssociation, bool>> FindExisting(MsBuildPropertyGroupListAssociation record)
            => existing
                => existing.MsBuildPropertyGroupId == record.MsBuildPropertyGroupId
                && existing.MsBuildPropertyGroupListId == record.MsBuildPropertyGroupListId;
    }
}
