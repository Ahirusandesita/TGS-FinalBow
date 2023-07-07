// --------------------------------------------------------- 
// EnemyDataEditor.cs 
// 
// CreateDay: 2023/07/07
// Creator  : TakayanagiSora
// --------------------------------------------------------- 
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BirdDataTable))]
public class EnemyDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        BirdDataTable instance = target as BirdDataTable;

        if (instance._moveType == MoveType.curve)
        {
            instance._arcHeight = EditorGUILayout.FloatField("å ÇÃçÇÇ≥", instance._arcHeight);
        }
    }
}