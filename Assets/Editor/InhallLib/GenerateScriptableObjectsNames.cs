// --------------------------------------------------------- 
// GenerateScriptableObjectsNames.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using UnityEditor;
using System.Collections;
public class GenerateScriptableObjectsNames : EditorWindow
{
    #region variable 
    string selectedDirectory;
 #endregion
 #region property
 #endregion
 #region method
 
    [MenuItem("Generate/GenerateScriptableObjectsNames")]
    private static void ShowWindows()
    {
        GenerateScriptableObjectsNames window = GetWindow<GenerateScriptableObjectsNames>();
        window.titleContent = new GUIContent("GenerateScriptableObjectsNames");
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("Selected Directory: " + selectedDirectory);

        if (GUILayout.Button("Select Directory"))
        {
            string initialPath = Application.dataPath; // 初期ディレクトリのパス
            string selectedPath = EditorUtility.OpenFolderPanel("Select Directory", initialPath, "");

            if (!string.IsNullOrEmpty(selectedPath))
            {
                selectedDirectory = selectedPath;
            }
        }
    }
    #endregion
}