using Common.EntityFrameworkServices;
using DevOps.Primitives.Strings;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevOps.Primitives.VisualStudio.Projects.EntityFramework.Services
{
    public class MsBuildTargetUpsertService<TDbContext> : UpsertService<TDbContext, MsBuildTarget>
        where TDbContext : VisualStudioProjectsDbContext
    {
        private readonly IUpsertService<TDbContext, AsciiStringReference> _strings;
        private readonly IUpsertUniqueListService<TDbContext, MsBuildTask, MsBuildTaskList, MsBuildTaskListAssociation> _tasks;

        public MsBuildTargetUpsertService(ICacheService<MsBuildTarget> cache, TDbContext database, ILogger<UpsertService<TDbContext, MsBuildTarget>> logger, IUpsertService<TDbContext, AsciiStringReference> strings, IUpsertUniqueListService<TDbContext, MsBuildTask, MsBuildTaskList, MsBuildTaskListAssociation> tasks)
            : base(cache, database, logger, database.MsBuildTargets)
        {
            CacheKey = record => $"{nameof(VisualStudio)}.{nameof(MsBuildTarget)}={record.MsBuildTaskListId}:{record.NameId}:{record.OutputsId}";
            _strings = strings ?? throw new ArgumentNullException(nameof(strings));
            _tasks = tasks ?? throw new ArgumentNullException(nameof(tasks));
        }

        protected override async Task<MsBuildTarget> AssignUpsertedReferences(MsBuildTarget record)
        {
            record.MsBuildTaskList = await _tasks.UpsertAsync(record.MsBuildTaskList);
            record.MsBuildTaskListId = record.MsBuildTaskList?.MsBuildTaskListId ?? record.MsBuildTaskListId;
            record.Name = await _strings.UpsertAsync(record.Name);
            record.NameId = record.Name?.AsciiStringReferenceId ?? record.NameId;
            record.Outputs = await _strings.UpsertAsync(record.Outputs);
            record.OutputsId = record.Outputs?.AsciiStringReferenceId ?? record.OutputsId;
            return record;
        }

        protected override IEnumerable<object> EnumerateReferences(MsBuildTarget record)
        {
            yield return record.MsBuildTaskList;
            yield return record.Name;
            yield return record.Outputs;
        }

        protected override Expression<Func<MsBuildTarget, bool>> FindExisting(MsBuildTarget record)
            => existing
                => existing.MsBuildTaskListId == record.MsBuildTaskListId
                && existing.NameId == record.NameId
                && ((existing.OutputsId == null && record.OutputsId == null) || (existing.OutputsId == record.OutputsId));
    }
}
