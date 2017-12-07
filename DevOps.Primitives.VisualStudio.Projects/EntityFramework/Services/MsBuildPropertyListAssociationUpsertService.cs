using Common.EntityFrameworkServices;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DevOps.Primitives.VisualStudio.Projects.EntityFramework.Services
{
    public class MsBuildPropertyListAssociationUpsertService<TDbContext> : UpsertService<TDbContext, MsBuildPropertyListAssociation>
        where TDbContext : VisualStudioProjectsDbContext
    {
        public MsBuildPropertyListAssociationUpsertService(ICacheService<MsBuildPropertyListAssociation> cache, TDbContext database, ILogger<UpsertService<TDbContext, MsBuildPropertyListAssociation>> logger)
            : base(cache, database, logger, database.MsBuildPropertyListAssociations)
        {
            CacheKey = record => $"{nameof(VisualStudio)}.{nameof(MsBuildPropertyListAssociation)}={record.MsBuildPropertyId}:{record.MsBuildPropertyListId}";
        }

        protected override IEnumerable<object> EnumerateReferences(MsBuildPropertyListAssociation record)
        {
            yield return record.MsBuildProperty;
            yield return record.MsBuildPropertyList;
        }

        protected override Expression<Func<MsBuildPropertyListAssociation, bool>> FindExisting(MsBuildPropertyListAssociation record)
            => existing
                => existing.MsBuildPropertyId == record.MsBuildPropertyId
                && existing.MsBuildPropertyListId == record.MsBuildPropertyListId;
    }
}
