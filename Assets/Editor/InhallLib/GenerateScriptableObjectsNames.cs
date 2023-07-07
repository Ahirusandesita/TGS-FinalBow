// --------------------------------------------------------- 
// GenerateScriptableObjectsNames.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
public class GenerateScriptableObjectsNames : EditorWindow
{
    private const string ADD = "/Scripts";
    #region variable 
    string non = "不正な選択です";
    string selectedDirectory = "未選択";
    bool canGen = false;

    List<string> cashCode = default;

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
            // 初期ディレクトリのパス
            string initialPath = Application.dataPath + ADD;
            // 選択させる
            string selectedPath = EditorUtility.OpenFolderPanel("Select Directory", initialPath, "");

            // アセットDBの使用部分を切り抜き
            selectedPath = selectedPath.Remove(0,Application.dataPath.Length - ADD.Length +2) ;

            // 空じゃないか
            if (!string.IsNullOrEmpty(selectedPath))
            {
                Object folder = AssetDatabase.LoadAssetAtPath<Object>(selectedPath);

                if(folder != null)
                {
                    selectedDirectory = AssetDatabase.GetAssetPath(folder);
                    canGen = true;
                    return;
                }
               
            }

            selectedDirectory = non;
            canGen = false;
        }

        if (GUILayout.Button("GenerateStart") && canGen)
        {
            StartSearchDirectory(selectedDirectory);
        }
    }

    private void StartSearchDirectory(string rootSearchDirectory)
    {
        SearchDirectory(rootSearchDirectory);

        GenerateCode();
    }

    private void SearchDirectory(string searchDirectory)
    {
        // ディレクトリ内の全GUIDを取得
        string[] guids = AssetDatabase.FindAssets("", new[] { searchDirectory });

        List<string> structCode = new List<string>();
        structCode.Add("");
        structCode.Add("struct " + GetPathEndName(searchDirectory));
        structCode.Add("{");

        // ディレクトリ
        foreach (string guid in guids)
        {
            string foundPath = AssetDatabase.GUIDToAssetPath(guid);

            // ディレクトリなら
            if (AssetDatabase.IsValidFolder(foundPath))
            {
                SearchDirectory(foundPath);
            }
            // それ以外なら(ファイルなら)
            else if (AssetDatabase.IsValidFolder(foundPath) == false)
            {
                string name = GetPathEndName(foundPath);

                structCode.Add("\tconst string " + name + " = " + name + ";");
            }
        }
        structCode.Add("}");

        // できた構造体の文字列をコードに追加する
        cashCode.AddRange(structCode);

    }

    private static string GetPathEndName(string pathName)
    {
        string[] split = pathName.Split("/");
        // パスの末尾を取得
        string pathEndName = split.GetValue(split.Length).ToString();
        return pathEndName;
    }

    private void GenerateCode()
    {

    }
    #endregion
}