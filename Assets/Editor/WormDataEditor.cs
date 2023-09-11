// --------------------------------------------------------- 
// WormDataEditor.cs 
// 
// CreateDay: 2023/09/11
// Creator  : TakayanagiSora
// --------------------------------------------------------- 

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GroundEnemyDataTable))]
public class WormDataEditor : Editor
{
    private GUIStyle _bold = new();

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        _bold.richText = true;

        GroundEnemyDataTable instance = target as GroundEnemyDataTable;

        if (!instance._onlySandDust)
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("<color=white><b>êˆÇÈÇ‹Ç≈ÇÃéûä‘</b></color>", _bold);
            instance._appearanceKeep_s = EditorGUILayout.FloatField("Appearance Keep_s", instance._appearanceKeep_s);

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("<color=white><b>èoåªÇÃédï˚</b></color>", _bold);
            instance._wormType = (WormType)EditorGUILayout.EnumPopup("Worm Type", instance._wormType);
        }
    }
}