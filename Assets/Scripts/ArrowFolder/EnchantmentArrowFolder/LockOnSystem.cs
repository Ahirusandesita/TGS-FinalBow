// --------------------------------------------------------- 
// LockOnSystem.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;

public interface IFLockOnSystem
{
    GameObject LockOnTarget { get; set; }
    void TargetLockOn(Vector3 bowPosition , Vector3 bowRotation);

}

public class LockOnSystem : MonoBehaviour , IFLockOnSystem
{
    #region variable 
    public GameObject LockOnTarget { get; set; }

    private GameObject _temporaryTarget = default;

    private ArrowEnchantment _enchant = default;
    #endregion
    #region property
    #endregion
    #region method

    private void Start()
    {
        _enchant = GameObject.FindGameObjectWithTag("ArrowEnchantmentController").GetComponent<ArrowEnchantment>();
    }

    public void TargetLockOn(Vector3 bowPosition , Vector3 bowRotation)
    {
        //if(_enchant._enchantmentStateNow == EnchantmentEnum.EnchantmentState.homing)
        //{

        //    LockOnTarget = gameObject; // ロックオンするオブジェクトをセット
        //}
    }

    public GameObject TargetSet(ArrowMove arrow)
    {
        return LockOnTarget;
    }
 #endregion
}