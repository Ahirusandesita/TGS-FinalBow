// --------------------------------------------------------- 
// ItemShooter.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class ItemShooter : MonoBehaviour, IFGimmickCallerUsePower
{
    [SerializeField] ItemShotObjectScriptable data = default;
    [SerializeField] Transform shotPosition = default;
    /// <summary>
    /// 位置のずれ,円形
    /// </summary>
    [SerializeField] float gapPosition = 0.1f;
    GameObject[] shotObjects = default;
    WaitForSeconds rapidSpeed = default;
    IFItemShoterObjectPhysics physics = default;
    bool isWorking = false;

    public bool IsFinish => false;

    public bool Moving => isWorking;

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        physics.ItemMove();
    }

   

    private void Initialize()
    {
        shotObjects = data.shotObjects;
        rapidSpeed = new WaitForSeconds(1 / data.rapidSpeed);
        if (TryGetComponent<IFItemShoterObjectPhysics>(out IFItemShoterObjectPhysics ph))
        {
            physics = ph;
        }
        else
        {
            Debug.LogError(this.name + "にPhysicsがアタッチされていません");
        }

    }

    private GameObject[] Create()
    {
        GameObject[] createdObjs = new GameObject[shotObjects.Length];
        int cnt = 0;
        Vector3 sponePos = shotPosition.position + new Vector3(Random.Range(0f, gapPosition), 0f, Random.Range(0f, gapPosition));
        foreach (GameObject obj in shotObjects)
        {

            createdObjs[cnt] = Instantiate(obj, sponePos, Quaternion.identity);
            cnt++;
        }

        return createdObjs;
    }

    /// <summary>
    /// うつやつ
    /// </summary>
    /// <param name="oneAction">一回切りのアクションかどうか</param>
    /// <returns></returns>
    IEnumerator RepeatShot(bool oneAction)
    {
        isWorking = true;
        while (isWorking)
        {
            physics.ItemMoveStart(Create());
            yield return rapidSpeed;
            if (oneAction)
            {
                isWorking = false;
            }
        }
    }

    /// <summary>
    /// 単発で呼ぶ用
    /// </summary>
    public void GimmickAction()
    {
        if(!isWorking)
        StartCoroutine(RepeatShot(false));
    }

    /// <summary>
    /// 連続で呼び出す用
    /// </summary>
    public void GimmickAction(float pow)
    {
        if (pow == 0)
            return;

        if (!isWorking)
        StartCoroutine(RepeatShot(true));
    }
}