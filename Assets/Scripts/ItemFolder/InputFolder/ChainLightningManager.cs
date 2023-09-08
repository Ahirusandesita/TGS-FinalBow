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

    const int THUNDER_DAMAGE = 1;

    int _enchantPower = 0;

    public void ChainLightning(Transform hitTransform, int numberOfChains, int enchantPower)
    {
        if (numberOfChains <= 0)
        {
            numberOfChains = 1;
        }
        _enchantPower = enchantPower;
        //EnemyStats[,] stats = getObjects.ChainLightningGetStatsHyper(hitTransform, numberOfChains);
        EnemyStats[] stats = getObjects.ChainLightningGetStats(hitTransform, numberOfChains);
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


            select.TakeThunder(_enchantPower);
            select.TakeDamage(THUNDER_DAMAGE);
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
                        final.TakeThunder(_enchantPower);
                        final.TakeDamage(THUNDER_DAMAGE);
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
                next.TakeThunder(_enchantPower);
                next.TakeDamage(THUNDER_DAMAGE);
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

    IEnumerator StartChain(EnemyStats[] enemyStats, float waitTime)
    {
        WaitForSeconds wait = new(waitTime);
        Vector3 chainRootPosition = transform.position;
        foreach (EnemyStats stats in enemyStats)
        {
            stats.TakeDamage(THUNDER_DAMAGE);
            stats.TakeThunder(_enchantPower);

            GameObject destroy = takeEffects.CreateEffect(chainRootPosition, stats.transform.position);

            yield return wait;

            takeEffects.DeleteEffect(destroy);
        }



    }
}