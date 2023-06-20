// --------------------------------------------------------- 
// ArrowPassiveEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Nomura
// --------------------------------------------------------- 
using UnityEngine;

public interface IFactoryAndInterfaceCheck
{
    //�C���^�[�t�F�[�X�̃��\�b�h��v���p�e�B��`
}
public class FactoryAndInterfaceCheck : IFactoryAndInterfaceCheck
{

    private FactoryAndInterfaceCheck()
    {
        //�v���C�x�[�g�ɃR���X�g���N�^�������@�O������͂��̃N���X���R���X�g���N�^���g�p���ăC���X�^���X�����ł��Ȃ�
    }


    /// <summary>
    /// �t�@�N�g�����\�b�h
    /// </summary>
    /// <returns>IMyInterface</returns>
    public static IFactoryAndInterfaceCheck CreateInstance()
    {
        //�C���^�[�t�F�[�X�^�ɃC���X�^���X�����N���X����
        IFactoryAndInterfaceCheck myInterface = new FactoryAndInterfaceCheck();
        //�C���^�[�t�F�[�X��Ԃ�
        return myInterface;
    }
}


public class FactoryMainProgram : MonoBehaviour
{
    //�C���^�[�t�F�[�X�^�̕ϐ�
    IFactoryAndInterfaceCheck myInterface;

    //�N���X�^�̕ϐ�
    FactoryAndInterfaceCheck factoryAndInterfaceCheck;
    private void Start()
    {
        //�t�@�N�g�����\�b�h�𗘗p���ăC���X�^���X����
        myInterface = FactoryAndInterfaceCheck.CreateInstance();

        //�ʏ�̃C���X�^���X�͂ł��Ȃ��@�R���X�g���N�^��Private�̂���
        //myInterface = new FactoryAndInterfaceCheck();


        //�܂��N���X�^�̕ϐ��ɂ͑���ł��Ȃ�
        //factoryAndInterfaceCheck = new FactoryAndInterfaceCheck();

        //factoryAndInterfaceCheck = FactoryAndInterfaceCheck.CreateInstance();
        

        /*�t�@�N�g�����\�b�h�@�t�@�N�g���N���X�Ƃ�
         * �I�u�W�F�N�g�̐�������ɍs�����\�b�h
         * �ʏ�̓N���X���ɐÓI���\�b�h�Ƃ��Ď��������
         * �N���X�̃R���X�g���N�^�Ƃ͈قȂ�A���_��ȃI�u�W�F�N�g�������@��񋟂��邱�Ƃ��ł���B
         * ���_
         * �@�C���X�^���X�̃��W�b�N���B�����邱�ƁB
         *  �t�@�N�g�����\�b�h���g�p���邱�ƂŁA�I�u�W�F�N�g�̐������@��ˑ��֌W�̏ڍׂ��B�����邱�Ƃ��ł���
         *  �N���C�A���g�͒P�ɁA�t�@�N�g�����\�b�h���Ăяo�������ŁA�����̐������W�b�N���ӎ�����K�v�͂Ȃ��B
         *  
         * �A�C���X�^���X�̃J�X�^�}�C�Y
         * �t�@�N�g�����\�b�h�͐��������I�u�W�F�N�g���J�X�^�}�C�Y���邽�߂̏_���񋟂���B
         * �p�����[�^���󂯎��A���������I�u�W�F�N�g�̏�������ݒ���s�����Ƃ��ł���B
         * 
         * �B�C���X�^���X�̍ė��p
         * �t�@�N�g�����\�b�h�͓����p�����[�^���g�p���ĕ�����Ăяo�����Ƃ��o����B
         * ����ɂ��A�I�u�W�F�N�g�̍ė��p��e�Ղɂ��邱�Ƃ��ł���B
         * 
         * �C�C���^�[�t�F�[�X�ɂ�钊�ۉ�
         * �t�@�N�g�����\�b�h�́A���ۃC���^�[�t�F�[�X����ăI�u�W�F�N�g�̐������s�����߁A
         * �N���C�A���g�R�[�h����̓I�ȃN���X�Ɉˑ����Ȃ��悤�ɂ��邱�Ƃ��ł���B
         * ���̂��߁A�R�[�h�̏_��A�ێ琫�����シ��
         * 
         * �J�v�Z����
         * 
         */
    }

}
