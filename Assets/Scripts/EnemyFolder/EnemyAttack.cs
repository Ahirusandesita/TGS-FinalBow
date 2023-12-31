// --------------------------------------------------------- 
// EnemyAttack.cs 
// 
// CreateDay: 2023/06/08
// Creator  : TakayanagiSora
// --------------------------------------------------------- 
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Tooltip("タグの名前")]
    public TagObject _PoolSystemTagData = default;


    [Tooltip("取得したObjectPoolSystemクラス")]
    protected ObjectPoolSystem _objectPoolSystem = default;

    [Tooltip("弾の間隔の角度")]
    private const float SPACE_ANGLE_COEFFICIENT = 8.1f;


    protected virtual void Start()
    {
        _objectPoolSystem = GameObject.FindWithTag(_PoolSystemTagData.TagName).GetComponent<ObjectPoolSystem>();
    }

    /// <summary>
    /// 敵の攻撃を扇状にスポーンする
    /// </summary>
    /// <param name="spawnObjectType">呼び出すオブジェクトの種類</param>
    /// <param name="spawnPlace">スポーンさせるオブジェクトのTransform情報</param>
    /// <param name="numberOfSpawn">一度にスポーンする量</param>
    protected virtual void SpawnEAttackFanForm(PoolEnum.PoolObjectType spawnObjectType, Transform spawnPlace, int numberOfSpawn)
    {
        // 一度のスポーン量分、繰り返す
        for (int i = 0; i < numberOfSpawn; i++)
        {
            // 扇状の範囲に、渡された数を均等間隔で配置する
            // スポーン位置は同じで、角度だけを変える
            // 扇の範囲はスポーン量によって小さくする
            _objectPoolSystem.CallObject(
                spawnObjectType,
                spawnPlace.position,
                Quaternion.Euler(
                    spawnPlace.eulerAngles.x, 
                    spawnPlace.eulerAngles.y + (SPACE_ANGLE_COEFFICIENT * (numberOfSpawn / 2)) - i * SPACE_ANGLE_COEFFICIENT, 
                    spawnPlace.eulerAngles.z));
        }
    }

    /// <summary>
    /// 敵の攻撃をひとつだけスポーンさせる
    /// </summary>
    /// <param name="spawnObjectType">呼び出すオブジェクトの種類</param>
    /// <param name="spawnPlace">スポーンさせるオブジェクトのTransform情報</param>
    protected virtual void SpawnEAttackOne(PoolEnum.PoolObjectType spawnObjectType, Transform spawnPlace)
    {
        _objectPoolSystem.CallObject(spawnObjectType, spawnPlace.position);
    }
}