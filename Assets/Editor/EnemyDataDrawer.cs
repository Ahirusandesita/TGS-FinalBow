// --------------------------------------------------------- 
// EnemyDataDrawer.cs 
// 
// CreateDay: 2023/07/27
// Creator  : TakayanagiSora
// --------------------------------------------------------- 

using System;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(BirdGoalInformation))]
public class EnemyDataDrawer : PropertyDrawer
{

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        using (new EditorGUI.PropertyScope(position, label, property))
        {
            position.height = EditorGUIUtility.singleLineHeight;

            Rect moveTypeRect = new Rect(position)
            {
                y = position.y
            };

            var moveTypeProperty = property.FindPropertyRelative("_moveType");
            moveTypeProperty.enumValueIndex = EditorGUI.Popup(position, "“®‚«‚ÌŽí—Þ", moveTypeProperty.enumValueIndex, Enum.GetNames(typeof(MoveType)));

            Rect goalPlaceRect = new Rect(moveTypeRect)
            {
                y = moveTypeRect.y + EditorGUIUtility.singleLineHeight + 2f
            };

            var goalPlaceProperty = property.FindPropertyRelative("_birdGoalPlace");
            //EditorGUI.

            switch ((MoveType)moveTypeProperty.enumValueIndex)
            {
                case MoveType.linear:
                    break;
            }
        }
    }
}