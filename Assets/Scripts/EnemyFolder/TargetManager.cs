// --------------------------------------------------------- 
// TargetManager.cs 
// 
// CreateDay: 2023/06/16
// Creator  : TakayanagiSora
// --------------------------------------------------------- 
using UnityEngine;

[RequireComponent(typeof(TargetStats), typeof(TargetMove), typeof(CashObjectInformation))]
public class TargetManager : MonoBehaviour
{
    [Tooltip("Žæ“¾‚µ‚½TargetStatsƒNƒ‰ƒX")]
    private TargetStats _targetStats = default;

    [SerializeField]
    private DropData dropData;

    private Drop drop;


    private void Start()
    {
        _targetStats = this.GetComponent<TargetStats>();
        drop = GameObject.FindObjectOfType<Drop>();
    }

    private void Update()
    {
        if (_targetStats.HP <= 0)
        {
            drop.DropStart(dropData, this.transform.position);
            //_targetStats.Death();
        }
    }
}