using Common.EntityFrameworkServices;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevOps.Primitives.VisualStudio.Projects.EntityFramework.Services
{
    public class MsBuildPropertyGroupUpsertService<TDbContext> : UpsertService<TDbContext, MsBuildPropertyGroup>
        where TDbContext : VisualStudioProjectsDbContext
    {
        private readonly IUpsertService<TDbContext, MsBuildCondition> _conditions;
        private readonly IUpsertUniqueListService<TDbContext, MsBuildProperty, MsBuildPropertyList, MsBuildPropertyListAssociation> _properties;

        public MsBuildPropertyGroupUpsertService(ICacheService<MsBuildPropertyGroup> cache, TDbContext database, ILogger<UpsertService<TDbContext, MsBuildPropertyGroup>> logger, IUpsertService<TDbContext, MsBuildCondition> conditions, IUpsertUniqueListService<TDbContext, MsBuildProperty, MsBuildPropertyList, MsBuildPropertyListAssociation> properties)
            : base(cache, database, logger, database.MsBuildPropertyGroups)
        {
            CacheKey = record => $"{nameof(VisualStudio)}.{nameof(MsBuildPropertyGroup)}={record.MsBuildPropertyListId}:{record.MsBuildConditionId}";
            _conditions = conditions ?? throw new ArgumentNullException(nameof(conditions));
            _properties = properties ?? throw new ArgumentNullException(nameof(properties));
        }

        protected override async Task<MsBuildPropertyGroup> AssignUpsertedReferences(MsBuildPropertyGroup record)
        {
            record.MsBuildCondition = await _conditions.UpsertAsync(record.MsBuildCondition);
            record.MsBuildConditionId = record.MsBuildCondition?.MsBuildConditionId ?? record.MsBuildConditionId;
            record.MsBuildPropertyList = await _properties.UpsertAsync(record.MsBuildPropertyList);
            record.MsBuildPropertyListId = record.MsBuildPropertyList?.MsBuildPropertyListId ?? record.MsBuildPropertyListId;
            return record;
        }

        protected override IEnumerable<object> EnumerateReferences(MsBuildPropertyGroup record)
        {
            yield return record.MsBuildCondition;
            yield return record.MsBuildPropertyList;
        }

        protected override Expression<Func<MsBuildPropertyGroup, bool>> FindExisting(MsBuildPropertyGroup record)
            => existing
                => existing.MsBuildPropertyListId == record.MsBuildPropertyListId
                && ((existing.MsBuildConditionId == null && record.MsBuildConditionId == null) || (existing.MsBuildConditionId == record.MsBuildConditionId));
    }
}
