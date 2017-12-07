using Common.EntityFrameworkServices;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevOps.Primitives.VisualStudio.Projects.EntityFramework.Services
{
    public class MsBuildConditionalConstructUpsertService<TDbContext> : UpsertService<TDbContext, MsBuildConditionalConstruct>
        where TDbContext : VisualStudioProjectsDbContext
    {
        private readonly IUpsertService<TDbContext, MsBuildConditionalConstructOtherwiseElement> _otherwise;
        private readonly IUpsertUniqueListService<TDbContext, MsBuildConditionalConstructWhenElement, MsBuildConditionalConstructWhenElementList, MsBuildConditionalConstructWhenElementListAssociation> _whens;

        public MsBuildConditionalConstructUpsertService(ICacheService<MsBuildConditionalConstruct> cache, TDbContext database, ILogger<UpsertService<TDbContext, MsBuildConditionalConstruct>> logger, IUpsertService<TDbContext, MsBuildConditionalConstructOtherwiseElement> otherwise, IUpsertUniqueListService<TDbContext, MsBuildConditionalConstructWhenElement, MsBuildConditionalConstructWhenElementList, MsBuildConditionalConstructWhenElementListAssociation> whens)
            : base(cache, database, logger, database.MsBuildConditionalConstructs)
        {
            CacheKey = record => $"{nameof(VisualStudio)}.{nameof(MsBuildConditionalConstruct)}={record.MsBuildConditionalConstructOtherwiseElementId}:{record.MsBuildConditionalConstructWhenElementListId}";
            _otherwise = otherwise ?? throw new ArgumentNullException(nameof(otherwise));
            _whens = whens ?? throw new ArgumentNullException(nameof(whens));
        }

        protected override async Task<MsBuildConditionalConstruct> AssignUpsertedReferences(MsBuildConditionalConstruct record)
        {
            record.MsBuildConditionalConstructOtherwiseElement = await _otherwise.UpsertAsync(record.MsBuildConditionalConstructOtherwiseElement);
            record.MsBuildConditionalConstructOtherwiseElementId = record.MsBuildConditionalConstructOtherwiseElement?.MsBuildConditionalConstructOtherwiseElementId ?? record.MsBuildConditionalConstructOtherwiseElementId;
            record.MsBuildConditionalConstructWhenElementList = await _whens.UpsertAsync(record.MsBuildConditionalConstructWhenElementList);
            record.MsBuildConditionalConstructWhenElementListId = record.MsBuildConditionalConstructWhenElementList?.MsBuildConditionalConstructWhenElementListId ?? record.MsBuildConditionalConstructWhenElementListId;
            return record;
        }

        protected override IEnumerable<object> EnumerateReferences(MsBuildConditionalConstruct record)
        {
            yield return record.MsBuildConditionalConstructOtherwiseElement;
            yield return record.MsBuildConditionalConstructWhenElementList;
        }

        protected override Expression<Func<MsBuildConditionalConstruct, bool>> FindExisting(MsBuildConditionalConstruct record)
            => existing
                => ((existing.MsBuildConditionalConstructOtherwiseElementId == null && record.MsBuildConditionalConstructOtherwiseElementId == null) || (existing.MsBuildConditionalConstructOtherwiseElementId == record.MsBuildConditionalConstructOtherwiseElementId))
                && existing.MsBuildConditionalConstructWhenElementListId == record.MsBuildConditionalConstructWhenElementListId;
    }
}
