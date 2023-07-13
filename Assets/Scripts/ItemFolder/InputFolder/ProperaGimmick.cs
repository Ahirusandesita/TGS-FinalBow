// --------------------------------------------------------- 
// ProperaGimmick.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections.Generic;

interface IFProperaLinkObject
{
    /// <summary>
    /// パワーが必要かどうか
    /// </summary>
    bool GetNeedPower { get; }

    /// <summary>
    /// リンク元のオブジェクトをセット
    /// </summary>
    ProperaGimmick SetLinkObject { set; }

    /// <summary>
    /// プロペラが回転しているときにこれが呼ばれる
    /// </summary>
    /// <param name="power">プロペラの回転パワー(0-1)</param>
    void UsePowerAction(float power);

}

public class ProperaGimmick : ObjectParent
{
    [SerializeField] float _maxRotateSpeed = 1000f;
    [SerializeField] float _dicreaceRotateSpeed = -300f;
    [SerializeField] float _addRotateSpeed = 600f;
    [SerializeField] RotateDirection direction = RotateDirection.Right;
    [SerializeField] List<GameObject> setLinkObject = default;
    List<IFProperaLinkObject> linkObjects = default;

    List<bool> isNeedPowerLinkObjects = default;

    internal IFProperaLinkObject[] GetObj { get => linkObjects.ToArray(); }
    enum RotateDirection
    {
        Right = 1,
        Left = -1
    }

    float _rotateSpeed = 0f;

    [SerializeReference] bool _canRotate = true;

    private void Start()
    {
        Initialize();
    }

    private void OnValidate()
    {
        Initialize();
    }

    private void Update()
    {
        OutputPower();

        DicreacePower();
    }

    /// <summary>
    /// なんか吸い込んでいる時に呼ばれるらしい
    /// </summary>
    public override void ObjectAction()
    {

        if (_canRotate)
        {
            AddPower();
        }
    }

    /// <summary>
    /// bool配列情報の更新
    /// </summary>
    /// <param name="caller">呼び出し元</param>
    internal void SetElementIsNeedPowerLinkObjects(IFProperaLinkObject caller)
    {
        // エラー
        if (!linkObjects.Contains(caller))
        {
            X_Debug.LogError(caller + "のリンク元オブジェクトと" + this + "のリンク先に異常があります");
            return;
        }

        int index = linkObjects.IndexOf(caller);

        isNeedPowerLinkObjects[index] = linkObjects[index].GetNeedPower;

        _canRotate = IsGetNeedPowerAllLinkObjects();
    }

    /// <summary>
    /// 初期化
    /// </summary>
    private void Initialize()
    {
        _maxRotateSpeed = Mathf.Abs(_maxRotateSpeed);

        _dicreaceRotateSpeed = -Mathf.Abs(_dicreaceRotateSpeed);

        _addRotateSpeed = Mathf.Abs(_addRotateSpeed);

        SetLinkObjectsList();

        if (linkObjects == null || linkObjects.Count == 0)
        {
            X_Debug.LogError("リンク先が登録されていません");
            return;
        }

        // 自身をリンク先に登録
        foreach (IFProperaLinkObject linkObject in linkObjects)
        {
            linkObject.SetLinkObject = this;
        }

        // リストの初期化
        if (isNeedPowerLinkObjects == null || isNeedPowerLinkObjects.Count != linkObjects.Count)
        {
            isNeedPowerLinkObjects = new List<bool>();

            for (int i = 0; i < linkObjects.Count; i++)
            {
                isNeedPowerLinkObjects.Add(linkObjects[i].GetNeedPower);
            }
        }

        _canRotate = IsGetNeedPowerAllLinkObjects();

        return;

        // リスト初期化 
        void SetLinkObjectsList()
        {
            linkObjects = new List<IFProperaLinkObject>();

            List<GameObject> removeObj = new List<GameObject>();

            if (setLinkObject.Count > 0)
            {
                foreach (GameObject obj in setLinkObject)
                {
                    if (obj.TryGetComponent<IFProperaLinkObject>(out IFProperaLinkObject canAddListObject))
                    {
                        linkObjects.Add(canAddListObject);
                    }
                    else
                    {
                        removeObj.Add(obj);
                    }
                }

                foreach (GameObject obj in removeObj)
                {
                    setLinkObject.Remove(obj);
                }
            }
        }
    }

    /// <summary>
    /// 全リンクオブジェクトのどれかがパワーを必要かどうか
    /// </summary>
    /// <returns>必要ならtrue</returns>
    private bool IsGetNeedPowerAllLinkObjects()
    {
        foreach (bool isNeed in isNeedPowerLinkObjects)
        {
            if (isNeed)
                return true;
        }
        return false;
    }

    private void OutputPower()
    {
        // 回転
        transform.Rotate(0f, 0f, _rotateSpeed * ((int)direction) * Time.deltaTime);

        // アクション呼ぶ
        foreach (IFProperaLinkObject linkObject in linkObjects)
        {
            linkObject.UsePowerAction(_rotateSpeed / _maxRotateSpeed);
        }
    }

    /// <summary>
    /// 加速
    /// </summary>
    private void AddPower()
    {

        if (_maxRotateSpeed <= _rotateSpeed)
        {
            _rotateSpeed = _maxRotateSpeed;
            return;
        }

        _rotateSpeed += _addRotateSpeed * Time.deltaTime;
    }

    /// <summary>
    /// 減速
    /// </summary>
    private void DicreacePower()
    {
        if (0f >= _rotateSpeed)
        {
            _rotateSpeed = 0f;
            return;
        }

        _rotateSpeed += _dicreaceRotateSpeed * Time.deltaTime;
    }



}