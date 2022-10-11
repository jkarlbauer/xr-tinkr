#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace Xrtinkr.Build
{
    class BuildProcessor : IPreprocessBuildWithReport
    {
        public int callbackOrder { get { return 0; } }

        public void OnPreprocessBuild(BuildReport report)
        {
            if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android)
            {
                //settings for quest build
            }
        }
    }
}
#endif