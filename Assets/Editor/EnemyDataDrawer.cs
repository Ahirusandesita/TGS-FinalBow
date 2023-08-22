// --------------------------------------------------------- 
// EnemyDataDrawer.cs 
// 
// CreateDay: 2023/07/27
// Creator  : TakayanagiSora
// --------------------------------------------------------- 

using System;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

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

            // _stayTime_s（停止する秒数）---------------------------------------------------------------------------------------------------------------------------------

            Rect stayTimeRect = new(speedRect)
            {
                y = speedRect.y + EditorGUIUtility.singleLineHeight + 3f
            };

            var stayTimeProperty = property.FindPropertyRelative("_stayTime_s");
            stayTimeProperty.floatValue = EditorGUI.FloatField(stayTimeRect, "停止する秒数", stayTimeProperty.floatValue);

            // _birdAttackType（攻撃方法の種類）-----------------------------------------------------------------------------------------------------------------------------------

            Rect attackTypeRect = new(stayTimeRect)
            {
                y = stayTimeRect.y + EditorGUIUtility.singleLineHeight + 3f
            };

            var attackTypeProperty = property.FindPropertyRelative("_birdAttackType");
            attackTypeProperty.enumValueIndex = EditorGUI.Popup(attackTypeRect, "攻撃方法の種類", attackTypeProperty.enumValueIndex, Enum.GetNames(typeof(BirdAttackType)));

            Rect attackIntervalRect;
            SerializedProperty attackIntervalProperty;
            Rect attackTimingsRect5;

            switch ((BirdAttackType)attackTypeProperty.enumValueIndex)
            {
                case BirdAttackType.equalIntervals:

                    attackIntervalRect = new(attackTypeRect)
                    {
                        y = attackTypeRect.y + EditorGUIUtility.singleLineHeight + 3f
                    };

                    attackIntervalProperty = property.FindPropertyRelative("_attackInterval_s");
                    attackIntervalProperty.floatValue = EditorGUI.FloatField(attackIntervalRect, "攻撃間隔", attackIntervalProperty.floatValue);

                    attackTimingsRect5 = new(attackIntervalRect)
                    {
                        y = attackIntervalRect.y
                    };

                    break;

                case BirdAttackType.specifySeconds:

                    Rect labelRect = new(attackTypeRect)
                    {
                        y = attackTypeRect.y + EditorGUIUtility.singleLineHeight + 3f
                    };

                    EditorGUI.LabelField(labelRect, "攻撃するタイミング（何秒後）");

                    Rect attackTimingsRect = new(labelRect)
                    {
                        y = labelRect.y + EditorGUIUtility.singleLineHeight + 3f
                    };

                    var attackTimingsProperty = property.FindPropertyRelative("_attackTimings_s1");
                    attackTimingsProperty.floatValue = EditorGUI.FloatField(attackTimingsRect, "Elements 1", attackTimingsProperty.floatValue);

                    Rect attackTimingsRect2 = new(attackTimingsRect)
                    {
                        y = attackTimingsRect.y + EditorGUIUtility.singleLineHeight + 3f
                    };

                    var attackTimingsProperty2 = property.FindPropertyRelative("_attackTimings_s2");
                    attackTimingsProperty2.floatValue = EditorGUI.FloatField(attackTimingsRect2, "Elements 2", attackTimingsProperty2.floatValue);

                    Rect attackTimingsRect3 = new(attackTimingsRect2)
                    {
                        y = attackTimingsRect2.y + EditorGUIUtility.singleLineHeight + 3f
                    };

                    var attackTimingsProperty3 = property.FindPropertyRelative("_attackTimings_s3");
                    attackTimingsProperty3.floatValue = EditorGUI.FloatField(attackTimingsRect3, "Elements 3", attackTimingsProperty3.floatValue);

                    Rect attackTimingsRect4 = new(attackTimingsRect3)
                    {
                        y = attackTimingsRect3.y + EditorGUIUtility.singleLineHeight + 3f
                    };

                    var attackTimingsProperty4 = property.FindPropertyRelative("_attackTimings_s4");
                    attackTimingsProperty4.floatValue = EditorGUI.FloatField(attackTimingsRect4, "Elements 4", attackTimingsProperty4.floatValue);

                    attackTimingsRect5 = new(attackTimingsRect4)
                    {
                        y = attackTimingsRect4.y + EditorGUIUtility.singleLineHeight + 3f
                    };

                    var attackTimingsProperty5 = property.FindPropertyRelative("_attackTimings_s5");
                    attackTimingsProperty5.floatValue = EditorGUI.FloatField(attackTimingsRect5, "Elements 5", attackTimingsProperty5.floatValue);

                    break;

                case BirdAttackType.consecutive:

                    Rect attackTimesRect = new(attackTypeRect)
                    {
                        y = attackTypeRect.y + EditorGUIUtility.singleLineHeight + 3f
                    };

                    var attackTimesProperty = property.FindPropertyRelative("_attackTimes");
                    attackTimesProperty.intValue = EditorGUI.IntSlider(attackTimesRect, "連続攻撃回数", attackTimesProperty.intValue, 0, 10);

                    Rect cooldownTimeRect = new(attackTimesRect)
                    {
                        y = attackTimesRect.y + EditorGUIUtility.singleLineHeight + 3f
                    };

                    var cooldownTimeProperty = property.FindPropertyRelative("_cooldownTime_s");
                    cooldownTimeProperty.floatValue = EditorGUI.FloatField(cooldownTimeRect, "連続攻撃クールダウン（秒）", cooldownTimeProperty.floatValue);

                    attackIntervalRect = new(cooldownTimeRect)
                    {
                        y = cooldownTimeRect.y + EditorGUIUtility.singleLineHeight + 3f
                    };

                    attackIntervalProperty = property.FindPropertyRelative("_attackInterval_s");
                    attackIntervalProperty.floatValue = EditorGUI.FloatField(attackIntervalRect, "攻撃間隔", attackIntervalProperty.floatValue);

                    attackTimingsRect5 = new(attackIntervalRect)
                    {
                        y = attackIntervalRect.y
                    };

                    break;

                default:

                    attackTimingsRect5 = new(attackTypeRect)
                    {
                        y = attackTypeRect.y
                    };

                    break;
            }

            // 最後にリストの高さを更新
            height = attackTimingsRect5.y - position.y + EditorGUIUtility.singleLineHeight + 1.5f;
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return height;
    }
}