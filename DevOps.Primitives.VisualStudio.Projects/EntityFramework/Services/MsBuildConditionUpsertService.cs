using Common.EntityFrameworkServices;
using DevOps.Primitives.Strings;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevOps.Primitives.VisualStudio.Projects.EntityFramework.Services
{
    public class MsBuildConditionUpsertService<TDbContext> : UpsertService<TDbContext, MsBuildCondition>
        where TDbContext : VisualStudioProjectsDbContext
    {
        private readonly IUpsertService<TDbContext, AsciiStringReference> _strings;

        public MsBuildConditionUpsertService(ICacheService<MsBuildCondition> cache, TDbContext database, ILogger<UpsertService<TDbContext, MsBuildCondition>> logger, IUpsertService<TDbContext, AsciiStringReference> strings)
            : base(cache, database, logger, database.MsBuildConditions)
        {
            CacheKey = record => $"{nameof(VisualStudio)}.{nameof(MsBuildCondition)}={record.ConditionId}";
            _strings = strings ?? throw new ArgumentNullException(nameof(strings));
        }

        protected override async Task<MsBuildCondition> AssignUpsertedReferences(MsBuildCondition record)
        {
            record.Condition = await _strings.UpsertAsync(record.Condition);
            record.ConditionId = record.Condition?.AsciiStringReferenceId ?? record.ConditionId;
            return record;
        }

        protected override IEnumerable<object> EnumerateReferences(MsBuildCondition record)
        {
            yield return record.Condition;
        }

        protected override Expression<Func<MsBuildCondition, bool>> FindExisting(MsBuildCondition record)
            => existing
                => existing.ConditionId == record.ConditionId;
    }
}
