// --------------------------------------------------------- 
// TutorialManager.cs 
// 
// CreateDay: 2023/09/08
// Creator  : TakayanagiSora
// --------------------------------------------------------- 

using UnityEngine;
using UnityEngine.Video;
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
    /// �c�������I
    /// </summary>
    end_A,
    /// <summary>
    /// �c����Ȋ�����
    /// </summary>
    end_B,
    /// <summary>
    /// �c���܂��g���ēG��|���܂��傤
    /// </summary>
    ending
}


public partial class TutorialManager : MonoBehaviour, ITextLikeSpeaking, ISceneFadeCallBack
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

    [SerializeField]
    private VideoPlayer _videoPlayer = default;

    [SerializeField]
    private MovieDisplayManager _videoDisplay = default;


    [Tooltip("��O�ꂽ�Ƃ��A��𐶐�����")]
    public Action _onArrowMissed_Create = default;

    [Tooltip("��O�ꂽ�Ƃ��A�����I�ɖ���΂�")]
    public Action<Vector3> _onArrowMissed_Shot = default;


    private ScoreFrameMaganer _frameManager = default;

    private InputManagement _input = default;

    private VR_BowManager _VRbowManager = default;

    private GameManager _gameManager = default;

    private FPSChanger _changer = default;

    private TutorialImageSystem _imageSystem = default;


    [Tooltip("�`���[�g���A���̐i�s�x")]
    private TutorialTextType _currentTutorialType = 0;    // opening

    [Tooltip("����f�[�^�̃C���f�b�N�X")]
    private int _subtitlesDataIndex = 0; // ���{��

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
    private bool _isRadialMenuDisplayed = false;

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

    [Tooltip("�ŏ��̃g���K�[����")]
    private bool _inputFirst = true;

    private bool _isHit = default;

    private bool _isEnchant1_Closed = false;

    private GameProgress gameProgress;

#if UNITY_EDITOR
    [Space]
    [SerializeField, Header("�`���[�g���A�����X�L�b�v�i�f�o�b�O�p�j")]
    private bool _skipTutorial = default;
#endif
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
#if UNITY_EDITOR
                    if (_skipTutorial)
                    {
                        gameProgress.TutorialEnding();
                        this.enabled = false;
                        return;
                    }
