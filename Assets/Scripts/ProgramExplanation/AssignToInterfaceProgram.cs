// --------------------------------------------------------- 
// ArrowPassiveEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Nomura
// --------------------------------------------------------- 
using UnityEngine;
interface IAssegnToInterFace
{
    //�C���^�[�t�F�[�X�̃��\�b�h��v���p�e�B��`
}
public class AssignToInterfaceProgram : IAssegnToInterFace
{
    //�C���^�[�t�F�[�X���������Ă��邩Check����
    public static void AssignToInterface(object obj)
    {
        if(obj is IAssegnToInterFace)
        {
            IAssegnToInterFace assegnToInterFace = (IAssegnToInterFace)obj;
        }
        else
        {
            throw new System.Exception("�N���X�^���C���^�[�t�F�[�X�ł�������ł��Ȃ�");
        }
    }
}
public class AsseignMainProgram
{
    IAssegnToInterFace assegnToInterFace = new AssignToInterfaceProgram();

    private void Check()
    {
        AssignToInterfaceProgram.AssignToInterface(assegnToInterFace);
    }
}

