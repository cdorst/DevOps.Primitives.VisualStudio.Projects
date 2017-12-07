using Common.EntityFrameworkServices;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevOps.Primitives.VisualStudio.Projects.EntityFramework.Services
{
    public class MsBuildConditionalContructItemGroupPropertyGroupSectionUpsertService<TDbContext> : UpsertService<TDbContext, MsBuildConditionalContructItemGroupPropertyGroupSection>
        where TDbContext : VisualStudioProjectsDbContext
    {
        private readonly IUpsertUniqueListService<TDbContext, MsBuildConditionalConstruct, MsBuildConditionalConstructList, MsBuildConditionalConstructListAssociation> _conditionals;
        private readonly IUpsertUniqueListService<TDbContext, MsBuildItemGroup, MsBuildItemGroupList, MsBuildItemGroupListAssociation> _itemGroups;
        private readonly IUpsertUniqueListService<TDbContext, MsBuildPropertyGroup, MsBuildPropertyGroupList, MsBuildPropertyGroupListAssociation> _propertyGroups;

        public MsBuildConditionalContructItemGroupPropertyGroupSectionUpsertService(ICacheService<MsBuildConditionalContructItemGroupPropertyGroupSection> cache, TDbContext database, ILogger<UpsertService<TDbContext, MsBuildConditionalContructItemGroupPropertyGroupSection>> logger, IUpsertUniqueListService<TDbContext, MsBuildConditionalConstruct, MsBuildConditionalConstructList, MsBuildConditionalConstructListAssociation> conditionals, IUpsertUniqueListService<TDbContext, MsBuildItemGroup, MsBuildItemGroupList, MsBuildItemGroupListAssociation> itemGroups, IUpsertUniqueListService<TDbContext, MsBuildPropertyGroup, MsBuildPropertyGroupList, MsBuildPropertyGroupListAssociation> propertyGroups)
            : base(cache, database, logger, database.MsBuildConditionalContructItemGroupPropertyGroupSections)
        {
            CacheKey = record => $"{nameof(VisualStudio)}.{nameof(MsBuildConditionalContructItemGroupPropertyGroupSection)}={record.MsBuildConditionalConstructListId}:{record.MsBuildItemGroupListId}:{record.MsBuildPropertyGroupListId}";
            _conditionals = conditionals ?? throw new ArgumentNullException(nameof(conditionals));
            _itemGroups = itemGroups ?? throw new ArgumentNullException(nameof(itemGroups));
            _propertyGroups = propertyGroups ?? throw new ArgumentNullException(nameof(propertyGroups));
        }

        protected override async Task<MsBuildConditionalContructItemGroupPropertyGroupSection> AssignUpsertedReferences(MsBuildConditionalContructItemGroupPropertyGroupSection record)
        {
            record.MsBuildConditionalConstructList = await _conditionals.UpsertAsync(record.MsBuildConditionalConstructList);
            record.MsBuildConditionalConstructListId = record.MsBuildConditionalConstructList?.MsBuildConditionalConstructListId ?? record.MsBuildConditionalConstructListId;
            return record;
        }

        protected override IEnumerable<object> EnumerateReferences(MsBuildConditionalContructItemGroupPropertyGroupSection record)
        {
            yield return record.MsBuildConditionalConstructList;
            yield return record.MsBuildItemGroupList;
            yield return record.MsBuildPropertyGroupList;
        }

        protected override Expression<Func<MsBuildConditionalContructItemGroupPropertyGroupSection, bool>> FindExisting(MsBuildConditionalContructItemGroupPropertyGroupSection record)
            => existing
                => ((existing.MsBuildConditionalConstructListId == null && record.MsBuildConditionalConstructListId == null) || (existing.MsBuildConditionalConstructListId == record.MsBuildConditionalConstructListId))
                && ((existing.MsBuildItemGroupListId == null && record.MsBuildItemGroupListId == null) || (existing.MsBuildItemGroupListId == record.MsBuildItemGroupListId))
                && ((existing.MsBuildPropertyGroupListId == null && record.MsBuildPropertyGroupListId == null) || (existing.MsBuildPropertyGroupListId == record.MsBuildPropertyGroupListId));
    }
}
