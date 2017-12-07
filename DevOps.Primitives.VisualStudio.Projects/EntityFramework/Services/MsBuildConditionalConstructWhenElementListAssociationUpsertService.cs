using Common.EntityFrameworkServices;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DevOps.Primitives.VisualStudio.Projects.EntityFramework.Services
{
    public class MsBuildConditionalConstructWhenElementListAssociationUpsertService<TDbContext> : UpsertService<TDbContext, MsBuildConditionalConstructWhenElementListAssociation>
        where TDbContext : VisualStudioProjectsDbContext
    {
        public MsBuildConditionalConstructWhenElementListAssociationUpsertService(ICacheService<MsBuildConditionalConstructWhenElementListAssociation> cache, TDbContext database, ILogger<UpsertService<TDbContext, MsBuildConditionalConstructWhenElementListAssociation>> logger)
            : base(cache, database, logger, database.MsBuildConditionalConstructWhenElementListAssociations)
        {
            CacheKey = record => $"{nameof(VisualStudio)}.{nameof(MsBuildConditionalConstructWhenElementListAssociation)}={record.MsBuildConditionalConstructWhenElementId}:{record.MsBuildConditionalConstructWhenElementListId}";
        }

        protected override IEnumerable<object> EnumerateReferences(MsBuildConditionalConstructWhenElementListAssociation record)
        {
            yield return record.MsBuildConditionalConstructWhenElement;
            yield return record.MsBuildConditionalConstructWhenElementList;
        }

        protected override Expression<Func<MsBuildConditionalConstructWhenElementListAssociation, bool>> FindExisting(MsBuildConditionalConstructWhenElementListAssociation record)
            => existing
                => existing.MsBuildConditionalConstructWhenElementId == record.MsBuildConditionalConstructWhenElementId
                && existing.MsBuildConditionalConstructWhenElementListId == record.MsBuildConditionalConstructWhenElementListId;
    }
}
