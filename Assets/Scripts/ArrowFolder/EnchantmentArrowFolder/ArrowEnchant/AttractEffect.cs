// --------------------------------------------------------- 
// AttractEffect.cs 
// 
// CreateDay: 2023/07/05
// Creator  : Nomura
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class AttractEffect : MonoBehaviour
{
    #region variable 
    ParticleSystem _attractEffect = default;

    ParticleSystem.MainModule _attractMain = default;

    [SerializeField] ScriptableEffectsColor color = default;
    [SerializeField] Yumi_Material_Controller controller = default;

    ParticleSystem[] _subParticles = new ParticleSystem[15];

    ObjectPoolSystem pool = default;
    #endregion
    #region property
    #endregion
    #region method
    private void Start()
    {
        Transform bow = GameObject.FindGameObjectWithTag(InhallLibTags.BowController).transform;

        foreach (Transform child in bow)
        {
            if (child.CompareTag(InhallLibTags.AttractEffect))
            {
                _attractEffect = child.GetComponent<ParticleSystem>();
            }
        }

        if (_attractEffect == null)
        {
            Debug.LogError("AttractEffectが見つからない");
        }

        _attractMain = _attractEffect.main;

        // エフェクト取得

        pool = GameObject.FindGameObjectWithTag(InhallLibTags.PoolSystem).GetComponent<ObjectPoolSystem>();

        if(controller == null)
        {
            controller = GameObject.FindGameObjectWithTag(InhallLibTags.MaaterialController).GetComponent<Yumi_Material_Controller>();
        }

        int firstNum = (int)EffectPoolEnum.EffectPoolState.newEnchantEffectBomb;

        Vector3 pos = _attractEffect.transform.position;

        for (int i = 0; i < _subParticles.Length; i++)
        {
            _subParticles[i] = pool.CallObject((EffectPoolEnum.EffectPoolState)firstNum + i, pos).GetComponent<ParticleSystem>();
            _subParticles[i].gameObject.SetActive(false);
            _subParticles[i].transform.parent = bow;
            _subParticles[i].transform.localScale = Vector3.one;
            _subParticles[i].transform.localPosition = new Vector3(0f, -4.43620443f, 20.5781441f);
        }
    }
    public void AttractEffectEffect_Normal()
    {
        PlayEffect(0);
    }


    public void AttractEffectEffect_Bomb()
    {
        PlayEffect(1);
    }



    public void AttractEffectEffect_Thunder()
    {
        PlayEffect(2);
    }





    public void AttractEffectEffect_KnockBack()
    {
        PlayEffect(3);
    }





    public void AttractEffectEffect_Homing()
    {
        PlayEffect(4);
    }






    public void AttractEffectEffect_Penetrate()
    {
        PlayEffect(5);
    }





    public void AttractEffectEffect_BombThunder()
    {
        PlayEffect(6);
    }






    public void AttractEffectEffect_BombKnockBack()
    {
        PlayEffect(7);
    }






    public void AttractEffectEffect_BombHoming()
    {
        PlayEffect(8);
    }






    public void AttractEffectEffect_BombPenetrate()
    {
        PlayEffect(9);
    }






    public void AttractEffectEffect_ThunderKnockBack()
    {
        PlayEffect(10);
    }






    public void AttractEffectEffect_ThunderHoming()
    {
        PlayEffect(11);
    }






    public void AttractEffectEffect_ThunderPenetrate()
    {
        PlayEffect(12);
    }






    public void AttractEffectEffect_KnockBackHoming()
    {
        PlayEffect(13);
    }






    public void AttractEffectEffect_KnockBackPenetrate()
    {
        PlayEffect(14);
    }





    public void AttractEffectEffect_HomingPenetrate()
    {
        PlayEffect(15);
    }


    private void PlayEffect(int num)
    {
        _attractEffect.Play();
        _attractMain.startColor = color.list[num].attractColor;
        // マテリアル変更
        controller.ChangeMaterialProcess((EnchantmentEnum.EnchantmentState)num);
        if (num > 0)
        {
            _subParticles[num - 1].gameObject.SetActive(true);
            _subParticles[num - 1].Play();
           
        }
            
        
    }
    #endregion
}