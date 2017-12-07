using Common.EntityFrameworkServices;
using DevOps.Primitives.Strings;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevOps.Primitives.VisualStudio.Projects.EntityFramework.Services
{
    public class MsBuildItemGroupListUpsertService<TDbContext> : UpsertService<TDbContext, MsBuildItemGroupList>
        where TDbContext : VisualStudioProjectsDbContext
    {
        private readonly IUpsertService<TDbContext, AsciiStringReference> _strings;

        public MsBuildItemGroupListUpsertService(ICacheService<MsBuildItemGroupList> cache, TDbContext database, ILogger<UpsertService<TDbContext, MsBuildItemGroupList>> logger, IUpsertService<TDbContext, AsciiStringReference> strings)
            : base(cache, database, logger, database.MsBuildItemGroupLists)
        {
            CacheKey = record => $"{nameof(VisualStudio)}.{nameof(MsBuildItemGroupList)}={record.ListIdentifierId}";
            _strings = strings ?? throw new ArgumentNullException(nameof(strings));
        }

        protected override async Task<MsBuildItemGroupList> AssignUpsertedReferences(MsBuildItemGroupList record)
        {
            record.ListIdentifier = await _strings.UpsertAsync(record.ListIdentifier);
            record.ListIdentifierId = record.ListIdentifier?.AsciiStringReferenceId ?? record.ListIdentifierId;
            return record;
        }

        protected override IEnumerable<object> EnumerateReferences(MsBuildItemGroupList record)
        {
            yield return record.ListIdentifier;
            yield return record.MsBuildItemGroupListAssociations;
        }

        protected override Expression<Func<MsBuildItemGroupList, bool>> FindExisting(MsBuildItemGroupList record)
            => existing => existing.ListIdentifierId == record.ListIdentifierId;
    }
}
