// --------------------------------------------------------- 
// BossAttack.cs 
// 
// CreateDay: 2023/06/08
// Creator  : TakayanagiSora
// --------------------------------------------------------- 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ボスの攻撃
/// </summary>
public class BossAttack : EnemyAttack
{
    #region 変数
    [Header("火の玉攻撃の雑魚の召喚位置"), Tooltip("召喚する雑魚のスポナーを登録")]
    public List<Transform> _FG_birdSpawnPlaces = new List<Transform>();

    [Header("火の玉攻撃の出現位置"), Tooltip("FireGatlingのスポナーのTransform情報")]
    public Transform _FG_spawnPlace = default;


    [Tooltip("攻撃間の休憩秒数")]
    private WaitForSeconds _FG_attackIntervalWait = default;

    [Tooltip("連続攻撃の間隔")]
    private WaitForSeconds _FG_attackRequiredWait = default;

    [Tooltip("召喚する/した雑魚の数")]
    private int _FG_numberOfBirds = default;

    [Header("パラメータ"), SerializeField, Tooltip("火の玉攻撃の攻撃回数")]
    private int _FG_numberOfAttack = 5;

    // デバッグ用↓↓↓
    [SerializeField, Tooltip("攻撃間の休憩秒数")]
    private float _FG_attackIntervalTime = 1.5f;

    [SerializeField, Tooltip("攻撃1セットの所要時間")]
    private float _FG_attackRequiredTime = 3f;


    [Tooltip("火の玉の最大数")]
    private int MAX_FIRE_BALL = 9;
    #endregion


    protected override void Start()
    {
        // PoolSystemのGetComponent
        base.Start();

        // 「召喚した雑魚の数」に、登録したスポナーリストの要素数を代入
        _FG_numberOfBirds = _FG_birdSpawnPlaces.Count;

        // 設定した休憩秒数をWaitForSecondsに代入
        _FG_attackIntervalWait = new WaitForSeconds(_FG_attackIntervalTime);

        // 設定した攻撃の所要時間から、一回ごとの値を計算してWaitForSecondsに代入
        _FG_attackRequiredWait = new WaitForSeconds(_FG_attackRequiredTime / _FG_numberOfAttack);
    }


    /// <summary>
    /// 火の玉攻撃のための特殊な雑魚を召喚する
    /// </summary>
    public void SpawnBirdsForFireGatling()
    {
        BirdStats birdStats;

        // 雑魚を召喚する
        for (int i = 0; i < _FG_numberOfBirds; i++)
        {
            // プールから鳥を取り出し、コンポーネントを取得
            birdStats = _objectPoolSystem.CallObject(PoolEnum.PoolObjectType.bird, _FG_birdSpawnPlaces[i].position, Quaternion.Euler(Vector3.up * 180f)).GetComponent<BirdStats>();

            // 取り出した鳥のbool変数をtrueにする
            birdStats.IsSummmon = true;

            // 取り出した鳥のデリゲート変数に、デクリメントするメソッドを登録
            birdStats._onDeathBird = DecrementFG_NumberOfBird;
        }

        // 火の玉攻撃をスタートさせる
        StartCoroutine(FireGatling());
    }

    /// <summary>
    /// ボス：火の玉攻撃（FG）
    /// <para>発動条件：残りHP40％以下</para>
    /// </summary>
    /// <returns></returns>
    private IEnumerator FireGatling()
    {
        // 一度にスポーンする火の玉の数
        int numberOfFireBall;

        // 召喚した雑魚が0になるまで攻撃を続ける
        while (_FG_numberOfBirds > 0)
        {
            // 火の玉の数を設定
            numberOfFireBall = _FG_numberOfBirds;

            // 火の玉の最大数にクランプ
            if (numberOfFireBall > MAX_FIRE_BALL)
            {
                numberOfFireBall = MAX_FIRE_BALL;
            }

            // 火の玉の数が偶数だったら1増やす
            if (numberOfFireBall % 2 == 0)
            {
                numberOfFireBall++;
            }

            // 攻撃回数が決められた値に到達するまで攻撃を続ける
            for (int i = 0; i < _FG_numberOfAttack; i++)
            {
                // 連続攻撃間のブレイク
                yield return _FG_attackRequiredWait;

                // 火の玉をスポーンさせる
                SpawnEAttackFanForm(PoolEnum.PoolObjectType.fireBullet, _FG_spawnPlace, numberOfFireBall);
            }

            // 攻撃間のブレイク
            yield return _FG_attackIntervalWait;
        }

        // 自爆処理
        X_Debug.Log("撃破！");
    }

    /// <summary>
    /// FireGatlingで召喚した雑魚のデクリメント処理
    /// </summary>
    private void DecrementFG_NumberOfBird()
    {
        // 排他制御
        lock (this.gameObject)
        {
            // 変数のデクリメント
            _FG_numberOfBirds--;
        }
    }
}