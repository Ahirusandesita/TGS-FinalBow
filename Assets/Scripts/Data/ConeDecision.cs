// --------------------------------------------------------- 
// ArrowPassiveEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Nomura
// --------------------------------------------------------- 
using System.Collections.Generic;
using UnityEngine;

public static class ConeDecision
{
    /// <summary>
    /// 円錐の中にいるか判定
    /// </summary>
    /// <param name="myTransform">自分のTransform</param>
    /// <param name="targetTransform">ターゲットのTransform</param>
    /// <param name="angle">円錐の角度</param>
    /// <param name="size">円錐の大きさ</param>
    /// <param name="sign">向き　Z軸 1か-1</param>
    /// <returns></returns>
    public static bool ConeInObject(Transform myTransform,Transform targetTransform,float angle,float size,int sign)
    {
        int direction = default;
        if (sign > 0)
        {
            direction = 1;
        }
        else if(sign < 0)
        {
            direction = -1;
        }
        else
        {
            return false;
        }
        Vector3 dir = targetTransform.position - myTransform.position;

        //距離
        float distance = dir.magnitude;

        //cosθ/2
        float cosHalf = Mathf.Cos(angle / 2 * Mathf.Deg2Rad);

        //自分とターゲットの向きの内積
        float inner = Vector3.Dot(direction * myTransform.forward, dir.normalized);

        return (inner > cosHalf && distance < size);
    }
    /// <summary>
    /// 円錐の中にいるか判断
    /// </summary>
    /// <param name="myTransform"></param>
    /// <param name="targetTransforms"></param>
    /// <param name="angle"></param>
    /// <param name="size"></param>
    /// <param name="sign"></param>
    /// <returns></returns>
    public static List<GameObject> ConeInObjects(Transform myTransform, List<GameObject> targetTransforms, float angle, float size, int sign)
    {
        int direction = default;
        if (sign > 0)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }
        List<GameObject> hitCone= new List<GameObject>();
        for (int i = 0; i < targetTransforms.Count; i++)
        {
            Vector3 dir = targetTransforms[i].transform.position - myTransform.position;

            //距離
            float distance = dir.magnitude;

            //cosθ/2
            float cosHalf = Mathf.Cos(angle / 2 * Mathf.Deg2Rad);

            //自分とターゲットの向きの内積
            float inner = Vector3.Dot(direction * myTransform.forward, dir.normalized);

            if (inner > cosHalf && distance < size)
            {
                hitCone.Add(targetTransforms[i].gameObject);
            }
        }
        return hitCone;
    }
    
    /// <summary>
    /// 円錐状の判定をするメソッド　Mathf未使用　誤差大きい　もしALL2が使えなかったとき用
    /// </summary>
    /// <param name="myTransform">円錐を発生させる始点</param>
    /// <param name="targetTransforms">判定対象を入れたリスト</param>
    /// <param name="angle">円錐の幅</param>
    /// <param name="size">円錐の奥行　使わない可能性が高い</param>
    /// <param name="sign">向き　現行意味はない</param>
    /// <returns></returns>
    public static List<GameObject> ConeSearchAll(Transform myTransform, List<GameObject> targetTransforms, float angle/*幅*/, float size/*奥行*/, int sign)
    {
        Vector3 bowVect;
        bowVect = myTransform.rotation.eulerAngles.normalized;
        List<GameObject> hitCone = new List<GameObject>();
        for (int i = 0; i < targetTransforms.Count; i++)
        {
            float distance;
            distance = Vector3.Distance(myTransform.position, targetTransforms[i].transform.position);

            Transform corePosition = default;
            corePosition.position = new Vector3(bowVect.x * distance, bowVect.y * distance, bowVect.z * distance);

            float radius;
            radius = distance * angle;

            float objectdistance;
            objectdistance = Vector3.Distance(corePosition.position,targetTransforms[i].transform.position);
            
            if (objectdistance < radius)
            {
                hitCone.Add(targetTransforms[i].gameObject);
            }
        }
        return hitCone;
    }

    /// <summary>
    /// 円錐状の判定をするメソッド　Mathf未使用　
    /// 誤差は少ないが処理は少し重い可能性あり
    /// </summary>
    /// <param name="myTransform">円錐を発生させる始点</param>
    /// <param name="targetTransforms">判定対象を入れたリスト</param>
    /// <param name="angle">円錐の幅</param>
    /// <param name="size">円錐の奥行　使わない可能性が高い</param>
    /// <param name="sign">向き　現行使わない可能性が高い</param>
    /// <returns></returns>
    public static List<GameObject> ConeSearchALL2(Transform myTransform, List<GameObject> targetTransforms, float angle/*幅*/, float size/*奥行*/, int sign)
    {
        Vector3     bowVect;    // 弓が向いているベクトルをノーマライズした値
        Vector3     objVect;    // 弓→ターゲット間のベクトルをノーマライズした値
        Vector3     diffVect;   // objVect - bowVect　の値
        Quaternion  objRot;     // 弓→ターゲット間のベクトル角度
        float       radius;     // 判定半径の限界地　angleが中心角である前提で計算しているため注意
        float       judge;      // 判定するための値　この値がradius内であればリストに代入

        // 判定内のオブジェクトのリスト
        List<GameObject> hitCone = new List<GameObject>();

        // 弓が向いている方向のベクトル角度のノーマライズを代入
        bowVect = myTransform.rotation.eulerAngles.normalized;

        // 円錐の内角の2乗を代入　のちの比較に使う
        radius = (angle / 90) * (angle / 90);


        // 判定開始　リストをfor文で網羅して判定
        for (int i = 0; i < targetTransforms.Count; i++)
        {
            // 弓からターゲットへの角度を取得
            objRot = Quaternion.LookRotation(targetTransforms[i].transform.position - myTransform.position);

            // 取得した角度からベクトル角度のノーマライズを取得
            objVect = objRot.eulerAngles.normalized;

            // ターゲットへのベクトルと弓自身のベクトルの差を求める
            diffVect = objVect - bowVect;

            // Z軸を除いた2軸それぞれの2乗の合計を算出
            judge = (diffVect.x * diffVect.x) + (diffVect.y * diffVect.y);

            // 算出した値が円錐の角度より小さいか判定
            if (judge <= radius)
            {
                // 判定内のオブジェクトをリストに代入
                hitCone.Add(targetTransforms[i].gameObject);
            }
        }

        return hitCone;
    }

    /// <summary>
    /// 円錐状の判定をするメソッド　Mathf未使用
    /// 向いている角度に最も近い一つのオブジェクトをサーチ
    /// </summary>
    /// <param name="myTransform">円錐を発生させる始点</param>
    /// <param name="targetTransforms">判定対象を入れたリスト</param>
    /// <param name="angle">円錐の幅</param>
    /// <returns></returns>
    public static GameObject ConeSearchNearest(Transform myTransform, List<GameObject> targetTransforms, float angle/*幅*/ /*, float size奥行*/)
    {
        Vector3 bowVect;    // 弓が向いているベクトルをノーマライズした値
        Vector3 objVect;    // 弓→ターゲット間のベクトルをノーマライズした値
        Vector3 diffVect;   // objVect - bowVect　の値
        Quaternion objRot;  // 弓→ターゲット間のベクトル角度
        float radius;       // 最も近いオブジェクトとの角度　最初は判定する円錐の角度
        float judge;        // 判定するための値　この値がradius内であればリストに代入

        // 最も近いオブジェクト
        GameObject mostNearObject = default;

        // 弓が向いている方向のベクトル角度のノーマライズを代入
        bowVect = myTransform.rotation.eulerAngles.normalized;

        // 円錐の内角の2乗を代入　のちの比較に使う
        radius = (angle / 90) * (angle / 90);


        // 判定開始　リストをfor文で網羅して判定
        for (int i = 0; i < targetTransforms.Count; i++)
        {
            // 弓からターゲットへの角度を取得
            objRot = Quaternion.LookRotation(targetTransforms[i].transform.position - myTransform.position);

            // 取得した角度からベクトル角度のノーマライズを取得
            objVect = objRot.eulerAngles.normalized;

            // ターゲットへのベクトルと弓自身のベクトルの差を求める
            diffVect = objVect - bowVect;

            // Z軸を除いた2軸それぞれの2乗の合計を算出
            judge = (diffVect.x * diffVect.x) + (diffVect.y * diffVect.y);

            // 算出した値が円錐の角度より小さいか判定
            if (judge <= radius)
            {
                // 最も近い角度を変更する
                radius = judge;

                // 判定内のオブジェクトをリストに代入
                mostNearObject = targetTransforms[i];
            }
        }

        return mostNearObject;
    }

}
