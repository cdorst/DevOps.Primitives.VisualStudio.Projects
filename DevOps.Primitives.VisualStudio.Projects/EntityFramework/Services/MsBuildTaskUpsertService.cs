using Common.EntityFrameworkServices;
using DevOps.Primitives.Strings;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevOps.Primitives.VisualStudio.Projects.EntityFramework.Services
{
    public class MsBuildTaskUpsertService<TDbContext> : UpsertService<TDbContext, MsBuildTask>
        where TDbContext : VisualStudioProjectsDbContext
    {
        private readonly IUpsertUniqueListService<TDbContext, MsBuildTaskAttribute, MsBuildTaskAttributeList, MsBuildTaskAttributeListAssociation> _attributes;
        private readonly IUpsertService<TDbContext, AsciiStringReference> _strings;

        public MsBuildTaskUpsertService(ICacheService<MsBuildTask> cache, TDbContext database, ILogger<UpsertService<TDbContext, MsBuildTask>> logger, IUpsertService<TDbContext, AsciiStringReference> strings, IUpsertUniqueListService<TDbContext, MsBuildTaskAttribute, MsBuildTaskAttributeList, MsBuildTaskAttributeListAssociation> attributes)
            : base(cache, database, logger, database.MsBuildTasks)
        {
            CacheKey = record => $"{nameof(VisualStudio)}.{nameof(MsBuildTask)}={record.ElementId}:{record.MsBuildTaskAttributeListId}";
            _attributes = attributes ?? throw new ArgumentNullException(nameof(attributes));
            _strings = strings ?? throw new ArgumentNullException(nameof(strings));
        }

        protected override async Task<MsBuildTask> AssignUpsertedReferences(MsBuildTask record)
        {
            record.Element = await _strings.UpsertAsync(record.Element);
            record.ElementId = record.Element?.AsciiStringReferenceId ?? record.ElementId;
            record.MsBuildTaskAttributeList = await _attributes.UpsertAsync(record.MsBuildTaskAttributeList);
            record.MsBuildTaskAttributeListId = record.MsBuildTaskAttributeList?.MsBuildTaskAttributeListId ?? record.MsBuildTaskAttributeListId;
            return record;
        }

        protected override IEnumerable<object> EnumerateReferences(MsBuildTask record)
        {
            yield return record.Element;
            yield return record.MsBuildTaskAttributeList;
        }

        protected override Expression<Func<MsBuildTask, bool>> FindExisting(MsBuildTask record)
            => existing
                => existing.ElementId == record.ElementId
                && existing.MsBuildTaskAttributeListId == record.MsBuildTaskAttributeListId;
    }
}
