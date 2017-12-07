using Common.EntityFrameworkServices;
using DevOps.Primitives.Strings;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevOps.Primitives.VisualStudio.Projects.EntityFramework.Services
{
    public class MsBuildTaskListUpsertService<TDbContext> : UpsertService<TDbContext, MsBuildTaskList>
        where TDbContext : VisualStudioProjectsDbContext
    {
        private readonly IUpsertService<TDbContext, AsciiStringReference> _strings;

        public MsBuildTaskListUpsertService(ICacheService<MsBuildTaskList> cache, TDbContext database, ILogger<UpsertService<TDbContext, MsBuildTaskList>> logger, IUpsertService<TDbContext, AsciiStringReference> strings)
            : base(cache, database, logger, database.MsBuildTaskLists)
        {
            CacheKey = record => $"{nameof(VisualStudio)}.{nameof(MsBuildTaskList)}={record.ListIdentifierId}";
            _strings = strings ?? throw new ArgumentNullException(nameof(strings));
        }

        protected override async Task<MsBuildTaskList> AssignUpsertedReferences(MsBuildTaskList record)
        {
            record.ListIdentifier = await _strings.UpsertAsync(record.ListIdentifier);
            record.ListIdentifierId = record.ListIdentifier?.AsciiStringReferenceId ?? record.ListIdentifierId;
            return record;
        }

        protected override IEnumerable<object> EnumerateReferences(MsBuildTaskList record)
        {
            yield return record.ListIdentifier;
            yield return record.MsBuildTaskListAssociations;
        }

        protected override Expression<Func<MsBuildTaskList, bool>> FindExisting(MsBuildTaskList record)
            => existing => existing.ListIdentifierId == record.ListIdentifierId;
    }
}
