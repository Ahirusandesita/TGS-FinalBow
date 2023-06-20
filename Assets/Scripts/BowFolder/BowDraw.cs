// --------------------------------------------------------- 
// ArrowPassiveEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Sasaki
// --------------------------------------------------------- 
using UnityEngine;

interface IFBowDraw
{
    /// <summary>
    /// �|����������
    /// </summary>
    /// <param name="handPosition">��̈ʒu</param>
    /// <param name="drawObject">�����I�u�W�F�N�g</param>
    /// <returns>����������</returns>
    public float BowDrawing(Vector3 handPosition, GameObject drawObject, Vector3 firstPosition);

    /// <summary>
    /// �|����������������ʂ̏���
    /// </summary>
    //void BowDrawingSlow();

    /// <summary>
    /// �|������������ʂ̏���
    /// </summary>
   //void BowDrawingFast();

    /// <summary>
    /// �x�������~�b�N�X(��)
    /// </summary>
    //void FastSlowMix();
}
public class BowDraw : MonoBehaviour,IFBowDraw
{   
    /// <summary>
    /// �|����������
    /// </summary>
    /// <param name="handPosition">��̈ʒu</param>
    /// <param name="drawObject">�����I�u�W�F�N�g</param>
    /// <returns>����������</returns>
    public float BowDrawing(Vector3 handPosition,GameObject drawObject,Vector3 firstPosition)
    {
        drawObject.transform.position = handPosition;
        
        return Vector3.Distance(firstPosition, drawObject.transform.localPosition);
    }

   
}
