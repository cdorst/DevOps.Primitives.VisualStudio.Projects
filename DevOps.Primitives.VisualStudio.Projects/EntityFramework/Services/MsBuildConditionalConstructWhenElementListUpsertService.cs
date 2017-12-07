using Common.EntityFrameworkServices;
using DevOps.Primitives.Strings;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevOps.Primitives.VisualStudio.Projects.EntityFramework.Services
{
    public class MsBuildConditionalConstructWhenElementListUpsertService<TDbContext> : UpsertService<TDbContext, MsBuildConditionalConstructWhenElementList>
        where TDbContext : VisualStudioProjectsDbContext
    {
        private readonly IUpsertService<TDbContext, AsciiStringReference> _strings;

        public MsBuildConditionalConstructWhenElementListUpsertService(ICacheService<MsBuildConditionalConstructWhenElementList> cache, TDbContext database, ILogger<UpsertService<TDbContext, MsBuildConditionalConstructWhenElementList>> logger, IUpsertService<TDbContext, AsciiStringReference> strings)
            : base(cache, database, logger, database.MsBuildConditionalConstructWhenElementLists)
        {
            CacheKey = record => $"{nameof(VisualStudio)}.{nameof(MsBuildConditionalConstructWhenElementList)}={record.ListIdentifierId}";
            _strings = strings ?? throw new ArgumentNullException(nameof(strings));
        }

        protected override async Task<MsBuildConditionalConstructWhenElementList> AssignUpsertedReferences(MsBuildConditionalConstructWhenElementList record)
        {
            record.ListIdentifier = await _strings.UpsertAsync(record.ListIdentifier);
            record.ListIdentifierId = record.ListIdentifier?.AsciiStringReferenceId ?? record.ListIdentifierId;
            return record;
        }

        protected override IEnumerable<object> EnumerateReferences(MsBuildConditionalConstructWhenElementList record)
        {
            yield return record.ListIdentifier;
            yield return record.MsBuildConditionalConstructWhenElementListAssociations;
        }

        protected override Expression<Func<MsBuildConditionalConstructWhenElementList, bool>> FindExisting(MsBuildConditionalConstructWhenElementList record)
            => existing => existing.ListIdentifierId == record.ListIdentifierId;
    }
}
