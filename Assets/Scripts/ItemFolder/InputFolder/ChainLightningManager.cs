// --------------------------------------------------------- 
// ChainLightningManager.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChainLightningManager : MonoBehaviour
{
    ChainLightningGetObjects getObjects = new ChainLightningGetObjects();

    ChainLightningTakeEffects takeEffects = new ChainLightningTakeEffects();

    [SerializeField] GameObject effect = default;

    [SerializeField] float _waitTime = 0.2f;

    List<GameObject> nextDestroy = new();

    public void ChainLightning(Transform myTransform, int numberOfChains)
    {
        EnemyStats[,] stats = getObjects.ChainLightningGetStats(myTransform, numberOfChains);
        print("aaa" + stats.Length);
        takeEffects.SetEffects = effect;

        StartCoroutine(StartChain(stats, _waitTime));
    }

    IEnumerator StartChain(EnemyStats[,] enemyStats, float waitTime)
    {
        const int CHAIN_GROUP = 0;
        const int CHAIN_INDEX = 1;


        WaitForSeconds wait = new(waitTime);

        yield return wait;

        for (int chainGroup = 0; chainGroup < enemyStats.GetLength(CHAIN_GROUP); chainGroup++)
        {
            EnemyStats select = enemyStats[chainGroup, 0];
            nextDestroy.Add(takeEffects.CreateEffect(transform.position, select.transform.position));

            select.TakeThunder(0);
        }

        yield return wait;
        ResetEffects(nextDestroy.ToArray());

        for (int chainIndex = 0; chainIndex < enemyStats.GetLength(CHAIN_INDEX); chainIndex++)
        {
            for (int chainGroup = 0; chainGroup < enemyStats.GetLength(CHAIN_GROUP); chainGroup++)
            {
                if (chainIndex >= enemyStats.GetLength(CHAIN_INDEX) - 1)
                {
                    EnemyStats final = enemyStats[chainGroup, chainIndex];
                    if (final is not null)
                    {
                        final.TakeThunder(0);
                    }

                    continue;
                }
                EnemyStats root = enemyStats[chainGroup, chainIndex];
                EnemyStats next = enemyStats[chainGroup, chainIndex + 1];

                if (root is null || next is null)
                {
                    continue;
                }

                nextDestroy.Add(takeEffects.CreateEffect(root.transform.position, next.transform.position));
                next.TakeThunder(0);
            }
            yield return wait;
            ResetEffects(nextDestroy.ToArray());
        }

        void ResetEffects(GameObject[] nexts)
        {
            foreach (GameObject eff in nexts)
            {
                takeEffects.DeleteEffect(eff);
                nextDestroy.Clear();
            }
        }



    }
}