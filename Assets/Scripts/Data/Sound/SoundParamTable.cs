// --------------------------------------------------------- 
// SoundParamTable.cs 
// 
// CreateDay: 2023/06/09
// Creator  : TakayanagiSora
// --------------------------------------------------------- 

using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �T�E���h�̃p�����[�^��ݒ肵�ĕۑ�����N���X
/// </summary>
// Assets > Create > Scriptables > CreateSoundTable�ŃA�Z�b�g��
[CreateAssetMenu(fileName = "SoundData", menuName = "Scriptables/CreateSoundTable")]
public class SoundParamTable : ScriptableObject
{
    // SoundInfomation�N���X�̓��e�����������X�g�𐶐�
    public List<SoundInformation> _scriptableSoundInformation = new List<SoundInformation>();
}

/// <summary>
/// �T�E���h�̎��
/// </summary>
// Inspector�ŕύX�����l���A�Z�b�g�Ƃ��ĕۑ������
[System.Serializable]
public class SoundInformation
{
    [Tooltip("�T�E���h��")]
    public string _name = default;

    [Tooltip("�T�E���h�f�[�^")]
    public AudioClip _audioClip = default;
}