using Common.EnumStringValues;

namespace DevOps.Primitives.VisualStudio.Projects
{
    public enum ProjectType : byte
    {
        [EnumStringValue(ProjectTypeConstants.MicrosoftNetSdk)]
        MicrosoftNetSdk,
        [EnumStringValue(ProjectTypeConstants.MsBuild2003)]
        MsBuild2003
    }
}
