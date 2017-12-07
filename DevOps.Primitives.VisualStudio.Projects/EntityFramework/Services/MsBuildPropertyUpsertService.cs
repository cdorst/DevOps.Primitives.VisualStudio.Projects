using Common.EntityFrameworkServices;
using DevOps.Primitives.Strings;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevOps.Primitives.VisualStudio.Projects.EntityFramework.Services
{
    public class MsBuildPropertyUpsertService<TDbContext> : UpsertService<TDbContext, MsBuildProperty>
        where TDbContext : VisualStudioProjectsDbContext
    {
        private readonly IUpsertService<TDbContext, MsBuildCondition> _conditions;
        private readonly IUpsertService<TDbContext, AsciiStringReference> _strings;

        public MsBuildPropertyUpsertService(ICacheService<MsBuildProperty> cache, TDbContext database, ILogger<UpsertService<TDbContext, MsBuildProperty>> logger, IUpsertService<TDbContext, MsBuildCondition> conditions, IUpsertService<TDbContext, AsciiStringReference> strings)
            : base(cache, database, logger, database.MsBuildProperties)
        {
            CacheKey = record => $"{nameof(VisualStudio)}.{nameof(MsBuildProperty)}={record.ElementNameId}:{record.ElementValueId}:{record.MsBuildConditionId}";
            _conditions = conditions ?? throw new ArgumentNullException(nameof(conditions));
            _strings = strings ?? throw new ArgumentNullException(nameof(strings));
        }

        protected override async Task<MsBuildProperty> AssignUpsertedReferences(MsBuildProperty record)
        {
            record.ElementName = await _strings.UpsertAsync(record.ElementName);
            record.ElementNameId = record.ElementName?.AsciiStringReferenceId ?? record.ElementNameId;
            record.ElementValue = await _strings.UpsertAsync(record.ElementValue);
            record.ElementValueId = record.ElementValue?.AsciiStringReferenceId ?? record.ElementValueId;
            record.MsBuildCondition = await _conditions.UpsertAsync(record.MsBuildCondition);
            record.MsBuildConditionId = record.MsBuildCondition?.MsBuildConditionId ?? record.MsBuildConditionId;
            return record;
        }

        protected override IEnumerable<object> EnumerateReferences(MsBuildProperty record)
        {
            yield return record.ElementName;
            yield return record.ElementValue;
            yield return record.MsBuildCondition;
        }

        protected override Expression<Func<MsBuildProperty, bool>> FindExisting(MsBuildProperty record)
            => existing
                => existing.ElementNameId == record.ElementNameId
                && existing.ElementValueId == record.ElementValueId
                && ((existing.MsBuildConditionId == null && record.MsBuildConditionId == null) || (existing.MsBuildConditionId == record.MsBuildConditionId));
    }
}
