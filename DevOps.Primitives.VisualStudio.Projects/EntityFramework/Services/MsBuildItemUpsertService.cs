using Common.EntityFrameworkServices;
using DevOps.Primitives.Strings;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevOps.Primitives.VisualStudio.Projects.EntityFramework.Services
{
    public class MsBuildItemUpsertService<TDbContext> : UpsertService<TDbContext, MsBuildItem>
        where TDbContext : VisualStudioProjectsDbContext
    {
        private readonly IUpsertUniqueListService<TDbContext, MsBuildItemAttribute, MsBuildItemAttributeList, MsBuildItemAttributeListAssociation> _attributes;
        private readonly IUpsertService<TDbContext, MsBuildCondition> _conditions;
        private readonly IUpsertService<TDbContext, AsciiStringReference> _strings;

        public MsBuildItemUpsertService(ICacheService<MsBuildItem> cache, TDbContext database, ILogger<UpsertService<TDbContext, MsBuildItem>> logger, IUpsertUniqueListService<TDbContext, MsBuildItemAttribute, MsBuildItemAttributeList, MsBuildItemAttributeListAssociation> attributes, IUpsertService<TDbContext, MsBuildCondition> conditions, IUpsertService<TDbContext, AsciiStringReference> strings)
            : base(cache, database, logger, database.MsBuildItems)
        {
            CacheKey = record => $"{nameof(VisualStudio)}.{nameof(MsBuildItem)}={record.ElementNameId}:{record.MsBuildConditionId}:{record.MsBuildItemAttributeListId}";
            _attributes = attributes ?? throw new ArgumentNullException(nameof(attributes));
            _conditions = conditions ?? throw new ArgumentNullException(nameof(conditions));
            _strings = strings ?? throw new ArgumentNullException(nameof(strings));
        }

        protected override async Task<MsBuildItem> AssignUpsertedReferences(MsBuildItem record)
        {
            record.ElementName = await _strings.UpsertAsync(record.ElementName);
            record.ElementNameId = record.ElementName?.AsciiStringReferenceId ?? record.ElementNameId;
            record.MsBuildCondition = await _conditions.UpsertAsync(record.MsBuildCondition);
            record.MsBuildConditionId = record.MsBuildCondition?.MsBuildConditionId ?? record.MsBuildConditionId;
            record.MsBuildItemAttributeList = await _attributes.UpsertAsync(record.MsBuildItemAttributeList);
            record.MsBuildItemAttributeListId = record.MsBuildItemAttributeList?.MsBuildItemAttributeListId ?? record.MsBuildItemAttributeListId;
            return record;
        }

        protected override IEnumerable<object> EnumerateReferences(MsBuildItem record)
        {
            yield return record.ElementName;
            yield return record.MsBuildCondition;
            yield return record.MsBuildItemAttributeList;
        }

        protected override Expression<Func<MsBuildItem, bool>> FindExisting(MsBuildItem record)
            => existing
                => existing.ElementNameId == record.ElementNameId
                && existing.MsBuildItemAttributeListId == record.MsBuildItemAttributeListId
                && ((existing.MsBuildConditionId == null && record.MsBuildConditionId == null) || (existing.MsBuildConditionId == record.MsBuildConditionId));
    }
}
