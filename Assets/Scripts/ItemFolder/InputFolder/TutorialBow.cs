// --------------------------------------------------------- 
// TutorialBow.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class TutorialBow : MonoBehaviour
{
    PlayerManager player = default;
    ObjectPoolSystem pool = default;
    [SerializeField] Transform shotPosition = default;
    [SerializeField] float arrowSpeed = 10000f;
    CashObjectInformation _arrow = default;
    TutorialManager tutorial = default;

    private void Awake()
    {
        pool = FindObjectOfType<ObjectPoolSystem>();
        tutorial = FindObjectOfType<TutorialManager>();
        tutorial._onArrowMissed_Shot = (a) => TutorialShot(a);
        tutorial._onArrowMissed_Create = () => TutorialArrowCreate();
    }
    public void TutorialArrowCreate()
    {
        _arrow = pool.CallObject(PoolEnum.PoolObjectType.arrow, shotPosition.position);
    }
    /// <summary>
    /// チュートリアルショット
    /// </summary>
    public void TutorialShot(Vector3 target)
    {
        player = FindObjectOfType<PlayerManager>();

        player.SetEnchantParameter(EnchantmentEnum.ItemAttributeState.bomb);

        for (int i = 0; i < 5; i++)
        {
            player.ArrowEnchantPlusDamage();
        }

        player.SetArrowMoveSpeed(arrowSpeed);

        player.ShotArrow(Vector3.zero);

        _arrow.transform.LookAt(target);

        _arrow.GetComponent<ArrowMove>().ReSetNormalSetting();
    }

}