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

public enum TutorialTextType
{
    /// <summary>
    /// �cVR������Ɍ����邩�ǂ����m�F���Ă�������
    /// </summary>
    opening,
    /// <summary>
    /// �c���ׂĂ̓I�ɖ�𓖂ĂĂ݂܂��傤
    /// </summary>
    shot1,
    /// <summary>
    /// �c�R���g���[���[�̍��X�e�B�b�N��|���Ă݂܂��傤
    /// </summary>
    enchant1,
    /// <summary>
    /// �c�����ɁA����/Explosion��I��ł݂܂��傤
    /// </summary>
    enchant2,
    /// <summary>
    /// �c�z�����񂾕��G���`�����g����������܂�
    /// </summary>
    attract1,
    /// <summary>
    /// �c���܂��g���ēG��|���܂��傤
    /// </summary>
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

    [Tooltip("��O�ꂽ�Ƃ��A��𐶐�����")]
    public Action _onArrowMissed_Create = default;

    [Tooltip("��O�ꂽ�Ƃ��A�����I�ɖ���΂�")]
    public Action<Transform> _onArrowMissed_Shot = default;


    private ScoreFrameMaganer _frameManager = default;

    private InputManagement _input = default;

    [Tooltip("�`���[�g���A���̐i�s�x")]
    private TutorialTextType _currentTutorialType = 0;    // opening

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

    [Tooltip("�ŏ��Ƀ��W�A�����j���[���\�����ꂽ")]
    private bool _isRadialMenuDisplayedFirst = true;

    [Tooltip("�{�����I�΂ꂽ")]
    private bool _isSelectedBomb = false;

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

    //#if UNITY_EDITOR
    [HideInInspector]
    public bool _skipTutorial = default;
    //#endif
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
                if (progressType == GameProgressType.tutorial)
                {
                    //#if UNITY_EDITOR
                    if (_skipTutorial)
                    {
                        gameProgress.TutorialEnding();
                        this.enabled = false;
                        return;
                    }
                    //#endif

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

        if (Input.GetKeyDown(KeyCode.O))
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
        if (_isHitFirst && (_currentTutorialType == TutorialTextType.enchant2 || _currentTutorialType == TutorialTextType.attract1))
        {
            _isHitFirst = false;
            StartCoroutine(RemoveTarget());
        }

        if (_isReStart && _currentTutorialType == TutorialTextType.attract1)
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
        //if (_isGrabTheStringFirst && _canGrabTheString)
        //{
        //    _isGrabTheStringFirst = false;
        //    ProgressingTheTutorial();
        //}
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
        if (_canSelectedBomb)
        {
            _canSelectedBomb = false;
            _isSelectedBomb = true;
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
            case TutorialTextType.opening:

                // ������������
                _kakashi.SetActive(false);
                ProgressingTheTutorial();
                break;

            case TutorialTextType.shot1:

                // �I���o��������
                CallSpawn();
                break;

            case TutorialTextType.enchant1:

                // �����Ń��W�A�����j���[�����m
                _canRadialMenuDisplayed = true;
                // �����ɔ����̑I�������m
                _canSelectedBomb = true;

                break;

            case TutorialTextType.enchant2:

                // �{�����I�������܂Ŕ񓯊��őҋ@���A�I���X�|�[��
                IEnumerator WaitBomb() { yield return new WaitUntil(() => _isSelectedBomb); CallSpawn(); }
                StartCoroutine(WaitBomb());

                break;

            case TutorialTextType.attract1:

                // �N���X�^�����o�������āA������
                StartCoroutine(Instantiate(_crystal, _crystalTransform.position, Quaternion.identity).GetComponent<TutorialCrystalBreak>().Break());

                // �����ŋz�����݂����m
                _canAttractCompleted = true;

                break;

            case TutorialTextType.ending:

                gameProgress.TutorialEnding();
                this.enabled = false;

                break;
        }
    }

    /// <summary>
    /// �Ō�̃e�L�X�g���\�����ꂽ�Ƃ��ɌĂ΂��
    /// </summary>
    public void ResponseComplete()
    {
        // �e�L�X�g���\�����ꂽ�^�C�~���O�Ŋ��ɔ������I������Ă�����A�����I���o��
        if (_currentTutorialType == TutorialTextType.enchant2 && _isSelectedBomb)
        {
            _isSelectedBomb = false;
            CallSpawn();
        }
    }

    private IEnumerator WaitPossibleHit()
    {
        yield return new WaitForSeconds(1.0f);
    }
    #endregion
}
