using Common.EntityFrameworkServices;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevOps.Primitives.VisualStudio.Projects.EntityFramework.Services
{
    public class MsBuildItemGroupUpsertService<TDbContext> : UpsertService<TDbContext, MsBuildItemGroup>
        where TDbContext : VisualStudioProjectsDbContext
    {
        private readonly IUpsertService<TDbContext, MsBuildCondition> _conditions;
        private readonly IUpsertUniqueListService<TDbContext, MsBuildItem, MsBuildItemList, MsBuildItemListAssociation> _items;

        public MsBuildItemGroupUpsertService(ICacheService<MsBuildItemGroup> cache, TDbContext database, ILogger<UpsertService<TDbContext, MsBuildItemGroup>> logger, IUpsertService<TDbContext, MsBuildCondition> conditions, IUpsertUniqueListService<TDbContext, MsBuildItem, MsBuildItemList, MsBuildItemListAssociation> items)
            : base(cache, database, logger, database.MsBuildItemGroups)
        {
            CacheKey = record => $"{nameof(VisualStudio)}.{nameof(MsBuildItemGroup)}={record.MsBuildItemListId}:{record.MsBuildConditionId}";
            _conditions = conditions ?? throw new ArgumentNullException(nameof(conditions));
            _items = items ?? throw new ArgumentNullException(nameof(items));
        }

        protected override async Task<MsBuildItemGroup> AssignUpsertedReferences(MsBuildItemGroup record)
        {
            record.MsBuildCondition = await _conditions.UpsertAsync(record.MsBuildCondition);
            record.MsBuildConditionId = record.MsBuildCondition?.MsBuildConditionId ?? record.MsBuildConditionId;
            record.MsBuildItemList = await _items.UpsertAsync(record.MsBuildItemList);
            record.MsBuildItemListId = record.MsBuildItemList?.MsBuildItemListId ?? record.MsBuildItemListId;
            return record;
        }

        protected override IEnumerable<object> EnumerateReferences(MsBuildItemGroup record)
        {
            yield return record.MsBuildCondition;
            yield return record.MsBuildItemList;
        }

        protected override Expression<Func<MsBuildItemGroup, bool>> FindExisting(MsBuildItemGroup record)
            => existing
                => existing.MsBuildItemListId == record.MsBuildItemListId
                && ((existing.MsBuildConditionId == null && record.MsBuildConditionId == null) || (existing.MsBuildConditionId == record.MsBuildConditionId));
    }
}
