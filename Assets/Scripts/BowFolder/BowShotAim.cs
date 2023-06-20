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
    /// �O���̕����x�N�g����Ԃ�
    /// </summary>
    /// <returns></returns>
    Vector3 GetAim();
}
public class BowShotAim : MonoBehaviour,IFBowShotAim
{
    
    /// <summary>
    /// �O���̕����x�N�g����Ԃ�
    /// </summary>
    /// <returns></returns>
    public Vector3 GetAim()
    {
        return transform.forward - transform.position;
    }

    /// <summary>
    /// ���[�e�[�V�����Ԃ�
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
        // �O����ray������
        //Gizmos.DrawRay(new Ray(transform.position,transform.forward));
    }
}
