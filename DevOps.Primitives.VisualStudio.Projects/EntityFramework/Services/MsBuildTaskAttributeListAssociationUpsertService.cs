using Common.EntityFrameworkServices;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DevOps.Primitives.VisualStudio.Projects.EntityFramework.Services
{
    public class MsBuildTaskAttributeListAssociationUpsertService<TDbContext> : UpsertService<TDbContext, MsBuildTaskAttributeListAssociation>
        where TDbContext : VisualStudioProjectsDbContext
    {
        public MsBuildTaskAttributeListAssociationUpsertService(ICacheService<MsBuildTaskAttributeListAssociation> cache, TDbContext database, ILogger<UpsertService<TDbContext, MsBuildTaskAttributeListAssociation>> logger)
            : base(cache, database, logger, database.MsBuildTaskAttributeListAssociations)
        {
            CacheKey = record => $"{nameof(VisualStudio)}.{nameof(MsBuildTaskAttributeListAssociation)}={record.MsBuildTaskAttributeId}:{record.MsBuildTaskAttributeListId}";
        }

        protected override IEnumerable<object> EnumerateReferences(MsBuildTaskAttributeListAssociation record)
        {
            yield return record.MsBuildTaskAttribute;
            yield return record.MsBuildTaskAttributeList;
        }

        protected override Expression<Func<MsBuildTaskAttributeListAssociation, bool>> FindExisting(MsBuildTaskAttributeListAssociation record)
            => existing
                => existing.MsBuildTaskAttributeId == record.MsBuildTaskAttributeId
                && existing.MsBuildTaskAttributeListId == record.MsBuildTaskAttributeListId;
    }
}
