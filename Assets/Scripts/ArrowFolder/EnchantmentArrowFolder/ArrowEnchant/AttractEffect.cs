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
            Debug.LogError("AttractEffect‚ªŒ©‚Â‚©‚ç‚È‚¢");
        }

        _attractMain = _attractEffect.main;

    }
    public void AttractEffectEffect_Normal()
    {

        _attractEffect.Play();
        _attractMain.startColor = color.list[0].attractColor;
    }


    public void AttractEffectEffect_Bomb()
    {

        _attractEffect.Play();
        _attractMain.startColor = color.list[1].attractColor;
    }





    public void AttractEffectEffect_Thunder()
    {
        _attractEffect.Play();
        _attractMain.startColor = color.list[2].attractColor;
    }





    public void AttractEffectEffect_KnockBack()
    {
        _attractEffect.Play();
        _attractMain.startColor = color.list[3].attractColor;
    }





    public void AttractEffectEffect_Homing()
    {
        _attractEffect.Play();
        _attractMain.startColor = color.list[4].attractColor;
    }






    public void AttractEffectEffect_Penetrate()
    {
        _attractEffect.Play();
        _attractMain.startColor = color.list[5].attractColor;
    }





    public void AttractEffectEffect_BombThunder()
    {
        _attractEffect.Play();
        _attractMain.startColor = color.list[6].attractColor;
    }






    public void AttractEffectEffect_BombKnockBack()
    {
        _attractEffect.Play();
        _attractMain.startColor = color.list[7].attractColor;
    }






    public void AttractEffectEffect_BombHoming()
    {
        _attractEffect.Play();
        _attractMain.startColor = color.list[8].attractColor;
    }






    public void AttractEffectEffect_BombPenetrate()
    {
        _attractEffect.Play();
        _attractMain.startColor = color.list[9].attractColor;
    }






    public void AttractEffectEffect_ThunderKnockBack()
    {
        _attractEffect.Play();
        _attractMain.startColor = color.list[10].attractColor;
    }






    public void AttractEffectEffect_ThunderHoming()
    {
        _attractEffect.Play();
        _attractMain.startColor = color.list[11].attractColor;
    }






    public void AttractEffectEffect_ThunderPenetrate()
    {
        _attractEffect.Play();
        _attractMain.startColor = color.list[12].attractColor;
    }






    public void AttractEffectEffect_KnockBackHoming()
    {
        _attractEffect.Play();
        _attractMain.startColor = color.list[13].attractColor;
    }






    public void AttractEffectEffect_KnockBackPenetrate()
    {
        _attractEffect.Play();
        _attractMain.startColor = color.list[14].attractColor;
    }





    public void AttractEffectEffect_HomingPenetrate()
    {
        _attractEffect.Play();
        _attractMain.startColor = color.list[15].attractColor;
    }


    #endregion
}