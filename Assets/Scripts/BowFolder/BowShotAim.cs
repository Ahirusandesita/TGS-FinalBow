// --------------------------------------------------------- 
// ArrowPassiveEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Sasaki
// --------------------------------------------------------- 
using UnityEngine;

interface IFBowShotAim
{
    /// <summary>
    /// 前方の方向ベクトルを返す
    /// </summary>
    /// <returns></returns>
    Vector3 GetAim();
}
public class BowShotAim : MonoBehaviour,IFBowShotAim
{
    
    /// <summary>
    /// 前方の方向ベクトルを返す
    /// </summary>
    /// <returns></returns>
    public Vector3 GetAim()
    {
        return transform.forward - transform.position;
    }

    /// <summary>
    /// ローテーション返す
    /// </summary>
    /// <returns></returns>
    public Quaternion GetRoteAim()
    {
        return transform.rotation;
    }

    private void Debug()
    {
        print(GetAim());
        GameObject obj = Instantiate(GameObject.CreatePrimitive(PrimitiveType.Sphere),transform.forward + transform.position,transform.rotation);
        obj.AddComponent<transformtansle>();
    }
    private void OnDrawGizmos()
    {
        // 前方のray見える
        //Gizmos.DrawRay(new Ray(transform.position,transform.forward));
    }
}
