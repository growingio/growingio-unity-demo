#if UNITY_IPHONE

using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;

public class BuildPostProcessor {

    [PostProcessBuildAttribute(1)]
    public static void OnPostProcessBuild(BuildTarget target, string path) {
        if (target == BuildTarget.iOS) {
            // Read.
            string projectPath = PBXProject.GetPBXProjectPath(path);
            PBXProject project = new PBXProject();
            project.ReadFromString(File.ReadAllText(projectPath));
            string targetGUID = project.GetUnityFrameworkTargetGuid();

            //  other linker flags 
            project.AddBuildProperty(targetGUID, "OTHER_LDFLAGS", "-ObjC");

            //  Frameworks
            AddFrameworks(project, targetGUID);

            //  Libraries
            AddLibraries(project, targetGUID);

            //  InfoPlist
            AddInfoPlist(path, "growing.f1c35f1dbd568007");

            // Write.
            File.WriteAllText(projectPath, project.WriteToString());
        }
    }

    static void AddFrameworks(PBXProject project, string targetGUID) {

        project.AddFrameworkToProject(targetGUID, "Foundation.framework", false);
        project.AddFrameworkToProject(targetGUID, "CoreTelephony.framework", false);
        project.AddFrameworkToProject(targetGUID, "SystemConfiguration.framework", false);
        project.AddFrameworkToProject(targetGUID, "AdSupport.framework", false);
        project.AddFrameworkToProject(targetGUID, "CoreLocation.framework", false);
        project.AddFrameworkToProject(targetGUID, "WebKit.framework", false);
    }

    static void AddLibraries(PBXProject project, string targetGUID) {
        string fileGuidSqlite = project.AddFile("usr/lib/libsqlite3.tbd", "Libraries/libsqlite3.tbd", PBXSourceTree.Sdk);
        project.AddFileToBuild(targetGUID, fileGuidSqlite);
    }

    static void AddInfoPlist(string path, string URLSchemeString) {

        var plistPath = Path.Combine(path, "Info.plist");
        var plist = new PlistDocument();
        plist.ReadFromFile(plistPath);
        PlistElementArray urlTypes = plist.root.CreateArray("CFBundleURLTypes");
        PlistElementDict itemDict;

        itemDict = urlTypes.AddDict();
        itemDict.SetString("CFBundleTypeRole", "Editor");
        itemDict.SetString("CFBundleURLName", "GIOURLScheme");
        PlistElementArray schemesArray = itemDict.CreateArray("CFBundleURLSchemes");
        schemesArray.AddString(URLSchemeString);
        File.WriteAllText(plistPath, plist.WriteToString());
    }

}

#endif