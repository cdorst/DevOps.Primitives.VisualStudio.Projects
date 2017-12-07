using Common.EntityFrameworkServices;
using DevOps.Primitives.Strings;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevOps.Primitives.VisualStudio.Projects.EntityFramework.Services
{
    public class MsBuildConditionalConstructListUpsertService<TDbContext> : UpsertService<TDbContext, MsBuildConditionalConstructList>
        where TDbContext : VisualStudioProjectsDbContext
    {
        private readonly IUpsertService<TDbContext, AsciiStringReference> _strings;

        public MsBuildConditionalConstructListUpsertService(ICacheService<MsBuildConditionalConstructList> cache, TDbContext database, ILogger<UpsertService<TDbContext, MsBuildConditionalConstructList>> logger, IUpsertService<TDbContext, AsciiStringReference> strings)
            : base(cache, database, logger, database.MsBuildConditionalConstructLists)
        {
            CacheKey = record => $"{nameof(VisualStudio)}.{nameof(MsBuildConditionalConstructList)}={record.ListIdentifierId}";
            _strings = strings ?? throw new ArgumentNullException(nameof(strings));
        }

        protected override async Task<MsBuildConditionalConstructList> AssignUpsertedReferences(MsBuildConditionalConstructList record)
        {
            record.ListIdentifier = await _strings.UpsertAsync(record.ListIdentifier);
            record.ListIdentifierId = record.ListIdentifier?.AsciiStringReferenceId ?? record.ListIdentifierId;
            return record;
        }

        protected override IEnumerable<object> EnumerateReferences(MsBuildConditionalConstructList record)
        {
            yield return record.ListIdentifier;
            yield return record.MsBuildConditionalConstructListAssociations;
        }

        protected override Expression<Func<MsBuildConditionalConstructList, bool>> FindExisting(MsBuildConditionalConstructList record)
            => existing => existing.ListIdentifierId == record.ListIdentifierId;
    }
}
