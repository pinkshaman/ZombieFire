using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

public class ScriptErrorChecker : EditorWindow
{
    [MenuItem("Debug/Check Script Errors")]
    public static void CheckAllScripts()
    {
        string[] scriptFiles = Directory.GetFiles(Application.dataPath, "*.cs", SearchOption.AllDirectories);
        int errorCount = 0;

        foreach (string scriptPath in scriptFiles)
        {
            string scriptContent = File.ReadAllText(scriptPath);

            // Kiểm tra gọi hàm trong Constructor
            if (Regex.IsMatch(scriptContent, @"public\s+\w+\s*\(\)"))
            {
                Debug.LogWarning($"[WARNING] Constructor detected: {scriptPath}");
                errorCount++;
            }

            // Kiểm tra gọi hàm trong Field Initializer
            if (Regex.IsMatch(scriptContent, @"=\s*\w+\("))
            {
                Debug.LogWarning($"[WARNING] Field initializer calling method: {scriptPath}");
                errorCount++;
            }

            // Kiểm tra gọi Repaint() sai luồng
            if (scriptContent.Contains("SceneView.RepaintAll()") || scriptContent.Contains("EditorWindow.Repaint()"))
            {
                Debug.LogError($"[ERROR] Possible Repaint issue: {scriptPath}");
                errorCount++;
            }
        }

        Debug.Log($"✅ Scan complete! Found {errorCount} potential issues.");
    }
}
