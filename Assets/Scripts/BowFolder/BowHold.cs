// --------------------------------------------------------- 
// ArrowPassiveEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Sasaki
// --------------------------------------------------------- 
using UnityEngine;

interface IFBowHold
{
    /// <summary>
    /// ����͂�
    /// </summary>
    /// <param name="handPos">��̃g�����X�t�H�[��</param>
    /// <param name="drawObject">�����I�u�W�F�N�g</param>
    void BowHoldStart(Vector3 handPos, GameObject drawObject);
}
public class BowHold : MonoBehaviour,IFBowHold
{
    /// <summary>
    /// ����͂�
    /// </summary>
    /// <param name="handPos">��̃g�����X�t�H�[��</param>
    /// <param name="drawObject">�����I�u�W�F�N�g</param>
    public void BowHoldStart(Vector3 handPos,GameObject drawObject)
    {
        drawObject.transform.position = handPos;
    }
}
