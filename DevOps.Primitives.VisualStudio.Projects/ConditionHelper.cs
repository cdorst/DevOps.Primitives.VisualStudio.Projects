using static System.String;

namespace DevOps.Primitives.VisualStudio.Projects
{
    internal static class ConditionHelper
    {
        public static MsBuildCondition Create(in string condition)
            => IsNullOrWhiteSpace(condition) ? null : new MsBuildCondition(in condition);
    }
}
