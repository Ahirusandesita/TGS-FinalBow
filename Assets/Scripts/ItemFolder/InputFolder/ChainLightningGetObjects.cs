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
    /// ����肸��
    /// </summary>
    List<GameObject> takedObjects = new();

    EnemyStats[,] takeParalysisHyper = default;

    EnemyStats[] takeParalysis = default;

    SelectArray selectArray = new SelectArray();

    const int FIRST_CHAIN_OBJECT = 0;

    int _layerMask = 1 << 6;

    float _firstSeachArea = 10f;

    float _arcSeach = 10f;

    #endregion
    #region property
    #endregion
    #region method


    public EnemyStats[,] ChainLightningGetStatsHyper(Transform hitTransform, int numberOfChain)
    {
        EnemyStats[] selectedArray = GetSelectedFirstStats(hitTransform);

        // ���`�F�C���O���[�v�@���`�F�C���̒���
        takeParalysisHyper = new EnemyStats[selectedArray.Length, numberOfChain];

        // �ŏ��ɑI�΂ꂵ�I����`�F�C���O���[�v�̐擪�ɓo�^����
        for (int i = 0; i < selectedArray.Length; i++)
        {
            takeParalysisHyper[i, FIRST_CHAIN_OBJECT] = selectedArray[i];
        }

        for (int i = 0; i < numberOfChain; i++)
        {
            ArcChain(i);
        }

        return takeParalysisHyper;
    }

    public EnemyStats[] ChainLightningGetStats(Transform hitTransform, int numberOfChain)
    {
        takeParalysis = new EnemyStats[numberOfChain];

        takedObjects = new();

        takedObjects.Add(hitTransform.gameObject);

        Transform chainRoot = hitTransform;

        for (int index = 0; index < numberOfChain; index++)
        {
            List<EnemyStats> selectedArray = new List<EnemyStats>
                (FindObjectInArea<EnemyStats>(chainRoot.position,_layerMask,_arcSeach,takedObjects.ToArray()));

            EnemyStats select = GetShotestDistanceObject<EnemyStats>(selectedArray.ToArray(), chainRoot.position);

            chainRoot = select.transform;

            takeParalysis[index] = select;

            takedObjects.Add(select.gameObject);

        }

        return takeParalysis;
    }

    private EnemyStats[] GetSelectedFirstStats(Transform hitTransform)
    {
        takedObjects = new();
        // �����͖�Ⴢ��珜�O
        takedObjects.Add(hitTransform.gameObject);

        // �ŏ��̃`�F�C���̔���
        GameObject[] firstFinds = FindObjectInArea(hitTransform.position, _layerMask, _firstSeachArea);

        // �����ς݂ɓo�^����
        takedObjects.AddRange(firstFinds);


        // �ŏ��̃`�F�C������G�l�~�[�����������ʂ�����
        EnemyStats[] selectedArray = selectArray.GetSelectedArray<EnemyStats>(firstFinds);
        return selectedArray;
    }



    private void ArcChain(int numberOfTimes)
    {
        // �`�F�C�������@��ԋ߂��I�u�W�F�N�g��I������
        for (int chainGroup = 0; chainGroup < takeParalysisHyper.GetLength(0); chainGroup++)
        {
            EnemyStats startEnemy = takeParalysisHyper[chainGroup, numberOfTimes];

            if (startEnemy is null)
            {
                return;
            }

            Vector3 startPositon = startEnemy.transform.position;

            GameObject[] findObjs =
            FindObjectInArea(startPositon, _layerMask, _arcSeach, takedObjects.ToArray());

            takedObjects.AddRange(findObjs);

            EnemyStats[] enemies = selectArray.GetSelectedArray<EnemyStats>(findObjs);

            if (enemies.Length <= 0)
            {
                continue;
            }

            Vector3 rootChainPosition = takeParalysisHyper[chainGroup, FIRST_CHAIN_OBJECT].transform.position;

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

            takeParalysisHyper[chainGroup, numberOfTimes + 1] = enemies[cacheIndex];

        }
    }


    private GameObject[] FindObjectInArea(Vector3 position, int layerMask, float radius)
    {
        List<GameObject> findObjects = new();

        int debug = 0;
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
                    debug++;

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

    private T GetShotestDistanceObject<T>(T[] search, Vector3 myPoint) where T : Component
    {
        float shotestDistance = Vector3.Distance(search[0].transform.position, myPoint);

        T shotestCompornent = search[0];

        for (int i = 1; i < search.Length; i++)
        {
            float distance = Vector3.Distance(search[i].transform.position, myPoint);

            if (distance < shotestDistance)
            {
                shotestDistance = distance;

                shotestCompornent = search[i];
            }
        }

        return shotestCompornent;
    }

    #endregion
}