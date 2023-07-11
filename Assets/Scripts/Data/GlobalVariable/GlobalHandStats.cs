// --------------------------------------------------------- 
// GlobalHandStats.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;

[CreateAssetMenu(fileName ="GlobalHandStats", menuName = "Scriptables/CreateGlobalHandStats")]
public class GlobalHandStats : ScriptableObject,ISerializationCallbackReceiver
{
    #region variable 
    [SerializeField] InputManagement.EmptyHand firstHand = InputManagement.EmptyHand.Right;
    [SerializeField] InputManagement.EmptyHand save = InputManagement.EmptyHand.Right;
    #endregion
    #region property

    private static GlobalHandStats instance;
    //�V���O���g���ɂ���Ȃ�X�N���v�^�u���̈Ӗ��Ȃ��Ȃ���
    public static GlobalHandStats Instance
    {
        get
        {
            if(instance == null)
            {
                instance = ScriptableObject.CreateInstance<GlobalHandStats>();
            }
            return instance;
        }
    }
    public InputManagement.EmptyHand SaveHands
    {
        get 
        {
            return save;
        }
        set
        {
            save = value;
        } 
    }

    // �f�o�b�O�J�n���ɏ�����
    public void OnAfterDeserialize()
    {
        Init();
    }

    public void OnBeforeSerialize()
    {
        
    }


    #endregion
    #region method

    /// <summary>
    /// ������
    /// </summary>
    private void Init()
    {
        SaveHands = firstHand;
    }


    #endregion
}