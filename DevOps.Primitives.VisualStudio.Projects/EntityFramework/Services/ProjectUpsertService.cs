using Common.EntityFrameworkServices;
using DevOps.Primitives.Strings;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevOps.Primitives.VisualStudio.Projects.EntityFramework.Services
{
    public class ProjectUpsertService<TDbContext> : UpsertService<TDbContext, Project>
        where TDbContext : VisualStudioProjectsDbContext
    {
        private readonly IUpsertService<TDbContext, MsBuildProjectFile> _projectFiles;
        private readonly IUpsertService<TDbContext, AsciiStringReference> _strings;

        public ProjectUpsertService(ICacheService<Project> cache, TDbContext database, ILogger<UpsertService<TDbContext, Project>> logger, IUpsertService<TDbContext, AsciiStringReference> strings, IUpsertService<TDbContext, MsBuildProjectFile> projectFiles)
            : base(cache, database, logger, database.Projects)
        {
            CacheKey = record => $"{nameof(VisualStudio)}.{nameof(Project)}={record.NameId}:{record.MsBuildProjectFileId}";
            _projectFiles = projectFiles ?? throw new ArgumentNullException(nameof(projectFiles));
            _strings = strings ?? throw new ArgumentNullException(nameof(strings));
        }

        protected override async Task<Project> AssignUpsertedReferences(Project record)
        {
            record.Name = await _strings.UpsertAsync(record.Name);
            record.NameId = record.Name?.AsciiStringReferenceId ?? record.NameId;
            record.MsBuildProjectFile = await _projectFiles.UpsertAsync(record.MsBuildProjectFile);
            record.MsBuildProjectFileId = record.MsBuildProjectFile?.MsBuildProjectFileId ?? record.MsBuildProjectFileId;
            return record;
        }

        protected override IEnumerable<object> EnumerateReferences(Project record)
        {
            yield return record.Name;
            yield return record.MsBuildProjectFile;
        }

        protected override Expression<Func<Project, bool>> FindExisting(Project record)
            => existing
                => existing.NameId == record.NameId
                && existing.MsBuildProjectFileId == record.MsBuildProjectFileId;
    }
}
