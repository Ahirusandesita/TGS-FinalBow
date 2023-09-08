// --------------------------------------------------------- 
// TargetDataEditor.cs 
// 
// CreateDay: 2023/09/08
// Creator  : TakayanagiSora
// --------------------------------------------------------- 

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TargetDataTable))]
public class TargetDataEditor : Editor
{
    private GUIStyle _bold = new();

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        _bold.richText = true;

        var instance = target as TargetDataTable;

        if (instance._needMove)
        {
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("<color=white><b>ゴールの位置</b></color>", _bold);
            instance._goalPlace = (Transform)EditorGUILayout.ObjectField("GoalPlace", instance._goalPlace, typeof(Transform), true);

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("<color=white><b>移動スピード</b></color>", _bold);
            instance._speed = EditorGUILayout.FloatField("Speed", instance._speed);

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("<color=white><b>停止時間</b></color>", _bold);
            instance._stayTime_s = EditorGUILayout.FloatField("StayTime_s", instance._stayTime_s);
        }
    }
}