using UnityEngine;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
public class BuildIncrementer : IPreprocessBuildWithReport
{
    public int callbackOrder => 0;
    private const string initialVersion = "0.0";

    public void OnPreprocessBuild(BuildReport report)
    {
        string currentVersion = FindCurrentVersion();
        UpdateVersion(currentVersion);
    }

    private string FindCurrentVersion()
    {
        string[] currentVersion = PlayerSettings.bundleVersion.Split('[',']');
        return currentVersion.Length == 1 ? initialVersion : currentVersion[1];
    }
    private void UpdateVersion(string version)
    {
        if (float.TryParse(version, out float versionNumber))
        {
            float newVersion = versionNumber + 0.1f;
            PlayerSettings.bundleVersion = string.Format("[0.{0}]", newVersion);
            
            Debug.Log(PlayerSettings.bundleVersion);
        }
       

    }
}
