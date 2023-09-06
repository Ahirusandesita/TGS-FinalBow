// --------------------------------------------------------- 
// EnchantThunderArc.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChainLightningGetObjects
{
    #region variable

    /// <summary>
    /// しょりずみ
    /// </summary>
    List<GameObject> takedObjects = default;

    EnemyStats[,] takeParalysis = default;

    SelectArray selectArray = new SelectArray();

    const int FIRST_CHAIN_OBJECT = 0;

    int _layerMask = 1 << 6;

    float _firstSeachArea = 5f;

    float _arcSeach = 8f;

    #endregion
    #region property
    #endregion
    #region method


    public EnemyStats[,] ChainLightningGetStats(Transform hitTransform, int numberOfChain)
    {       
        // 自分は麻痺から除外
        takedObjects.Add(hitTransform.gameObject);

        // 最初のチェインの判定
        GameObject[] firstFinds = FindObjectInArea(hitTransform.position, _layerMask, _firstSeachArea);

        // 処理済みに登録する
        takedObjects.AddRange(firstFinds);

        // 最初のチェインからエネミーすたっつだけぬけだす
        EnemyStats[] selectedArray = selectArray.GetSelectedArray<EnemyStats>(firstFinds);

        // 第一チェイングループ　第二チェインの長さ
        takeParalysis = new EnemyStats[selectedArray.Length, numberOfChain];

        // 最初に選ばれし選手をチェイングループの先頭に登録する
        for (int i = 0; i < selectedArray.Length; i++)
        {
            takeParalysis[i, FIRST_CHAIN_OBJECT] = selectedArray[i];
        }

        for(int i = 0; i < numberOfChain;i++)
        {
            ArcChain(hitTransform,i);
        }

        return takeParalysis;
    }

    private void ArcChain(Transform hitTransform,int numberOfTimes)
    {
        // チェイン処理　一番近いオブジェクトを選択する
        for (int chainGroup = 0; chainGroup < takeParalysis.GetLength(0); chainGroup++)
        {
            GameObject[] findObjs =
            FindObjectInArea(hitTransform.position, _layerMask, _arcSeach, takedObjects.ToArray());

            takedObjects.AddRange(findObjs);

            EnemyStats[] enemies = selectArray.GetSelectedArray<EnemyStats>(findObjs);

            if (enemies.Length <= 0)
            {
                continue;
            }

            Vector3 rootChainPosition = takeParalysis[chainGroup, FIRST_CHAIN_OBJECT].transform.position;

            float minDistance = float.MaxValue;

            int cacheIndex = 0;

            for (int i = 0; i < enemies.Length; i++)
            {
                EnemyStats enemy = enemies[i];
                float distance = Vector3.Distance(enemy.transform.position, rootChainPosition);

                if (distance < minDistance)
                {
                    cacheIndex = i;
                }
            }

            takeParalysis[chainGroup, numberOfTimes] = enemies[cacheIndex];

        }
    }

    private GameObject[] FindObjectInArea(Vector3 position, int layerMask, float radius)
    {
        List<GameObject> findObjects = default;

        if (Physics.CheckSphere(position, radius, layerMask))
        {
            Collider[] colliders = Physics.OverlapSphere(position, radius, layerMask);

            foreach (Collider col in colliders)
            {
                GameObject check = col.gameObject;
                if (findObjects.Contains(check))
                {
                    continue;
                }
                else
                {
                    findObjects.Add(check);
                }
            }
        }

        return findObjects.ToArray();
    }

    private GameObject[] FindObjectInArea(Vector3 position, int layerMask, float radius, GameObject[] delete)
    {
        GameObject[] gameObjects = FindObjectInArea(position, layerMask, radius);

        HashSet<GameObject> hash = new HashSet<GameObject>(gameObjects);

        hash.ExceptWith(delete);

        GameObject[] deleteds = new GameObject[hash.Count];

        hash.CopyTo(deleteds);

        return deleteds;
    }

    private T[] FindObjectInArea<T>(Vector3 position, int layerMask, float radius, GameObject[] delete) where T : MonoBehaviour
    {
        GameObject[] objects = FindObjectInArea(position, layerMask, radius, delete);

        List<T> tList = new();

        foreach (GameObject obj in objects)
        {
            if (obj.TryGetComponent<T>(out T tObj))
            {
                tList.Add(tObj);
            }
        }

        return tList.ToArray();
    }



    #endregion
}