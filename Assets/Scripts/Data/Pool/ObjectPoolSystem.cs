// --------------------------------------------------------- 
// ObjectPoolSystem.cs 
// 
// CreateDay: 2023/06/08
// Creator  : TakayanagiSora
// --------------------------------------------------------- 

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// �v�[���I�u�W�F�N�g�������@
/// </summary>
public enum CreateType
{
    /// <summary>
    /// �����I�ɔ�\���ɂ���
    /// </summary>
    automatic,
    /// <summary>
    /// �蓮�ł̔�\�����K�v
    /// </summary>
    manual
}


/// <summary>
/// �I�u�W�F�N�g�v�[���̊Ǘ��N���X
/// </summary>
public class ObjectPoolSystem : MonoBehaviour
{
    #region �ϐ�
    [Tooltip("�擾����PoolObjectParamTable�N���X")]
    public PoolObjectParamTable _poolObjectParamTable = default;

    [Tooltip("���������v���n�u���i�[���邽�߂̃L���[�z��")]
    private Queue<CashObjectInformation>[] _objectQueues = default;

    [Tooltip("���������G�t�F�N�g���i�[���邽�߂̃L���[�z��")]
    private Queue<GameObject>[] _effectQueues = default;

    [Tooltip("����/�擾�����I�u�W�F�N�g���i�[���钆�ԕϐ�")]
    private CashObjectInformation _temporaryObject = default;

    [Tooltip("����/�擾�����G�t�F�N�g���i�[���钆�ԕϐ�")]
    private GameObject _temporaryEffect = default;

    // ������萔
    private const string ST_SHORTAGE_QUEUE = "�L���[�̗e�ʕs��";
    #endregion


    private void Awake()
    {
        // �v�[���I�u�W�F�N�g�̎�ޕ��A�z��̈���m�ۂ���
        _objectQueues = new Queue<CashObjectInformation>[_poolObjectParamTable._scriptablePoolInformation.Count];

        // �v�[���G�t�F�N�g�̎�ޕ��A�z��̈���m�ۂ���
        _effectQueues = new Queue<GameObject>[_poolObjectParamTable._scriptableEffectInformation.Count];

        // �v�[���̐���
        CreatePool();
    }


    #region Call
    /// <summary>
    /// �I�u�W�F�N�g���Ăяo���i�I�u�W�F�N�g�v���n�u�j
    /// </summary>
    /// <param name="poolObjectType">���o���I�u�W�F�N�g�̎��</param>
    /// <param name="spawnPosition">�X�|�[�����W</param>
    /// <param name="spawnAngle">�����p�x</param>
    public CashObjectInformation CallObject(PoolEnum.PoolObjectType poolObjectType, Vector3 spawnPosition, Quaternion? spawnAngle = null)
    {
        try
        {
            // enum�Ŏw�肳�ꂽ�L���[����I�u�W�F�N�g�����o��
            _temporaryObject = _objectQueues[(int)poolObjectType].Dequeue();
        }
        // �e�ʕs���ŃL���[����̎��o���Ɏ��s�����Ƃ�
        catch (InvalidOperationException)
        {
            Debug.LogWarning(poolObjectType + ST_SHORTAGE_QUEUE);

            // �I�u�W�F�N�g�𐶐�����
            _temporaryObject = Instantiate(_poolObjectParamTable._scriptablePoolInformation[(int)poolObjectType]._prefab);

            // ���������I�u�W�F�N�g�ɁA�ǂ̃v�[���I�u�W�F�N�g���̏�����������
            _temporaryObject.GetComponent<CashObjectInformation>()._myObjectType = poolObjectType;

            // ���������I�u�W�F�N�g���L���[�ɒǉ�����
            _objectQueues[(int)poolObjectType].Enqueue(_temporaryObject);
        }

        // ���o�����I�u�W�F�N�g�̈ʒu���A�w�肳�ꂽ�ʒu�ɕύX
        _temporaryObject.transform.position = spawnPosition;

        // Quaternion�������w�肳��Ă����Ƃ�
        if (spawnAngle != null)
        {
            // ���o�����I�u�W�F�N�g�̊p�x���A�w�肳�ꂽ�p�x�ɕύX
            _temporaryObject.transform.rotation = (Quaternion)spawnAngle;
        }

        // ���o�����I�u�W�F�N�g��\������
        _temporaryObject.gameObject.SetActive(true);

        return _temporaryObject;
    }

    /// <summary>
    /// �I�u�W�F�N�g���Ăяo���i�G�t�F�N�g�j
    /// </summary>
    /// <param name="poolObjectType">���o���G�t�F�N�g�̎��</param>
    /// <param name="spawnPosition">�X�|�[�����W</param>
    /// <param name="spawnAngle">�����p�x</param>
    public GameObject CallObject(EffectPoolEnum.EffectPoolState poolEffectType, Vector3 spawnPosition, Quaternion? spawnAngle = null)
    {
        try
        {
            // enum�Ŏw�肳�ꂽ�L���[����G�t�F�N�g�����o��
            _temporaryEffect = _effectQueues[(int)poolEffectType].Dequeue();
        }
        // �e�ʕs���ŃL���[����̎��o���Ɏ��s�����Ƃ�
        catch (InvalidOperationException)
        {
            Debug.LogWarning(poolEffectType + ST_SHORTAGE_QUEUE);

            // �G�t�F�N�g�𐶐�����
            _temporaryEffect = Instantiate(_poolObjectParamTable._scriptableEffectInformation[(int)poolEffectType]._prefab);

            // ���������G�t�F�N�g���L���[�ɒǉ�����
            _effectQueues[(int)poolEffectType].Enqueue(_temporaryEffect);
        }

        // ���o�����G�t�F�N�g�̈ʒu���A�w�肳�ꂽ�ʒu�ɕύX
        _temporaryEffect.transform.position = spawnPosition;

        // Quaternion�������w�肳��Ă����Ƃ�
        if (spawnAngle != null)
        {
            // ���o�����G�t�F�N�g�̊p�x���A�w�肳�ꂽ�p�x�ɕύX
            _temporaryEffect.transform.rotation = (Quaternion)spawnAngle;
        }

        // ���o�����G�t�F�N�g��\������
        _temporaryEffect.SetActive(true);

        return _temporaryEffect;
    }
    #endregion


