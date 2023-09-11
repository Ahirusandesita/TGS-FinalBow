// --------------------------------------------------------- 
// TutorialManager.cs 
// 
// CreateDay: 2023/09/08
// Creator  : TakayanagiSora
// --------------------------------------------------------- 

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public enum TutorialIventType
{
    opening,
    shot1,
    shot2,
}

public class TutorialManager : MonoBehaviour, ITextLikeSpeaking
{
    #region class
    /// <summary>
    /// TutorialOpening�e�L�X�g
    /// </summary>
    private class TutorialOpening : ITextLikeSpeaking
    {
        private GameObject _kakashi;
        private Action _action;

        /// <param name="kakashi">������</param>
        /// <param name="action">ProgressingTheTutorial</param>
        public TutorialOpening(GameObject kakashi, Action action)
        {
            _kakashi = kakashi;
            _action = action;
        }

        public void IsComplete()
        {
            _kakashi.SetActive(false);
            _action();
        }
    }

    /// <summary>
    /// TutorialShot1�e�L�X�g
    /// </summary>
    private class TutorialShot1 : ITextLikeSpeaking
    {
        public void IsComplete()
        {
            // ����͂񂾌�̏���
        }
    }

    private class TutorialShot2 : ITextLikeSpeaking
    {
        public delegate IEnumerator TargetSpawn(int a);

        private int _targetAmount;
        private TargetSpawn _targetSpawn;
        private MonoBehaviour _mono;
        private Action _action;

        /// <param name="targetAmount">�X�|�[�����鐔</param>
        /// <param name="action">ProgressingTheTutorial</param>
        public TutorialShot2(int targetAmount, TargetSpawn targetSpawn, MonoBehaviour mono, Action action)
        {
            _targetAmount = targetAmount;
            _targetSpawn = targetSpawn;
            _mono = mono;
            _action = action;
        }

        public void IsComplete()
        {
            for (int i = 0; i < _targetAmount; i++)
            {
                _mono.StartCoroutine(_targetSpawn(i));
            }

            _action();
        }
    }
    #endregion

    #region variable 
    [SerializeField]
    private StageDataTable _stageDataTable = default;

    private ObjectPoolSystem _poolSystem = default;

    [SerializeField]
    private List<TutorialManagementData> _tutorialTextsData = new();

    [SerializeField]
    private TextSystem _textSystem = default;

    [SerializeField]
    private GameObject _kakashi = default;

    [Tooltip("�`���[�g���A���̐i�s�x")]
    private TutorialIventType _currentTutorialType = 0;    // opening

    [Tooltip("���݂̓I�̏o����")]
    private int _targetSpawnCount = 0;

    [Tooltip("�e�L�X�g�\�����I��")]
    private bool _isFinishTextDisplayed = false;
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
        //StartCoroutine(SpawnTarget(0));
        Tutorial();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            _textSystem.NextText();
        }
    }


    private void Tutorial()
    {
        StartCoroutine(CallText(2f, _currentTutorialType));
    }

    /// <summary>
    /// �`���[�g���A���i�s����
    /// </summary>
    private void ProgressingTheTutorial()
    {
        _currentTutorialType++;
        StartCoroutine(CallText(2f, _currentTutorialType));
    }

    /// <summary>
    /// �e�L�X�g��\������
    /// </summary>
    /// <param name="waitTime">�ҋ@����</param>
    /// <param name="currentTime">�Ăяo��������</param>
    private IEnumerator CallText(float waitTime, TutorialIventType textIndex)
    {
        _isFinishTextDisplayed = false;

        yield return new WaitForSeconds(waitTime);
        _textSystem.TextLikeSpeaking(_tutorialTextsData[(int)textIndex], GenerateClass(_currentTutorialType));
    }

    /// <summary>
    /// �`���[�g���A���̐i�s�x�ɂ���āA�N���X�̃C���X�^���X����鏈��
    /// </summary>
    /// <param name="tutorialType">���݂�</param>
    /// <returns></returns>
    private ITextLikeSpeaking GenerateClass(TutorialIventType tutorialType)
    {
        ITextLikeSpeaking generatedClass = default;

        switch (tutorialType)
        {
            case TutorialIventType.opening:

                generatedClass = new TutorialOpening(_kakashi, ProgressingTheTutorial);
                break;

            case TutorialIventType.shot1:

                generatedClass = this;
                break;

            case TutorialIventType.shot2:

                generatedClass = new TutorialShot2(_stageDataTable._waveInformation[_targetSpawnCount]._targetData.Count, SpawnTarget, this, ProgressingTheTutorial);
                break;

            default:
                break;
        }

        return generatedClass;
    }

    /// <summary>
    /// �I�̃X�|�[��
    /// </summary>
    /// <param name="targetIndex">�X�|�[��������I�̐�</param>
    /// <returns></returns>
    private IEnumerator SpawnTarget(int targetIndex)
    {
        // �`���[�g���A���p�̃f�[�^�̃p�X���擾
        TargetDataTable dataPath = _stageDataTable._waveInformation[_targetSpawnCount]._targetData[targetIndex];

        // ���ԍ��X�|�[������
        yield return new WaitForSeconds(dataPath._spawnDelay_s);

        TargetMove target = _poolSystem.CallObject(PoolEnum.PoolObjectType.targetObject, dataPath._spawnPlace.position).GetComponent<TargetMove>();
        target.TargetData = dataPath;

        target.InitializeWhenEnable();
    }

    /// <summary>
    /// ����͂�
    /// </summary>
    public void OnGrabTheString()
    {

    }

    /// <summary>
    /// �z�����݊�������
    /// </summary>
    public void OnAttractCompleted()
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
    /// ���W�A�����j���[���\�����ꂽ
    /// </summary>
    public void OnRadialMenuDisplayed()
    {

    }

    /// <summary>
    /// �e�L�X�g�\���̏I�����m�i���ʁj
    /// </summary>
    public void IsComplete()
    {
        _isFinishTextDisplayed = true;
        //---------------------------------------------------
        ProgressingTheTutorial();// ��
        //---------------------------------------------------
    }
    #endregion
}
