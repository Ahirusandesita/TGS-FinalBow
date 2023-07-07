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
    string non = "�s���ȑI���ł�";
    string selectedDirectory = "���I��";
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
            // �����f�B���N�g���̃p�X
            string initialPath = Application.dataPath + ADD;
            // �I��������
            string selectedPath = EditorUtility.OpenFolderPanel("Select Directory", initialPath, "");

            // �A�Z�b�gDB�̎g�p������؂蔲��
            selectedPath = selectedPath.Remove(0,Application.dataPath.Length - ADD.Length +2) ;

            // �󂶂�Ȃ���
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
        // �f�B���N�g�����̑SGUID���擾
        string[] guids = AssetDatabase.FindAssets("", new[] { searchDirectory });

        List<string> structCode = new List<string>();
        structCode.Add("");
        structCode.Add("struct " + GetPathEndName(searchDirectory));
        structCode.Add("{");

        // �f�B���N�g��
        foreach (string guid in guids)
        {
            string foundPath = AssetDatabase.GUIDToAssetPath(guid);

            // �f�B���N�g���Ȃ�
            if (AssetDatabase.IsValidFolder(foundPath))
            {
                SearchDirectory(foundPath);
            }
            // ����ȊO�Ȃ�(�t�@�C���Ȃ�)
            else if (AssetDatabase.IsValidFolder(foundPath) == false)
            {
                string name = GetPathEndName(foundPath);

                structCode.Add("\tconst string " + name + " = " + name + ";");
            }
        }
        structCode.Add("}");

        // �ł����\���̂̕�������R�[�h�ɒǉ�����
        cashCode.AddRange(structCode);

    }

    private static string GetPathEndName(string pathName)
    {
        string[] split = pathName.Split("/");
        // �p�X�̖������擾
        string pathEndName = split.GetValue(split.Length).ToString();
        return pathEndName;
    }

    private void GenerateCode()
    {

    }
    #endregion
}