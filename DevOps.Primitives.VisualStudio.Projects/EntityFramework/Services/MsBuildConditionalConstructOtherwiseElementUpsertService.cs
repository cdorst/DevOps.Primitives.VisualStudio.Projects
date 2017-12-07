using Common.EntityFrameworkServices;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevOps.Primitives.VisualStudio.Projects.EntityFramework.Services
{
    public class MsBuildConditionalConstructOtherwiseElementUpsertService<TDbContext> : UpsertService<TDbContext, MsBuildConditionalConstructOtherwiseElement>
        where TDbContext : VisualStudioProjectsDbContext
    {
        private readonly IUpsertService<TDbContext, MsBuildConditionalContructItemGroupPropertyGroupSection> _sections;

        public MsBuildConditionalConstructOtherwiseElementUpsertService(ICacheService<MsBuildConditionalConstructOtherwiseElement> cache, TDbContext database, ILogger<UpsertService<TDbContext, MsBuildConditionalConstructOtherwiseElement>> logger, IUpsertService<TDbContext, MsBuildConditionalContructItemGroupPropertyGroupSection> sections)
            : base(cache, database, logger, database.MsBuildConditionalConstructOtherwiseElements)
        {
            CacheKey = record => $"{nameof(VisualStudio)}.{nameof(MsBuildConditionalConstructOtherwiseElement)}={record.MsBuildConditionalContructItemGroupPropertyGroupSectionId}";
            _sections = sections ?? throw new ArgumentNullException(nameof(sections));
        }

        protected override async Task<MsBuildConditionalConstructOtherwiseElement> AssignUpsertedReferences(MsBuildConditionalConstructOtherwiseElement record)
        {
            record.MsBuildConditionalContructItemGroupPropertyGroupSection = await _sections.UpsertAsync(record.MsBuildConditionalContructItemGroupPropertyGroupSection);
            record.MsBuildConditionalContructItemGroupPropertyGroupSectionId = record.MsBuildConditionalContructItemGroupPropertyGroupSection?.MsBuildConditionalContructItemGroupPropertyGroupSectionId ?? record.MsBuildConditionalContructItemGroupPropertyGroupSectionId;
            return record;
        }

        protected override IEnumerable<object> EnumerateReferences(MsBuildConditionalConstructOtherwiseElement record)
        {
            yield return record.MsBuildConditionalContructItemGroupPropertyGroupSection;
        }

        protected override Expression<Func<MsBuildConditionalConstructOtherwiseElement, bool>> FindExisting(MsBuildConditionalConstructOtherwiseElement record)
            => existing
                => existing.MsBuildConditionalContructItemGroupPropertyGroupSectionId == record.MsBuildConditionalContructItemGroupPropertyGroupSectionId;
    }
}
