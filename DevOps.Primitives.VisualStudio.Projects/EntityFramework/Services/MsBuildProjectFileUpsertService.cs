using Common.EntityFrameworkServices;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevOps.Primitives.VisualStudio.Projects.EntityFramework.Services
{
    public class MsBuildProjectFileUpsertService<TDbContext> : UpsertService<TDbContext, MsBuildProjectFile>
        where TDbContext : VisualStudioProjectsDbContext
    {
        private readonly IUpsertService<TDbContext, MsBuildConditionalContructItemGroupPropertyGroupSection> _sections;
        private readonly IUpsertUniqueListService<TDbContext, MsBuildTarget, MsBuildTargetList, MsBuildTargetListAssociation> _targets;

        public MsBuildProjectFileUpsertService(ICacheService<MsBuildProjectFile> cache, TDbContext database, ILogger<UpsertService<TDbContext, MsBuildProjectFile>> logger, IUpsertService<TDbContext, MsBuildConditionalContructItemGroupPropertyGroupSection> sections, IUpsertUniqueListService<TDbContext, MsBuildTarget, MsBuildTargetList, MsBuildTargetListAssociation> targets)
            : base(cache, database, logger, database.MsBuildProjectFiles)
        {
            CacheKey = record => $"{nameof(VisualStudio)}.{nameof(MsBuildProjectFile)}={record.ProjectType}:{record.MsBuildConditionalContructItemGroupPropertyGroupSectionId}:{record.MsBuildTargetListId}";
            _sections = sections ?? throw new ArgumentNullException(nameof(sections));
            _targets = targets ?? throw new ArgumentNullException(nameof(targets));
        }

        protected override async Task<MsBuildProjectFile> AssignUpsertedReferences(MsBuildProjectFile record)
        {
            record.MsBuildConditionalContructItemGroupPropertyGroupSection = await _sections.UpsertAsync(record.MsBuildConditionalContructItemGroupPropertyGroupSection);
            record.MsBuildConditionalContructItemGroupPropertyGroupSectionId = record.MsBuildConditionalContructItemGroupPropertyGroupSection?.MsBuildConditionalContructItemGroupPropertyGroupSectionId ?? record.MsBuildConditionalContructItemGroupPropertyGroupSectionId;
            record.MsBuildTargetList = await _targets.UpsertAsync(record.MsBuildTargetList);
            record.MsBuildTargetListId = record.MsBuildTargetList?.MsBuildTargetListId ?? record.MsBuildTargetListId;
            return record;
        }

        protected override IEnumerable<object> EnumerateReferences(MsBuildProjectFile record)
        {
            yield return record.MsBuildConditionalContructItemGroupPropertyGroupSection;
            yield return record.MsBuildTargetList;
        }

        protected override Expression<Func<MsBuildProjectFile, bool>> FindExisting(MsBuildProjectFile record)
            => existing
                => existing.ProjectType == record.ProjectType
                && existing.MsBuildConditionalContructItemGroupPropertyGroupSectionId == record.MsBuildConditionalContructItemGroupPropertyGroupSectionId
                && ((existing.MsBuildTargetListId == null && record.MsBuildTargetListId == null) || (existing.MsBuildTargetListId == record.MsBuildTargetListId));
    }
}
