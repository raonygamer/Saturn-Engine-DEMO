/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using System.IO;
using Saturn.HybClasses;

public class Build : IPostprocessBuildWithReport
{
    public int callbackOrder { get { return 0; } }
    public void OnPostprocessBuild(BuildReport buildWithReport)
    {
        string directoryToCopy = Path.Combine(StaticVariables.Root, "mods");
        string dirToCreate = directoryToCopy.Replace(directoryToCopy, Path.Combine(buildWithReport.summary.outputPath, "mods"));
        Directory.CreateDirectory(dirToCreate);
    }
}*/
//
