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
    enchant1,
    enchant2,
    attract1,
    ending
}


public partial class TutorialManager : MonoBehaviour, ITextLikeSpeaking
{
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

    [SerializeField]
    private GameObject _textFrame = default;

    [SerializeField]
    private GameObject _crystal = default;

    [SerializeField]
    private Transform _crystalTransform = default;

    [SerializeField]
    private SceneObject _sceneObject = default;

    [SerializeField]
    private Transform _playerTransform = default;

    [SerializeField]
    private GameObject _player = default;

    [SerializeField]
    private GameObject _debugPlayer = default;


    private ScoreFrameMaganer _frameManager = default;

    private InputManagement _input = default;

    [Tooltip("�`���[�g���A���̐i�s�x")]
    private TutorialIventType _currentTutorialType = 0;    // opening

    [Tooltip("���݂̓I�̏o����")]
    private int _targetSpawnCount = 0;

    [Tooltip("���݂���I�̐�")]
    private int _spawndTargetAmount = default;

    [Tooltip("�ŏ��ɓI�ɓ�������")]
    private bool _isHitFirst = true;

    [Tooltip("�ŏ��Ɍ���͂�")]
    private bool _isGrabTheStringFirst = true;

    [Tooltip("�ŏ��ɋz�����݂���")]
    private bool _isAttractCompletedFirst = true;

    [Tooltip("�ŏ��ɖ��������")]
    private bool _isShotFirst = true;

    [Tooltip("�ŏ��Ƀ{�����I�΂ꂽ")]
    private bool _isSelectedBombFirst = true;

    [Tooltip("�ŏ��Ƀ��W�A�����j���[���\�����ꂽ")]
    private bool _isRadialMenuDisplayedFirst = true;

    [Tooltip("����͂񂾒ʒm�̋���")]
    private bool _canGrabTheString = false;

    [Tooltip("�z�����݂����ʒm�̋���")]
    private bool _canAttractCompleted = false;

    [Tooltip("����������ʒm�̋���")]
    private bool _canShotFirst = false;

    [Tooltip("�{�����I�΂ꂽ�ʒm�̋���")]
    private bool _canSelectedBomb = false;

    [Tooltip("���W�A�����j���[���\�����ꂽ�ʒm�̋���")]
    private bool _canRadialMenuDisplayed = false;

    [Tooltip("���X�^�[�g")]
    private bool _isReStart = false;

    [Tooltip("�ŏ��̃g���K�[����")]
    private bool _inputFirst = true;

    private bool _isHit { get; set; }

    private GameProgress gameProgress;
    #endregion

    #region property
    #endregion

    #region method
    private void Awake()
    {
        gameProgress = GameObject.FindObjectOfType<GameProgress>();
        gameProgress.readOnlyGameProgressProperty.Subject.Subscribe(
            progressType =>
            {
                if(progressType == GameProgressType.tutorial)
                {
                    Tutorial();
                }
            }
            );

        try
        {
            _poolSystem = GameObject.FindWithTag("PoolSystem").GetComponent<ObjectPoolSystem>();
        }
        catch (Exception)
        {
            X_Debug.LogError("ObjectPoolSystem���A�^�b�`����Ă��܂���B");
        }

        try
        {
            _frameManager = _textFrame.GetComponent<ScoreFrameMaganer>();
        }
        catch (Exception)
        {
            X_Debug.LogError("ScoreFrameManager���擾�ł��Ă��܂���B");
        }

        try
        {
            _input = GameObject.FindWithTag("InputController").GetComponent<InputManagement>();
        }
        catch (Exception)
        {
            X_Debug.LogError("InputManagemant���擾�ł��Ă��܂���B");
        }
    }

    private void Start()
    {
        _player.transform.position = _playerTransform.position;
        _player.transform.rotation = _playerTransform.rotation;
        _debugPlayer.transform.position = _playerTransform.position;
        _debugPlayer.transform.rotation = _playerTransform.rotation;
    }

