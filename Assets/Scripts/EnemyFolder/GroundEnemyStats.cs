// --------------------------------------------------------- 
// GroundEnemyStats.cs 
// 
// CreateDay: 2023/07/14
// Creator  : TakayanagiSora
// --------------------------------------------------------- 


using UnityEngine;

public class GroundEnemyStats : CommonEnemyStats
{
    [Tooltip("���̃��[�������񂾂Ƃ��Ɏ��s / �u�G�̎c�����v�̃f�N�������g������o�^")]
    public OnDeathEnemy _onDeathEnemy;

    public override void Death()
    {
        _drop.DropStart(_dropData, this.transform.position);
        _onDeathEnemy();

        base.Death();
    }

    protected override void OnDeathReactions(Transform arrowTransform, Vector3 arrowVector)
    {

    }
}