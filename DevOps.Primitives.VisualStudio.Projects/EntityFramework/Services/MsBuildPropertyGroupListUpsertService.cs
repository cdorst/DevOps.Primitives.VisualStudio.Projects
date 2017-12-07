using Common.EntityFrameworkServices;
using DevOps.Primitives.Strings;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevOps.Primitives.VisualStudio.Projects.EntityFramework.Services
{
    public class MsBuildPropertyGroupListUpsertService<TDbContext> : UpsertService<TDbContext, MsBuildPropertyGroupList>
        where TDbContext : VisualStudioProjectsDbContext
    {
        private readonly IUpsertService<TDbContext, AsciiStringReference> _strings;

        public MsBuildPropertyGroupListUpsertService(ICacheService<MsBuildPropertyGroupList> cache, TDbContext database, ILogger<UpsertService<TDbContext, MsBuildPropertyGroupList>> logger, IUpsertService<TDbContext, AsciiStringReference> strings)
            : base(cache, database, logger, database.MsBuildPropertyGroupLists)
        {
            CacheKey = record => $"{nameof(VisualStudio)}.{nameof(MsBuildPropertyGroupList)}={record.ListIdentifierId}";
            _strings = strings ?? throw new ArgumentNullException(nameof(strings));
        }

        protected override async Task<MsBuildPropertyGroupList> AssignUpsertedReferences(MsBuildPropertyGroupList record)
        {
            record.ListIdentifier = await _strings.UpsertAsync(record.ListIdentifier);
            record.ListIdentifierId = record.ListIdentifier?.AsciiStringReferenceId ?? record.ListIdentifierId;
            return record;
        }

        protected override IEnumerable<object> EnumerateReferences(MsBuildPropertyGroupList record)
        {
            yield return record.ListIdentifier;
            yield return record.MsBuildPropertyGroupListAssociations;
        }

        protected override Expression<Func<MsBuildPropertyGroupList, bool>> FindExisting(MsBuildPropertyGroupList record)
            => existing => existing.ListIdentifierId == record.ListIdentifierId;
    }
}
