// --------------------------------------------------------- 
// ArrowPassiveEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Sasaki
// --------------------------------------------------------- 
using UnityEngine;

// 多分もう使わん
public class BowShot : MonoBehaviour
{
    

    //デバック用
    public PlayerManager playerManager;
    /// <summary>
    /// 入力で射出開始
    /// </summary>
    /// <param name="aim">射出される方向ベクトルだけどLookRotation使うならtransform.position足して</param>
    public void StartShotArrow(Vector3 aim)
    {
        // うつしょりなんかかいて
        //GameObject obj=  Instantiate(debug, transform.position, Quaternion.LookRotation(aim + transform.position));
        //playerManager.ShotArrow(aim);
    }

   
}
