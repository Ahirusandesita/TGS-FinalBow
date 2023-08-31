// --------------------------------------------------------- 
// ProperaGimmick.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections.Generic;

public interface IFProperaLinkObject
{
    /// <summary>
    /// �p���[���K�v���ǂ���
    /// </summary>
    bool GetNeedPower { get; }

    /// <summary>
    /// �����N���̃I�u�W�F�N�g���Z�b�g
    /// </summary>
    ProperaGimmick SetLinkObject { set; }

    /// <summary>
    /// �v���y������]���Ă���Ƃ��ɂ��ꂪ�Ă΂��
    /// </summary>
    /// <param name="power">�v���y���̉�]�p���[(0-1)</param>
    void UsePowerAction(float power);

}
[RequireComponent(typeof(ItemStatus))]
public class ProperaGimmick : ObjectParent
{
    [SerializeField] float _maxRotateSpeed = 1000f;
    [SerializeField] float _dicreaceRotateSpeed = -300f;
    [SerializeField] float _addRotateSpeed = 600f;
    [SerializeField] Vector3 rotateDirection = Vector3.right;
    [SerializeField] List<GameObject> setLinkObject = default;
    [SerializeField] bool useScriptable = false;

    public GimmickData _gimmickData;

    List<IFProperaLinkObject> linkObjects = default;

    List<bool> isNeedPowerLinkObjects = default;

    internal IFProperaLinkObject[] GetObj { get => linkObjects.ToArray(); }
 
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
    /// �Ȃ񂩋z������ł��鎞�ɌĂ΂��炵��
    /// </summary>
    public override void ObjectAction()
    {
        if (_canRotate)
        {
            AddPower();
        }
    }

    /// <summary>
    /// bool�z����̍X�V
    /// </summary>
    /// <param name="caller">�Ăяo����</param>
    internal void SetElementIsNeedPowerLinkObjects(IFProperaLinkObject caller)
    {
        // �G���[
        if (!linkObjects.Contains(caller))
        {
            X_Debug.LogError(caller + "�̃����N���I�u�W�F�N�g��" + this + "�̃����N��Ɉُ킪����܂�");
            return;
        }

        int index = linkObjects.IndexOf(caller);

        isNeedPowerLinkObjects[index] = linkObjects[index].GetNeedPower;

        _canRotate = IsGetNeedPowerAllLinkObjects();
    }

    /// <summary>
    /// ������
    /// </summary>
    private void Initialize()
    {
        _maxRotateSpeed = Mathf.Abs(_maxRotateSpeed);

        _dicreaceRotateSpeed = -Mathf.Abs(_dicreaceRotateSpeed);

        _addRotateSpeed = Mathf.Abs(_addRotateSpeed);

        SetLinkObjectsList();

        if (linkObjects == null || linkObjects.Count == 0)
        {
            X_Debug.LogError("�����N�悪�o�^����Ă��܂���");
            return;
        }

        // ���g�������N��ɓo�^
        foreach (IFProperaLinkObject linkObject in linkObjects)
        {
            linkObject.SetLinkObject = this;
        }

        // ���X�g�̏�����
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

        // ���X�g������ 
        void SetLinkObjectsList()
        {
            if (useScriptable)
            {
                linkObjects = new List<IFProperaLinkObject>();

                List<GameObject> removeObj = new List<GameObject>();



                //�����ǉ��\��P�@�X�N���v�^�u���̃Q�[���I�u�W�F�N�g���C���X�^���X�@�����v�[�����g�p����

                if (_gimmickData.gimmickLinkObjectDataBases.Count > 0)
                {
                    //foreach (GameObject obj in setLinkObject)
                    //{
                    //    if (obj.TryGetComponent<IFProperaLinkObject>(out IFProperaLinkObject canAddListObject))
                    //    {
                    //        linkObjects.Add(canAddListObject);
                    //    }
                    //    else
                    //    {
                    //        removeObj.Add(obj);
                    //    }
                    //}

                    for (int i = 0; i < _gimmickData.gimmickLinkObjectDataBases.Count; i++)
                    {
                        GameObject gimmickObject = Instantiate(
                            _gimmickData.gimmickLinkObjectDataBases[i].gimmickLinkObject,
                            _gimmickData.gimmickLinkObjectDataBases[i].spawnPosition, Quaternion.Euler(_gimmickData.gimmickLinkObjectDataBases[i].gimmickObjectRotation)
                            );

                        if (gimmickObject.TryGetComponent<IFProperaLinkObject>(out IFProperaLinkObject canAddListObject))
                        {
                            linkObjects.Add(canAddListObject);
                        }
                        else
                        {
                            removeObj.Add(gimmickObject);
                        }
                    }

                    foreach (GameObject obj in removeObj)
                    {
                        setLinkObject.Remove(obj);
                    }
                }


            }
            else
            {
                SelectArray select = new SelectArray();
                GameObject[] objs = select.GetSelectedArrayReturnGameObjects
                    <IFProperaLinkObject>(setLinkObject.ToArray());
                IFProperaLinkObject[] IFs = select.GetSelectedArray<IFProperaLinkObject>
                    (setLinkObject.ToArray());
                setLinkObject = new List<GameObject>();
                setLinkObject.AddRange(objs);
                linkObjects = new List<IFProperaLinkObject>();
                linkObjects.AddRange(IFs);

            }
        }
    }

    /// <summary>
    /// �S�����N�I�u�W�F�N�g�̂ǂꂩ���p���[��K�v���ǂ���
    /// </summary>
    /// <returns>�K�v�Ȃ�true</returns>
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
        // ��]
        transform.Rotate( _rotateSpeed * rotateDirection * Time.deltaTime);

        // �A�N�V�����Ă�
        foreach (IFProperaLinkObject linkObject in linkObjects)
        {
            linkObject.UsePowerAction(_rotateSpeed / _maxRotateSpeed);
        }
    }

    /// <summary>
    /// ����
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
    /// ����
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