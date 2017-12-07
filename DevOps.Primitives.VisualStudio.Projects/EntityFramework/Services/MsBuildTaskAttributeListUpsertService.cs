using Common.EntityFrameworkServices;
using DevOps.Primitives.Strings;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevOps.Primitives.VisualStudio.Projects.EntityFramework.Services
{
    public class MsBuildTaskAttributeListUpsertService<TDbContext> : UpsertService<TDbContext, MsBuildTaskAttributeList>
        where TDbContext : VisualStudioProjectsDbContext
    {
        private readonly IUpsertService<TDbContext, AsciiStringReference> _strings;

        public MsBuildTaskAttributeListUpsertService(ICacheService<MsBuildTaskAttributeList> cache, TDbContext database, ILogger<UpsertService<TDbContext, MsBuildTaskAttributeList>> logger, IUpsertService<TDbContext, AsciiStringReference> strings)
            : base(cache, database, logger, database.MsBuildTaskAttributeLists)
        {
            CacheKey = record => $"{nameof(VisualStudio)}.{nameof(MsBuildTaskAttributeList)}={record.ListIdentifierId}";
            _strings = strings ?? throw new ArgumentNullException(nameof(strings));
        }

        protected override async Task<MsBuildTaskAttributeList> AssignUpsertedReferences(MsBuildTaskAttributeList record)
        {
            record.ListIdentifier = await _strings.UpsertAsync(record.ListIdentifier);
            record.ListIdentifierId = record.ListIdentifier?.AsciiStringReferenceId ?? record.ListIdentifierId;
            return record;
        }

        protected override IEnumerable<object> EnumerateReferences(MsBuildTaskAttributeList record)
        {
            yield return record.ListIdentifier;
            yield return record.MsBuildTaskAttributeListAssociations;
        }

        protected override Expression<Func<MsBuildTaskAttributeList, bool>> FindExisting(MsBuildTaskAttributeList record)
            => existing => existing.ListIdentifierId == record.ListIdentifierId;
    }
}
