using Common.EntityFrameworkServices;
using DevOps.Primitives.Strings;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevOps.Primitives.VisualStudio.Projects.EntityFramework.Services
{
    public class MsBuildTargetListUpsertService<TDbContext> : UpsertService<TDbContext, MsBuildTargetList>
        where TDbContext : VisualStudioProjectsDbContext
    {
        private readonly IUpsertService<TDbContext, AsciiStringReference> _strings;

        public MsBuildTargetListUpsertService(ICacheService<MsBuildTargetList> cache, TDbContext database, ILogger<UpsertService<TDbContext, MsBuildTargetList>> logger, IUpsertService<TDbContext, AsciiStringReference> strings)
            : base(cache, database, logger, database.MsBuildTargetLists)
        {
            CacheKey = record => $"{nameof(VisualStudio)}.{nameof(MsBuildTargetList)}={record.ListIdentifierId}";
            _strings = strings ?? throw new ArgumentNullException(nameof(strings));
        }

        protected override async Task<MsBuildTargetList> AssignUpsertedReferences(MsBuildTargetList record)
        {
            record.ListIdentifier = await _strings.UpsertAsync(record.ListIdentifier);
            record.ListIdentifierId = record.ListIdentifier?.AsciiStringReferenceId ?? record.ListIdentifierId;
            return record;
        }

        protected override IEnumerable<object> EnumerateReferences(MsBuildTargetList record)
        {
            yield return record.ListIdentifier;
            yield return record.MsBuildTargetListAssociations;
        }

        protected override Expression<Func<MsBuildTargetList, bool>> FindExisting(MsBuildTargetList record)
            => existing => existing.ListIdentifierId == record.ListIdentifierId;
    }
}
