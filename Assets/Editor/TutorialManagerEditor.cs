// --------------------------------------------------------- 
// TutorialManagerEditor.cs 
// 
// CreateDay: 2023/09/15
// Creator  : TakayanagiSora
// --------------------------------------------------------- 

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TutorialManager))]
public class TutorialManagerEditor : Editor
{
    private GUIStyle _bold = new();

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var instance = target as TutorialManager;
        _bold.richText = true;

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("<color=white><b>チュートリアルのスキップ（デバッグ用）</b></color>", _bold);
        instance._skipTutorial = EditorGUILayout.Toggle("Skip Tutorial", instance._skipTutorial);
    }
}