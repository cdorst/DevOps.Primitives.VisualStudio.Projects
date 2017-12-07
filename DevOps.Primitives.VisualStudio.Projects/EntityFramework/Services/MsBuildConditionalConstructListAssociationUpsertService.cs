using Common.EntityFrameworkServices;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DevOps.Primitives.VisualStudio.Projects.EntityFramework.Services
{
    public class MsBuildConditionalConstructListAssociationUpsertService<TDbContext> : UpsertService<TDbContext, MsBuildConditionalConstructListAssociation>
        where TDbContext : VisualStudioProjectsDbContext
    {
        public MsBuildConditionalConstructListAssociationUpsertService(ICacheService<MsBuildConditionalConstructListAssociation> cache, TDbContext database, ILogger<UpsertService<TDbContext, MsBuildConditionalConstructListAssociation>> logger)
            : base(cache, database, logger, database.MsBuildConditionalConstructListAssociations)
        {
            CacheKey = record => $"{nameof(VisualStudio)}.{nameof(MsBuildConditionalConstructListAssociation)}={record.MsBuildConditionalConstructId}:{record.MsBuildConditionalConstructListId}";
        }

        protected override IEnumerable<object> EnumerateReferences(MsBuildConditionalConstructListAssociation record)
        {
            yield return record.MsBuildConditionalConstruct;
            yield return record.MsBuildConditionalConstructList;
        }

        protected override Expression<Func<MsBuildConditionalConstructListAssociation, bool>> FindExisting(MsBuildConditionalConstructListAssociation record)
            => existing
                => existing.MsBuildConditionalConstructId == record.MsBuildConditionalConstructId
                && existing.MsBuildConditionalConstructListId == record.MsBuildConditionalConstructListId;
    }
}
