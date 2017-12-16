using Common.EntityFrameworkServices;
using DevOps.Primitives.Strings;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevOps.Primitives.VisualStudio.Projects.EntityFramework.Services
{
    public class MsBuildItemAttributeUpsertService<TDbContext> : UpsertService<TDbContext, MsBuildItemAttribute>
        where TDbContext : VisualStudioProjectsDbContext
    {
        private readonly IUpsertService<TDbContext, AsciiStringReference> _strings;

        public MsBuildItemAttributeUpsertService(ICacheService<MsBuildItemAttribute> cache, TDbContext database, ILogger<UpsertService<TDbContext, MsBuildItemAttribute>> logger, IUpsertService<TDbContext, AsciiStringReference> strings)
            : base(cache, database, logger, database.MsBuildItemAttributes)
        {
            CacheKey = record => $"{nameof(VisualStudio)}.{nameof(MsBuildItemAttribute)}={record.AttributeId}:{record.ValueId}";
            _strings = strings ?? throw new ArgumentNullException(nameof(strings));
        }

        protected override async Task<MsBuildItemAttribute> AssignUpsertedReferences(MsBuildItemAttribute record)
        {
            record.Attribute = await _strings.UpsertAsync(record.Attribute);
            record.AttributeId = record.Attribute?.AsciiStringReferenceId ?? record.AttributeId;
            record.Value = await _strings.UpsertAsync(record.Value);
            record.ValueId = record.Value?.AsciiStringReferenceId ?? record.ValueId;
            return record;
        }

        protected override IEnumerable<object> EnumerateReferences(MsBuildItemAttribute record)
        {
            yield return record.Attribute;
            yield return record.Value;
        }

        protected override Expression<Func<MsBuildItemAttribute, bool>> FindExisting(MsBuildItemAttribute record)
            => existing
                => existing.AttributeId == record.AttributeId
                && existing.ValueId == record.ValueId;
    }
}
