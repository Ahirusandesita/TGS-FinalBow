// --------------------------------------------------------- 
// ItemShooter.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class ItemShooter : MonoBehaviour, IFCanTakeArrowButton
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
    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        physics.ItemMove();
    }

    public void ButtonPush()
    {
        if (isWorking == false)
        {
            isWorking = true;

            StartCoroutine(RepeatShot());
        }
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


    IEnumerator RepeatShot()
    {
        while (isWorking)
        {
            physics.ItemMoveStart(Create());
            yield return rapidSpeed;

        }
    }
}