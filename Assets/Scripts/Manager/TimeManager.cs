// --------------------------------------------------------- 
// ArrowPassiveEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Nomura
// --------------------------------------------------------- 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
interface ITime
{
    /// <summary>
    /// �c�莞��
    /// </summary>
    /// <returns></returns>
    float TimeCounter();
}
public class TimeManager : ITime
{
    public float TimeCounter()
    {
        //�L���b�`�̂Ƃ��Ƃ��ɌĂ�
        return 1;
    }




}