#endif

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

        try
        {
            _VRbowManager = FindObjectOfType<VR_BowManager>();
        }
        catch (Exception)
        {
            X_Debug.LogError("VR_BowManager���擾�ł��Ă��܂���B");
        }

        try
        {
            _gameManager = FindObjectOfType<GameManager>();
        }
        catch (Exception)
        {
            X_Debug.LogError("GameManager���擾�ł��Ă��܂���B");
        }

        if (_gameManager.SubtitlesType == SubtitlesType.English)
        {
            _subtitlesDataIndex = 8; // ��������p��f�[�^
        }

        _changer = FindObjectOfType<FPSChanger>();
        _imageSystem = this.GetComponent<TutorialImageSystem>();
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
        //if (_input.ButtonLeftDownTrigger() || _input.ButtonRightDownTrigger() || _input.ButtonLeftUpTrigger() || _input.ButtonRightUpTrigger())
        //{
        //    if (_inputFirst)
        //    {
        //        _inputFirst = false;
        //        _textSystem.NextText();
        //    }
        //}
        //else
        //{
        //    _inputFirst = true;
        //}

        if (Input.GetKeyDown(KeyCode.O))
        {
            _textSystem.NextText();
        }

        if (Input.GetKeyDown(KeyCode.F2))
            OnRadialMenuDisplayed();

        if (Input.GetKeyDown(KeyCode.F3))
            OnSelectedBomb();
    }


    private void Tutorial()
    {
        IEnumerator WaitArrowUp()
        {
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.UpArrow));
            StartCoroutine(CallText(1f));
        }
        StartCoroutine(WaitArrowUp());
    }

    /// <summary>
    /// �`���[�g���A���i�s����
    /// </summary>
    private void ProgressingTheTutorial(float textTime = 1f)
    {
        _currentTutorialType++;
        _isHitFirst = true;

        StartCoroutine(CallText(textTime));
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
        _textSystem.TextLikeSpeaking(_tutorialTextsData[(int)_currentTutorialType + _subtitlesDataIndex], this);
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
        _isHit = true;

        // �ŏ��̔���
        if (_isHitFirst && (_currentTutorialType == TutorialTextType.enchant2 || _currentTutorialType == TutorialTextType.attract1))
        {
            _isHitFirst = false;
            StartCoroutine(RemoveTarget());
        }

        if (_spawndTargetAmount <= 0)
        {
            if (_currentTutorialType == TutorialTextType.shot1)
            {
                _videoPlayer.Stop();
                _videoDisplay.CloseFrame();
            }

            if (_currentTutorialType == TutorialTextType.end_B)
            {
                ProgressingTheTutorial(2f);
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
        yield return new WaitForSeconds(0.35f);

        TargetMove[] targets = FindObjectsOfType<TargetMove>();

        for (int i = 0; i < targets.Length; i++)
        {
            targets[i].GetComponent<TargetStats>()._canDeath = false;
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
            _isHit = false;
            _canShotFirst = true;
            CallSpawn();
        }
    }

    /// <summary>
    /// ��𔭎˂���
    /// </summary>
    public void OnShot()
    {
        if (_isShotFirst && _canShotFirst)
        {
            _isShotFirst = false;

            // ������ĂȂ�����
            _VRbowManager.CantShotBecauseYouMissed = false;
            if (!_changer.vr)
                FindObjectOfType<FPSBow>().CantDrawBowBecauseYouMissed = false;

            IEnumerator WaitPossibleHit()
            {
                yield return new WaitForSeconds(0.5f);

                if (_isHit) yield break;

                _currentTutorialType++;

                // �������ĂȂ�������I��������
                StartCoroutine(RemoveTarget());

                // �O�����Ƃ��p�̃e�L�X�g����
                CallText(0f);
            }

            StartCoroutine(WaitPossibleHit());
        }
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
        if (_canRadialMenuDisplayed)
        {
            _isRadialMenuDisplayed = true;
        }
    }

    /// <summary>
    /// �e�L�X�g�\���̏I�����m�i���ʁj
    /// </summary>
    public void AllComplete()
    {
        _frameManager.CloseFrame();

        switch (_currentTutorialType)
        {
            case TutorialTextType.opening:

                // ������������
                _kakashi.SetActive(false);
                ProgressingTheTutorial();

                IEnumerator WaitVideo()
                {
                    yield return new WaitForSeconds(2f);

                    _videoPlayer.time = 0f;
                    _videoDisplay.OpenFrame();
                    yield return new WaitUntil(() => _videoDisplay._endOpen);

                    yield return new WaitForSeconds(1f);

                    _videoPlayer.Play();
                }
                StartCoroutine(WaitVideo());
                break;

            case TutorialTextType.shot1:

                _imageSystem.StringPointerClose();
                // �I���o��������
                CallSpawn();
                break;

            case TutorialTextType.enchant1:

                _isEnchant1_Closed = true;
                break;

            case TutorialTextType.attract1:

                // �N���X�^�����o�������āA������
                StartCoroutine(Instantiate(_crystal, _crystalTransform.position, Quaternion.identity).GetComponent<TutorialCrystalBreak>().Break());

                // �����ŋz�����݂����m
                _canAttractCompleted = true;
                break;

            case TutorialTextType.end_A:

                _currentTutorialType++;
                ProgressingTheTutorial();
                break;

            case TutorialTextType.ending:

                _VRbowManager.CantShotBecauseYouMissed = true;
                if (!_changer.vr)
                    FindObjectOfType<FPSBow>().CantDrawBowBecauseYouMissed = true;

                this.SceneFadeOutStart();
                break;
        }
    }

    /// <summary>
    /// �Ō�̃e�L�X�g���\�����ꂽ�Ƃ��ɌĂ΂��
    /// </summary>
    public void ResponseComplete()
    {
        switch (_currentTutorialType)
        {
            case TutorialTextType.shot1:

                _imageSystem.StringPointerOpen();
                break;

            case TutorialTextType.enchant1:

                // �����Ń��W�A�����j���[�����m
                _canRadialMenuDisplayed = true;
                // �����ɔ����̑I�������m
                _canSelectedBomb = true;

                // ���W�A�����j���[���o�����܂Ŕ񓯊��őҋ@���A�i�s����
                IEnumerator WaitRadialMenu()
                {
                    yield return new WaitUntil(() => _isRadialMenuDisplayed);

                    if (!_isEnchant1_Closed)
                        _textSystem.NextText();

                    ProgressingTheTutorial();
                }
                StartCoroutine(WaitRadialMenu());
                break;

            case TutorialTextType.enchant2:

                // �e�L�X�g���\�����ꂽ�^�C�~���O�Ŋ��ɔ������I������Ă�����A�����I���o��
                if (_isSelectedBomb)
                {
                    IEnumerator WaitBomb()
                    {
                        yield return new WaitForSeconds(1f);

                        CallSpawn();
                        _textSystem.NextText();
                    }
                    StartCoroutine(WaitBomb());
                }
                // ��������Ȃ���΁A�{�����I�������܂Ŕ񓯊��őҋ@���A�I���X�|�[��
                else
                {
                    IEnumerator WaitBomb() { yield return new WaitUntil(() => _isSelectedBomb); CallSpawn(); }
                    StartCoroutine(WaitBomb());
                }

                break;

            case TutorialTextType.end_B:

                IEnumerator WaitSpawn()
                {
                    yield return new WaitForSeconds(1f);

                    // �I���ăX�|�[��
                    _targetSpawnCount--;
                    CallSpawn();
                    _textSystem.NextText();

                    yield return new WaitForSeconds(0.8f);

                    // �����I�ɖ���΂��ēI�ɓ��Ă�
                    _onArrowMissed_Create();
                    _onArrowMissed_Shot(_stageDataTable._waveInformation[2]._targetData[2]._spawnPlace.position + Vector3.up * 15f);
                }
                StartCoroutine(WaitSpawn());

                break;
        }
    }

    public void SceneFadeInComplete()
    {
        throw new NotImplementedException();
    }

    public void SceneFadeOutComplete()
    {
        gameProgress.TutorialEnding();
        this.enabled = false;
    }
    #endregion
}
