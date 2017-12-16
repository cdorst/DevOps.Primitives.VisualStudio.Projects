namespace DevOps.Primitives.VisualStudio.Projects
{
    internal static class ConditionHelper
    {
        public static MsBuildCondition Create(string condition)
            => !string .IsNullOrWhiteSpace(condition) ? new MsBuildCondition(condition) : null;
    }
}
