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

            // _moveType�i�����̎�ށj---------------------------------------------------------------------------------------------------------------------------------------------

            var moveTypeProperty = property.FindPropertyRelative("_moveType");
            moveTypeProperty.enumValueIndex = EditorGUI.Popup(position, "�����̎��", moveTypeProperty.enumValueIndex, Enum.GetNames(typeof(MoveType)));

            // MoveType�ɂ���ĕ\����؂�ւ�

            Rect arcMoveDirectionRect;

            switch ((MoveType)moveTypeProperty.enumValueIndex)
            {
                case MoveType.curve:

                    // _arcHeight�i�ʂ̍����j-------------------------------------------------------------------------------------------------------------------------------------

                    Rect arcHieghtRect = new(position)
                    {
                        y = position.y + EditorGUIUtility.singleLineHeight + 3f
                    };

                    var arcHieghtProperty = property.FindPropertyRelative("_arcHeight");
                    arcHieghtProperty.floatValue = EditorGUI.Slider(arcHieghtRect, "�ʂ̍���", arcHieghtProperty.floatValue, 1f, 30f);

                    // _arcMoveDirection�i�ʂ̌����j------------------------------------------------------------------------------------------------------------------------------

                    arcMoveDirectionRect = new(arcHieghtRect)
                    {
                        y = arcHieghtRect.y + EditorGUIUtility.singleLineHeight + 3f
                    };

                    var arcMoveDirectionProperty = property.FindPropertyRelative("_arcMoveDirection");
                    arcMoveDirectionProperty.enumValueIndex = EditorGUI.Popup(arcMoveDirectionRect, "�ʂ̌���", arcMoveDirectionProperty.enumValueIndex, Enum.GetNames(typeof(ArcMoveDirection)));

                    break;

                default:

                    // �������ǉ���Rect�𐶐��i����ɓK�������邽�߁j
                    arcMoveDirectionRect = new(position)
                    {
                        y = position.y
                    };

                    break;
            }

            // _birdGoalPlace�i�S�[���j--------------------------------------------------------------------------------------------------------------------------------------------

            Rect goalPlaceRect = new(arcMoveDirectionRect)
            {
                y = arcMoveDirectionRect.y + EditorGUIUtility.singleLineHeight + 3f
            };

            var goalPlaceProperty = property.FindPropertyRelative("_birdGoalPlace");
            goalPlaceProperty.objectReferenceValue = EditorGUI.ObjectField(goalPlaceRect, "�S�[���i�i�s��j", goalPlaceProperty.objectReferenceValue, typeof(Transform), true);

            // _speed�i�ړ��X�s�[�h�j----------------------------------------------------------------------------------------------------------------------------------------------

            Rect speedRect = new(goalPlaceRect)
            {
                y = goalPlaceRect.y + EditorGUIUtility.singleLineHeight + 3f
            };

            var speedProperty = property.FindPropertyRelative("_speed");
            speedProperty.floatValue = EditorGUI.FloatField(speedRect, "�ړ��X�s�[�h", speedProperty.floatValue);

            // _stayTime_s�i��~����b���j---------------------------------------------------------------------------------------------------------------------------------

            Rect stayTimeRect = new(speedRect)
            {
                y = speedRect.y + EditorGUIUtility.singleLineHeight + 3f
            };

            var stayTimeProperty = property.FindPropertyRelative("_stayTime_s");
            stayTimeProperty.floatValue = EditorGUI.FloatField(stayTimeRect, "��~����b��", stayTimeProperty.floatValue);

            // _birdAttackType�i�U�����@�̎�ށj-----------------------------------------------------------------------------------------------------------------------------------

            Rect attackTypeRect = new(stayTimeRect)
            {
                y = stayTimeRect.y + EditorGUIUtility.singleLineHeight + 3f
            };

            var attackTypeProperty = property.FindPropertyRelative("_birdAttackType");
            attackTypeProperty.enumValueIndex = EditorGUI.Popup(attackTypeRect, "�U�����@�̎��", attackTypeProperty.enumValueIndex, Enum.GetNames(typeof(BirdAttackType)));

            switch ((BirdAttackType)attackTypeProperty.enumValueIndex)
            {
                case BirdAttackType.equalIntervals:

                    Rect attackIntervalRect = new(attackTypeRect)
                    {
                        y = attackTypeRect.y + EditorGUIUtility.singleLineHeight + 3f
                    };

                    var attackIntervalProperty = property.FindPropertyRelative("_attackInterval_s");
                    attackIntervalProperty.floatValue = EditorGUI.FloatField(attackIntervalRect, "�U���Ԋu", attackIntervalProperty.floatValue);

                    break;

                case BirdAttackType.specifySeconds:

                    Rect attackTimingsRect = new(attackTypeRect)
                    {
                        y = attackTypeRect.y + EditorGUIUtility.singleLineHeight + 3f
                    };

                    var attackTimingsProperty = property.FindPropertyRelative("_attackTimings_s");
                    //attackIntervalProperty.floatValue = EditorGUI.FloatField(attackTimingsRect, )

                    break;

                case BirdAttackType.consecutive:

                    break;

                default:

                    break;
            }

            // �Ō�Ƀ��X�g�̍������X�V
            height = attackTypeRect.y - position.y + EditorGUIUtility.singleLineHeight + 1.5f;
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return height;
    }
}