    #region Return
    /// <summary>
    /// �I�u�W�F�N�g��ԋp����i�I�u�W�F�N�g�v���n�u�j
    /// </summary>
    /// <param name="returnObject">�ԋp����I�u�W�F�N�g</param>
    public void ReturnObject(CashObjectInformation returnObject)
    {
        // �n���ꂽ�I�u�W�F�N�g���\���ɂ���
        returnObject.gameObject.SetActive(false);

        // �n���ꂽ�I�u�W�F�N�g���w�肳�ꂽ�v�[���ɕԂ�
        _objectQueues[(int)returnObject._myObjectType].Enqueue(returnObject);
    }

    /// <summary>
    /// �I�u�W�F�N�g��ԋp����i�G�t�F�N�g�j
    /// </summary>
    /// <param name="poolEffectType">�ԋp����G�t�F�N�g�̎��</param>
    /// <param name="returnObject">�ԋp����G�t�F�N�g</param>
    public void ReturnObject(EffectPoolEnum.EffectPoolState poolEffectType, GameObject returnObject)
    {
        // �n���ꂽ�G�t�F�N�g���\���ɂ���
        returnObject.SetActive(false);

        // �n���ꂽ�G�t�F�N�g���w�肳�ꂽ�v�[���ɕԂ�
        _effectQueues[(int)poolEffectType].Enqueue(returnObject);
    }
    #endregion


    #region Create
    /// <summary>
    /// �v�[���𐶐�����
    /// </summary>
    private void CreatePool()
    {
        // �I�u�W�F�N�g�v���n�u�̐�������--------------------------------------------------

        // �z��̗v�f�� = �v�[���I�u�W�F�N�g�̎�ޕ��A�J��Ԃ�
        for (int i = 0; i < _objectQueues.Length; i++)
        {
            // �z��̒��̊e�L���[�𐶐�
            _objectQueues[i] = new Queue<CashObjectInformation>();

            // ���C���V�[���ȊO�Ő������Ȃ��I�u�W�F�N�g�͏������X�L�b�v
            if (_poolObjectParamTable._scriptablePoolInformation[i]._onlyMainScene && SceneManager.GetActiveScene().name != "HDRPDebugscene")
            {
                continue;
            }

            // �e�v�[���I�u�W�F�N�g�ɐݒ肳�ꂽ�L���[�̍ő�e�ʂ܂ŁA�I�u�W�F�N�g�������J��Ԃ�
            for (int k = 0; k < _poolObjectParamTable._scriptablePoolInformation[i]._queueMax; k++)
            {
                // �I�u�W�F�N�g�𐶐�����
                _temporaryObject = Instantiate(_poolObjectParamTable._scriptablePoolInformation[i]._prefab);

                // �ݒ肳�ꂽenum�ɂ���ď������X�L�b�v
                if (_poolObjectParamTable._scriptablePoolInformation[i]._createType == CreateType.automatic)
                {
                    // ���������I�u�W�F�N�g���\���ɂ���
                    _temporaryObject.gameObject.SetActive(false);
                }

                // ���������I�u�W�F�N�g�ɁA�ǂ̃v�[���I�u�W�F�N�g���̏�����������
                _temporaryObject.GetComponent<CashObjectInformation>()._myObjectType = (PoolEnum.PoolObjectType)i;

                // ���������I�u�W�F�N�g���e�L���[�ɒǉ�����
                _objectQueues[i].Enqueue(_temporaryObject);
            }
        }

        // �G�t�F�N�g�̐�������------------------------------------------------------------

        // �z��̗v�f�� = �v�[���G�t�F�N�g�̎�ޕ��A�J��Ԃ�
        for (int i = 0; i < _effectQueues.Length; i++)
        {
            // �z��̒��̊e�L���[�𐶐�
            _effectQueues[i] = new Queue<GameObject>();

            // �e�v�[���G�t�F�N�g�ɐݒ肳�ꂽ�L���[�̍ő�e�ʂ܂ŁA�I�u�W�F�N�g�������J��Ԃ�
            for (int k = 0; k < _poolObjectParamTable._scriptableEffectInformation[i]._queueMax; k++)
            {
                // �I�u�W�F�N�g�𐶐�����
                _temporaryEffect = Instantiate(_poolObjectParamTable._scriptableEffectInformation[i]._prefab);

                // ���������I�u�W�F�N�g���\���ɂ���
                _temporaryEffect.SetActive(false);

                // ���������I�u�W�F�N�g���e�L���[�ɒǉ�����
                _effectQueues[i].Enqueue(_temporaryEffect);
            }
        }
    }
    #endregion
}
