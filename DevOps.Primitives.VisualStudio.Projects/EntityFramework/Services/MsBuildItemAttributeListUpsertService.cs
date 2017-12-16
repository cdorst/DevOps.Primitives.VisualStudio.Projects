using Common.EntityFrameworkServices;
using DevOps.Primitives.Strings;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevOps.Primitives.VisualStudio.Projects.EntityFramework.Services
{
    public class MsBuildItemAttributeListUpsertService<TDbContext> : UpsertService<TDbContext, MsBuildItemAttributeList>
        where TDbContext : VisualStudioProjectsDbContext
    {
        private readonly IUpsertService<TDbContext, AsciiStringReference> _strings;

        public MsBuildItemAttributeListUpsertService(ICacheService<MsBuildItemAttributeList> cache, TDbContext database, ILogger<UpsertService<TDbContext, MsBuildItemAttributeList>> logger, IUpsertService<TDbContext, AsciiStringReference> strings)
            : base(cache, database, logger, database.MsBuildItemAttributeLists)
        {
            CacheKey = record => $"{nameof(VisualStudio)}.{nameof(MsBuildItemAttributeList)}={record.ListIdentifierId}";
            _strings = strings ?? throw new ArgumentNullException(nameof(strings));
        }

        protected override async Task<MsBuildItemAttributeList> AssignUpsertedReferences(MsBuildItemAttributeList record)
        {
            record.ListIdentifier = await _strings.UpsertAsync(record.ListIdentifier);
            record.ListIdentifierId = record.ListIdentifier?.AsciiStringReferenceId ?? record.ListIdentifierId;
            return record;
        }

        protected override IEnumerable<object> EnumerateReferences(MsBuildItemAttributeList record)
        {
            yield return record.ListIdentifier;
            yield return record.MsBuildItemAttributeListAssociations;
        }

        protected override Expression<Func<MsBuildItemAttributeList, bool>> FindExisting(MsBuildItemAttributeList record)
            => existing => existing.ListIdentifierId == record.ListIdentifierId;
    }
}
