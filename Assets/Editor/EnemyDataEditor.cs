// --------------------------------------------------------- 
// EnemyDataEditor.cs 
// 
// CreateDay: 2023/07/07
// Creator  : TakayanagiSora
// --------------------------------------------------------- 
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
            instance._arcHeight = EditorGUILayout.Slider("ArcHeight Åiå ÇÃçÇÇ≥Åj", instance._arcHeight, 1f, 100f);
        }
    }
}