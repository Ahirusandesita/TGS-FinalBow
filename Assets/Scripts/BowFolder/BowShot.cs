// --------------------------------------------------------- 
// ArrowPassiveEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Sasaki
// --------------------------------------------------------- 
using UnityEngine;

// ���������g���
public class BowShot : MonoBehaviour
{
    

    //�f�o�b�N�p
    public PlayerManager playerManager;
    /// <summary>
    /// ���͂Ŏˏo�J�n
    /// </summary>
    /// <param name="aim">�ˏo���������x�N�g��������LookRotation�g���Ȃ�transform.position������</param>
    public void StartShotArrow(Vector3 aim)
    {
        // �������Ȃ񂩂�����
        //GameObject obj=  Instantiate(debug, transform.position, Quaternion.LookRotation(aim + transform.position));
        //playerManager.ShotArrow(aim);
    }

   
}
