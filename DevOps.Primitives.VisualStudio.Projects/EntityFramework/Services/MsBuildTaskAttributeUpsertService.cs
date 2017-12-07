using Common.EntityFrameworkServices;
using DevOps.Primitives.Strings;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevOps.Primitives.VisualStudio.Projects.EntityFramework.Services
{
    public class MsBuildTaskAttributeUpsertService<TDbContext> : UpsertService<TDbContext, MsBuildTaskAttribute>
        where TDbContext : VisualStudioProjectsDbContext
    {
        private readonly IUpsertService<TDbContext, AsciiStringReference> _strings;

        public MsBuildTaskAttributeUpsertService(ICacheService<MsBuildTaskAttribute> cache, TDbContext database, ILogger<UpsertService<TDbContext, MsBuildTaskAttribute>> logger,  IUpsertService<TDbContext, AsciiStringReference> strings)
            : base(cache, database, logger, database.MsBuildTaskAttributes)
        {
            CacheKey = record => $"{nameof(VisualStudio)}.{nameof(MsBuildTaskAttribute)}={record.AttributeId}:{record.ValueId}";
            _strings = strings ?? throw new ArgumentNullException(nameof(strings));
        }

        protected override async Task<MsBuildTaskAttribute> AssignUpsertedReferences(MsBuildTaskAttribute record)
        {
            record.Attribute = await _strings.UpsertAsync(record.Attribute);
            record.AttributeId = record.Attribute?.AsciiStringReferenceId ?? record.AttributeId;
            record.Value = await _strings.UpsertAsync(record.Value);
            record.ValueId = record.Value?.AsciiStringReferenceId ?? record.ValueId;
            return record;
        }

        protected override IEnumerable<object> EnumerateReferences(MsBuildTaskAttribute record)
        {
            yield return record.Attribute;
            yield return record.Value;
        }

        protected override Expression<Func<MsBuildTaskAttribute, bool>> FindExisting(MsBuildTaskAttribute record)
            => existing
                => existing.AttributeId == record.AttributeId
                && existing.ValueId == record.ValueId;
    }
}
