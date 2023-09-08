// --------------------------------------------------------- 
// EnemyDataEditor.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BirdDataTable))]
public class EnemyDataEditor : Editor
{
    private GUIStyle _bold = new();

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var instance = target as BirdDataTable;
        _bold.richText = true;

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("<color=white><b>ループする</b></color>", _bold);
        instance._needRoop = EditorGUILayout.Toggle("NeedRoop", instance._needRoop);

        if (instance._needRoop)
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("<color=white><b>ループ先のゴール番号（リストのインデックス）</b></color>", _bold);
            instance._goalIndexOfRoop = EditorGUILayout.IntField("GoalIndexOfRoop", instance._goalIndexOfRoop);

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("<color=white><b>デスポーン時間</b></color>", _bold);
            instance._despawnTime_s = EditorGUILayout.FloatField("DespawnTime_s", instance._despawnTime_s);
        }

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
    }
}