    private void Update()
    {
        if (_input.ButtonLeftDownTrigger() || _input.ButtonRightDownTrigger() || _input.ButtonLeftUpTrigger() || _input.ButtonRightUpTrigger())
        {
            if (_inputFirst)
            {
                _inputFirst = false;
                _textSystem.NextText();
            }
        }
        else
        {
            _inputFirst = true;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            _textSystem.NextText();
        }

        if (Input.GetKeyDown(KeyCode.F1))
            OnGrabTheString();

        if (Input.GetKeyDown(KeyCode.F2))
            OnRadialMenuDisplayed();

        if (Input.GetKeyDown(KeyCode.F3))
            OnSelectedBomb();
    }


    private void Tutorial()
    {
        StartCoroutine(CallText(1f));
    }

    /// <summary>
    /// �`���[�g���A���i�s����
    /// </summary>
    private void ProgressingTheTutorial()
    {
        _currentTutorialType++;
        _isHitFirst = true;
        _isHit = false;

        StartCoroutine(CallText(2f));
    }

    /// <summary>
    /// �e�L�X�g��\������
    /// </summary>
    /// <param name="waitTime">�ҋ@����</param>
    /// <param name="currentTime">�Ăяo��������</param>
    private IEnumerator CallText(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        _textFrame.SetActive(true);
        _frameManager.OpenFrame();

        while (!_frameManager._endOpen)
            yield return null;

        _frameManager._endOpen = false;

        _textSystem.TextLikeSpeaking(_tutorialTextsData[(int)_currentTutorialType], this);
    }

    /// <summary>
    /// �I�̏o���p�̃R���[�`�����Ăяo������
    /// </summary>
    private void CallSpawn()
    {
        for (int i = 0; i < _stageDataTable._waveInformation[_targetSpawnCount]._targetData.Count; i++)
            StartCoroutine(SpawnTarget(i));

        _targetSpawnCount++;
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

        GameObject target = _poolSystem.CallObject(PoolEnum.PoolObjectType.targetObject, dataPath._spawnPlace.position).gameObject;
        TargetMove targetMove = target.GetComponent<TargetMove>();
        targetMove.TargetData = dataPath;
        targetMove.InitializeWhenEnable();

        target.GetComponent<TargetStats>()._decrementTargetAmount = DecrementTargetAmount;

        _spawndTargetAmount++;
    }

    /// <summary>
    /// �I���f�N�������g���鏈��
    /// </summary>
    private void DecrementTargetAmount()
    {
        _spawndTargetAmount--;

        // �ŏ��̔���
        if (_isHitFirst && (_currentTutorialType == TutorialIventType.enchant2 || _currentTutorialType == TutorialIventType.attract1))
        {
            _isHitFirst = false;
            //_isHit = true;
            //_canShotFirst = false;
            StartCoroutine(RemoveTarget());
        }

        if (_isReStart && _currentTutorialType == TutorialIventType.attract1)
        {

        }

        if (_spawndTargetAmount <= 0)
        {
            if (_isReStart)
            {
                _isReStart = false;
                _isHit = false;
                return;
            }

            ProgressingTheTutorial();
        }
    }

    /// <summary>
    /// �I�̏�������
    /// </summary>
    private IEnumerator RemoveTarget()
    {
        yield return new WaitForSeconds(0.8f);

        TargetMove[] targets = FindObjectsOfType<TargetMove>();

        //if (targets.Length == 0)
        //{
        //    ProgressingTheTutorial();
        //}

        for (int i = 0; i < targets.Length; i++)
        {
            StartCoroutine(targets[i].RotateAtDespawn());
        }
    }

    /// <summary>
    /// ����͂�
    /// </summary>
    public void OnGrabTheString()
    {
        if (_isGrabTheStringFirst && _canGrabTheString)
        {
            _isGrabTheStringFirst = false;
            ProgressingTheTutorial();
        }
    }

