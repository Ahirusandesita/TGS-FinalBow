// --------------------------------------------------------- 
// TargetManager.cs 
// 
// CreateDay: 2023/06/16
// Creator  : TakayanagiSora
// --------------------------------------------------------- 
using UnityEngine;

[RequireComponent(typeof(TargetStats), typeof(CashObjectInformation))]
public class TargetManager : MonoBehaviour
{
    [Tooltip("�擾����TargetStats�N���X")]
    private TargetStats _targetStats = default;


    private void Start()
    {
        _targetStats = this.GetComponent<TargetStats>();
    }

    private void Update()
    {
        if (_targetStats.HP <= 0)
        {
            _targetStats.Death();
        }
    }

    private void Move()
    {

    }
}