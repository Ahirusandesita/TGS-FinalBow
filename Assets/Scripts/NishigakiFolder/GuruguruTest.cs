// --------------------------------------------------------- 
// ArrowPassiveEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Nishigaki
using UnityEngine;

public class GuruguruTest : MonoBehaviour
{
    #region　bool一覧
    public bool StartFlag = false;                  //移動を開始するフラグ　trueなら移動　falseなら停止　使うときにtrueにしてね
    private bool unionFlag = false;                 //初期半径から周回軌道半径に合流したかどうかを判定するフラグ　合流したらtrueにする
    #endregion

    #region　Unity変数一覧
    private Vector3 nowPosition = default;          //現在座標の３軸　localtransfarmを直接設定するために使用
    private Vector3 startPos = default;             //移動開始地点　使うかどうかは不明
    private Transform goalPos = default;            //移動目標の座標　最終的に到着する場所
    #endregion

    #region　float変数一覧　グロ注意

    private float attract_Power = 1f;              //調整用変数　引き寄せる力の大きさ　調整出来たらATTRACT_SPEEDに変更

    private float distance = default;               //距離　対象と目標の距離　各軸の差の絶対値の合計
                                                    //周回半径を求めるために使用
                                                    //注意事項としては"半径"ではなく"対象物との距離"
    private float startAngle = default;             //開始時の中心軸との角度　三角関数の初期値を決めるのに使用
                                                    // ０ ≦ startAngle ＜ ３６０　マイナスにならないように補正する必要あり
    private float startValue = default;             //初期値n　三角関数の初期位置を設定するのに使用
                                                    //式： startAngle ÷ 360 × 2π
                                                    //三角関数の経過量を０～１で表した値
    private float radius = default;                 //半径r 中点との距離　今回は雑に三平方で
                                                    //周回軌道半径へ合流するために使用
                                                    //値はlocalで取る
    private float addRadius = default;              //半径加算値　初期半径から周回軌道上に乗せるための加算値
                                                    //元の半径に加算することで、徐々に周回軌道へ広げていく
    private float difRadius = default;              //半径のずれ　初期半径と周回軌道半径の差　addRadiusの上限値
                                                    //Lerpの目標値として設定する
    private float time_AddRadius = default;         //半径修正の経過時間　lerpで使用  経過時間÷LERP_TIME_ADDRADIUS
    private float trajectory = default;             //周回軌道半径　距離×距離係数　距離ごとの想定軌道円周
    private float moveValue_x = default;            //X軸の移動値
    private float moveValue_y = default;            //Y軸の移動値
    private float lerp_Time_AddRadius = default;    //初期半径から周回軌道に乗せるまでの時間  目標に到達する時間の半分
    #endregion

    #region　float定数一覧
    const float ZERO = 0f;                          //０　ゼロ　ただのゼロ　ニュース番組ではない
    const float ALL_ROUND = 360f;                   //３６０°　一回転　でやぁぁぁぁああ！
    const float CUT = 0.7f;                         //０．７倍　それだけ
    const float COEF_DIST_RADI = 0.1f;              //距離係数　距離によって半径を決めるための係数
    const float PERIOD_VALUE = Mathf.PI * 1f;       //回転周期　π×秒数で指定　なお誤差あり　ゴミやん
    const float straightRange = 0f;                 //回転せずにまっすぐ飛んでくる距離　０でずっと回る　
                                                    //NでlocaltransfarmがNの地点まで回りNからはまっすぐ飛んでくる
    const float ATTRACT_SPEED = default;            //引き寄せる速度　最初から最高速かつ等速で引き寄せる　
                                                    //今は調整用にattract_Powerにしているけど、最終的にはこっちにしてほしい
    #endregion

    /// <summary>
    /// 周回運動の初期設定　最初に一回だけ呼ぶ
    /// </summary>
    private void MoveSetUp()
    {
        //初期位置の代入と計算
        goalPos = transform.parent;
        startPos = transform.localPosition;
        startAngle = Mathf.Atan2(startPos.x, startPos.y) * Mathf.Rad2Deg;

        //距離を代入
        distance = startPos.z;

        //初期半径から周回軌道上に乗せるための時間を設定
        lerp_Time_AddRadius = distance / attract_Power * CUT;

        //初期角度を　０～３６０に補正
        if (startAngle < 0)
        {
            startAngle += 360;
        }

        //初期値nの計算
        startValue = startAngle / ALL_ROUND * Mathf.PI * 2f;

        //半径計算
        radius = Mathf.Sqrt(Mathf.Pow(transform.localPosition.x, 2f) + Mathf.Pow(transform.localPosition.y, 2f));

    }

    /// <summary>
    /// 周回運動メソッド　これをアップデートで回せばできるはず
    /// </summary>
    private void ItemAttract()
    {
        if (!StartFlag)
        {
            MoveSetUp();
        }
        //フラグがtrueになったらスタート
        if (StartFlag)
        {
            //周回軌道円周計算
            trajectory = Mathf.Clamp((distance - straightRange) * COEF_DIST_RADI, ZERO, Mathf.Infinity);

            //各軸移動値計算
            //周回軌道上に乗った後の動き
            if (unionFlag)
            {
                //軌道上を周回運動　各軸を直に指定
                moveValue_x = Mathf.Sin(startValue + (Time.time) * PERIOD_VALUE) * trajectory;
                moveValue_y = Mathf.Cos(startValue + (Time.time) * PERIOD_VALUE) * trajectory;
            }
            //周回軌道へ移動しつつ回転するときの動き
            else
            {
                //初期半径と周回軌道の差を算出
                difRadius = trajectory - radius;

                //初期半径からの加算値を算出
                addRadius = Mathf.Lerp(ZERO, difRadius, time_AddRadius);
                //Lerpに使う時間計算
                time_AddRadius += (Time.deltaTime) / lerp_Time_AddRadius;

                //各軸の位置を指定
                moveValue_x = Mathf.Sin(startValue + (Time.time) * PERIOD_VALUE) * (radius + addRadius);
                moveValue_y = Mathf.Cos(startValue + (Time.time) * PERIOD_VALUE) * (radius + addRadius);

                //周回軌道に乗ったら終了　時間で判定
                if (time_AddRadius > 1)
                {
                    unionFlag = true;
                }
            }

            //座標の代入処理
            nowPosition.x = moveValue_x;
            nowPosition.y = moveValue_y;
            nowPosition.z = distance;

            //位置の確定
            transform.localPosition = nowPosition;

            //距離を縮める処理
            //距離が０じゃないなら引き寄せ
            if (distance > 0)
            {
                distance -= attract_Power * Time.deltaTime;
            }
            //距離が０になったら移動終了およびオブジェクトの消失
            else
            {
                StartFlag = false;
                gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// オブジェクトを引き寄せる速度を設定するメソッド
    /// </summary>
    /// <param name="set"></param>
    public void SetAttractPower(float set)
    {
        attract_Power = set;
    }

}
