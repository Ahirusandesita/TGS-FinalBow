// --------------------------------------------------------- 
// AttractEffectCustam.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;

public interface IFAttractEffectCustom
{
    void SetActive(bool set);

    void SetEffectSize(float size);

}

public class AttractEffectCustom : MonoBehaviour,IFAttractEffectCustom
{
    #region variable 

    [SerializeField] GameObject _particleObject = default;

    ParticleSystem _particleSystem = default;

    ParticleSystem.Particle _particle = default;

    Vector3 startRootSize = default;

    ParticleSystem.MainModule _particleColor = default;


    #endregion
    #region property
    #endregion
    #region method

    private void Start()
    {
        _particleSystem = _particleObject.GetComponent<ParticleSystem>();

        _particleColor = _particleSystem.main;

        startRootSize = _particle.startSize3D;
    }

    public void SetActive(bool set) => _particleObject.SetActive(set);

    public void SetEffectSize(float size)
    {
        if(size < 0.1f)
        {
            size = 0.1f;
        }

        _particle.startSize3D = startRootSize * size;

        //_particleColor.startColor = Color.white * size;



    }
    #endregion
}