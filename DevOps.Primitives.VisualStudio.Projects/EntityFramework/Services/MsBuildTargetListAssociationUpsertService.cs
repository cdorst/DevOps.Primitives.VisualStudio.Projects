using Common.EntityFrameworkServices;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DevOps.Primitives.VisualStudio.Projects.EntityFramework.Services
{
    public class MsBuildTargetListAssociationUpsertService<TDbContext> : UpsertService<TDbContext, MsBuildTargetListAssociation>
        where TDbContext : VisualStudioProjectsDbContext
    {
        public MsBuildTargetListAssociationUpsertService(ICacheService<MsBuildTargetListAssociation> cache, TDbContext database, ILogger<UpsertService<TDbContext, MsBuildTargetListAssociation>> logger)
            : base(cache, database, logger, database.MsBuildTargetListAssociations)
        {
            CacheKey = record => $"{nameof(VisualStudio)}.{nameof(MsBuildTargetListAssociation)}={record.MsBuildTargetId}:{record.MsBuildTargetListId}";
        }

        protected override IEnumerable<object> EnumerateReferences(MsBuildTargetListAssociation record)
        {
            yield return record.MsBuildTarget;
            yield return record.MsBuildTargetList;
        }

        protected override Expression<Func<MsBuildTargetListAssociation, bool>> FindExisting(MsBuildTargetListAssociation record)
            => existing
                => existing.MsBuildTargetId == record.MsBuildTargetId
                && existing.MsBuildTargetListId == record.MsBuildTargetListId;
    }
}
