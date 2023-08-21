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
    private float height = default;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        using (new EditorGUI.PropertyScope(position, label, property))
        {
            position.height = EditorGUIUtility.singleLineHeight;

            // _moveType（動きの種類）---------------------------------------------------------------------------------------------------------------------------------------------

            var moveTypeProperty = property.FindPropertyRelative("_moveType");
            moveTypeProperty.enumValueIndex = EditorGUI.Popup(position, "動きの種類", moveTypeProperty.enumValueIndex, Enum.GetNames(typeof(MoveType)));

            // MoveTypeによって表示を切り替え

            Rect arcMoveDirectionRect;

            switch ((MoveType)moveTypeProperty.enumValueIndex)
            {
                case MoveType.curve:

                    // _arcHeight（弧の高さ）-------------------------------------------------------------------------------------------------------------------------------------

                    Rect arcHieghtRect = new(position)
                    {
                        y = position.y + EditorGUIUtility.singleLineHeight + 3f
                    };

                    var arcHieghtProperty = property.FindPropertyRelative("_arcHeight");
                    arcHieghtProperty.floatValue = EditorGUI.Slider(arcHieghtRect, "弧の高さ", arcHieghtProperty.floatValue, 1f, 30f);

                    // _arcMoveDirection（弧の向き）------------------------------------------------------------------------------------------------------------------------------

                    arcMoveDirectionRect = new(arcHieghtRect)
                    {
                        y = arcHieghtRect.y + EditorGUIUtility.singleLineHeight + 3f
                    };

                    var arcMoveDirectionProperty = property.FindPropertyRelative("_arcMoveDirection");
                    arcMoveDirectionProperty.enumValueIndex = EditorGUI.Popup(arcMoveDirectionRect, "弧の向き", arcMoveDirectionProperty.enumValueIndex, Enum.GetNames(typeof(ArcMoveDirection)));

                    break;

                default:

                    // 汚いけど仮のRectを生成（分岐に適応させるため）
                    arcMoveDirectionRect = new(position)
                    {
                        y = position.y
                    };

                    break;
            }

            // _birdGoalPlace（ゴール）--------------------------------------------------------------------------------------------------------------------------------------------

            Rect goalPlaceRect = new(arcMoveDirectionRect)
            {
                y = arcMoveDirectionRect.y + EditorGUIUtility.singleLineHeight + 3f
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

            // _birdAttackType（攻撃方法の種類）-----------------------------------------------------------------------------------------------------------------------------------

            Rect AttackTypeRect = new(stayTimeRect)
            {
                y = stayTimeRect.y + EditorGUIUtility.singleLineHeight + 3f
            };

            var AttackTypeProperty = property.FindPropertyRelative("_birdAttackType");
            AttackTypeProperty.enumValueIndex = EditorGUI.Popup(AttackTypeRect, "攻撃方法の種類", AttackTypeProperty.enumValueIndex, Enum.GetNames(typeof(BirdAttackType)));

            switch ((BirdAttackType)AttackTypeProperty.enumValueIndex)
            {
                case BirdAttackType.equalIntervals:

                    break;

                case BirdAttackType.specifySeconds:

                    break;

                case BirdAttackType.consecutive:

                    break;

                default:

                    break;
            }

            // 最後にリストの高さを更新
            height = AttackTypeRect.y - position.y + EditorGUIUtility.singleLineHeight + 1.5f;
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return height;
    }
}