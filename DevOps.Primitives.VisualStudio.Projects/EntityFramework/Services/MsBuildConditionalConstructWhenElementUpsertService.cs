using Common.EntityFrameworkServices;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevOps.Primitives.VisualStudio.Projects.EntityFramework.Services
{
    public class MsBuildConditionalConstructWhenElementUpsertService<TDbContext> : UpsertService<TDbContext, MsBuildConditionalConstructWhenElement>
        where TDbContext : VisualStudioProjectsDbContext
    {
        private readonly IUpsertService<TDbContext, MsBuildCondition> _conditions;
        private readonly IUpsertService<TDbContext, MsBuildConditionalContructItemGroupPropertyGroupSection> _sections;

        public MsBuildConditionalConstructWhenElementUpsertService(ICacheService<MsBuildConditionalConstructWhenElement> cache, TDbContext database, ILogger<UpsertService<TDbContext, MsBuildConditionalConstructWhenElement>> logger, IUpsertService<TDbContext, MsBuildCondition> conditions, IUpsertService<TDbContext, MsBuildConditionalContructItemGroupPropertyGroupSection> sections)
            : base(cache, database, logger, database.MsBuildConditionalConstructWhenElements)
        {
            CacheKey = record => $"{nameof(VisualStudio)}.{nameof(MsBuildConditionalConstructWhenElement)}={record.MsBuildConditionalContructItemGroupPropertyGroupSectionId}";
            _conditions = conditions ?? throw new ArgumentNullException(nameof(conditions));
            _sections = sections ?? throw new ArgumentNullException(nameof(sections));
        }

        protected override async Task<MsBuildConditionalConstructWhenElement> AssignUpsertedReferences(MsBuildConditionalConstructWhenElement record)
        {
            record.MsBuildCondition = await _conditions.UpsertAsync(record.MsBuildCondition);
            record.MsBuildConditionId = record.MsBuildCondition?.MsBuildConditionId ?? record.MsBuildConditionId;
            record.MsBuildConditionalContructItemGroupPropertyGroupSection = await _sections.UpsertAsync(record.MsBuildConditionalContructItemGroupPropertyGroupSection);
            record.MsBuildConditionalContructItemGroupPropertyGroupSectionId = record.MsBuildConditionalContructItemGroupPropertyGroupSection?.MsBuildConditionalContructItemGroupPropertyGroupSectionId ?? record.MsBuildConditionalContructItemGroupPropertyGroupSectionId;
            return record;
        }

        protected override IEnumerable<object> EnumerateReferences(MsBuildConditionalConstructWhenElement record)
        {
            yield return record.MsBuildCondition;
            yield return record.MsBuildConditionalContructItemGroupPropertyGroupSection;
        }

        protected override Expression<Func<MsBuildConditionalConstructWhenElement, bool>> FindExisting(MsBuildConditionalConstructWhenElement record)
            => existing
                => existing.MsBuildConditionId == record.MsBuildConditionId
                && existing.MsBuildConditionalContructItemGroupPropertyGroupSectionId == record.MsBuildConditionalContructItemGroupPropertyGroupSectionId;
    }
}
