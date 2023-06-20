// --------------------------------------------------------- 
// ArrowPassiveEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Nomura
// --------------------------------------------------------- 
using UnityEngine;
interface IAssegnToInterFace
{
    //インターフェースのメソッドやプロパティ定義
}
public class AssignToInterfaceProgram : IAssegnToInterFace
{
    //インターフェースを実装しているかCheckする
    public static void AssignToInterface(object obj)
    {
        if(obj is IAssegnToInterFace)
        {
            IAssegnToInterFace assegnToInterFace = (IAssegnToInterFace)obj;
        }
        else
        {
            throw new System.Exception("クラス型をインターフェースでしか代入できない");
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

