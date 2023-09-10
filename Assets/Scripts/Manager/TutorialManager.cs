// --------------------------------------------------------- 
// TutorialManager.cs 
// 
// CreateDay: 2023/09/08
// Creator  : TakayanagiSora
// --------------------------------------------------------- 

using UnityEngine;
using System;
using System.Collections;

public class TutorialManager : MonoBehaviour
{
    #region variable 
    [SerializeField]
    private StageDataTable _stageDataTable = default;

    private ObjectPoolSystem _poolSystem = default;

    [Tooltip("�`���[�g���A���̐i�s�x")]
    private int _currentTutorialIndex = 0;
    #endregion
    #region property
    #endregion
    #region method

    private void Awake()
    {
        try
        {
            _poolSystem = GameObject.FindWithTag("PoolSystem").GetComponent<ObjectPoolSystem>();
        }
        catch (Exception)
        {
            X_Debug.LogError("ObjectPoolSystem���A�^�b�`����Ă��܂���B");
        }
    }

    private void Start()
    {
        StartCoroutine(SpawnTarget(0));
    }

    private void Update()
    {

    }

    private IEnumerator SpawnTarget(int listIndex)
    {
        // �`���[�g���A���p�̃f�[�^�̃p�X���擾�i�`���[�g���A���Ȃ̂�
        TargetDataTable dataPath = _stageDataTable._waveInformation[_currentTutorialIndex]._targetData[listIndex];

        // ���ԍ��X�|�[������
        yield return new WaitForSeconds(dataPath._spawnDelay_s);

        TargetMove target = _poolSystem.CallObject(PoolEnum.PoolObjectType.targetObject, dataPath._spawnPlace.position).GetComponent<TargetMove>();
        target.TargetData = dataPath;
    }

    /// <summary>
    /// �z�����݊���
    /// </summary>
    public void CompleteAttract()
    {

    }

    /// <summary>
    /// ��𔭎˂���
    /// </summary>
    public void OnShot()
    {

    }

    /// <summary>
    /// �{�����I�΂ꂽ
    /// </summary>
    public void OnSelectedBomb()
    {

    }

    /// <summary>
    /// ���W�A�����j���[���\�����ꂽ�Ƃ�
    /// </summary>
    public void OnRadialMenuDisplayed()
    {

    }
    #endregion
}