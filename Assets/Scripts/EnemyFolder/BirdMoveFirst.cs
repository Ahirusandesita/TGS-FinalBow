// --------------------------------------------------------- 
// BirdMoveFirst.cs 
// 
// CreateDay: 2023/06/21
// Creator  : TakayanagiSora
// --------------------------------------------------------- 
using UnityEngine;

public class BirdMoveFirst : BirdMoveBase
{
    protected override void EachMovement(ref float movedDistance)
    {
        // �I����������
        base.EachMovement(ref movedDistance);
        
        // �ړ�����i�ړ������̃x�N�g�� * �ړ����x�j
        _transform.Translate((_goalPosition - _startPosition).normalized * _movementSpeed * Time.deltaTime, Space.World); �@// �������Ȃ��ƃo�O��
        // �ړ��ʂ����Z
        movedDistance += ((_goalPosition - _startPosition).normalized * _movementSpeed * Time.deltaTime).magnitude;
    }
}