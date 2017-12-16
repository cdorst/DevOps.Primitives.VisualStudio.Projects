using Common.EntityFrameworkServices;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DevOps.Primitives.VisualStudio.Projects.EntityFramework.Services
{
    public class MsBuildItemAttributeListAssociationUpsertService<TDbContext> : UpsertService<TDbContext, MsBuildItemAttributeListAssociation>
        where TDbContext : VisualStudioProjectsDbContext
    {
        public MsBuildItemAttributeListAssociationUpsertService(ICacheService<MsBuildItemAttributeListAssociation> cache, TDbContext database, ILogger<UpsertService<TDbContext, MsBuildItemAttributeListAssociation>> logger)
            : base(cache, database, logger, database.MsBuildItemAttributeListAssociations)
        {
            CacheKey = record => $"{nameof(VisualStudio)}.{nameof(MsBuildItemAttributeListAssociation)}={record.MsBuildItemAttributeId}:{record.MsBuildItemAttributeListId}";
        }

        protected override IEnumerable<object> EnumerateReferences(MsBuildItemAttributeListAssociation record)
        {
            yield return record.MsBuildItemAttribute;
            yield return record.MsBuildItemAttributeList;
        }

        protected override Expression<Func<MsBuildItemAttributeListAssociation, bool>> FindExisting(MsBuildItemAttributeListAssociation record)
            => existing
                => existing.MsBuildItemAttributeId == record.MsBuildItemAttributeId
                && existing.MsBuildItemAttributeListId == record.MsBuildItemAttributeListId;
    }
}
