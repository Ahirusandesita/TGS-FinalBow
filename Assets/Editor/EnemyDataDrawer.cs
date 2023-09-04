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
    private GUIStyle _bold = new();

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        _bold.richText = true;

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

            // _stayTime_s�i��~����b���j-----------------------------------------------------------------------------------------------------------------------------------------

            Rect stayTimeRect = new(speedRect)
            {
                y = speedRect.y + EditorGUIUtility.singleLineHeight + 3f
            };

            var stayTimeProperty = property.FindPropertyRelative("_stayTime_s");
            stayTimeProperty.floatValue = EditorGUI.FloatField(stayTimeRect, "��~����b��", stayTimeProperty.floatValue);

            // ---�ړ���-----------------------------------------------------------------------------------------------------------------------------------------------------------
            // _birdAttackType�i�U�����@�̎�ށj-----------------------------------------------------------------------------------------------------------------------------------

            Rect labelRect1 = new(stayTimeRect)
            {
                y = stayTimeRect.y + EditorGUIUtility.singleLineHeight + 3f
            };

            EditorGUI.LabelField(labelRect1, "<color=white><b>---�ړ���---</b></color>", _bold);

            Rect directionMoveRect = new(labelRect1)
            {
                y = labelRect1.y + EditorGUIUtility.singleLineHeight + 3f
            };

            var directionMoveProperty = property.FindPropertyRelative("_directionType_moving");
            directionMoveProperty.enumValueIndex = EditorGUI.Popup(directionMoveRect, "�ړ����̌���", directionMoveProperty.enumValueIndex, Enum.GetNames(typeof(DirectionType_AtMoving)));

            Rect attackTypeRect = new(directionMoveRect)
            {
                y = directionMoveRect.y + EditorGUIUtility.singleLineHeight + 3f
            };

            var attackTypeProperty = property.FindPropertyRelative("_birdAttackType_a");
            attackTypeProperty.enumValueIndex = EditorGUI.Popup(attackTypeRect, "�U�����@�̎��", attackTypeProperty.enumValueIndex, Enum.GetNames(typeof(BirdAttackType)));

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

                    attackIntervalProperty = property.FindPropertyRelative("_attackInterval_s_a");
                    attackIntervalProperty.floatValue = EditorGUI.FloatField(attackIntervalRect, "�U���Ԋu", attackIntervalProperty.floatValue);

                    attackTimingsRect5 = new(attackIntervalRect)
                    {
                        y = attackIntervalRect.y
                    };

                    break;

                case BirdAttackType.specifySeconds:

                    Rect labelRect2 = new(attackTypeRect)
                    {
                        y = attackTypeRect.y + EditorGUIUtility.singleLineHeight + 3f
                    };

                    EditorGUI.LabelField(labelRect2, "�U������^�C�~���O�i���b��j");

                    Rect attackTimingsRect = new(labelRect2)
                    {
                        y = labelRect2.y + EditorGUIUtility.singleLineHeight + 3f
                    };

                    var attackTimingsProperty = property.FindPropertyRelative("_attackTiming_s1_a");
                    attackTimingsProperty.floatValue = EditorGUI.FloatField(attackTimingsRect, "Elements 1", attackTimingsProperty.floatValue);

                    Rect attackTimingsRect2 = new(attackTimingsRect)
                    {
                        y = attackTimingsRect.y + EditorGUIUtility.singleLineHeight + 3f
                    };

                    var attackTimingsProperty2 = property.FindPropertyRelative("_attackTiming_s2_a");
                    attackTimingsProperty2.floatValue = EditorGUI.FloatField(attackTimingsRect2, "Elements 2", attackTimingsProperty2.floatValue);

                    Rect attackTimingsRect3 = new(attackTimingsRect2)
                    {
                        y = attackTimingsRect2.y + EditorGUIUtility.singleLineHeight + 3f
                    };

                    var attackTimingsProperty3 = property.FindPropertyRelative("_attackTiming_s3_a");
                    attackTimingsProperty3.floatValue = EditorGUI.FloatField(attackTimingsRect3, "Elements 3", attackTimingsProperty3.floatValue);

                    Rect attackTimingsRect4 = new(attackTimingsRect3)
                    {
                        y = attackTimingsRect3.y + EditorGUIUtility.singleLineHeight + 3f
                    };

                    var attackTimingsProperty4 = property.FindPropertyRelative("_attackTiming_s4_a");
                    attackTimingsProperty4.floatValue = EditorGUI.FloatField(attackTimingsRect4, "Elements 4", attackTimingsProperty4.floatValue);

                    attackTimingsRect5 = new(attackTimingsRect4)
                    {
                        y = attackTimingsRect4.y + EditorGUIUtility.singleLineHeight + 3f
                    };

                    var attackTimingsProperty5 = property.FindPropertyRelative("_attackTiming_s5_a");
                    attackTimingsProperty5.floatValue = EditorGUI.FloatField(attackTimingsRect5, "Elements 5", attackTimingsProperty5.floatValue);

                    break;

                case BirdAttackType.consecutive:

                    Rect attackTimesRect = new(attackTypeRect)
                    {
                        y = attackTypeRect.y + EditorGUIUtility.singleLineHeight + 3f
                    };

                    var attackTimesProperty = property.FindPropertyRelative("_attackTimes_a");
                    attackTimesProperty.intValue = EditorGUI.IntSlider(attackTimesRect, "�A���U����", attackTimesProperty.intValue, 0, 10);

                    Rect cooldownTimeRect = new(attackTimesRect)
                    {
                        y = attackTimesRect.y + EditorGUIUtility.singleLineHeight + 3f
                    };

                    var cooldownTimeProperty = property.FindPropertyRelative("_cooldownTime_s_a");
                    cooldownTimeProperty.floatValue = EditorGUI.FloatField(cooldownTimeRect, "�A���U���N�[���_�E���i�b�j", cooldownTimeProperty.floatValue);

                    attackIntervalRect = new(cooldownTimeRect)
                    {
                        y = cooldownTimeRect.y + EditorGUIUtility.singleLineHeight + 3f
                    };

                    attackIntervalProperty = property.FindPropertyRelative("_attackInterval_s_a");
                    attackIntervalProperty.floatValue = EditorGUI.FloatField(attackIntervalRect, "�U���Ԋu", attackIntervalProperty.floatValue);

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

            // ---��~��-----------------------------------------------------------------------------------------------------------------------------------------------------------

            Rect labelRect3 = new(attackTimingsRect5)
            {
                y = attackTimingsRect5.y + EditorGUIUtility.singleLineHeight + 3f
            };

            EditorGUI.LabelField(labelRect3, "<color=white><b>---��~��---</b></color>", _bold);

            Rect attackTypeRect2 = new(labelRect3)
            {
                y = labelRect3.y + EditorGUIUtility.singleLineHeight + 3f
            };

            var attackTypeProperty2 = property.FindPropertyRelative("_birdAttackType_b");
            attackTypeProperty2.enumValueIndex = EditorGUI.Popup(attackTypeRect2, "�U�����@�̎��", attackTypeProperty2.enumValueIndex, Enum.GetNames(typeof(BirdAttackType)));

            Rect attackIntervalRect2;
            SerializedProperty attackIntervalProperty2;
            Rect attackTimingsRect10 = default;

            switch ((BirdAttackType)attackTypeProperty2.enumValueIndex)
            {
                case BirdAttackType.equalIntervals:

                    attackIntervalRect2 = new(attackTypeRect2)
                    {
                        y = attackTypeRect2.y + EditorGUIUtility.singleLineHeight + 3f
                    };

                    attackIntervalProperty2 = property.FindPropertyRelative("_attackInterval_s_b");
                    attackIntervalProperty2.floatValue = EditorGUI.FloatField(attackIntervalRect2, "�U���Ԋu", attackIntervalProperty2.floatValue);

                    attackTimingsRect10 = new(attackIntervalRect2)
                    {
                        y = attackIntervalRect2.y
                    };

                    break;

                case BirdAttackType.specifySeconds:

                    Rect labelRect4 = new(attackTypeRect2)
                    {
                        y = attackTypeRect2.y + EditorGUIUtility.singleLineHeight + 3f
                    };

                    EditorGUI.LabelField(labelRect4, "�U������^�C�~���O�i���b��j");

                    Rect attackTimingsRect6 = new(labelRect4)
                    {
                        y = labelRect4.y + EditorGUIUtility.singleLineHeight + 3f
                    };

                    var attackTimingsProperty6 = property.FindPropertyRelative("_attackTiming_s1_b");
                    attackTimingsProperty6.floatValue = EditorGUI.FloatField(attackTimingsRect6, "Elements 1", attackTimingsProperty6.floatValue);

                    Rect attackTimingsRect7 = new(attackTimingsRect6)
                    {
                        y = attackTimingsRect6.y + EditorGUIUtility.singleLineHeight + 3f
                    };

                    var attackTimingsProperty7 = property.FindPropertyRelative("_attackTiming_s2_b");
                    attackTimingsProperty7.floatValue = EditorGUI.FloatField(attackTimingsRect7, "Elements 2", attackTimingsProperty7.floatValue);

                    Rect attackTimingsRect8 = new(attackTimingsRect7)
                    {
                        y = attackTimingsRect7.y + EditorGUIUtility.singleLineHeight + 3f
                    };

                    var attackTimingsProperty8 = property.FindPropertyRelative("_attackTiming_s3_b");
                    attackTimingsProperty8.floatValue = EditorGUI.FloatField(attackTimingsRect8, "Elements 3", attackTimingsProperty8.floatValue);

                    Rect attackTimingsRect9 = new(attackTimingsRect8)
                    {
                        y = attackTimingsRect8.y + EditorGUIUtility.singleLineHeight + 3f
                    };

                    var attackTimingsProperty9 = property.FindPropertyRelative("_attackTiming_s4_b");
                    attackTimingsProperty9.floatValue = EditorGUI.FloatField(attackTimingsRect9, "Elements 4", attackTimingsProperty9.floatValue);

                    attackTimingsRect10 = new(attackTimingsRect9)
                    {
                        y = attackTimingsRect9.y + EditorGUIUtility.singleLineHeight + 3f
                    };

                    var attackTimingsProperty10 = property.FindPropertyRelative("_attackTiming_s5_b");
                    attackTimingsProperty10.floatValue = EditorGUI.FloatField(attackTimingsRect10, "Elements 5", attackTimingsProperty10.floatValue);

                    break;

                case BirdAttackType.consecutive:

                    Rect attackTimesRect2 = new(attackTypeRect2)
                    {
                        y = attackTypeRect2.y + EditorGUIUtility.singleLineHeight + 3f
                    };

                    var attackTimesProperty2 = property.FindPropertyRelative("_attackTimes_b");
                    attackTimesProperty2.intValue = EditorGUI.IntSlider(attackTimesRect2, "�A���U����", attackTimesProperty2.intValue, 0, 10);

                    Rect cooldownTimeRect2 = new(attackTimesRect2)
                    {
                        y = attackTimesRect2.y + EditorGUIUtility.singleLineHeight + 3f
                    };

                    var cooldownTimeProperty2 = property.FindPropertyRelative("_cooldownTime_s_b");
                    cooldownTimeProperty2.floatValue = EditorGUI.FloatField(cooldownTimeRect2, "�A���U���N�[���_�E���i�b�j", cooldownTimeProperty2.floatValue);

                    attackIntervalRect2 = new(cooldownTimeRect2)
                    {
                        y = cooldownTimeRect2.y + EditorGUIUtility.singleLineHeight + 3f
                    };

                    attackIntervalProperty2 = property.FindPropertyRelative("_attackInterval_s_b");
                    attackIntervalProperty2.floatValue = EditorGUI.FloatField(attackIntervalRect2, "�U���Ԋu", attackIntervalProperty2.floatValue);

                    attackTimingsRect10 = new(attackIntervalRect2)
                    {
                        y = attackIntervalRect2.y
                    };

                    break;

                default:

                    attackTimingsRect10 = new(attackTypeRect2)
                    {
                        y = attackTypeRect2.y
                    };

                    break;
            }

            // �����p�ɂ����ЂƂ�
            switch ((BirdAttackType)attackTypeProperty2.enumValueIndex)
            {
                case BirdAttackType.none:

                    Rect directionStopRect = new(attackTimingsRect10)
                    {
                        y = attackTimingsRect10.y + EditorGUIUtility.singleLineHeight + 3f
                    };

                    var directionStopProperty = property.FindPropertyRelative("_directionType_stopping");
                    directionStopProperty.enumValueIndex = EditorGUI.Popup(directionStopRect, "��~���̌���", directionStopProperty.enumValueIndex, Enum.GetNames(typeof(DirectionType_AtStopping)));

                    break;

                default:

                    Rect directionAttackRect = new(attackTimingsRect10)
                    {
                        y = attackTimingsRect10.y + EditorGUIUtility.singleLineHeight + 3f
                    };

                    var directionAttackProperty = property.FindPropertyRelative("_directionType_attack");
                    directionAttackProperty.enumValueIndex = EditorGUI.Popup(directionAttackRect, "�U���̌���", directionAttackProperty.enumValueIndex, Enum.GetNames(typeof(DirectionType_AtAttack)));

                    break;
            }
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float height = 0f;
        var moveTypeProperty = property.FindPropertyRelative("_moveType");

        switch ((MoveType)moveTypeProperty.enumValueIndex)
        {
            case MoveType.linear:
                break;

            case MoveType.curve:

                height += 40f;
                break;
        }

        var attackTypeProperty_a = property.FindPropertyRelative("_birdAttackType_a");

        switch ((BirdAttackType)attackTypeProperty_a.enumValueIndex)
        {
            case BirdAttackType.equalIntervals:

                height += 170f;
                break;

            case BirdAttackType.specifySeconds:

                height += 275f;
                break;

            case BirdAttackType.consecutive:

                height += 210f;
                break;

            default:

                height += 150f;
                break;
        }

        var attackTypeProperty_b = property.FindPropertyRelative("_birdAttackType_b");

        switch ((BirdAttackType)attackTypeProperty_b.enumValueIndex)
        {
            case BirdAttackType.equalIntervals:

                height += 90f;
                break;

            case BirdAttackType.specifySeconds:

                height += 195f;
                break;

            case BirdAttackType.consecutive:

                height += 130f;
                break;

            default:

                height += 70f;
                break;
        }

        return height + 3f;
    }
}