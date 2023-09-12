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
    attract2
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


    [Tooltip("�`���[�g���A���̐i�s�x")]
    private TutorialIventType _currentTutorialType = 0;    // opening

    [Tooltip("���݂̓I�̏o����")]
    private int _targetSpawnCount = 0;

    [Tooltip("���݂���I�̐�")]
    private int _spawndTargetAmount = default;

    [Tooltip("�ŏ��ɓI�ɓ�������")]
    private bool _isHitFirst = true;
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
        _textFrame.SetActive(false);
        _crystal.SetActive(false);
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
        StartCoroutine(CallText(2f));
    }

    /// <summary>
    /// �`���[�g���A���i�s����
    /// </summary>
    private void ProgressingTheTutorial()
    {
        _currentTutorialType++;
        _isHitFirst = true;
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

        if (_isHitFirst && _currentTutorialType == TutorialIventType.enchant2)
            StartCoroutine(RemoveTarget());

        if (_spawndTargetAmount <= 0)
            ProgressingTheTutorial();
    }

    /// <summary>
    /// �I�̏�������
    /// </summary>
    private IEnumerator RemoveTarget()
    {
        _isHitFirst = false;
        yield return new WaitForSeconds(0.5f);

        TargetMove[] targets = FindObjectsOfType<TargetMove>();

        for (int i = 0; i < targets.Length; i++)
            StartCoroutine(targets[i].RotateAtDespawn());
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

                //-----------------------------------------------
                // �����Ō���͂񂾂��ǂ������m
                //-----------------------------------------------
                ProgressingTheTutorial();
                break;

            // �I�ɓ��Ă���
            case TutorialIventType.shot2:

                // �I���o��������
                CallSpawn();

                break;

            // ���W�A�����j���[�W�J���w��������
            case TutorialIventType.enchant1:

                //-----------------------------------------------
                // �����Ń��W�A�����j���[�����m
                //-----------------------------------------------
                ProgressingTheTutorial();
                break;

            // �����G���`�����g�̑I�����w��������
            case TutorialIventType.enchant2:

                //-----------------------------------------------
                // �����Ŕ����̑I�������m
                //-----------------------------------------------
                CallSpawn();
                // �����ł́A��ꔭ�ł������ꂩ�̓I�ɓ��������玟�̃`���[�g���A���֐i�s����

                break;

            // �z�����݂̏Љ��������
            case TutorialIventType.attract1:

                _crystal.SetActive(true);
                StartCoroutine(_crystal.GetComponent<TutorialCrystalBreak>().Break());

                break;

            case TutorialIventType.attract2:

                break;

            default:
                break;
        }
    }
    #endregion
}
