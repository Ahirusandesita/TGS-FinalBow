// --------------------------------------------------------- 
// FireBallMove.cs 
// 
// CreateDay: 2023/06/08
// Creator  : TakayanagiSora
// --------------------------------------------------------- 
using UnityEngine;

public class FireBallMove : EAttackMoveBase
{
    private void Start()
    {
        _transform = this.transform;

        // ボスの攻撃をプールするオブジェクトからプールを取得
        _objectPoolSystem = GameObject.FindWithTag(_PoolSystemTagData.TagName).GetComponent<ObjectPoolSystem>();

        // Return用に自分自身の情報を取得
        _cashObjectInformation = this.GetComponent<CashObjectInformation>();

        // プレイヤーから当たり判定のクラスを取得
        _playerHitZone = GameObject.FindWithTag(_PlayerControllerTagData.TagName).GetComponent<PlayerHitZone>();
    }

    private void OnEnable()
    {
        // 攻撃の生存時間を設定
        _lifeTime = 5f;

        // フラグを初期化
        _canMove = true;
    }

    private void Update()
    {
        // 動けなければ返す
        if (!_canMove)
        {
            return;
        }

        // まっすぐ飛ばす
        Straight();

        // プレイヤーにヒットしているかどうかを判定
        _playerHitZone.HitZone(_transform.position);

        // 自身の残生存時間をデクリメント
        _lifeTime -= Time.deltaTime;

        // 生存時間が0になったら消す
        if (_lifeTime <= 0f)
        {
            _objectPoolSystem.ReturnObject(_cashObjectInformation);
        }
    }
}
