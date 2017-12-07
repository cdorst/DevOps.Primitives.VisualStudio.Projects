using Common.EntityFrameworkServices;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DevOps.Primitives.VisualStudio.Projects.EntityFramework.Services
{
    public class MsBuildItemListAssociationUpsertService<TDbContext> : UpsertService<TDbContext, MsBuildItemListAssociation>
        where TDbContext : VisualStudioProjectsDbContext
    {
        public MsBuildItemListAssociationUpsertService(ICacheService<MsBuildItemListAssociation> cache, TDbContext database, ILogger<UpsertService<TDbContext, MsBuildItemListAssociation>> logger)
            : base(cache, database, logger, database.MsBuildItemListAssociations)
        {
            CacheKey = record => $"{nameof(VisualStudio)}.{nameof(MsBuildItemListAssociation)}={record.MsBuildItemId}:{record.MsBuildItemListId}";
        }

        protected override IEnumerable<object> EnumerateReferences(MsBuildItemListAssociation record)
        {
            yield return record.MsBuildItem;
            yield return record.MsBuildItemList;
        }

        protected override Expression<Func<MsBuildItemListAssociation, bool>> FindExisting(MsBuildItemListAssociation record)
            => existing
                => existing.MsBuildItemId == record.MsBuildItemId
                && existing.MsBuildItemListId == record.MsBuildItemListId;
    }
}
