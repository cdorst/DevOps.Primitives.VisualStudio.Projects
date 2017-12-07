using Common.EntityFrameworkServices;
using DevOps.Primitives.Strings;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevOps.Primitives.VisualStudio.Projects.EntityFramework.Services
{
    public class MsBuildPropertyListUpsertService<TDbContext> : UpsertService<TDbContext, MsBuildPropertyList>
        where TDbContext : VisualStudioProjectsDbContext
    {
        private readonly IUpsertService<TDbContext, AsciiStringReference> _strings;

        public MsBuildPropertyListUpsertService(ICacheService<MsBuildPropertyList> cache, TDbContext database, ILogger<UpsertService<TDbContext, MsBuildPropertyList>> logger, IUpsertService<TDbContext, AsciiStringReference> strings)
            : base(cache, database, logger, database.MsBuildPropertyLists)
        {
            CacheKey = record => $"{nameof(VisualStudio)}.{nameof(MsBuildPropertyList)}={record.ListIdentifierId}";
            _strings = strings ?? throw new ArgumentNullException(nameof(strings));
        }

        protected override async Task<MsBuildPropertyList> AssignUpsertedReferences(MsBuildPropertyList record)
        {
            record.ListIdentifier = await _strings.UpsertAsync(record.ListIdentifier);
            record.ListIdentifierId = record.ListIdentifier?.AsciiStringReferenceId ?? record.ListIdentifierId;
            return record;
        }

        protected override IEnumerable<object> EnumerateReferences(MsBuildPropertyList record)
        {
            yield return record.ListIdentifier;
            yield return record.MsBuildPropertyListAssociations;
        }

        protected override Expression<Func<MsBuildPropertyList, bool>> FindExisting(MsBuildPropertyList record)
            => existing => existing.ListIdentifierId == record.ListIdentifierId;
    }
}
