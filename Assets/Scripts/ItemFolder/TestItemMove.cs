// --------------------------------------------------------- 
// ArrowPassiveEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Nomura
// --------------------------------------------------------- 
using UnityEngine;

public class TestItemMove : MonoBehaviour
{
    public Transform gollObjectTransform;
    public bool isStart = false;

    public float speed;

    private Transform _myTransform;
    private bool _canStartOne = true;
    private ObjectPoolSystem _objectPool;
    private void Start()
    {
        _myTransform = this.transform;
    }
    void Update()
    {
        if (!isStart)
        {
            return;
        }
        if (_canStartOne)
        {
            _myTransform.localScale = _myTransform.localScale / 10f;
            _canStartOne = false;
        }
        //transform.rotation = Quaternion.LookRotation(transform.position - gollObjectTransform.position);
        _myTransform.LookAt(gollObjectTransform);
        print("unko" + _myTransform.rotation.eulerAngles);
        transform.Translate(_myTransform.forward * speed * Time.deltaTime,Space.Self);
        _objectPool = StaticAttack.attackObj.GetComponent<ObjectPoolSystem>();
        if (Vector3.Distance(gollObjectTransform.position, _myTransform.position) <= 1f)
        {
           // _objectPool.ReturnObject(this.gameObject);
        }
    }
    public void SetObjectPoos(ObjectPoolSystem objectPool)
    {
        _objectPool = objectPool;
    }

    public void SetObj(GameObject gameObject)
    {
        gollObjectTransform = gameObject.transform;
        
    }

}
