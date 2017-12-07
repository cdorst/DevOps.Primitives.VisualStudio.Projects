using Common.EntityFrameworkServices;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DevOps.Primitives.VisualStudio.Projects.EntityFramework.Services
{
    public class MsBuildTaskListAssociationUpsertService<TDbContext> : UpsertService<TDbContext, MsBuildTaskListAssociation>
        where TDbContext : VisualStudioProjectsDbContext
    {
        public MsBuildTaskListAssociationUpsertService(ICacheService<MsBuildTaskListAssociation> cache, TDbContext database, ILogger<UpsertService<TDbContext, MsBuildTaskListAssociation>> logger)
            : base(cache, database, logger, database.MsBuildTaskListAssociations)
        {
            CacheKey = record => $"{nameof(VisualStudio)}.{nameof(MsBuildTaskListAssociation)}={record.MsBuildTaskId}:{record.MsBuildTaskListId}";
        }

        protected override IEnumerable<object> EnumerateReferences(MsBuildTaskListAssociation record)
        {
            yield return record.MsBuildTask;
            yield return record.MsBuildTaskList;
        }

        protected override Expression<Func<MsBuildTaskListAssociation, bool>> FindExisting(MsBuildTaskListAssociation record)
            => existing
                => existing.MsBuildTaskId == record.MsBuildTaskId
                && existing.MsBuildTaskListId == record.MsBuildTaskListId;
    }
}
