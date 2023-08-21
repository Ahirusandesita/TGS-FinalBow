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

            // _moveType（動きの種類）---------------------------------------------------------------------------------------------------------------------------------------------

            Rect moveTypeRect = new(position)
            {
                y = position.y
            };

            var moveTypeProperty = property.FindPropertyRelative("_moveType");
            moveTypeProperty.enumValueIndex = EditorGUI.Popup(moveTypeRect, "動きの種類", moveTypeProperty.enumValueIndex, Enum.GetNames(typeof(MoveType)));

            // _birdGoalPlace（ゴール）--------------------------------------------------------------------------------------------------------------------------------------------

            Rect goalPlaceRect = new(moveTypeRect)
            {
                y = moveTypeRect.y + EditorGUIUtility.singleLineHeight + 3f
            };

            var goalPlaceProperty = property.FindPropertyRelative("_birdGoalPlace");
            goalPlaceProperty.objectReferenceValue = EditorGUI.ObjectField(goalPlaceRect, "ゴール（進行先）", goalPlaceProperty.objectReferenceValue, typeof(Transform), true);

            // _speed（移動スピード）----------------------------------------------------------------------------------------------------------------------------------------------

            Rect speedRect = new(goalPlaceRect)
            {
                y = goalPlaceRect.y + EditorGUIUtility.singleLineHeight + 3f
            };

            var speedProperty = property.FindPropertyRelative("_speed");
            speedProperty.floatValue = EditorGUI.FloatField(speedRect, "移動スピード", speedProperty.floatValue);

            // _stayTime_s（停止して攻撃する秒数）---------------------------------------------------------------------------------------------------------------------------------

            Rect stayTimeRect = new(speedRect)
            {
                y = speedRect.y + EditorGUIUtility.singleLineHeight + 3f
            };

            var stayTimeProperty = property.FindPropertyRelative("_stayTime_s");
            stayTimeProperty.floatValue = EditorGUI.FloatField(stayTimeRect, "停止して攻撃する秒数", stayTimeProperty.floatValue);


            // MoveTypeによって表示を切り替え

            switch ((MoveType)moveTypeProperty.enumValueIndex)
            {
                case MoveType.linear:
                    break;

                case MoveType.curve:

                    // _arcHeight（弧の高さ）-------------------------------------------------------------------------------------------------------------------------------------

                    Rect arcHieghtRect = new(stayTimeRect)
                    {
                        y = stayTimeRect.y + EditorGUIUtility.singleLineHeight + 3f
                    };

                    var arcHieghtProperty = property.FindPropertyRelative("_arcHeight");
                    arcHieghtProperty.floatValue = EditorGUI.Slider(arcHieghtRect, "弧の高さ", arcHieghtProperty.floatValue, 1f, 30f);

                    // _arcMoveDirection（弧の向き）------------------------------------------------------------------------------------------------------------------------------

                    Rect arcMoveDirectionRect = new(arcHieghtRect)
                    {
                        y = arcHieghtRect.y + EditorGUIUtility.singleLineHeight + 3f
                    };

                    var arcMoveDirectionProperty = property.FindPropertyRelative("_arcMoveDirection");
                    arcMoveDirectionProperty.enumValueIndex = EditorGUI.Popup(arcMoveDirectionRect, "弧の向き", arcMoveDirectionProperty.enumValueIndex, Enum.GetNames(typeof(ArcMoveDirection)));

                    break;
            }
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float height = EditorGUIUtility.singleLineHeight;

        switch ((MoveType)property.FindPropertyRelative("_moveType").enumValueIndex)
        {
            case MoveType.linear:
                height = 100f;
                break;
            case MoveType.curve:
                height = 140f;
                break;
        }

        return height;
    }
}