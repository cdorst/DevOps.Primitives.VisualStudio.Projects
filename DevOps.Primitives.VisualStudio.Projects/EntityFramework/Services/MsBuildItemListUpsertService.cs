using Common.EntityFrameworkServices;
using DevOps.Primitives.Strings;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevOps.Primitives.VisualStudio.Projects.EntityFramework.Services
{
    public class MsBuildItemListUpsertService<TDbContext> : UpsertService<TDbContext, MsBuildItemList>
        where TDbContext : VisualStudioProjectsDbContext
    {
        private readonly IUpsertService<TDbContext, AsciiStringReference> _strings;

        public MsBuildItemListUpsertService(ICacheService<MsBuildItemList> cache, TDbContext database, ILogger<UpsertService<TDbContext, MsBuildItemList>> logger, IUpsertService<TDbContext, AsciiStringReference> strings)
            : base(cache, database, logger, database.MsBuildItemLists)
        {
            CacheKey = record => $"{nameof(VisualStudio)}.{nameof(MsBuildItemList)}={record.ListIdentifierId}";
            _strings = strings ?? throw new ArgumentNullException(nameof(strings));
        }

        protected override async Task<MsBuildItemList> AssignUpsertedReferences(MsBuildItemList record)
        {
            record.ListIdentifier = await _strings.UpsertAsync(record.ListIdentifier);
            record.ListIdentifierId = record.ListIdentifier?.AsciiStringReferenceId ?? record.ListIdentifierId;
            return record;
        }

        protected override IEnumerable<object> EnumerateReferences(MsBuildItemList record)
        {
            yield return record.ListIdentifier;
            yield return record.MsBuildItemListAssociations;
        }

        protected override Expression<Func<MsBuildItemList, bool>> FindExisting(MsBuildItemList record)
            => existing => existing.ListIdentifierId == record.ListIdentifierId;
    }
}