    /// <summary>
    /// �z�����݊�������
    /// </summary>
    public void OnAttractCompleted()
    {
        if (_isAttractCompletedFirst && _canAttractCompleted)
        {
            _isAttractCompletedFirst = false;
            _canAttractCompleted = false;
            _canShotFirst = true;
            CallSpawn();
        }
    }

    /// <summary>
    /// ��𔭎˂���
    /// </summary>
    public void OnShot()
    {
        //if (_isShotFirst && _canShotFirst)
        //{
        //    _isShotFirst = false;
        //    StartCoroutine(WaitPossibleHit());

        //    if (_isHit)
        //    {
        //        return;
        //    }

        //    _isReStart = true;
        //    _targetSpawnCount--;

        //    StartCoroutine(RemoveTarget());

        //    StartCoroutine(WaitTargetDespawn());

        //    // ���Z�b�g
        //    _isAttractCompletedFirst = true;
        //    _canAttractCompleted = true;
        //}
    }

    /// <summary>
    /// �{�����I�΂ꂽ
    /// </summary>
    public void OnSelectedBomb()
    {
        if (_isSelectedBombFirst && _canSelectedBomb)
        {
            _isSelectedBombFirst = false;
            CallSpawn();
        }
    }

    /// <summary>
    /// ���W�A�����j���[���\�����ꂽ
    /// </summary>
    public void OnRadialMenuDisplayed()
    {
        if (_isRadialMenuDisplayedFirst && _canRadialMenuDisplayed)
        {
            _isRadialMenuDisplayedFirst = false;
            ProgressingTheTutorial();
        }
    }

    /// <summary>
    /// �e�L�X�g�\���̏I�����m�i���ʁj
    /// </summary>
    public void AllComplete()
    {
        _textFrame.SetActive(false);

        switch (_currentTutorialType)
        {
            // VR�������邩�m�F������
            case TutorialIventType.opening:

                // ������������
                _kakashi.SetActive(false);
                ProgressingTheTutorial();
                break;

            // ����͂񂾌�
            case TutorialIventType.shot1:

                // �����Ō���͂񂾂��ǂ������m
                _canGrabTheString = true;
                break;

            // �I���ăe�L�X�g�̌�
            case TutorialIventType.shot2:

                // �I���o��������
                CallSpawn();
                break;

            // ���W�A�����j���[�W�J���w��������
            case TutorialIventType.enchant1:

                // �����Ń��W�A�����j���[�����m
                _canRadialMenuDisplayed = true;
                break;

            // �����G���`�����g�̑I�����w��������
            case TutorialIventType.enchant2:

                // �����Ŕ����̑I�������m
                _canSelectedBomb = true;
                // �����ł́A��ꔭ�ł������ꂩ�̓I�ɓ��������玟�̃`���[�g���A���֐i�s����

                break;

            // �z�����݂̏Љ��������
            case TutorialIventType.attract1:

                // �N���X�^�����o�������āA������
                StartCoroutine(Instantiate(_crystal, _crystalTransform.position, Quaternion.identity).GetComponent<TutorialCrystalBreak>().Break());

                // �����ŋz�����݂����m
                _canAttractCompleted = true;

                break;

            case TutorialIventType.ending:
                //FindObjectOfType<SceneManagement>().SceneLoadSpecifyMove(_sceneObject);
                gameProgress.TutorialEnding();
                break;
        }
    }

    public void ResponseComplete() { }

    private IEnumerator WaitTargetDespawn()
    {
        float wait = 1f;
        float time = 0f;

        while (time <= wait)
        {
            time += Time.deltaTime;
            yield return null;
        }

        // �N���X�^�����o�������āA������
        StartCoroutine(Instantiate(_crystal, _crystalTransform.position, Quaternion.identity).GetComponent<TutorialCrystalBreak>().Break());
        _isShotFirst = true;
    }

    private IEnumerator WaitPossibleHit()
    {
        yield return new WaitForSeconds(1.0f);
    }
    #endregion
}
