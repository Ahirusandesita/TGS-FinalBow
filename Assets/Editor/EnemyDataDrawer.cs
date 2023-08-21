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

            // _moveType�i�����̎�ށj---------------------------------------------------------------------------------------------------------------------------------------------

            Rect moveTypeRect = new(position)
            {
                y = position.y
            };

            var moveTypeProperty = property.FindPropertyRelative("_moveType");
            moveTypeProperty.enumValueIndex = EditorGUI.Popup(moveTypeRect, "�����̎��", moveTypeProperty.enumValueIndex, Enum.GetNames(typeof(MoveType)));

            // _birdGoalPlace�i�S�[���j--------------------------------------------------------------------------------------------------------------------------------------------

            Rect goalPlaceRect = new(moveTypeRect)
            {
                y = moveTypeRect.y + EditorGUIUtility.singleLineHeight + 3f
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

            // _stayTime_s�i��~���čU������b���j---------------------------------------------------------------------------------------------------------------------------------

            Rect stayTimeRect = new(speedRect)
            {
                y = speedRect.y + EditorGUIUtility.singleLineHeight + 3f
            };

            var stayTimeProperty = property.FindPropertyRelative("_stayTime_s");
            stayTimeProperty.floatValue = EditorGUI.FloatField(stayTimeRect, "��~���čU������b��", stayTimeProperty.floatValue);


            // MoveType�ɂ���ĕ\����؂�ւ�

            switch ((MoveType)moveTypeProperty.enumValueIndex)
            {
                case MoveType.linear:
                    break;

                case MoveType.curve:

                    // _arcHeight�i�ʂ̍����j-------------------------------------------------------------------------------------------------------------------------------------

                    Rect arcHieghtRect = new(stayTimeRect)
                    {
                        y = stayTimeRect.y + EditorGUIUtility.singleLineHeight + 3f
                    };

                    var arcHieghtProperty = property.FindPropertyRelative("_arcHeight");
                    arcHieghtProperty.floatValue = EditorGUI.Slider(arcHieghtRect, "�ʂ̍���", arcHieghtProperty.floatValue, 1f, 30f);

                    // _arcMoveDirection�i�ʂ̌����j------------------------------------------------------------------------------------------------------------------------------

                    Rect arcMoveDirectionRect = new(arcHieghtRect)
                    {
                        y = arcHieghtRect.y + EditorGUIUtility.singleLineHeight + 3f
                    };

                    var arcMoveDirectionProperty = property.FindPropertyRelative("_arcMoveDirection");
                    arcMoveDirectionProperty.enumValueIndex = EditorGUI.Popup(arcMoveDirectionRect, "�ʂ̌���", arcMoveDirectionProperty.enumValueIndex, Enum.GetNames(typeof(ArcMoveDirection)));

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