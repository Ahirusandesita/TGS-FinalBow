// --------------------------------------------------------- 
// ArrowPassiveEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Nomura
// --------------------------------------------------------- 
using System.Collections.Generic;
using UnityEngine;

public static class ConeDecision
{
    /// <summary>
    /// �~���̒��ɂ��邩����
    /// </summary>
    /// <param name="myTransform">������Transform</param>
    /// <param name="targetTransform">�^�[�Q�b�g��Transform</param>
    /// <param name="angle">�~���̊p�x</param>
    /// <param name="size">�~���̑傫��</param>
    /// <param name="sign">�����@Z�� 1��-1</param>
    /// <returns></returns>
    public static bool ConeInObject(Transform myTransform,Transform targetTransform,float angle,float size,int sign)
    {
        int direction = default;
        if (sign > 0)
        {
            direction = 1;
        }
        else if(sign < 0)
        {
            direction = -1;
        }
        else
        {
            return false;
        }
        Vector3 dir = targetTransform.position - myTransform.position;

        //����
        float distance = dir.magnitude;

        //cos��/2
        float cosHalf = Mathf.Cos(angle / 2 * Mathf.Deg2Rad);

        //�����ƃ^�[�Q�b�g�̌����̓���
        float inner = Vector3.Dot(direction * myTransform.forward, dir.normalized);

        return (inner > cosHalf && distance < size);
    }
    /// <summary>
    /// �~���̒��ɂ��邩���f
    /// </summary>
    /// <param name="myTransform"></param>
    /// <param name="targetTransforms"></param>
    /// <param name="angle"></param>
    /// <param name="size"></param>
    /// <param name="sign"></param>
    /// <returns></returns>
    public static List<GameObject> ConeInObjects(Transform myTransform, List<GameObject> targetTransforms, float angle, float size, int sign)
    {
        int direction = default;
        if (sign > 0)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }
        List<GameObject> hitCone= new List<GameObject>();
        for (int i = 0; i < targetTransforms.Count; i++)
        {
            Vector3 dir = targetTransforms[i].transform.position - myTransform.position;

            //����
            float distance = dir.magnitude;

            //cos��/2
            float cosHalf = Mathf.Cos(angle / 2 * Mathf.Deg2Rad);

            //�����ƃ^�[�Q�b�g�̌����̓���
            float inner = Vector3.Dot(direction * myTransform.forward, dir.normalized);

            if (inner > cosHalf && distance < size)
            {
                hitCone.Add(targetTransforms[i].gameObject);
            }
        }
        return hitCone;
    }
    
    /// <summary>
    /// �~����̔�������郁�\�b�h�@Mathf���g�p�@�덷�傫���@����ALL2���g���Ȃ������Ƃ��p
    /// </summary>
    /// <param name="myTransform">�~���𔭐�������n�_</param>
    /// <param name="targetTransforms">����Ώۂ���ꂽ���X�g</param>
    /// <param name="angle">�~���̕�</param>
    /// <param name="size">�~���̉��s�@�g��Ȃ��\��������</param>
    /// <param name="sign">�����@���s�Ӗ��͂Ȃ�</param>
    /// <returns></returns>
    public static List<GameObject> ConeSearchAll(Transform myTransform, List<GameObject> targetTransforms, float angle/*��*/, float size/*���s*/, int sign)
    {
        Vector3 bowVect;
        bowVect = myTransform.rotation.eulerAngles.normalized;
        List<GameObject> hitCone = new List<GameObject>();
        for (int i = 0; i < targetTransforms.Count; i++)
        {
            float distance;
            distance = Vector3.Distance(myTransform.position, targetTransforms[i].transform.position);

            Transform corePosition = default;
            corePosition.position = new Vector3(bowVect.x * distance, bowVect.y * distance, bowVect.z * distance);

            float radius;
            radius = distance * angle;

            float objectdistance;
            objectdistance = Vector3.Distance(corePosition.position,targetTransforms[i].transform.position);
            
            if (objectdistance < radius)
            {
                hitCone.Add(targetTransforms[i].gameObject);
            }
        }
        return hitCone;
    }

    /// <summary>
    /// �~����̔�������郁�\�b�h�@Mathf���g�p�@
    /// �덷�͏��Ȃ��������͏����d���\������
    /// </summary>
    /// <param name="myTransform">�~���𔭐�������n�_</param>
    /// <param name="targetTransforms">����Ώۂ���ꂽ���X�g</param>
    /// <param name="angle">�~���̕�</param>
    /// <param name="size">�~���̉��s�@�g��Ȃ��\��������</param>
    /// <param name="sign">�����@���s�g��Ȃ��\��������</param>
    /// <returns></returns>
    public static List<GameObject> ConeSearchALL2(Transform myTransform, List<GameObject> targetTransforms, float angle/*��*/, float size/*���s*/, int sign)
    {
        Vector3     bowVect;    // �|�������Ă���x�N�g�����m�[�}���C�Y�����l
        Vector3     objVect;    // �|���^�[�Q�b�g�Ԃ̃x�N�g�����m�[�}���C�Y�����l
        Vector3     diffVect;   // objVect - bowVect�@�̒l
        Quaternion  objRot;     // �|���^�[�Q�b�g�Ԃ̃x�N�g���p�x
        float       radius;     // ���蔼�a�̌��E�n�@angle�����S�p�ł���O��Ōv�Z���Ă��邽�ߒ���
        float       judge;      // ���肷�邽�߂̒l�@���̒l��radius���ł���΃��X�g�ɑ��

        // ������̃I�u�W�F�N�g�̃��X�g
        List<GameObject> hitCone = new List<GameObject>();

        // �|�������Ă�������̃x�N�g���p�x�̃m�[�}���C�Y����
        bowVect = myTransform.rotation.eulerAngles.normalized;

        // �~���̓��p��2������@�̂��̔�r�Ɏg��
        radius = (angle / 90) * (angle / 90);


        // ����J�n�@���X�g��for���Ŗԗ����Ĕ���
        for (int i = 0; i < targetTransforms.Count; i++)
        {
            // �|����^�[�Q�b�g�ւ̊p�x���擾
            objRot = Quaternion.LookRotation(targetTransforms[i].transform.position - myTransform.position);

            // �擾�����p�x����x�N�g���p�x�̃m�[�}���C�Y���擾
            objVect = objRot.eulerAngles.normalized;

            // �^�[�Q�b�g�ւ̃x�N�g���Ƌ|���g�̃x�N�g���̍������߂�
            diffVect = objVect - bowVect;

            // Z����������2�����ꂼ���2��̍��v���Z�o
            judge = (diffVect.x * diffVect.x) + (diffVect.y * diffVect.y);

            // �Z�o�����l���~���̊p�x��菬����������
            if (judge <= radius)
            {
                // ������̃I�u�W�F�N�g�����X�g�ɑ��
                hitCone.Add(targetTransforms[i].gameObject);
            }
        }

        return hitCone;
    }

    /// <summary>
    /// �~����̔�������郁�\�b�h�@Mathf���g�p
    /// �����Ă���p�x�ɍł��߂���̃I�u�W�F�N�g���T�[�`
    /// </summary>
    /// <param name="myTransform">�~���𔭐�������n�_</param>
    /// <param name="targetTransforms">����Ώۂ���ꂽ���X�g</param>
    /// <param name="angle">�~���̕�</param>
    /// <returns></returns>
    public static GameObject ConeSearchNearest(Transform myTransform, List<GameObject> targetTransforms, float angle/*��*/ /*, float size���s*/)
    {
        Vector3 bowVect;    // �|�������Ă���x�N�g�����m�[�}���C�Y�����l
        Vector3 objVect;    // �|���^�[�Q�b�g�Ԃ̃x�N�g�����m�[�}���C�Y�����l
        Vector3 diffVect;   // objVect - bowVect�@�̒l
        Quaternion objRot;  // �|���^�[�Q�b�g�Ԃ̃x�N�g���p�x
        float radius;       // �ł��߂��I�u�W�F�N�g�Ƃ̊p�x�@�ŏ��͔��肷��~���̊p�x
        float judge;        // ���肷�邽�߂̒l�@���̒l��radius���ł���΃��X�g�ɑ��

        // �ł��߂��I�u�W�F�N�g
        GameObject mostNearObject = default;

        // �|�������Ă�������̃x�N�g���p�x�̃m�[�}���C�Y����
        bowVect = myTransform.rotation.eulerAngles.normalized;

        // �~���̓��p��2������@�̂��̔�r�Ɏg��
        radius = (angle / 90) * (angle / 90);


        // ����J�n�@���X�g��for���Ŗԗ����Ĕ���
        for (int i = 0; i < targetTransforms.Count; i++)
        {
            // �|����^�[�Q�b�g�ւ̊p�x���擾
            objRot = Quaternion.LookRotation(targetTransforms[i].transform.position - myTransform.position);

            // �擾�����p�x����x�N�g���p�x�̃m�[�}���C�Y���擾
            objVect = objRot.eulerAngles.normalized;

            // �^�[�Q�b�g�ւ̃x�N�g���Ƌ|���g�̃x�N�g���̍������߂�
            diffVect = objVect - bowVect;

            // Z����������2�����ꂼ���2��̍��v���Z�o
            judge = (diffVect.x * diffVect.x) + (diffVect.y * diffVect.y);

            // �Z�o�����l���~���̊p�x��菬����������
            if (judge <= radius)
            {
                // �ł��߂��p�x��ύX����
                radius = judge;

                // ������̃I�u�W�F�N�g�����X�g�ɑ��
                mostNearObject = targetTransforms[i];
            }
        }

        return mostNearObject;
    }

}
