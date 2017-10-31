using System;

namespace Brilliant.ProjectStudio
{
    /// <summary>
    /// 系统界面控件的GUID
    /// </summary>
    static class GuidList
    {
        public const string guidProjectStudioPkgString = "d936b09e-bb64-4d81-9865-2cac48cc322b";
        public const string guidProjectStudioCmdSetString = "a63381b8-06fe-4be3-a6af-11e90dbce597";
        public const string guidWinDBMgrPersistanceString = "b31fc0e7-11a2-4a46-a80d-b93cabf5c661";
        public const string guidWinTemplateMgrPersistanceString = "B6BFAA2E-732E-411E-AF02-A4F587F5ADD6";
        public static readonly Guid guidProjectStudioCmdSet = new Guid(guidProjectStudioCmdSetString);
    };
}